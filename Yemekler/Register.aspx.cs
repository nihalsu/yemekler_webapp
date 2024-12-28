using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;

namespace Yemekler
{
    public partial class Register : System.Web.UI.Page
    {
        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            PageAsyncTask task = new PageAsyncTask(async ct =>
            {
                string username = txtUsername.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    lblMessage.Text = "Lütfen tüm alanları doldurunuz.";
                    lblMessage.Visible = true;
                    return;
                }



                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "http://localhost:44393/api/users/register";

                        // Yeni kullanıcı verilerini JSON formatında hazırlıyoruz
                        var newUser = new
                        {
                            Username = username,
                            Email = email,
                            Password = password
                        };

                        string json = JsonConvert.SerializeObject(newUser);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        // API'ye POST isteği gönderiyoruz
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            lblMessage.Text = "Kayıt başarılı. Giriş yapabilirsiniz.";
                            lblMessage.Visible = true;

                            // Başarılı kayıttan sonra yönlendirme
                            Response.Redirect("Login.aspx", false);
                        }
                        else
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            lblMessage.Text = $"Kayıt başarısız: {errorContent}";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (HttpRequestException httpEx)
                    {
                        lblMessage.Text = $"API isteği başarısız oldu: {httpEx.Message}";
                        lblMessage.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = $"Bir hata oluştu: {ex.Message}";
                        lblMessage.Visible = true;
                    }
                }
            });

            // Asenkron işlemi başlatıyoruz
            Page.RegisterAsyncTask(task);
            Page.ExecuteRegisteredAsyncTasks();
        }
    }
}
