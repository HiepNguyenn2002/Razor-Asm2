using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public IActionResult OnGet()
        {
            if (_customerRepository != null)
            {
                Customer = _customerRepository.List();
            }
            else
            {
                ViewData["ErrorMessage"] = "Null";
            }
            return Page();
        }
    }
}
