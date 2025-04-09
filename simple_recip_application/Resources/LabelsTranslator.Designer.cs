using System;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace simple_recip_application.Resources;

public class LabelsTranslator
{
    private static readonly ResourceManager resourceManager =
        new ResourceManager("simple_recip_application.Resources.LabelsTranslator",
            Assembly.GetExecutingAssembly());

    public static string IngredientName => resourceManager.GetString("IngredientName", CultureInfo.CurrentCulture) ?? "IngredientName";
    public static string ImageLabel => resourceManager.GetString("ImageLabel", CultureInfo.CurrentCulture) ?? "ImageLabel";
    public static string SubmitButton => resourceManager.GetString("SubmitButton", CultureInfo.CurrentCulture) ?? "SubmitButton";
    public static string Home => resourceManager.GetString("Home", CultureInfo.CurrentCulture) ?? "Home";
    public static string Ingredients => resourceManager.GetString("Ingredients", CultureInfo.CurrentCulture) ?? "Ingredients";
    public static string SelectLanguage => resourceManager.GetString("SelectLanguage", CultureInfo.CurrentCulture) ?? "Select Language";
    public static string French => resourceManager.GetString("French", CultureInfo.CurrentCulture) ?? "French";
    public static string English => resourceManager.GetString("English", CultureInfo.CurrentCulture) ?? "English";
    public static string LoadIngredients => resourceManager.GetString("LoadIngredients", CultureInfo.CurrentCulture) ?? "Load Ingredients";
    public static string AddIngredient => resourceManager.GetString("AddIngredient", CultureInfo.CurrentCulture) ?? "Add Ingredient";
    public static string IngredientList => resourceManager.GetString("IngredientList", CultureInfo.CurrentCulture) ?? "Ingredient List";
    public static string Update => resourceManager.GetString("Update", CultureInfo.CurrentCulture) ?? "Update";
    public static string Delete => resourceManager.GetString("Delete", CultureInfo.CurrentCulture) ?? "Delete";
    public static string Options => resourceManager.GetString("Options", CultureInfo.CurrentCulture) ?? "Options";
    public static string ImportIngredients => resourceManager.GetString("ImportIngredients", CultureInfo.CurrentCulture) ?? "ImportIngredients";
    public static string Import => resourceManager.GetString("Import", CultureInfo.CurrentCulture) ?? "Import";
    public static string AddRecipe => resourceManager.GetString("AddRecipe", CultureInfo.CurrentCulture) ?? "AddRecipe";
    public static string EditRecipe => resourceManager.GetString("EditRecipe", CultureInfo.CurrentCulture) ?? "EditRecipe";
    public static string RecipeName => resourceManager.GetString("RecipeName", CultureInfo.CurrentCulture) ?? "RecipeName";
    public static string RecipeDescription => resourceManager.GetString("RecipeDescription", CultureInfo.CurrentCulture) ?? "RecipeDescription";
    public static string RecipeInstructions => resourceManager.GetString("RecipeInstructions", CultureInfo.CurrentCulture) ?? "RecipeInstructions";
    public static string RecipePreparationTime => resourceManager.GetString("RecipePreparationTime", CultureInfo.CurrentCulture) ?? "RecipePreparationTime";
    public static string RecipeCookingTime => resourceManager.GetString("RecipeCookingTime", CultureInfo.CurrentCulture) ?? "RecipeCookingTime";
    public static string RecipeImageLabel => resourceManager.GetString("RecipeImageLabel", CultureInfo.CurrentCulture) ?? "RecipeImageLabel";
    public static string RecipeCategory => resourceManager.GetString("RecipeCategory", CultureInfo.CurrentCulture) ?? "RecipeCategory";
    public static string EditIngredient => resourceManager.GetString("EditIngredient", CultureInfo.CurrentCulture) ?? "EditIngredient";
    public static string Cancel => resourceManager.GetString("Cancel", CultureInfo.CurrentCulture) ?? "Cancel";
    public static string ImportLabel => resourceManager.GetString("ImportLabel", CultureInfo.CurrentCulture) ?? "ImportLabel";
    public static string Search => resourceManager.GetString("Search", CultureInfo.CurrentCulture) ?? "Search";
    public static string Recipes => resourceManager.GetString("Recipes", CultureInfo.CurrentCulture) ?? "Recipes";
    public static string NoIngredientsFound => resourceManager.GetString("NoIngredientsFound", CultureInfo.CurrentCulture) ?? "NoIngredientsFound";
    public static string NoRecipesFound => resourceManager.GetString("NoRecipesFound", CultureInfo.CurrentCulture) ?? "NoRecipesFound";
    public static string Previous => resourceManager.GetString("Previous", CultureInfo.CurrentCulture) ?? "Previous";
    public static string Next => resourceManager.GetString("Next", CultureInfo.CurrentCulture) ?? "Next";
    public static string GenerateShoppingList => resourceManager.GetString("GenerateShoppingList", CultureInfo.CurrentCulture) ?? "GenerateShoppingList";
    public static string Quantity => resourceManager.GetString("Quantity", CultureInfo.CurrentCulture) ?? "Quantity";
    public static string MeasureUnit => resourceManager.GetString("MeasureUnit", CultureInfo.CurrentCulture) ?? "MeasureUnit";
    public static string Done => resourceManager.GetString("Done", CultureInfo.CurrentCulture) ?? "Done";
    public static string Importation => resourceManager.GetString("Importation", CultureInfo.CurrentCulture) ?? "Importation";
    public static string Planning => resourceManager.GetString("Planning", CultureInfo.CurrentCulture) ?? "Planning";
    public static string NoRecipesForThisDay => resourceManager.GetString("NoRecipesForThisDay", CultureInfo.CurrentCulture) ?? "NoRecipesForThisDay";
    public static string SelectARecipe => resourceManager.GetString("SelectARecipe", CultureInfo.CurrentCulture) ?? "SelectARecipe";
    public static string Morning => resourceManager.GetString("Morning", CultureInfo.CurrentCulture) ?? "Morning";
    public static string Noon => resourceManager.GetString("Noon", CultureInfo.CurrentCulture) ?? "Noon";
    public static string Evening => resourceManager.GetString("Evening", CultureInfo.CurrentCulture) ?? "Evening";
    public static string MomentOfTheDay => resourceManager.GetString("MomentOfTheDay", CultureInfo.CurrentCulture) ?? "MomentOfTheDay";
    public static string PlanifiedRecipesAutomaticaly => resourceManager.GetString("PlanifiedRecipesAutomaticaly", CultureInfo.CurrentCulture) ?? "PlanifiedRecipesAutomaticaly";
    public static string Pantry => resourceManager.GetString("Pantry", CultureInfo.CurrentCulture) ?? "Pantry";
    public static string AddProduct => resourceManager.GetString("AddProduct", CultureInfo.CurrentCulture) ?? "AddProduct";
    public static string NoProductsFound => resourceManager.GetString("NoProductsFound", CultureInfo.CurrentCulture) ?? "NoProductsFound";
    public static string Products => resourceManager.GetString("Products", CultureInfo.CurrentCulture) ?? "Products";
    public static string EditProduct => resourceManager.GetString("EditProduct", CultureInfo.CurrentCulture) ?? "EditProduct";
    public static string ProductName => resourceManager.GetString("ProductName", CultureInfo.CurrentCulture) ?? "ProductName";
    public static string NoItems => resourceManager.GetString("NoItems", CultureInfo.CurrentCulture) ?? "NoItems";
    public static string ImportStrategyLabel => resourceManager.GetString("ImportStrategyLabel", CultureInfo.CurrentCulture) ?? "ImportStrategyLabel";
}
