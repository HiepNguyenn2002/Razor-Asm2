using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.RoomInfomations
{
    public class CreateModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public CreateModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IActionResult OnGet()
        {
            try
            {
                ViewData["RoomTypeId"] = new SelectList(_roomRepository.ListRoomType(), "RoomTypeId", "RoomTypeName");
                return Page();
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid || _roomRepository == null || RoomInformation == null)
                {
                    return Page();
                }

                _roomRepository.Add(RoomInformation);

                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
          
        }
    }
}
