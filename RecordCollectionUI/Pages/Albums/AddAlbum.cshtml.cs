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

        public IActionResult OnPost()
        {
   
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "INSERT INTO Album(AlbumTitle, NumberOfTracks, ReleaseDate, Price, AlbumCoverImageURL)" +
                        "VALUES (@albumTitle, @numberOfTracks, @releaseDate, @price, @albumCoverImageURL)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@albumTitle", NewAlbum.AlbumTitle);
                    cmd.Parameters.AddWithValue("@numberOfTracks", NewAlbum.NumberOfTracks);
                    cmd.Parameters.AddWithValue("@releaseDate", NewAlbum.ReleaseDate);
                    cmd.Parameters.AddWithValue("@price", NewAlbum.Price);
                    cmd.Parameters.AddWithValue("@albumCoverImageURL", NewAlbum.AlbumCoverImageURL);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    // doesn't work yet because need to link up with artistID
                    /*
                     *Microsoft.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'ArtistID', 
                     *table 'RecordCollection.dbo.Album'; column does not allow nulls. INSERT fails.
                     *The statement has been terminated.'
                     */
                }
                return RedirectToPage("Index"); 
            }
            return Page();
            //else statement
        }
    }
}
