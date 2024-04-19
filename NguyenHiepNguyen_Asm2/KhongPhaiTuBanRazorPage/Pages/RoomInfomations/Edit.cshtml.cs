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

namespace KhongPhaiTuBanRazorPage.Pages.RoomInfomations
{
    public class EditModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public EditModel(IRoomRepository roomRepository)
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
                RoomInformation = roominformation;
                ViewData["RoomTypeId"] = new SelectList(_roomRepository.ListRoomType(), "RoomTypeId", "RoomTypeName");
                return Page();
            }catch (Exception ex)
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
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                try
                {
                    _roomRepository.Update(RoomInformation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomInformationExists(RoomInformation.RoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        private bool RoomInformationExists(int? id)
        {
          return _roomRepository.List().Any(e => e.RoomId == id);
        }
    }
}
