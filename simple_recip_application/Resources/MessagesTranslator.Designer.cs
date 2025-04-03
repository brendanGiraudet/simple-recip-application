using System;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace simple_recip_application.Resources;

public class MessagesTranslator
{
    private static readonly ResourceManager resourceManager =
        new ResourceManager("simple_recip_application.Resources.MessagesTranslator",
            Assembly.GetExecutingAssembly());

    public static string NameRequired => resourceManager.GetString("NameRequired", CultureInfo.CurrentCulture) ?? "NameRequired";
    public static string ImageRequired => resourceManager.GetString("ImageRequired", CultureInfo.CurrentCulture) ?? "ImageRequired";
    public static string LoadIngredientErrorMessage => resourceManager.GetString("LoadIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "LoadIngredientErrorMessage";
    public static string LoadPlanifiedRecipesErrorMessage => resourceManager.GetString("LoadPlanifiedRecipesErrorMessage", CultureInfo.CurrentCulture) ?? "LoadPlanifiedRecipesErrorMessage";
    public static string AddIngredientSuccessMessage => resourceManager.GetString("AddIngredientSuccessMessage", CultureInfo.CurrentCulture) ?? "AddIngredientSuccessMessage";
    public static string AddIngredientErrorMessage => resourceManager.GetString("AddIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "AddIngredientErrorMessage";
    public static string AddPlanifiedRecipeSuccessMessage => resourceManager.GetString("AddPlanifiedRecipeSuccessMessage", CultureInfo.CurrentCulture) ?? "AddPlanifiedRecipeSuccessMessage";
    public static string AddPlanifiedRecipeErrorMessage => resourceManager.GetString("AddPlanifiedRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "AddPlanifiedRecipeErrorMessage";
    public static string DeletePlanifiedRecipeErrorMessage => resourceManager.GetString("DeletePlanifiedRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "DeletePlanifiedRecipeErrorMessage";
    public static string DeleteIngredientErrorMessage => resourceManager.GetString("DeleteIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "DeleteIngredientErrorMessage";
    public static string LoadingInProgress => resourceManager.GetString("LoadingInProgress", CultureInfo.CurrentCulture) ?? "LoadingInProgress";
    public static string ImportFailure => resourceManager.GetString("ImportFailure", CultureInfo.CurrentCulture) ?? "ImportFailure";
    public static string ImportSuccess => resourceManager.GetString("ImportSuccess", CultureInfo.CurrentCulture) ?? "ImportSuccess";
    public static string PreparationTimeRequired => resourceManager.GetString("PreparationTimeRequired", CultureInfo.CurrentCulture) ?? "PreparationTimeRequired";
    public static string CookingTimeRequired => resourceManager.GetString("CookingTimeRequired", CultureInfo.CurrentCulture) ?? "CookingTimeRequired";
    public static string MaxAllowedSizeError => resourceManager.GetString("MaxAllowedSizeError", CultureInfo.CurrentCulture) ?? "MaxAllowedSizeError";
    public static string QuantityRequired => resourceManager.GetString("QuantityRequired", CultureInfo.CurrentCulture) ?? "QuantityRequired";
    public static string MeasureUnitRequired => resourceManager.GetString("MeasureUnitRequired", CultureInfo.CurrentCulture) ?? "MeasureUnitRequired";
    public static string DeleteIngredientSuccessMessage => resourceManager.GetString("DeleteIngredientSuccessMessage", CultureInfo.CurrentCulture) ?? "DeleteIngredientSuccessMessage";
    public static string UpdateIngredientSuccessMessage => resourceManager.GetString("UpdateIngredientSuccessMessage", CultureInfo.CurrentCulture) ?? "UpdateIngredientSuccessMessage";
    public static string UpdateIngredientErrorMessage => resourceManager.GetString("UpdateIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "UpdateIngredientErrorMessage";
    public static string LoadRecipeErrorMessage => resourceManager.GetString("LoadRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "LoadRecipeErrorMessage";
    public static string AddRecipeSuccessMessage => resourceManager.GetString("AddRecipeSuccessMessage", CultureInfo.CurrentCulture) ?? "AddRecipeSuccessMessage";
    public static string AddRecipeErrorMessage => resourceManager.GetString("AddRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "AddRecipeErrorMessage";
    public static string DeleteRecipeSuccessMessage => resourceManager.GetString("DeleteRecipeSuccessMessage", CultureInfo.CurrentCulture) ?? "DeleteRecipeSuccessMessage";
    public static string DeleteRecipeErrorMessage => resourceManager.GetString("DeleteRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "DeleteRecipeErrorMessage";
    public static string UpdateRecipeSuccessMessage => resourceManager.GetString("UpdateRecipeSuccessMessage", CultureInfo.CurrentCulture) ?? "UpdateRecipeSuccessMessage";
    public static string UpdateRecipeErrorMessage => resourceManager.GetString("UpdateRecipeErrorMessage", CultureInfo.CurrentCulture) ?? "UpdateRecipeErrorMessage";
    public static string MinIngredientErrorMessage => resourceManager.GetString("MinIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "MinIngredientErrorMessage";
    public static string GenerateShoppingListCsvContentErrorMessage => resourceManager.GetString("GenerateShoppingListCsvContentErrorMessage", CultureInfo.CurrentCulture) ?? "GenerateShoppingListCsvContentErrorMessage";
    public static string AccessDenied => resourceManager.GetString("AccessDenied", CultureInfo.CurrentCulture) ?? "AccessDenied";
    public static string LoadPantryIngredientsFailed => resourceManager.GetString("LoadPantryIngredientsFailed", CultureInfo.CurrentCulture) ?? "LoadPantryIngredientsFailed";
    public static string SavePantryIngredientSuccess => resourceManager.GetString("SavePantryIngredientSuccess", CultureInfo.CurrentCulture) ?? "SavePantryIngredientSuccess";
    public static string SavePantryIngredientFailed => resourceManager.GetString("SavePantryIngredientFailed", CultureInfo.CurrentCulture) ?? "SavePantryIngredientFailed";
    public static string DeletePantryIngredientSuccess => resourceManager.GetString("DeletePantryIngredientSuccess", CultureInfo.CurrentCulture) ?? "DeletePantryIngredientSuccess";
    public static string DeletePantryIngredientFailed => resourceManager.GetString("DeletePantryIngredientFailed", CultureInfo.CurrentCulture) ?? "DeletePantryIngredientFailed";
    public static string LoadProductErrorMessage => resourceManager.GetString("LoadProductErrorMessage", CultureInfo.CurrentCulture) ?? "LoadProductErrorMessage";
    public static string AddProductErrorMessage => resourceManager.GetString("AddProductErrorMessage", CultureInfo.CurrentCulture) ?? "AddProductErrorMessage";
    public static string AddProductSuccessMessage => resourceManager.GetString("AddProductSuccessMessage", CultureInfo.CurrentCulture) ?? "AddProductSuccessMessage";
    public static string DeleteProductErrorMessage => resourceManager.GetString("DeleteProductErrorMessage", CultureInfo.CurrentCulture) ?? "DeleteProductErrorMessage";
    public static string DeleteProductSuccessMessage => resourceManager.GetString("DeleteProductSuccessMessage", CultureInfo.CurrentCulture) ?? "DeleteProductSuccessMessage";
    public static string UpdateProductErrorMessage => resourceManager.GetString("UpdateProductErrorMessage", CultureInfo.CurrentCulture) ?? "UpdateProductErrorMessage";
    public static string UpdateProductSuccessMessage => resourceManager.GetString("UpdateProductSuccessMessage", CultureInfo.CurrentCulture) ?? "UpdateProductSuccessMessage";
}
