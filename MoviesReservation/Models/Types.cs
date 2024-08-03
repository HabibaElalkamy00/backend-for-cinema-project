using System.ComponentModel.DataAnnotations;

namespace MoviesReservation.Models
{
    public class Types
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
