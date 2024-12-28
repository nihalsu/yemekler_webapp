using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;

namespace YemekTarifleriWeb
{
    public partial class savedRecipes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSavedRecipes();
            }
        }

        private async void LoadSavedRecipes()
        {
            try
            {
                // Kullanıcı ID'sini oturumdan alıyoruz
                int userId = Convert.ToInt32(Session["UserId"]);
                if (userId == 0)
                {
                    lblMessage.Text = "Giriş yapmanız gerekiyor.";
                    lblMessage.Visible = true;
                    return;
                }

                string apiUrl = $"http://localhost:44393/api/users/{userId}/savedRecipes";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var savedRecipes = JsonConvert.DeserializeObject<List<Recipe>>(json);

                        gvSavedRecipes.DataSource = savedRecipes;
                        gvSavedRecipes.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "Kaydedilen tarifler yüklenirken bir hata oluştu.";
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Bir hata oluştu: {ex.Message}";
                lblMessage.Visible = true;
            }
        }

        public class Recipe
        {
            public string YemekAdi { get; set; }
            public string Kategori { get; set; }
            public string HazirlikSuresi { get; set; }
            public string TarifMetni { get; set; }
            public string Malzemeler { get; set; }
        }
    }
}
