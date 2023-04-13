using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Albums
{
    public class IndexModel : PageModel
    {
        public Album NewAlbum = new Album();
        [BindProperty]
        public List<Album> AlbumList { get; set; } = new List<Album>();
        //global variable
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                //step 2
                string sql = "SELECT * FROM Album ORDER BY AlbumTitle";
                //step 3
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = sql;
                //cmd.Connection = conn;
                //Above 3 lines is the same as the one below line
                SqlCommand cmd = new SqlCommand(sql, conn);
                //step 4
                conn.Open();
                //step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Album album = new Album();
                        album.AlbumTitle = reader["AlbumTitle"].ToString();
                        album.NumberOfTracks = (int)reader["NumberOfTracks"];
                        album.ReleaseYear = (int)reader["ReleaseYear"];
                        album.Price = (decimal)reader["Price"]; //'Unable to cast object of type 'System.Decimal' to type 'System.Int32'.'
                        album.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                        album.AlbumID = int.Parse(reader["AlbumID"].ToString());
                        AlbumList.Add(album);
                    }
                }
                //ExecuteNonQuery executes without caring about result
                //Insert and delete usually use these
                //ExecuteReader
                //Select uses this
            }
           
        }

        public void allArtists()
        {

        }
    }
}
