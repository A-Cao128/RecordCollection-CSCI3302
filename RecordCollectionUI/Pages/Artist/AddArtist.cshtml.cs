using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artist
{
    public class AddArtistModel : PageModel
    {
        [BindProperty]
        public Artist NewArtist { get; set; } = new Artist();
        public void OnGet()
        {
        }
        
        public void OnPost() 
        { 
        }
    }
}
