using System;
using System.Collections.Generic;

namespace Yemekler
{
    public partial class RecipeResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Session'dan tarif sonuçlarını al
                List<dynamic> recipes = Session["Recipes"] as List<dynamic>;

                if (recipes != null && recipes.Count > 0)
                {
                    // Sonuçları Repeater'a bağla
                    rptRecipes.DataSource = recipes;
                    rptRecipes.DataBind();
                }
                else
                {
                    // Eğer tarif yoksa bir mesaj göster
                    Response.Write("<script>alert('Sonuç bulunamadı.');</script>");
                }
            }
        }
    }
}
