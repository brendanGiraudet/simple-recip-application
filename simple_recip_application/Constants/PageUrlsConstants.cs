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

    // HouseholdProducts
    public const string HouseholdProducts = "/household-products";

    // User Pantry
    public const string UserPantry = "/user-pantry";

    // Tags
    public const string TagManagement = "/tags";

    // Shopping List
    public const string ShoppingList = "/shopping-list";

    // Calendar
    public const string CalendarsPage = "/calendars";

    // Calendar user access
    public static string GetCalendarUserAccessesPage(Guid calendarId) => $"/calendar/{calendarId}/user-access";
    public static string GetAcceptedCalendarUserAccessesPage(Guid calendarId) => $"/email/accepted-calendar-user-access/{calendarId}";
}