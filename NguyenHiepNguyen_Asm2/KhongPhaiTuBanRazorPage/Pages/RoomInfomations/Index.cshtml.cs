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
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public IndexModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IList<RoomInformation> RoomInformation { get;set; } = default!;

        public IActionResult OnGet()
        {
            try
            {
                if (_roomRepository != null)
                {
                    RoomInformation = _roomRepository.List();
                }
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
            
        }
    }
}
