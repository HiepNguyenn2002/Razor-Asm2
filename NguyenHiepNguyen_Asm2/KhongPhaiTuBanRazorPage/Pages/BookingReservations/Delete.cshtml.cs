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
    public class DeleteModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;

        public DeleteModel(IBookingReservationRepository bookingReservationRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
        }

        [BindProperty]
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
            } catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null || _bookingReservationRepository == null)
                {
                    return NotFound();
                }
                var bookingreservation = _bookingReservationRepository.FindOne(m => m.BookingReservationId == id);

                if (bookingreservation != null)
                {
                    BookingReservation = bookingreservation;
                    _bookingReservationRepository.Delete(BookingReservation);
                }

                return RedirectToPage("./Index");
            } catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }
    }
}
