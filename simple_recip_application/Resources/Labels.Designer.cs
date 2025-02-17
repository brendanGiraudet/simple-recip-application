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
}
