using System;
using System.Collections.Generic;
using Yemekler;

namespace Yemekler
{
    public partial class AramaSonuclari : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string query = Request.QueryString["query"];
                if (!string.IsNullOrEmpty(query))
                {
                    RecipeService recipeService = new RecipeService();
                    List<dynamic> yemekler = recipeService.GetRecipesByName(query);

                    if (yemekler.Count > 0)
                    {
                        rptAramaSonuclari.DataSource = yemekler;
                        rptAramaSonuclari.DataBind();
                    }
                    else
                    {
                        Response.Write("<p>Aradığınız kriterlere uygun tarif bulunamadı.</p>");
                    }
                }
            }
        }
    }
}
