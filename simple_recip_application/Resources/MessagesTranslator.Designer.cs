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
    public static string AddIngredientSuccessMessage => resourceManager.GetString("AddIngredientSuccessMessage", CultureInfo.CurrentCulture) ?? "AddIngredientSuccessMessage";
    public static string AddIngredientErrorMessage => resourceManager.GetString("AddIngredientErrorMessage", CultureInfo.CurrentCulture) ?? "AddIngredientErrorMessage";
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
}
