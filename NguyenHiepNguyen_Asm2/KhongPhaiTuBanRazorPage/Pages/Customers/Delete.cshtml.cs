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
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteModel(ICustomerRepository customerRepository)
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
                else
                {
                    Customer = customer;
                }
                return Page();
            }catch (Exception ex) {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        public IActionResult OnPost(int? id)
        {
            try
            {
                if (id == null || _customerRepository == null)
                {
                    return NotFound();
                }
                var customer = _customerRepository.FindOne(m => m.CustomerId == id);

                if (customer != null)
                {
                    Customer = customer;
                    _customerRepository.Delete(Customer);
                }

                return RedirectToPage("./Index");
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Index");
            }
            
        }

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _customerRepository == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }*/

        /*public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                Customer = customer;
                _context.Customers.Remove(Customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }*/

    }
}
