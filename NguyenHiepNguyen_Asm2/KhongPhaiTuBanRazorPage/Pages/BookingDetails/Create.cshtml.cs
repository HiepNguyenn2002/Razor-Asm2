using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.BookingDetails
{
    public class CreateModel : PageModel
    {
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IBookingReservationRepository _bookingReservationRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateModel(IBookingDetailRepository bookingDetailRepository, IBookingReservationRepository bookingReservationRepository, IRoomRepository roomRepository)
        {
            _bookingDetailRepository = bookingDetailRepository;
            _bookingReservationRepository = bookingReservationRepository;
            _roomRepository = roomRepository;
        }
        [BindProperty]
        public BookingDetail BookingDetail { get; set; } = default!;
        public BookingReservation BookingReservation { get; set; } = default!;
        public IActionResult OnGet(int? BookingReservationId)
        {
            try
            {
                ViewData["BookingReservationId"] = new SelectList(_bookingReservationRepository.List().Where(b => b.BookingReservationId == BookingReservationId), "BookingReservationId", "BookingReservationId");
                ViewData["RoomId"] = new SelectList(_roomRepository.List(), "RoomId", "RoomDetailDescription");
                BookingReservation = _bookingReservationRepository.FindOne(b => b.BookingReservationId == BookingReservationId);
                return Page();
            }catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index", new { BookingReservationId = BookingReservation.BookingReservationId });
            }
        }

        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                if (_bookingDetailRepository == null || BookingDetail == null)
                {
                    return Page();
                }

                _bookingDetailRepository.Add(BookingDetail);

                return RedirectToPage("./Index", new { BookingReservationId = BookingDetail.BookingReservationId });
            }catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index", new { BookingReservationId = BookingDetail.BookingReservationId });
            }
          
        }
    }
}
