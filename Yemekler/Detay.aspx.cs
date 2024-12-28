using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
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
                            // Yemek adı
                            ltlYemekAdi.Text = reader["YemekAdi"].ToString();

                            // Yemek resmi
                            string imageUrl = reader["ResimUrl"].ToString();
                            ViewState["ResimUrl"] = imageUrl; // Resmi ViewState ile saklıyoruz

                            // Malzemeler
                            string malzemeler = reader["Malzemeler"].ToString();
                            string[] malzemeListesi = malzemeler.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            rptMalzemeler.DataSource = malzemeListesi;
                            rptMalzemeler.DataBind();

                            // Tarif metni
                            ltlTarif.Text = reader["TarifMetni"].ToString();
                        }
                    }
                }
            }
        }



        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

            // Yemek resmi için img tag'i dinamik olarak güncelleniyor
            string resimUrl = ViewState["ResimUrl"]?.ToString();
            if (!string.IsNullOrEmpty(resimUrl))
            {
                writer.Write($"<script>document.querySelector('.recipe-image img').src = '{resimUrl}';</script>");
            }
        }
        protected void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            // Zaman uyumsuz işlemi PageAsyncTask ile sar
            PageAsyncTask task = new PageAsyncTask(async ct =>
            {
                int recipeId = Convert.ToInt32(Request.QueryString["Id"]);
                string recipeName = ltlYemekAdi.Text;

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var postData = new FormUrlEncodedContent(new[]
                        {
                    new KeyValuePair<string, string>("recipeId", recipeId.ToString()),
                    new KeyValuePair<string, string>("recipeName", recipeName)
                });

                        string apiUrl = "http://localhost:44393/UsersAPI.ashx?action=saverecipe";
                        HttpResponseMessage response = await client.PostAsync(apiUrl, postData);

                        if (response.IsSuccessStatusCode)
                        {
                            lblMessage.Text = "Tarif başarıyla kaydedildi!";
                        }
                        else
                        {
                            lblMessage.Text = "Tarif kaydedilirken bir hata oluştu.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = $"Bir hata oluştu: {ex.Message}";
                    }
                }
            });

            // Zaman uyumsuz işlemi sayfada kaydet ve çalıştır
            Page.RegisterAsyncTask(task);
            Page.ExecuteRegisteredAsyncTasks();
        }




    }
}
