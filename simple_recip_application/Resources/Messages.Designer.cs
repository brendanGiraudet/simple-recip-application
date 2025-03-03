using System;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace simple_recip_application.Resources;

public class Messages
{
    private static readonly ResourceManager resourceManager =
        new ResourceManager("simple_recip_application.Resources.Messages",
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
}
