using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Yemekler
{
    public partial class Detay : System.Web.UI.Page
    {
        private readonly string connectionString = "Server=localhost;Database=Rec;User Id=root;Password=1234;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["Id"] != null && int.TryParse(Request.QueryString["Id"], out int recipeId))
            {
                LoadRecipeDetails(recipeId);
            }
        }

        private void LoadRecipeDetails(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT YemekAdi, ResimUrl, TarifMetni, Malzemeler FROM tarifler WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ltlYemekAdi.Text = reader["YemekAdi"].ToString();
                            recipeImage.Src = reader["ResimUrl"].ToString();

                            string malzemeler = reader["Malzemeler"].ToString();
                            string[] malzemeListesi = malzemeler.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            rptMalzemeler.DataSource = malzemeListesi;
                            rptMalzemeler.DataBind();

                            ltlTarif.Text = reader["TarifMetni"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            int recipeId = Convert.ToInt32(Request.QueryString["Id"]);
            string recipeName = ltlYemekAdi.Text;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM SavedRecipes WHERE UserId = @UserId AND RecipeId = @RecipeId";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserId", userId);
                        checkCmd.Parameters.AddWithValue("@RecipeId", recipeId);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            lblMessage.Text = "Bu tarif zaten kaydedilmiş.";
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO SavedRecipes (UserId, RecipeId, RecipeName) VALUES (@UserId, @RecipeId, @RecipeName)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                        cmd.Parameters.AddWithValue("@RecipeName", recipeName);

                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Tarif başarıyla kaydedildi!";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Bir hata oluştu: {ex.Message}";
                }
            }
        }
    }
}
