using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)] // Max length for room number
        public required String RoomNumber { get; set; }

        [Required]
        [StringLength(50)] // Max length for room type
        public required String RoomType { get; set; }

        [Required]
        [Range(0, 10000)] // Price range from 0 to 10,000
        public int PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true; // Default to available
    }
}
