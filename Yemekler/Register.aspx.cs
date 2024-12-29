using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace Yemekler
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
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

            // Veritabanı bağlantısı
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı adı veya e-posta kontrolü
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        checkCommand.Parameters.AddWithValue("@Email", email);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            lblMessage.Text = "Bu kullanıcı adı veya e-posta zaten kayıtlı.";
                            lblMessage.Visible = true;
                            return;
                        }
                    }

                    // Yeni kullanıcı kaydı
                    string insertQuery = "INSERT INTO Users (Username, Password, Email, CreatedAt) VALUES (@Username, @Password, @Email, NOW())";
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@Password", password);
                        insertCommand.Parameters.AddWithValue("@Email", email);

                        int result = insertCommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            lblMessage.Text = "Kayıt başarılı. Giriş yapabilirsiniz.";
                            lblMessage.Visible = true;

                            // Başarılı kayıttan sonra yönlendirme
                            Response.Redirect("Login.aspx", false);
                        }
                        else
                        {
                            lblMessage.Text = "Kayıt başarısız oldu. Lütfen tekrar deneyin.";
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
        }
    }
}
