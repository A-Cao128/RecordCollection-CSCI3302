using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordCollectionUI.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace RecordCollectionUI.Pages.Artists
{
    
    public class AddArtistModel : PageModel
    {
        [BindProperty]

        public Artist NewArtist { get; set; } = new Artist();
        public void OnGet()
        {
        }
        public void OnPost() {
     
        }
    }
}
