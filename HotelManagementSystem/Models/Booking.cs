using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)] // Ensures date format
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)] // Ensures date format
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int GuestId { get; set; }

        public Guest? Guests { get; set; } // Navigation property

        [Required]
        public int RoomId { get; set; }

        public Room? Rooms { get; set; } // Navigation property

        public ICollection<Invoice>? Invoices { get; set; } // Navigation property
    }
}
