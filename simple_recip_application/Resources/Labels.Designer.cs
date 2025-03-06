using System;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace simple_recip_application.Resources;

public class Labels
{
    private static readonly ResourceManager resourceManager =
        new ResourceManager("simple_recip_application.Resources.Labels",
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
    public static string EditRecipe => resourceManager.GetString("EditRecipeTitle", CultureInfo.CurrentCulture) ?? "EditRecipeTitle";
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
}
