using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class EditAlbumModel : PageModel
    {
        [BindProperty]
        public Album ExistingAlbum{ get; set; } = new Album();
        public List<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> GenresSelected { get; set; }
        public void OnGet(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "SELECT * FROM Album WHERE AlbumID = @albumID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@albumID", id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ExistingAlbum.AlbumTitle = reader["AlbumTitle"].ToString();
                        ExistingAlbum.ArtistID = int.Parse(reader["ArtistID"].ToString()); /*Obj to int*/
                        ExistingAlbum.NumberOfTracks = int.Parse(reader["NumberOfTracks"].ToString());/*Obj to int*/
                        ExistingAlbum.ReleaseYear = int.Parse(reader["ReleaseYear"].ToString());/*Obj to DateTime*/
                        ExistingAlbum.Price = decimal.Parse(reader["Price"].ToString());/*Obj to int*/
                        ExistingAlbum.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                    }
                }
            }
        }
    }
}
