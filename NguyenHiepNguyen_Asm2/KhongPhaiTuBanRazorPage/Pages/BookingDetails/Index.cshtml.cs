using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.BookingDetails
{
    public class IndexModel : PageModel
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;

        public IndexModel(IBookingDetailRepository bookingDetailRepository, IBookingReservationRepository bookingReservationRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingReservationRepository = bookingReservationRepository;
        }

        public IList<BookingDetail> BookingDetail { get;set; } = default!;
        public BookingReservation BookingReservation { get; set; } = default!;

        public IActionResult OnGet(int? BookingReservationId)
        {
            if (_bookingDetailRepository != null)
            {
                if (BookingReservationId != null)
                {
                    BookingDetail = _bookingDetailRepository.List().Where(bd => bd.BookingReservationId == BookingReservationId).ToList();
                    BookingReservation = _bookingReservationRepository.FindOne(b => b.BookingReservationId == BookingReservationId);
                    ViewData["ErrorMessage"] = TempData["ErrorMessage"];
                    return Page();
                }
            }
            return RedirectToPage("./Index");

        }
    }
}
