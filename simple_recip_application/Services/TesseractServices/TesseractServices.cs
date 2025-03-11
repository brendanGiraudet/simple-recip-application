using Tesseract;

namespace simple_recip_application.Services;

public class TesseractServices : ITesseractServices
{
    public string ExtractTextFromImage(byte[] imageData)
    {
        using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
        {
            using (var img = Pix.LoadFromMemory(imageData))
            {
                var page = engine.Process(img);
                return page.GetText(); // Renvoie le texte extrait
            }
        }
    }
}