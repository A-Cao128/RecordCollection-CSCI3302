using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Genres
{
    public class ResultsGenreModel : PageModel
    {
        [BindProperty]
        public List<Album> AlbumsGenres { get; set; } = new List<Album>();

        public void OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Album FULL JOIN AlbumGenre ON Album.AlbumID = AlbumGenre.AlbumID FULL JOIN Genre ON Album.AlbumID = Genre.AlbumID WHERE Genre.GenreID = @genreID";
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.AddWithValue("@genreID", id);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Album album = new Album();
                        album.AlbumTitle = reader["AlbumTitle"].ToString();
                        album.NumberOfTracks = int.Parse(reader["NumberOfTracks"].ToString());
                        album.ReleaseYear = int.Parse(reader["ReleaseYear"].ToString());
                        album.Price = decimal.Parse(reader["Price"].ToString());
                        album.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                        album.AlbumID = int.Parse(reader["AlbumID"].ToString());
                        AlbumsGenres.Add(album);
                    }
                }

            }
        }


    }
}

