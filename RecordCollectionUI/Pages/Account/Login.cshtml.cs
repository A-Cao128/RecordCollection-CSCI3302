using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RecordCollectionUI.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential LoginInfo { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Verify the login info

            // Create security context
        }
    }

    public class Credential
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
