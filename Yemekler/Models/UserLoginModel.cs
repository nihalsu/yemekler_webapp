using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemekler.Models
{
    public class UserLoginModel
    {
        public string Email { get; set; }  // Kullanıcının e-posta adresi
        public string Password { get; set; }  // Kullanıcının şifresi
    }
}