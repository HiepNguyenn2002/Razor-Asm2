using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Repositories;

namespace KhongPhaiTuBanRazorPage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        public LoginModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [BindProperty, Required]
        public string Email { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Session.GetString("role") != null)
            {
                return RedirectToPage("/Index");
            }

            var account = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("account");
            if (!String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                if (Email.Equals(account["username"]) && Password.Equals(account["password"]))
                {
                    HttpContext.Session.SetString("ID", "0");
                    HttpContext.Session.SetString("role", "admin");
                    HttpContext.Session.SetString("name", "Admin");
                }
                else
                {
                    Customer c = _customerRepository.FindEmailAndPass(Email, Password);
                    if (c == null)
                    {
                        TempData["Error"] = "Email và mật khẩu không hợp lệ!";
                        return Page();
                    }
                    HttpContext.Session.SetString("ID", c.CustomerId.ToString());
                    HttpContext.Session.SetString("role", "customer");
                    HttpContext.Session.SetString("name", c.CustomerFullName);
                }
            }
            else
            {
                TempData["Error"] = "Email và mật khẩu không hợp lệ!";
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
