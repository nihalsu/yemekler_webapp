using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace Yemekler
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    RecipeService recipeService = new RecipeService();
                    await recipeService.FetchAndSaveRecipesAsync("pasta");
                    Response.Write("Tarifler başarıyla kaydedildi!");
                }
                catch (Exception ex)
                {
                    Response.Write($"Hata: {ex.Message}");
                }
            }
        }

        protected async void btnFetchRecipes_Click(object sender, EventArgs e)
        {
            try
            {
                // RecipeService sınıfını kullanarak tarifleri çekip veritabanına kaydet
                RecipeService recipeService = new RecipeService();

                // API'den veri çek ve kaydet
                await recipeService.FetchAndSaveRecipesAsync("pasta"); // Örneğin "pasta" sorgusu gönderiliyor

                // İşlem başarılı olursa
                lblStatus.Text = "Tarifler başarıyla güncellendi!";
            }
            catch (Exception ex)
            {
                // Hata durumunda
                lblStatus.Text = "Hata: " + ex.Message;
            }
        }
    }
}