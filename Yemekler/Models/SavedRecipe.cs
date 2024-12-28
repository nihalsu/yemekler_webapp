using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yemekler.Models
{
    public class SavedRecipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
    }
}