using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Booking.Include(b => b.Guests).Include(b => b.Rooms);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guests)
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Set<Guest>(), "Id", "FirstName");
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "RoomNumber");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Create([Bind("Id,CheckInDate,CheckOutDate,GuestId,RoomId")] Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(booking);
        //        await _context.SaveChangesAsync();

        //        // Create the Invoice automatically after booking is saved
        //        var invoice = new Invoice
        //        {
        //            BookingId = booking.Id,
        //            InvoiceDate = DateTime.Now,
        //            TotalAmount = CalculateTotalAmount(booking), // Implement this method to calculate the total
        //            PaymentStatus = "Unpaid" // Set initial payment status to unpaid
        //        };

        //        _context.Add(invoice);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["GuestId"] = new SelectList(_context.Set<Guest>(), "Id", "FirstName", booking.GuestId);
        //    ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "RoomNumber", booking.RoomId);
        //    return View(booking);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckInDate,CheckOutDate,GuestId,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Booking.Add(booking);
                await _context.SaveChangesAsync();

                // Automatically generate an invoice
                var invoice = new Invoice
                {
                    BookingId = booking.Id,
                    InvoiceDate = DateTime.Now,
                    TotalAmount = CalculateTotalAmount(booking.CheckInDate, booking.CheckOutDate, booking.RoomId), // Replace this with the actual calculation logic
                    PaymentStatus = "Unpaid" // Set initial payment status to unpaid
                };

                _context.Invoice.Add(invoice);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }


        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Set<Guest>(), "Id", "FirstName", booking.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckInDate,CheckOutDate,GuestId,RoomId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Set<Guest>(), "Id", "FirstName", booking.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "RoomNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guests)
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }

        // Method to calculate the total amount based on booking details
        private double CalculateTotalAmount(DateTime checkInDate, DateTime checkOutDate, int roomId)
        {
            var room = _context.Room.Find(roomId);
            if (room == null) throw new Exception("Room not found.");

            var totalDays = (checkOutDate - checkInDate).Days;
            return totalDays * room.PricePerNight;
        }

    }
}