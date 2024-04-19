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
    public class DeleteModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public DeleteModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [BindProperty]
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

        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null || _roomRepository == null)
                {
                    return NotFound();
                }
                var roominformation = _roomRepository.FindOne(m => m.RoomId == id);

                if (roominformation != null)
                {
                    RoomInformation = roominformation;
                    _roomRepository.Delete(RoomInformation);
                }
                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }
    }
}
