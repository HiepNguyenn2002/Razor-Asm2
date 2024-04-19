using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.RoomInfomations
{
    public class DetailsModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        public DetailsModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

      public RoomInformation RoomInformation { get; set; } = default!; 

        public IActionResult OnGet(int? id)
        {
            try
            {
                if (id == null || _roomRepository == null)
                {
                    return NotFound();
                }

                var roominformation = _roomRepository.FindOne(m => m.RoomId == id);
                if (roominformation == null)
                {
                    return NotFound();
                }
                else
                {
                    RoomInformation = roominformation;
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
