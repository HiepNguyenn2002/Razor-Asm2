using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.BookingReservations
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;

        public DetailsModel(IBookingReservationRepository bookingReservationRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
        }

        public BookingReservation BookingReservation { get; set; } = default!; 

        public IActionResult OnGet(int? id)
        {
            try
            {
                if (id == null || _bookingReservationRepository == null)
                {
                    return NotFound();
                }

                var bookingreservation = _bookingReservationRepository.FindOne(m => m.BookingReservationId == id);
                if (bookingreservation == null)
                {
                    return NotFound();
                }
                else
                {
                    BookingReservation = bookingreservation;
                }
                return Page();
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }
    }
}
