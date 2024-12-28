using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Yemekler
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (!IsPostBack)
            {
                LoadSavedRecipes();
            }
        }

        private void LoadSavedRecipes()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT RecipeName FROM SavedRecipes WHERE UserId = @UserId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            rptSavedRecipes.DataSource = dt;
                            rptSavedRecipes.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "Henüz tarif kaydedilmemiş.";
                        }
                    }
                }
            }
        }

        protected void btnAddRecipe_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string recipeName = txtRecipeName.Text.Trim();

            if (!string.IsNullOrEmpty(recipeName))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlDbContext"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO SavedRecipes (UserId, RecipeName) VALUES (@UserId, @RecipeName)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@RecipeName", recipeName);

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadSavedRecipes();
                lblMessage.Text = "Tarif başarıyla kaydedildi.";
            }
            else
            {
                lblMessage.Text = "Lütfen tarif adını girin.";
            }
        }
    }
}
