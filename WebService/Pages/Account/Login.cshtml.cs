using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebService.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty, Required]
        public string Id { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public ActionResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Id)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return Redirect("/");
        }
    }
}
