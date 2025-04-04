namespace simple_recip_application.Constants;

public static class PageUrlsConstants
{
    // Recipe
    public const string RecipesPage = "/recipes";
    public const string RecipeDetailsPage = "/recipes/{0}";
    public static string GetRecipeDetailsPage(Guid? id) => string.Format(RecipeDetailsPage, id);

    // Ingredients
    public const string IngredientsPage = "/ingredients";
    
    // Importation
    public const string ImportationPage = "/importation";
    
    // Recipes planning
    public const string RecipePlannigPage = "/recipe-planning";
    
    // Authentication
    public const string Authentication = "/authentication/login";

    // Pantry
    public const string PantryIngredients = "/pantry-ingredients";
}