using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class AddAlbumModel : PageModel
    {
        [BindProperty]
        public Album NewAlbum { get; set; } = new Album();
        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
