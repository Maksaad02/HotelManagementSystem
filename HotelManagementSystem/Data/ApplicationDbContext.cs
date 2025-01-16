using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HotelManagementSystem.Models.Booking> Booking { get; set; } = default!;
        public DbSet<HotelManagementSystem.Models.Guest> Guest { get; set; } = default!;
        public DbSet<HotelManagementSystem.Models.Invoice> Invoice { get; set; } = default!;
        public DbSet<HotelManagementSystem.Models.Role> Role { get; set; } = default!;
        public DbSet<HotelManagementSystem.Models.Room> Room { get; set; } = default!;
        public DbSet<HotelManagementSystem.Models.User> User { get; set; } = default!;
    }
}
