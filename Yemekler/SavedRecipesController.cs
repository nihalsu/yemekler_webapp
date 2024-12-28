using System.Linq;
using System.Web.Http;
using Yemekler.Models;

public class SavedRecipesController : ApiController
{
    private AppDbContext _context = new AppDbContext();

    [HttpGet]
    [Route("api/savedrecipes/{userId}")]
    public IHttpActionResult GetSavedRecipes(int userId)
    {
        var savedRecipes = _context.SavedRecipes.Where(sr => sr.UserId == userId).ToList();
        return Ok(savedRecipes);
    }

    [HttpPost]
    [Route("api/savedrecipes")]
    public IHttpActionResult SaveRecipe(SavedRecipe savedRecipe)
    {
        _context.SavedRecipes.Add(savedRecipe);
        _context.SaveChanges();
        return Ok("Tarif kaydedildi!");
    }

    [HttpDelete]
    [Route("api/savedrecipes/{id}")]
    public IHttpActionResult DeleteSavedRecipe(int id)
    {
        var savedRecipe = _context.SavedRecipes.Find(id);
        if (savedRecipe == null)
            return NotFound();

        _context.SavedRecipes.Remove(savedRecipe);
        _context.SaveChanges();
        return Ok("Kaydedilen tarif silindi!");
    }
}
