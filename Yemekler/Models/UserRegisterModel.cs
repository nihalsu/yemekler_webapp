using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemekler.Models
{
    public class UserRegisterModel
    {
        public string Username { get; set; }  // Kullanıcı adı
        public string Email { get; set; }  // Kullanıcının e-posta adresi
        public string Password { get; set; }  // Kullanıcının şifresi
    }
}