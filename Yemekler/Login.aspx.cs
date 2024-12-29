using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace Yemekler
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Lütfen email ve şifre alanlarını doldurunuz.";
                lblMessage.Visible = true;
                return;
            }

            // Veritabanı bağlantısı
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı kontrolü
                    string query = "SELECT Id, Username FROM Users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Kullanıcı bulundu, session bilgileri kaydediliyor
                                Session["UserId"] = reader["Id"].ToString();
                                Session["Username"] = reader["Username"].ToString();

                                lblMessage.Text = "Giriş başarılı, yönlendiriliyorsunuz.";
                                lblMessage.Visible = true;

                                // Account sayfasına yönlendirme
                                Response.Redirect("Account.aspx", false);
                            }
                            else
                            {
                                // Kullanıcı bulunamadı
                                lblMessage.Text = "Email veya şifre yanlış. Lütfen tekrar deneyin.";
                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = $"Bir hata oluştu: {ex.Message}";
                    lblMessage.Visible = true;
                }
            }
        }
    }
}
