using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadisItalia.Models
{
    [Table("CaratteristicheFornitore")]
    public class SupplierCharacteristics
    {
        [Key]
        public int FornitoreID { get; }
        public bool IsTargetLusso {  get; set; }
        public bool IsTargetAlto { get; set; }
        public bool IsTargetMedioAlto { get; set; }
        public bool IsTargetMedio { get; set; }
        public bool IsTargetMedioBasso { get; set; }
        public bool isAccettaAnimali { get; set; }
        public bool isAccessibileDisabili { get; set; }
        public short? NumGiorniScadPreArrivoGruppo { get; set; }
        public short? NumGiorniScadPostArrivoGruppo { get; set; }
        public bool IsFornitoreAlberghiero { get; set; }
        public bool IsLocalePubblico { get; set; }
        public bool IsGuida { get; set; }
        public bool IsFornitoreTrasporti { get; set; }
        public bool IsAltroTipoForn { get; set; }
        public string? NoteSuCaratteristiche { get; set; }
    }
}