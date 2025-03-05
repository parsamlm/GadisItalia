using GadisItalia.Models;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace GadisItalia.Utils
{
    public static class SupplierUtils
    {
        public static string? FormatWebsiteUrl(string? url)
        {
            if (url == null) return null;
            return url.Contains("https://") || url.Contains("http://") ? url : "http://" + url;
        }

        public static string FormatCodicePostale(string? codicePostale)
        {
            return $"(Codice Postale: {codicePostale})";
        }

        public static string GetComuneDestinazione(int? comuneDestinazioneID)
        {
            if (comuneDestinazioneID == null) return string.Empty;
            using (var context = new GadisDbContext())
            {
                return context.Database.SqlQuery<string>($"SELECT Nome FROM Destinazioni WHERE DestinazioneID = {comuneDestinazioneID}").FirstOrDefault();
            }
        }

        public static SupplierCharacteristics? GetSupplierCharacteristics(int fornitoreID)
        {
            using (var context = new GadisDbContext())
            {
                return context.Database.SqlQuery<SupplierCharacteristics>($"SELECT * FROM CaratteristicheFornitore WHERE FornitoreID = {fornitoreID}").FirstOrDefault();
            }
        }

        public static string[]? GetSupplierReferences(int fornitoreID)
        {
            using (var context = new GadisDbContext())
            {
                return context.Database.SqlQuery<string>($"SELECT EMAIL FROM RiferimentiFornitore WHERE FornitoreID = {fornitoreID}").ToArray();
            }
        }

        public static string? GetDescrizione(long? descrizioneID, int languageCode)
        {
            if (descrizioneID == null) return null;
            using (var context = new GadisDbContext())
            {
                return context.Database.SqlQuery<string>($"SELECT testo FROM StringheLingue WHERE idStringa = {descrizioneID} AND idLingua = {languageCode}").FirstOrDefault()?.Trim();
            }
        }

        public static BitmapImage? LoadFavoriteImage(int fornitoreID)
        {
            using (var context = new GadisDbContext())
            {
                int immagineID = context.Database.SqlQuery<int>($"SELECT ImmagineID FROM UsiImmagine WHERE FornitoreID = {fornitoreID}").FirstOrDefault();
                if (immagineID == 0) return null;

                string? nomeFile = context.Database.SqlQuery<string>($"SELECT NomeFile FROM Immagini WHERE ImmagineID = {immagineID}").FirstOrDefault();
                if (string.IsNullOrEmpty(nomeFile)) return null;

                string resourcesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                string filePath = Path.Combine(resourcesDirectory, "images", nomeFile);

                if (!File.Exists(filePath)) return null;

                try
                {
                    return new BitmapImage(new Uri(filePath, UriKind.Absolute));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading image: {ex.Message}");
                    return null;
                }
            }
        }

        public static int GetLanguageCode(string language)
        {
            return language switch
            {
                "it" => 1,
                "en" => 2,
                "de" => 3,
                "fr" => 4,
                _ => 1,
            };
        }
    }
}
