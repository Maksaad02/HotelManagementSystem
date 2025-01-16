using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }

        public Booking? Bookings { get; set; } // Navigation property

        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } // When the invoice was generated

        [Required]
        [Range(0, double.MaxValue)] // Total amount must be positive
        public double TotalAmount { get; set; } // Total cost for the stay

        [Required]
        [StringLength(20)] // Restrict length for status
        public String? PaymentStatus { get; set; } // e.g., "Paid", "Pending"
    }
}
