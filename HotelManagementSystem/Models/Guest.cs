using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HotelManagementSystem.Models
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public String CIN { get; set; }
        [StringLength(50)]
        public String FirstName { get; set; }
        [StringLength(50)]
        public String LastName { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        [Phone]
        public String PhoneNumber { get; set; }
        public ICollection<Booking>? Bookings { get; set; } // Navigation property



    }
}
