using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Yemekler
{
    public partial class Main : System.Web.UI.Page
    {
        private readonly RecipeService recipeService = new RecipeService();
        private readonly string connectionString = "Server=localhost;Database=Rec;User Id=root;Password=1234;";

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kategorilere ait tarifleri API'den çekip veri tabanına kaydet
                foreach (var category in recipeService.GetCategories())
                {
                    await recipeService.FetchAndSaveRecipesByCategoryAsync(category);
                }

                // Rastgele tarifleri yükle
                LoadRandomRecipes();

               
            }
        }

        private void LoadRandomRecipes()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, YemekAdi, ResimUrl, HazirlikSuresi FROM tarifler ORDER BY RAND() LIMIT 18";


                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    rptRandomRecipes.DataSource = dataTable;
                    rptRandomRecipes.DataBind();
                }
            }
        }

   

        public class Category
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
