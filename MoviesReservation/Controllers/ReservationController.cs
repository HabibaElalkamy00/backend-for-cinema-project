using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesReservation.Models;
using MoviesReservation.ViewModel;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoviesReservation.Controllers
{
    public class ReservationController : Controller
    {
        private ApplicationDbContext _MyDatabase;
        public ReservationController(ApplicationDbContext MyDatabase)
        {
            _MyDatabase = MyDatabase;
        }
        public IActionResult Index(int ?id,int ?idHall)
        {
            ReservationView reservationView = new ReservationView();
            if(id == null)
            {
                TempData["Validation"] = "Invalid Number of parameter Try Again later";
                return RedirectToAction("index", "home");
            }
            else
            {

                var myMovie=_MyDatabase.MovieHalls.Include(m=>m.Movie).ToList();
                var uniqueMyMovie = myMovie.GroupBy(mh => mh.MovieId).Select(group => group.First()).ToList();
                
                int idSearch = 0;
                reservationView.AllMovieTopick = uniqueMyMovie;
                if (id == -1)
                {
                    reservationView.PickedMovie = _MyDatabase.MovieHalls.Include(m => m.Movie).FirstOrDefault(m => m.Id == idHall);
                    idSearch = reservationView.PickedMovie.MovieId;

                }
                else
                {
                    reservationView.PickedMovie = _MyDatabase.MovieHalls.Include(m => m.Movie).FirstOrDefault(m => m.MovieId == id);
                    idSearch = (int)id;
                }
                if (reservationView.PickedMovie == null)
                {
                    TempData["Validation"] = "Reservation Is closed please select another Movie";

                    reservationView.PickedMovie = _MyDatabase.MovieHalls.Include(m => m.Movie).ToList()[0];
                }
                List<DateView> AllDate=new List<DateView>();
                foreach(var MovieSel in myMovie)
                {
                    if(MovieSel.MovieId == idSearch)
                    {
                        
                        string dayName = MovieSel.StartDateTime.ToString("dddd");
                        string fullDate = MovieSel.StartDateTime.ToString("yyyy-MM-dd");
                        string hours = MovieSel.StartDateTime.ToString("hh tt");
                        if (hours[0]=='0')
                            hours = hours.Substring(1);
                        DateView dateView = new DateView()
                        {
                            Starthour= hours,
                            dayName=dayName,
                            date=fullDate,
                            Price= MovieSel.price,
                            id= MovieSel.Id,
                            
                        };
                        AllDate.Add(dateView);
                    }
                   

                }
                reservationView.AllAvaliableDay = AllDate;
                var Allseat=_MyDatabase.Seats.Where(n=>n.HallId==1).ToList();
                reservationView.AllSeat = new List<SeatView>();
                reservationView.Column = 0;
                reservationView.Row = 0;
                List< SeatView > seatViews = new List<SeatView >();
                foreach (var ava in Allseat)
                {
                    if(ava.Row> reservationView.Row)
                        reservationView.Row=ava.Row;
                    if(ava.Column> reservationView.Column) 
                        reservationView.Column=ava.Column;

                    var check = _MyDatabase.ticketSeats.SingleOrDefault(m => m.seatId == ava.Id && m.ticket.MovieHall.Id == reservationView.PickedMovie.Id);
                    SeatView seatView = new SeatView();
                    if (check != null)
                    {

                        seatView.seat = ava;
                        seatView.Avaliable = false;
                        
                    }
                    else
                    {

                        seatView.seat = ava;
                        seatView.Avaliable = true;
                        
                    }
                   
                    seatViews.Add(seatView);
                }
                reservationView.AllSeat = seatViews;

            }


            return View(reservationView);
        }
   
        public JsonResult PaymentFormSave([FromBody] transferTicket data)
        {
            var MovieHall=_MyDatabase.MovieHalls.SingleOrDefault(m=>m.Id==data.ReservationId);
            List<Seat> Choseseats=new List<Seat>();
            foreach(var item in data.SeatsIndex)
            {
                
                int row = item/10;
                row += 1;
                int column = item%10;
                column += 1;
                var seat = _MyDatabase.Seats.SingleOrDefault(m => m.HallId == MovieHall.HallId && m.Column == column && m.Row == row);
                Choseseats.Add(seat);


            }
            Ticket ticket = new Ticket()
            {
                MovieHall= MovieHall,
                MovieHallId= MovieHall.Id

            };
            _MyDatabase.Tickets.Add(ticket);
            _MyDatabase.SaveChanges();
            int savedTicketId = ticket.Id;
            foreach(var seat in Choseseats)
            {
                string qrCodeData = $"Seat: {seat.Row}-{seat.Column}";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeImage = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                QRCode qrcode = new QRCode(qrCodeImage);
                Bitmap qrCodeImages = qrcode.GetGraphic(20);
                string filePath = Path.Combine("wwwroot", "images", "qrcode", $"qrcode_{seat.Id}.png");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                qrCodeImages.Save(filePath, ImageFormat.Png);
                ticketSeat ticketSeat = new ticketSeat()
                {
                    seat = seat,
                    seatId = seat.Id,
                    ticket = ticket,
                    ticketId = savedTicketId,
                    qrCode= $"qrcode_{seat.Id}.png"
                };
                _MyDatabase.ticketSeats.Add(ticketSeat);
               
            }

            
            _MyDatabase.SaveChanges();
            return Json(new
            {
                TicketId = savedTicketId,
                message = "success massege"
            });
        }
        public IActionResult PaymentForm(int id)
        {
            var ticket = _MyDatabase.Tickets.SingleOrDefault(m=>m.Id == id);
            User user=new User()
            {
                Id = id,
            };
            return View(user);
        }
        public IActionResult PaymentFormCreate(User data)
        {
            if (!ModelState.IsValid)
            {
                
                return View("PaymentForm", data);
            }

            var ticket = _MyDatabase.Tickets.SingleOrDefault(m => m.Id == data.Id);
            ticket.CVV = int.Parse(data.CVV);
            ticket.CardNumber
                = data.CardNumber;
            ticket.PhoneNumber = data.PhoneNumber;
            ticket.UserName = data.UserName;
            ticket.Email = data.Email;
            ticket.ExpiryDate = data.ExpiryDate;
            _MyDatabase.SaveChanges();


            return RedirectToAction("Ticket",new { id=ticket.Id });
        }

        public IActionResult Ticket(int id)
        {
            var ticket= _MyDatabase.Tickets.Include(m=>m.MovieHall).Include(m=>m.MovieHall.Movie).SingleOrDefault(m=>m.Id==id);
            var AllSeat = _MyDatabase.ticketSeats.Include(m => m.seat).Where(m => m.ticketId == id).ToList();
            TicketView ticketFinal = new TicketView()
            {
                ticket= ticket,
                ticketSeats=AllSeat
            };
            foreach (var seat in AllSeat)
            {

                
            }

            return View(ticketFinal);
        }
    }
}
