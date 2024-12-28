using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;

namespace Yemekler
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            PageAsyncTask task = new PageAsyncTask(async ct =>
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    lblMessage.Text = "Lütfen email ve şifre alanlarını doldurunuz.";
                    lblMessage.Visible = true;
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string apiUrl = "http://localhost:44393/api/users/login";

                        // Kullanıcı giriş verilerini JSON formatında hazırlıyoruz
                        var loginData = new
                        {
                            Email = email,
                            Password = password
                        };

                        string jsonContent = JsonConvert.SerializeObject(loginData);
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        // API'ye POST isteği gönderiyoruz
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                            // Kullanıcı bilgilerini session'a kaydediyoruz
                            Session["UserId"] = result.UserId;
                            Session["Username"] = result.Username;

                            lblMessage.Text = "Giriş başarılı, yönlendiriliyorsunuz.";
                            lblMessage.Visible = true;

                            // Başarılı girişten sonra yönlendirme
                            Response.Redirect("Account.aspx", false);
                        }
                        else
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            lblMessage.Text = $"Giriş başarısız: {errorContent}";
                            lblMessage.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {

                        string errorDetails = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        lblMessage.Text = $"Bir hata oluştu: {errorDetails}";
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
