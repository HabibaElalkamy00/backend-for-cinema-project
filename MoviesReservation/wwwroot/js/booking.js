const container = document.querySelector('.container');
const seats = document.querySelectorAll('.row .seat:not(.occupied');
const count = document.getElementById('count');
const total = document.getElementById('total');
const movieSelect = document.getElementById('movie');
const UpdatemovieSelect = document.getElementById('PickMovie');

//populateUI();
let ticketPrice = +movieSelect.value;

// Save selected movie index and price
function setMovieData(movieIndex, moviePrice) {
  localStorage.setItem('selectedMovieIndex', movieIndex);
  localStorage.setItem('selectedMoviePrice', moviePrice);
}

// update total and count
function updateSelectedCount() {
  const selectedSeats = document.querySelectorAll('.row .seat.selected');

  const seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));

  localStorage.setItem('selectedSeats', JSON.stringify(seatsIndex));

  //copy selected seats into arr
  // map through array
  //return new array of indexes

  const selectedSeatsCount = selectedSeats.length;

  count.innerText = selectedSeatsCount;
  total.innerText = selectedSeatsCount * ticketPrice;
}

// get data from localstorage and populate ui
function populateUI() {
  const selectedSeats = JSON.parse(localStorage.getItem('selectedSeats'));
  if (selectedSeats !== null && selectedSeats.length > 0) {
    seats.forEach((seat, index) => {
      if (selectedSeats.indexOf(index) > -1) {
        seat.classList.add('selected');
      }
    });
  }

  const selectedMovieIndex = localStorage.getItem('selectedMovieIndex');

  if (selectedMovieIndex !== null) {
    movieSelect.selectedIndex = selectedMovieIndex;
  }
}


//

UpdatemovieSelect.addEventListener('change', (e) => {
    
    window.location.href = '/Reservation/Index/' + e.target.value;

});
const UpdatDateSelect = document.getElementById('PickeDay');

UpdatDateSelect.addEventListener('change', (e) => {

    window.location.href = '/Reservation/Index/?id=-1&idHall=' + e.target.value;

});
// Movie select event
movieSelect.addEventListener('change', (e) => {
    ticketPrice = +e.target.value;
  setMovieData(e.target.selectedIndex, e.target.value);
  updateSelectedCount();
});
var AllIndex = Array()
// Seat click event
container.addEventListener('click', (e) => {
  if (e.target.classList.contains('seat') && !e.target.classList.contains('occupied')) {
      e.target.classList.toggle('selected');
      let selectedSeats = document.querySelectorAll('.row .seat.selected');

      let seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));
      
      AllIndex=seatsIndex
      
      //console.log(document.getElementById("Pickedmovie").value)
    updateSelectedCount();
  }
});
document.getElementById("Sumbit").addEventListener("click", ()=> {
    let data = {
        ReservationId:parseInt(document.getElementById("Pickedmovie").value),
        SeatsIndex: AllIndex
    }
    //console.log(data)
    //window.location.href = '/Reservation/PaymentForm/' + data;
    fetch('/Reservation/PaymentFormSave', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }) .then(response => response.json())
        .then(responseData => {
            console.log(responseData.TicketId)
            window.location.href = '/Reservation/PaymentForm/' + responseData.ticketId;
        });
})
// intial count and total
updateSelectedCount();
