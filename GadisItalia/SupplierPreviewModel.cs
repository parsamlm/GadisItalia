using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace GadisItalia
{
    public class SupplierPreviewModel
    {
        public int FornitoreID { get; set; }

        public string selectedLanguage { get; set; }
        public string RagioneSociale { get; set; }
        public string? RagioneSocialePerDocumenti { get; set; }
        public string? Responsabile { get; set; }
        public string? Sitoweb { get; set; }

        public int? LocalitaDestinazioneID { get; set; }
        public string ComuneDestinazione { get; set; }
        public string? CodicePostale { get; set; }
        public string? Indirizzo { get; set; }

        public string? CodTipoFornitore { get; set; }
        public bool HaLamentele { get; set; }
        public string? Descrizione { get; set; }
        public string? DescrizioneLogistica { get; set; }
        public BitmapImage? FavoriteImage { get; set; }
        public SupplierCharacteristics? Characteristics { get; }
        private long? descrizioneID { get; set; }

        public SupplierPreviewModel(Supplier supplier)
        {
            FornitoreID = supplier.FornitoreID;
            descrizioneID = supplier.Descrizione;
            RagioneSociale = supplier.RagioneSociale;
            RagioneSocialePerDocumenti = supplier.RagioneSocialePerDocumenti;
            Responsabile = supplier.Responsabile;
            string url = supplier.Sitoweb;
            if (url != null)
            {
                if (url.Contains("https://") || url.Contains("http://"))
                {
                    Sitoweb = url;
                }
                else
                {
                    Sitoweb = "http://" + url;
                }
            }
            LocalitaDestinazioneID = supplier.LocalitaDestinazioneID;
            CodicePostale = $"(Codice Postale: {supplier.CodicePostale})";
            Indirizzo = supplier.Indirizzo;
            CodTipoFornitore = supplier.CodTipoFornitoreGo2;
            HaLamentele = supplier.HaLamentele;
            if (supplier.ComuneDestinazioneID != null)
            {
                using (var context = new GadisDbContext())
                {
                    ComuneDestinazione = context.Database.SqlQuery<string>($"SELECT Nome FROM Destinazioni WHERE DestinazioneID = {supplier.ComuneDestinazioneID}").FirstOrDefault();
                    Characteristics = context.Database.SqlQuery<SupplierCharacteristics>($"SELECT * FROM CaratteristicheFornitore WHERE FornitoreID = {supplier.FornitoreID}").FirstOrDefault();

                    if (descrizioneID != null)
                    {
                        Descrizione = context.Database.SqlQuery<string>($"SELECT testo FROM StringheLingue WHERE idStringa = {descrizioneID} AND idLingua = 1").FirstOrDefault()?.Trim();
                    }
                    int ImmagineID = context.Database.SqlQuery<int>($"SELECT ImmagineID FROM UsiImmagine WHERE FornitoreID = {supplier.FornitoreID}").FirstOrDefault();
                    if (ImmagineID != 0)
                    {
                        string? NomeFile = context.Database.SqlQuery<string>($"SELECT NomeFile FROM Immagini WHERE ImmagineID = {ImmagineID}").FirstOrDefault();
                        if (!string.IsNullOrEmpty(NomeFile))
                        {
                            string resourcesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                            string filePath = Path.Combine(resourcesDirectory, NomeFile);

                            if (File.Exists(filePath))
                            {
                                try
                                {
                                    FavoriteImage = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Error loading image: {ex.Message}");
                                }
                            }
                            else
                            {
                                Debug.WriteLine("File does not exist: " + filePath);
                            }
                        }
                    }
                }
            }
            DescrizioneLogistica = supplier.DescrizioneLogistica.ToString();
        }


        public void UpdateDescrizione(string language)
        {
            using (var context = new GadisDbContext())
            {
                int languageCode = 0;
                switch (language)
                {
                    case "it": languageCode = 1; break;
                    case "en": languageCode = 2; break;
                    case "de": languageCode = 3; break;
                    case "fr": languageCode = 4; break;
                    default: languageCode = 1; break;
                }
                Descrizione = context.Database.SqlQuery<string>($"SELECT testo FROM StringheLingue WHERE idStringa = {descrizioneID} AND idLingua = {languageCode}").FirstOrDefault()?.Trim();
            }
        }
    }

}