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
    public static string SuccessMessage => resourceManager.GetString("SuccessMessage", CultureInfo.CurrentCulture) ?? "SuccessMessage";
}
