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
            PopulateArtistList();
            PopulateGenreList();
            PopulateAlbumInfo(id);
        }

        private void PopulateAlbumInfo(int id) 
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
                    ExistingAlbum.AlbumID = id;
                    ExistingAlbum.AlbumTitle = reader["AlbumTitle"].ToString();
                    ExistingAlbum.ArtistID = int.Parse(reader["ArtistID"].ToString()); /*Obj to int*/
                    ExistingAlbum.NumberOfTracks = int.Parse(reader["NumberOfTracks"].ToString());/*Obj to int*/
                    ExistingAlbum.ReleaseYear = int.Parse(reader["ReleaseYear"].ToString());/*Obj to DateTime*/
                    ExistingAlbum.Price = decimal.Parse(reader["Price"].ToString());/*Obj to int*/
                    ExistingAlbum.AlbumCoverImageURL = reader["AlbumCoverImageURL"].ToString();
                }
            }
        }

        private void PopulateArtistList() 
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlArtist = "SELECT ArtistID, ArtistName FROM Artist Order By ArtistName";
                SqlCommand cmd = new SqlCommand(sqlArtist, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var artist = new SelectListItem();
                        artist.Text = reader["ArtistName"].ToString();
                        artist.Value = reader["ArtistID"].ToString();
                        Artists.Add(artist);
                    }
                }
            }
            foreach (SelectListItem i in Artists)
            {
                if (i.Value == ExistingAlbum.ArtistID.ToString())
                {
                    i.Selected = true;
                    break;
                }
            }
        }

        private void PopulateGenreList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenre = "SELECT GenreID, GenreName FROM Genre Order By GenreName";
                SqlCommand cmd = new SqlCommand(sqlGenre, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new SelectListItem();
                        genre.Text = reader["GenreName"].ToString();
                        genre.Value = reader["GenreID"].ToString();
                        GenreList.Add(genre);
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlGenres = "SELECT GenreID FROM AlbumGenre WHERE AlbumID = @albumID";
                SqlCommand cmdGenre = new SqlCommand(sqlGenres, conn);
                cmdGenre.Parameters.AddWithValue("@albumID", ExistingAlbum.AlbumID);
                conn.Open();
                SqlDataReader readerGenre = cmdGenre.ExecuteReader();
                if (readerGenre.HasRows)
                {
                    while (readerGenre.Read())
                    {
                        foreach (SelectListItem i in GenreList)
                        {
                            if (i.Value == readerGenre["GenreID"].ToString())
                            {
                                i.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /* the actual updating part*/
        public IActionResult OnPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                if (ModelState.IsValid)
                {
                    string sql = "UPDATE Album SET AlbumTitle=@albumTitle, ArtistID=@artistID, " +
                    "NumberOfTracks=@numberOfTracks, ReleaseYear=@releaseYear, Price=@price, " +
                    "AlbumCoverImageURL=@albumCoverImageURL WHERE AlbumID=@albumID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@albumTitle", ExistingAlbum.AlbumTitle);
                    cmd.Parameters.AddWithValue("@artistID", ExistingAlbum.ArtistID);
                    cmd.Parameters.AddWithValue("@numberOfTracks", ExistingAlbum.NumberOfTracks);
                    cmd.Parameters.AddWithValue("@releaseYear", ExistingAlbum.ReleaseYear);
                    cmd.Parameters.AddWithValue("@price", ExistingAlbum.Price);
                    cmd.Parameters.AddWithValue("@albumCoverImageURL", ExistingAlbum.AlbumCoverImageURL);
                    cmd.Parameters.AddWithValue("@albumID", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    sql = "DELETE FROM AlbumGenre WHERE AlbumID=@albumID";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@albumID", ExistingAlbum.AlbumID);

                    int r = cmd.ExecuteNonQuery();

                    string sqlAlbumGenre = "INSERT INTO AlbumGenre(AlbumID, GenreID) VALUES(@albumID, @genreID)";
                    cmd.CommandText = sqlAlbumGenre;
                    cmd.Parameters.Clear();
                    for (int i = 0; i < GenresSelected.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue("@albumID", ExistingAlbum.AlbumID);
                        cmd.Parameters.AddWithValue("@genreID", GenresSelected[i].ToString());
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    return RedirectToPage("Index");
                }
                else
                {
                    return Page();
                }


            }
        }
    }
}
