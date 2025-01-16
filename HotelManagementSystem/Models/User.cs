using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)] // Limite la longueur du nom du rôle
        public required String Name { get; set; } // e.g., "Admin", "Host"
    }
}
