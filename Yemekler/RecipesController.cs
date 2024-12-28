using System.Linq;
using System.Web.Http;
using Yemekler.Models;

public class RecipesController : ApiController
{
    private AppDbContext _context = new AppDbContext();

    [HttpGet]
    [Route("api/recipes")]
    public IHttpActionResult GetRecipes()
    {
        var recipes = _context.Recipes.ToList();
        return Ok(recipes);
    }

    [HttpPost]
    [Route("api/recipes")]
    public IHttpActionResult AddRecipe(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        _context.SaveChanges();
        return Ok("Tarif eklendi!");
    }

    [HttpDelete]
    [Route("api/recipes/{id}")]
    public IHttpActionResult DeleteRecipe(int id)
    {
        var recipe = _context.Recipes.Find(id);
        if (recipe == null)
            return NotFound();

        _context.Recipes.Remove(recipe);
        _context.SaveChanges();
        return Ok("Tarif silindi!");
    }
}
