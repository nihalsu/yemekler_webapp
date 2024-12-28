using System.Data.Entity;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using Yemekler.Models;

[DbConfigurationType(typeof(MySqlEFConfiguration))]
public class AppDbContext : DbContext
{
    public AppDbContext() : base("MySqlConnection") { }

    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<SavedRecipe> SavedRecipes { get; set; }
}
