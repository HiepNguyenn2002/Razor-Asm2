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

namespace KhongPhaiTuBanRazorPage.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public EditModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            try
            {
                if (id == null || _customerRepository == null)
                {
                    return NotFound();
                }

                var customer = _customerRepository.FindOne(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }
                Customer = customer;
                return Page();
            }
            catch (Exception ex)
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

                _customerRepository.Update(Customer);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
        }

        private bool CustomerExists(int? id)
        {
            return _customerRepository.List().Any(m => m.CustomerId == id);
        }
    }
}
