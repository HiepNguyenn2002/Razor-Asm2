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

namespace KhongPhaiTuBanRazorPage.Pages.BookingDetails
{
    public class EditModel : PageModel
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IRoomRepository _roomRepository;
        public EditModel(IBookingDetailRepository bookingDetailRepository, IBookingReservationRepository bookingReservationRepository, IRoomRepository roomRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingReservationRepository = bookingReservationRepository;
            _roomRepository = roomRepository;
        }

        [BindProperty]
        public BookingDetail BookingDetail { get; set; } = default!;

        public IActionResult OnGet(int? BookingReservationId, int? RoomId)
        {
            try
            {
                if (BookingReservationId == null || RoomId == null || _bookingDetailRepository == null)
                {
                    return NotFound();
                }

                var bookingdetail = _bookingDetailRepository.FindOne(e => e.BookingReservationId == BookingReservationId && e.RoomId == RoomId);
                if (bookingdetail == null)
                {
                    return NotFound();
                }
                BookingDetail = bookingdetail;
                ViewData["BookingReservationId"] = new SelectList(_bookingReservationRepository.List().Where(br => br.BookingReservationId == BookingReservationId), "BookingReservationId", "BookingReservationId");
                ViewData["RoomId"] = new SelectList(_roomRepository.List(), "RoomId", "RoomDetailDescription");
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index", new { BookingReservationId = BookingReservationId });
            }

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                _bookingDetailRepository.Update(BookingDetail);

                return RedirectToPage("./Index", new { BookingReservationId = BookingDetail.BookingReservationId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index", new { BookingReservationId = BookingDetail.BookingReservationId });
            }
        }

        private bool BookingDetailExists(int BookingReservationId, int RoomId)
        {
            return _bookingDetailRepository.List().Any(e => e.BookingReservationId == BookingReservationId && e.RoomId == RoomId);
        }
    }
}
