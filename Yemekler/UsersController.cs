using MySql.Data.MySqlClient;
using Yemekler.Models;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net;

namespace Yemekler
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=Rec;UserId=root;Password=1234;SslMode=None;";

        // Kullanıcı Giriş
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(UserLoginModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return Content(HttpStatusCode.BadRequest, new { Success = false, Message = "Email ve şifre zorunludur." });
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Username FROM Users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Password", model.Password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Ok(new
                                {
                                    Success = true,
                                    Message = "Giriş başarılı.",
                                    UserId = reader.GetInt32("Id"),
                                    Username = reader.GetString("Username")
                                });
                            }
                            else
                            {
                                return Content(HttpStatusCode.Unauthorized, new { Success = false, Message = "Geçersiz email veya şifre." });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        // Kullanıcı Kayıt
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(UserRegisterModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return Content(HttpStatusCode.BadRequest, new { Success = false, Message = "Tüm alanlar doldurulmalıdır." });
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Email kontrol
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", model.Email);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            return Content(HttpStatusCode.BadRequest, new { Success = false, Message = "Bu e-posta zaten kullanılıyor." });
                        }
                    }

                    // Kullanıcı ekle
                    string insertQuery = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Username", model.Username);
                        insertCmd.Parameters.AddWithValue("@Email", model.Email);
                        insertCmd.Parameters.AddWithValue("@Password", model.Password);

                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return Ok(new { Success = true, Message = "Kullanıcı başarıyla kaydedildi." });
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, new { Success = false, Message = "Kayıt işlemi başarısız oldu." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
