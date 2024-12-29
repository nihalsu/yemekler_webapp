using System;
using System.Data;
using System.Web.UI.WebControls;
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
                lblUserName.Text = Session["Username"].ToString(); // Kullanıcı adını göster
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
                string query = "SELECT RecipeId, RecipeName FROM SavedRecipes WHERE UserId = @UserId";

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
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
        }
        

        protected void rptSavedRecipes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                // Seçilen satırın index'ini alıyoruz
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // GridView'deki DataKey'den RecipeId'yi alıyoruz
                int recipeId = Convert.ToInt32(rptSavedRecipes.DataKeys[rowIndex].Value);

                // Detay sayfasına yönlendiriyoruz
                Response.Redirect($"Detay.aspx?Id={recipeId}");
            }
        }
    }
}
