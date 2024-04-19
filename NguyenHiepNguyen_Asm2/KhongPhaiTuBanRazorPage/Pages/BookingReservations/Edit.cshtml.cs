using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.BookingReservations
{
    public class EditModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly ICustomerRepository _customerRepository;

        public EditModel(IBookingReservationRepository bookingReservationRepository, ICustomerRepository customerRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _customerRepository = customerRepository;
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
                BookingReservation = bookingreservation;
                ViewData["CustomerId"] = new SelectList(_customerRepository.List(), "CustomerId", "CustomerFullName");
                return Page(); 
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (_bookingReservationRepository == null)
                {
                    return Page();
                }

                _bookingReservationRepository.Update(BookingReservation);

                return RedirectToPage("./Index");
            } catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        private bool BookingReservationExists(int id)
        {
          return (_bookingReservationRepository.List()?.Any(e => e.BookingReservationId == id)).GetValueOrDefault();
        }
    }
}
