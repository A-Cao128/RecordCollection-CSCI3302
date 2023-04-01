using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class EditAlbumModel : PageModel
    {
        [BindProperty]
        public Album ExistingAlbum{ get; set; } = new Album();
        public void OnGet(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "SELECT * FROM Artist WHERE ArtistID = @artistID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@artistID", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ExistingAlbum.AlbumTitle = reader["AlbumTitle"].ToString();
                        ExistingAlbum.ArtistID = (int)reader["ArtistID"]; /*Obj to int*/
                        ExistingAlbum.NumberOfTracks = (int)reader["NumberOfTracks"];/*Obj to int*/
                        ExistingAlbum.ReleaseYear = reader["AuthorWebsite"].ToString();/*Obj to DateTime*/
                        ExistingAlbum.Price = (decimal)reader["Price"];/*Obj to int*/
                        ExistingAlbum.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                    }
                }
            }
        }
    }
}
