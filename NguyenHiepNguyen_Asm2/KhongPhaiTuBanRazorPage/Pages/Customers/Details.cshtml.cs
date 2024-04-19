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
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public DetailsModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

      public Customer Customer { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            try
            {
                if (HttpContext.Session.GetString("role") == "customer")
                {
                    string customerID = HttpContext.Session.GetString("ID");
                    var customer = _customerRepository.FindOne(m => m.CustomerId == int.Parse(customerID));
                    if (customer == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        Customer = customer;
                    }
                    return Page();
                }
                else if (HttpContext.Session.GetString("role") == "admin")
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
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Index");
            }
            
        }

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
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
    }
}
