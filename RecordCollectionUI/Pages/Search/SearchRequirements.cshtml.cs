using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using RecordCollectionUI.Models;

namespace RecordCollectionUI.Pages.Search
{
    public class SearchRequirementsModel : PageModel
    {
        [BindProperty]
        public Album NewAlbum { get; set; } = new Album();
        [BindProperty]
        public List<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<SelectListItem> GenreList { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<int> GenresSelected { get; set; }

        public void OnGet()
        {
            PopulateAuthorList();
            PopulateGenreList();
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
                        genre.Value = reader["GenreID"].ToString();
                        genre.Text = reader["GenreName"].ToString();
                        GenreList.Add(genre);
                    }
                }
            }
        }

        private void PopulateAuthorList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sqlAuthor = "SELECT ArtistID, ArtistName FROM Artist Order By ArtistName";
                SqlCommand cmd = new SqlCommand(sqlAuthor, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var artist = new SelectListItem();
                        artist.Value = reader["ArtistID"].ToString();
                        artist.Text = reader["ArtistName"].ToString();
                        Artists.Add(artist);
                    }
                }
            }
        }
    }

}