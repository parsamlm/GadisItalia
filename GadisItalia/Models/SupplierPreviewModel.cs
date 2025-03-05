using System.Windows.Media.Imaging;
using GadisItalia.Utils;

namespace GadisItalia.Models
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
        public string[]? Riferimenti { get; set; }
        public BitmapImage? FavoriteImage { get; set; }
        public SupplierCharacteristics? Characteristics { get; }
        private long? DescrizioneID { get; set; }

        public SupplierPreviewModel(Supplier supplier)
        {
            FornitoreID = supplier.FornitoreID;
            DescrizioneID = supplier.Descrizione;
            RagioneSociale = supplier.RagioneSociale;
            RagioneSocialePerDocumenti = supplier.RagioneSocialePerDocumenti;
            Responsabile = supplier.Responsabile;
            Sitoweb = SupplierUtils.FormatWebsiteUrl(supplier.Sitoweb);
            LocalitaDestinazioneID = supplier.LocalitaDestinazioneID;
            CodicePostale = SupplierUtils.FormatCodicePostale(supplier.CodicePostale);
            Indirizzo = supplier.Indirizzo;
            CodTipoFornitore = supplier.CodTipoFornitoreGo2;
            HaLamentele = supplier.HaLamentele;
            ComuneDestinazione = SupplierUtils.GetComuneDestinazione(supplier.ComuneDestinazioneID);
            Characteristics = SupplierUtils.GetSupplierCharacteristics(supplier.FornitoreID);
            Riferimenti = SupplierUtils.GetSupplierReferences(supplier.FornitoreID);
            Descrizione = SupplierUtils.GetDescrizione(DescrizioneID, 1);
            FavoriteImage = SupplierUtils.LoadFavoriteImage(supplier.FornitoreID);
            DescrizioneLogistica = supplier.DescrizioneLogistica?.ToString();
        }

        public void UpdateDescrizione(string language)
        {
            int languageCode = SupplierUtils.GetLanguageCode(language);
            Descrizione = SupplierUtils.GetDescrizione(DescrizioneID, languageCode);
        }
    }
}
