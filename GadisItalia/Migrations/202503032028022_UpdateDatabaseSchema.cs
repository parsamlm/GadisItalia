namespace GadisItalia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fornitori",
                c => new
                    {
                        FornitoreID = c.Int(nullable: false, identity: true),
                        RagioneSociale = c.String(),
                        RagioneSocialePerDocumenti = c.String(),
                        NomeInternoFornitore = c.String(),
                        Responsabile = c.String(),
                        CodiceFiscale = c.String(),
                        PartitaIva = c.String(),
                        Indirizzo = c.String(),
                        ComuneDestinazioneID = c.Int(nullable: false),
                        LocalitaDestinazioneID = c.Int(),
                        CodicePostale = c.String(),
                        Sitoweb = c.String(),
                        Sitoweb2 = c.String(),
                        NoteContabili = c.String(),
                        CodFornitoreContabilita = c.String(),
                        Descrizione = c.Long(),
                        DescrizioneLogistica = c.Long(),
                        FlagAttivo = c.Boolean(nullable: false),
                        CodiceLingua = c.Short(),
                        DatiBancari = c.String(),
                        NotePerPratica = c.String(),
                        NoteInterne = c.String(),
                        DataLavoro = c.DateTime(),
                        OperatoreCreazione = c.String(),
                        GruppoFornitoreID = c.Int(),
                        Giudizio = c.Short(),
                        IntermediarioID = c.Int(),
                        FornitoreFatturazioneID = c.Int(),
                        Valuta = c.Short(),
                        GratuitaID = c.String(),
                        NumGiorniScadenzaReleaseFornitore = c.Short(),
                        HaLamentele = c.Boolean(nullable: false),
                        StatoGDPR = c.String(),
                        DataContrattoGDPR = c.DateTime(),
                        CodTipoFornitoreGo2 = c.String(),
                        LocalitaNonConforme = c.String(),
                        AnnoUltimaPratica = c.Int(),
                        NumUltimaPratica = c.Int(),
                        DataUltimaPratica = c.DateTime(),
                        OperUltAgg = c.String(),
                        DataUltAgg = c.DateTime(nullable: false),
                        CodSDI = c.String(),
                        IndirizzoPEC = c.String(),
                        ZonaDestinazioneID = c.Int(),
                    })
                .PrimaryKey(t => t.FornitoreID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fornitori");
        }
    }
}
