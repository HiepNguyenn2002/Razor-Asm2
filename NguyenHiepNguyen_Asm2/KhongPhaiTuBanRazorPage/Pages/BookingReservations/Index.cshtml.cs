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
    public class IndexModel : PageModel
    {
        private readonly IBookingReservationRepository _bookingReservationRepository;

        public IndexModel(IBookingReservationRepository bookingReservationRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
        }

        public IList<BookingReservation> BookingReservation { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") == "customer")
            {
                string customerID = HttpContext.Session.GetString("ID");
                if (_bookingReservationRepository != null && customerID != null)
                {
                    BookingReservation = _bookingReservationRepository.List().Where(f => f.CustomerId == int.Parse(customerID)).ToList();
                }
            }
            else if(HttpContext.Session.GetString("role") == "admin")
            {
                BookingReservation = _bookingReservationRepository.List();
            }
            return Page();
        }
    }
}
