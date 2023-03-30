using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Artists
{
    public class EditArtistModel : PageModel
    {
        [BindProperty]
        public Artist ExistingArtist { get; set; } = new Artist();
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
                        ExistingArtist.ArtistName = reader["ArtistName"].ToString();
                        ExistingArtist.ArtistBio = reader["ArtistBio"].ToString();
                        ExistingArtist.ArtistImageURL = reader["ArtistImageURL"].ToString();
                        ExistingArtist.ArtistWebsite = reader["ArtistWebsite"].ToString();
                    }


                }
            }
        }
    }
}
