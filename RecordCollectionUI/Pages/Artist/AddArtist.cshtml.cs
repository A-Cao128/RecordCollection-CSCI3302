using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artist
{
    public class AddArtistModel : PageModel
    {
        public Artist NewArtist { get; set; } = new Artist();
        public void OnGet()
        {
        }
    }
}
