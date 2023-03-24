using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
            // capture user input and add new album to the database
            /* 
             * 1. create SQl connection obj
             * 2. write a query
             * 3. create SQL command obj
             * 4. open connection
             * 5. execute the SQL command obj
             */

            // step 1
            SqlConnection conn = new SqlConnection();
            // step 2
            string sql = "INSERT INTO Album(AlbumTitle, NumberOfTracks, ReleaseDate, Price, AlbumImageCoverURL)" + 
                "VALUES ('" + NewAlbum.AlbumTitle + "', '" + NewAlbum.NumberOfTracks + "', '" + NewAlbum.ReleaseDate+ "', '"
                + NewAlbum.Price + "', '" + NewAlbum.AlbumImageURL + "')'";

        }
    }
}
