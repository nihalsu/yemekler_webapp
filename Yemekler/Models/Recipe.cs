using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemekler.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string YemekAdi { get; set; }
        public string ResimUrl { get; set; }
        public string HazirlikSuresi { get; set; }
        public string Kategori { get; set; }
        public string TarifMetni { get; set; }
        public string Malzemeler { get; set; }
    }
}