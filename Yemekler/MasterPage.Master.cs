using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Yemekler
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories(); // Kategorileri yükle
            }
        }

        // Arama butonuna tıklama işlevi
        protected void btnAra_Click(object sender, EventArgs e)
        {
            string query = txtArama.Text.Trim(); // Kullanıcının arama kutusuna yazdığı metni al
            if (!string.IsNullOrEmpty(query))
            {
                // Kullanıcıyı AramaSonuclari.aspx sayfasına yönlendir
                Response.Redirect($"~/AramaSonuclari.aspx?query={query}");
            }
        }
        protected void btnIngredientSearch_Click(object sender, EventArgs e)
        {
            // Malzeme arama sayfasına yönlendirme
            Response.Redirect("~/IngredientSearch.aspx");
        }


        // Kategorileri yükleyen işlev
        private void LoadCategories()
        {
            // Kategorileri ve görsellerini sabit olarak tanımlayın
            List<Category> categories = new List<Category>
            {
                new Category { Name = "main course", ImageUrl = "images/maincourse.jpg" },
                new Category { Name = "dessert", ImageUrl = "images/dessert.jpg" },
                new Category { Name = "appetizer", ImageUrl = "images/appetizer.jpg" },
                new Category { Name = "breakfast", ImageUrl = "images/breakfast.jpg" },
                new Category { Name = "salad", ImageUrl = "images/salad.jpg" },
                new Category { Name = "soup", ImageUrl = "images/soup.jpg" },
                new Category { Name = "beverage", ImageUrl = "images/beverage.jpg" }
            };

            // Kategorileri Repeater kontrolüne bağlayın
            rptCategories.DataSource = categories;
            rptCategories.DataBind();
        }

        // Kategori sınıfı
        public class Category
        {
            public string Name { get; set; } // Kategori adı
            public string ImageUrl { get; set; } // Kategori görsel URL'si
        }
    }
}
