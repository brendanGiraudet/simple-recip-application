namespace simple_recip_application.Extensions;

public static class ByteArrayExtensions
{
    public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);

    public static string GetImageSource(this byte[] bytes)
    {
        if (bytes != null && bytes.Length > 0)
        {
            var base64String = bytes.ToBase64String();

            return $"data:image/png;base64,{base64String}";
        }
        
        return "placeholder.png"; // Image par défaut
    }
}
