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
}