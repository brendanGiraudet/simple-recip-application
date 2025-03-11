namespace simple_recip_application.Services;

public interface ITesseractServices
{
    string ExtractTextFromImage(byte[] imageData);
}