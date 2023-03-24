using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class IndexModel : PageModel
    {
        public Album NewAlbum = new Album();
        public void OnGet()
        {
        }
    }
}
