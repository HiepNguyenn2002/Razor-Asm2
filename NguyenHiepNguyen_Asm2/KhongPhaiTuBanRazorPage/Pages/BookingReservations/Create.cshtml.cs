using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace KhongPhaiTuBanRazorPage.Pages.BookingReservations
{
    public class CreateModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateModel(IBookingReservationRepository bookingReservationRepository, ICustomerRepository customerRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") == "customer")
            {
                string customerID = HttpContext.Session.GetString("ID");
                if (customerID != null)
                {
                    ViewData["CustomerId"] = new SelectList(_customerRepository.List().Where(f => f.CustomerId == int.Parse(customerID)), "CustomerId", "CustomerFullName");
                }
            }
            else if (HttpContext.Session.GetString("role") == "admin")
            {
                ViewData["CustomerId"] = new SelectList(_customerRepository.List(), "CustomerId", "CustomerFullName");
            }
            return Page();
        }

        [BindProperty]
        public BookingReservation BookingReservation { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                
                if (_bookingReservationRepository == null || BookingReservation == null)
                {
                    if (HttpContext.Session.GetString("role") == "customer")
                    {
                        string customerID = HttpContext.Session.GetString("ID");
                        if (customerID != null)
                        {
                            ViewData["CustomerId"] = new SelectList(_customerRepository.List().Where(f => f.CustomerId == int.Parse(customerID)), "CustomerId", "CustomerFullName");
                        }
                    }
                    else if (HttpContext.Session.GetString("role") == "admin")
                    {
                        ViewData["CustomerId"] = new SelectList(_customerRepository.List(), "CustomerId", "CustomerFullName");
                    }
                    return Page();
                }

                _bookingReservationRepository.Add(BookingReservation);

                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
          
        }
    }
}
