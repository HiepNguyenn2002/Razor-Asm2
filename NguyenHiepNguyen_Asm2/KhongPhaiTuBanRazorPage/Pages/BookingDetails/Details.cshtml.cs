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
    public class DetailsModel : PageModel
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;

        public DetailsModel(IBookingDetailRepository bookingDetailRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
        }

        public BookingDetail BookingDetail { get; set; } = default!; 

        public IActionResult OnGet(int? BookingReservationId, int? RoomId)
        {
            try
            {
                if (BookingReservationId == null || RoomId == null || _bookingDetailRepository == null)
                {
                    return NotFound();
                }

                var bookingdetail = _bookingDetailRepository.FindOne(m => m.BookingReservationId == BookingReservationId && m.RoomId == RoomId);
                if (bookingdetail == null)
                {
                    return NotFound();
                }
                BookingDetail = bookingdetail;
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index", new { BookingReservationId = BookingReservationId });
            }

        }
    }
}
