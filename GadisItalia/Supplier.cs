using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadisItalia
{
    [Table("Fornitori")]
    public class Supplier
    {
        [Key]
        public int FornitoreID { get; set; }
        public string RagioneSociale { get; set; }
        public string? RagioneSocialePerDocumenti { get; set; }
        public string? NomeInternoFornitore { get; set; }
        public string? Responsabile { get; set; }
        public string? CodiceFiscale { get; set; }
        public string? PartitaIva { get; set; }
        public string? Indirizzo { get; set; }
        public int? ComuneDestinazioneID { get; set; }
        public int? LocalitaDestinazioneID { get; set; }
        public string? CodicePostale { get; set; }
        public string? Sitoweb { get; set; }
        public string? Sitoweb2 { get; set; }
        public string? NoteContabili { get; set; }
        public string? CodFornitoreContabilita { get; set; }
        public long? Descrizione { get; set; }
        public long? DescrizioneLogistica { get; set; }
        public bool FlagAttivo { get; set; }
        public short? CodiceLingua { get; set; }
        public string? DatiBancari { get; set; }
        public string? NotePerPratica { get; set; }
        public string? NoteInterne { get; set; }
        public DateTime? DataLavoro { get; set; }
        public string? OperatoreCreazione { get; set; }
        public int? GruppoFornitoreID { get; set; }
        public short? Giudizio { get; set; }
        public int? IntermediarioID { get; set; }
        public int? FornitoreFatturazioneID { get; set; }
        public short? Valuta { get; set; }
        public string? GratuitaID { get; set; }
        public short? NumGiorniScadenzaReleaseFornitore { get; set; }
        public bool HaLamentele { get; set; }
        public string? StatoGDPR { get; set; }
        public DateTime? DataContrattoGDPR { get; set; }
        public string? CodTipoFornitoreGo2 { get; set; }
        public string? LocalitaNonConforme { get; set; }
        public int? AnnoUltimaPratica { get; set; }
        public int? NumUltimaPratica { get; set; }
        public DateTime? DataUltimaPratica { get; set; }
        public string? OperUltAgg { get; set; }
        public DateTime DataUltAgg { get; set; }
        public string? CodSDI { get; set; }
        public string? IndirizzoPEC { get; set; }
        public int? ZonaDestinazioneID { get; set; }
    }
}
