using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Yemekler
{
    public partial class Category : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string category = Request.QueryString["category"];
                if (!string.IsNullOrEmpty(category))
                {
                    lblCategoryName.Text = char.ToUpper(category[0]) + category.Substring(1);

                    // API'den veri çekme ve veri tabanına kaydetme
                    RecipeService recipeService = new RecipeService();
                    await recipeService.FetchAndSaveRecipesByCategoryAsync(category);

                    // Veri tabanından tarifleri yükleme
                    LoadRecipesByCategory(category);
                }
                else
                {
                    Response.Write("Geçerli bir kategori seçilmedi.");
                }
            }
        }

        private void LoadRecipesByCategory(string category)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Rec;User Id=root;Password=1234;"))
            {
                conn.Open();
                string query = "SELECT Id, YemekAdi, ResimUrl, HazirlikSuresi FROM tarifler WHERE Kategori = @Kategori";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kategori", category);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    rptCategoryRecipes.DataSource = dataTable; // Doğru ID kullanıldı
                    rptCategoryRecipes.DataBind();
                }
            }
        }


        private bool DoesCategoryDataExist(string category)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=Rec;User Id=root;Password=1234;"))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM tarifler WHERE Kategori = @Kategori";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Kategori", category);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

       
    }
}
