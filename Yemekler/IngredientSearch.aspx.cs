using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Yemekler
{
    public partial class IngredientSearch : System.Web.UI.Page
    {
        private static List<string> ingredients = new List<string>();
        private readonly string connectionString = "Server=localhost;Database=Rec;User Id=root;Password=1234;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindIngredients();
            }
        }

        protected void btnAddIngredient_Click(object sender, EventArgs e)
        {
            // FindControl ile TextBox'u bul
            TextBox txtIngredient = (TextBox)Master.FindControl("MainContent").FindControl("txtIngredient");
            if (txtIngredient != null)
            {
                string ingredient = txtIngredient.Text.Trim();
                if (!string.IsNullOrEmpty(ingredient) && !ingredients.Contains(ingredient))
                {
                    ingredients.Add(ingredient);
                }
                txtIngredient.Text = string.Empty;
                BindIngredients();
            }
        }
        protected void btnRemoveIngredient_Click(object sender, EventArgs e)
        {
            // Butonun "CommandArgument" değerini alın
            string ingredientToRemove = ((Button)sender).CommandArgument;

            // Listedeki malzemeyi kaldır
            if (ingredients.Contains(ingredientToRemove))
            {
                ingredients.Remove(ingredientToRemove);
            }

            // Listeyi tekrar bağlayın
            BindIngredients();
        }


        private void BindIngredients()
        {
            // FindControl ile Repeater'ı bul
            Repeater rptIngredients = (Repeater)Master.FindControl("MainContent").FindControl("rptIngredients");
            if (rptIngredients != null)
            {
                rptIngredients.DataSource = ingredients;
                rptIngredients.DataBind();
            }
        }

        protected void btnSearchRecipes_Click(object sender, EventArgs e)
        {
            // Malzemelere göre tarif arama işlemi
            List<dynamic> recipes = GetRecipesByIngredients(ingredients);

            if (recipes.Count > 0)
            {
                // Tarif sonuçlarını Session değişkenine kaydet
                Session["Recipes"] = recipes;

                // Sonuç sayfasına yönlendir
                Response.Redirect("~/RecipeResults.aspx");
            }
            else
            {
                // Tarif bulunamadığında uyarı ver
                Response.Write("<script>alert('Eşleşen tarif bulunamadı.');</script>");
            }
        }



        private List<dynamic> GetRecipesByIngredients(List<string> ingredients)
        {
            List<dynamic> recipes = new List<dynamic>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT DISTINCT YemekAdi, ResimUrl, Id 
                       FROM tarifler 
                       WHERE ";

                for (int i = 0; i < ingredients.Count; i++)
                {
                    sql += $"LOWER(Malzemeler) LIKE LOWER(@Ingredient{i})";
                    if (i < ingredients.Count - 1)
                    {
                        sql += " OR ";
                    }
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    for (int i = 0; i < ingredients.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@Ingredient{i}", $"%{ingredients[i]}%");
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new
                            {
                                YemekAdi = reader["YemekAdi"].ToString(),
                                ResimUrl = reader["ResimUrl"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            });
                        }
                    }
                }
            }

            return recipes;
        }


    }
}
