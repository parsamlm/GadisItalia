using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace GadisItalia
{
    public class SupplierPreviewModel
    {
        public int FornitoreID { get; set; }
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

        public SupplierPreviewModel(Supplier supplier)
        {
            FornitoreID = supplier.FornitoreID;
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
            if(supplier.ComuneDestinazioneID != null)
            {
                using (var context = new GadisDbContext())
                {
                    ComuneDestinazione = context.Database.SqlQuery<string>($"SELECT Nome FROM Destinazioni WHERE DestinazioneID = {supplier.ComuneDestinazioneID}").FirstOrDefault();
                    Characteristics = context.Database.SqlQuery<SupplierCharacteristics>($"SELECT * FROM CaratteristicheFornitore WHERE FornitoreID = {supplier.FornitoreID}").FirstOrDefault();
                }
            }
            Descrizione = supplier.Descrizione.ToString();
            DescrizioneLogistica = supplier.DescrizioneLogistica.ToString();
        }
    }
}