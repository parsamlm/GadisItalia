using GadisItalia;
using Microsoft.IdentityModel.Tokens;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace GadisItalia
{
    public partial class Fornitore : Window
    {
        private SupplierPreviewModel _supplierPreviewModel;
        private ResourceManager _resourceManager;

        public Fornitore(SupplierPreviewModel supplierPreviewModel, string language = "it")
        {
            InitializeComponent();
            _supplierPreviewModel = supplierPreviewModel;
            DataContext = _supplierPreviewModel;
            SetLanguage(language);

            SupplierDetailsLabel.Content = $"Fornitore {_supplierPreviewModel.FornitoreID} - {_supplierPreviewModel.RagioneSocialePerDocumenti}";
            RagioneSociale.Text = _supplierPreviewModel.RagioneSociale;
            RagioneSocialePerDocumenti.Text = _supplierPreviewModel.RagioneSocialePerDocumenti;
            Responsabile.Text = _supplierPreviewModel.Responsabile;
            SitoWeb.Text = _supplierPreviewModel.Sitoweb;
            var url = _supplierPreviewModel.Sitoweb;
            if (url != null)
            {
                SitoWeb_HL.NavigateUri = new Uri(url);
            }
            Indirizzo.Text = _supplierPreviewModel.Indirizzo;
            CodicePostale.Text = _supplierPreviewModel.CodicePostale;
            ComuneDestinazione.Text = _supplierPreviewModel.ComuneDestinazione;
            CodTipoFornitore.Text = _supplierPreviewModel.CodTipoFornitore;
            Descrizione.Text = _supplierPreviewModel.Descrizione?.ToString();
            DescrizioneLogistica.Text = _supplierPreviewModel.DescrizioneLogistica?.ToString();
            if(_supplierPreviewModel.FavoriteImage != null)
            {
                ImmaginePreferita.Source = _supplierPreviewModel.FavoriteImage;
            }

            if (_supplierPreviewModel.Characteristics != null)
            {
                var characteristics = _supplierPreviewModel.Characteristics;
                var characteristicsList = new List<string>();

                if (characteristics.IsTargetLusso) characteristicsList.Add("Target Lusso");
                if (characteristics.IsTargetAlto) characteristicsList.Add("Target Alto");
                if (characteristics.IsTargetMedioAlto) characteristicsList.Add("Target Medio Alto");
                if (characteristics.IsTargetMedio) characteristicsList.Add("Target Medio");
                if (characteristics.IsTargetMedioBasso) characteristicsList.Add("Target Medio Basso");
                if (characteristics.isAccettaAnimali) characteristicsList.Add("Accetta Animali");
                if (characteristics.isAccessibileDisabili) characteristicsList.Add("Accessibile Disabili");
                if (characteristics.IsFornitoreAlberghiero) characteristicsList.Add("Fornitore Alberghiero");
                if (characteristics.IsLocalePubblico) characteristicsList.Add("Locale Pubblico");
                if (characteristics.IsGuida) characteristicsList.Add("Guida");
                if (characteristics.IsFornitoreTrasporti) characteristicsList.Add("Fornitore Trasporti");
                if (characteristics.IsAltroTipoForn) characteristicsList.Add("Altro Tipo Forn");

                AltreCaratteristiche.Text = string.Join(", ", characteristicsList);
            }

            if (_supplierPreviewModel.Riferimenti != null)
            {
                Riferimenti.Text = string.Join(", ", _supplierPreviewModel.Riferimenti);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Subscribe to the SelectionChanged event of the ComboBox
            var languageComboBox = FindName("SelezioneLinguaComboBox") as ComboBox;
            if (languageComboBox != null)
            {
                languageComboBox.SelectionChanged += LanguageChanged;
            }
        }

        private void SetLanguage(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            _resourceManager = new ResourceManager("GadisItalia.Resources", typeof(Fornitore).Assembly);
            _supplierPreviewModel.UpdateDescrizione(language);
            Descrizione.Text = _supplierPreviewModel.Descrizione?.ToString();
            SupplierInformation_Label.Content = _resourceManager.GetString("InformazioniSulFornitore");
            Responsabile_Label.Content = $"{_resourceManager.GetString("Responsabile")}: ";
            SitoWeb_Label.Content = $"{_resourceManager.GetString("Sitoweb")}: ";
            Riferimenti_Label.Content = $"{_resourceManager.GetString("Riferimenti")}: ";
            RagioneSociale_Label.Content = $"{_resourceManager.GetString("RagioneSociale")}: ";
            Localizzazione_Label.Content = _resourceManager.GetString("Localizzazione");
            ComuneDestinazione_Label.Content = $"{_resourceManager.GetString("ComuneDestinazione")}: ";
            Indirizzo_Label.Content = $"{_resourceManager.GetString("Indirizzo")}: ";
            Caratteristiche_Label.Content = _resourceManager.GetString("Caratteristiche");
            Descrizione_Label.Content = $"{_resourceManager.GetString("Descrizione")}: ";
            DescrizioneLogistica_Label.Content = $"{_resourceManager.GetString("DescrizioneLogistica")}: ";
            CodiceTipoFornitore_Label.Content = $"{_resourceManager.GetString("CodTipoFornitore")}: ";
            AltreCaratteristiche_Label.Content = $"{_resourceManager.GetString("AltreCaratteristiche")}: ";
            Immagini_Label.Content = $"{_resourceManager.GetString("Immagini")}";
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedLang = selectedItem.Tag.ToString();
                SetLanguage(selectedLang);
            }
        }

        private List<(string Header, List<string> Lines)> PrepareDocumentData()
        {
            var characteristicsList = new List<string>();
            if (_supplierPreviewModel.Characteristics != null)
            {
                var characteristics = _supplierPreviewModel.Characteristics;
                if (characteristics.IsTargetLusso) characteristicsList.Add("Target Lusso");
                if (characteristics.IsTargetAlto) characteristicsList.Add("Target Alto");
                if (characteristics.IsTargetMedioAlto) characteristicsList.Add("Target Medio Alto");
                if (characteristics.IsTargetMedio) characteristicsList.Add("Target Medio");
                if (characteristics.IsTargetMedioBasso) characteristicsList.Add("Target Medio Basso");
                if (characteristics.isAccettaAnimali) characteristicsList.Add("Accetta Animali");
                if (characteristics.isAccessibileDisabili) characteristicsList.Add("Accessibile Disabili");
                if (characteristics.IsFornitoreAlberghiero) characteristicsList.Add("Fornitore Alberghiero");
                if (characteristics.IsLocalePubblico) characteristicsList.Add("Locale Pubblico");
                if (characteristics.IsGuida) characteristicsList.Add("Guida");
                if (characteristics.IsFornitoreTrasporti) characteristicsList.Add("Fornitore Trasporti");
                if (characteristics.IsAltroTipoForn) characteristicsList.Add("Altro Tipo Forn");
            }

            var sections = Nota_TextBox.Text.Length > 0 ? new List<(string Header, List<string> Lines)>
            {
                ("InformazioniSulFornitore", new List<string>
                {
                    $"{_resourceManager.GetString("Responsabile")}: {_supplierPreviewModel.Responsabile}",
                    $"{_resourceManager.GetString("RagioneSociale")}: {_supplierPreviewModel.RagioneSociale}",
                    $"{_resourceManager.GetString("Sitoweb")}: {_supplierPreviewModel.Sitoweb}"
                }),
                ("Localizzazione", new List<string>
                {
                    $"{_resourceManager.GetString("ComuneDestinazione")}: {_supplierPreviewModel.ComuneDestinazione}",
                    $"{_resourceManager.GetString("Indirizzo")}: {_supplierPreviewModel.Indirizzo}",
                    $"{_resourceManager.GetString("Riferimenti")}: {string.Join(", ", _supplierPreviewModel.Riferimenti)}",
                }),
                ("Caratteristiche", new List<string>
                {
                    $"{_resourceManager.GetString("Descrizione")}: {_supplierPreviewModel.Descrizione}",
                    $"{_resourceManager.GetString("DescrizioneLogistica")}: {_supplierPreviewModel.DescrizioneLogistica}",
                    $"{_resourceManager.GetString("CodTipoFornitore")}: {_supplierPreviewModel.CodTipoFornitore}",
                    $"{_resourceManager.GetString("AltreCaratteristiche")}: {string.Join(", ", characteristicsList)}"
                }),
                ("Extra", new List<string>
                {
                    $"{_resourceManager.GetString("Nota")}: {Nota_TextBox.Text}",
                }),
                ("Immagini", new List<string>{}),
            } : new List<(string Header, List<string> Lines)>
            {
                ("InformazioniSulFornitore", new List<string>
                {
                    $"{_resourceManager.GetString("Responsabile")}: {_supplierPreviewModel.Responsabile}",
                    $"{_resourceManager.GetString("RagioneSociale")}: {_supplierPreviewModel.RagioneSociale}",
                    $"{_resourceManager.GetString("Sitoweb")}: {_supplierPreviewModel.Sitoweb}"
                }),
                ("Localizzazione", new List<string>
                {
                    $"{_resourceManager.GetString("ComuneDestinazione")}: {_supplierPreviewModel.ComuneDestinazione}",
                    $"{_resourceManager.GetString("Indirizzo")}: {_supplierPreviewModel.Indirizzo}",
                    $"{_resourceManager.GetString("Riferimenti")}: {string.Join(", ", _supplierPreviewModel.Riferimenti)}",
                }),
                ("Caratteristiche", new List<string>
                {
                    $"{_resourceManager.GetString("Descrizione")}: {_supplierPreviewModel.Descrizione}",
                    $"{_resourceManager.GetString("DescrizioneLogistica")}: {_supplierPreviewModel.DescrizioneLogistica}",
                    $"{_resourceManager.GetString("CodTipoFornitore")}: {_supplierPreviewModel.CodTipoFornitore}",
                    $"{_resourceManager.GetString("AltreCaratteristiche")}: {string.Join(", ", characteristicsList)}"
                }),
                ("Immagini", new List<string>{}),
            };

            return sections;
        }

        private void ExportToPDF(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            _resourceManager = new ResourceManager("GadisItalia.Resources", typeof(Fornitore).Assembly);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 10, XFontStyleEx.Regular);
            XFont boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);

            double yPoint = 40;

            // Draw header
            DrawString(gfx, _supplierPreviewModel.RagioneSocialePerDocumenti ?? _supplierPreviewModel.RagioneSociale, new XFont("Verdana", 14, XFontStyleEx.Bold), XBrushes.Black, 0, yPoint, page.Width.Point, XStringFormats.TopCenter);
            yPoint += 40;

            // Draw sections
            var sections = PrepareDocumentData();
            foreach (var section in sections)
            {
                DrawSection(gfx, section.Header, section.Lines.ToArray(), boldFont, font, ref yPoint, page.Width.Point);
            }

            // Draw image if available
            if (_supplierPreviewModel.FavoriteImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(_supplierPreviewModel.FavoriteImage));
                    encoder.Save(ms);
                    XImage xImage = XImage.FromStream(ms);
                    gfx.DrawImage(xImage, 40, yPoint, 128, 128);
                    yPoint += 110;
                }
            }

            // Save the document
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{_supplierPreviewModel.FornitoreID}_{_resourceManager.GetString("Fornitore")}.pdf");
            document.Save(filePath);

            MessageBox.Show($"PDF saved at: {filePath}");
        }

        private void DrawString(XGraphics gfx, string text, XFont font, XBrush brush, double x, double y, double width, XStringFormat format = null)
        {
            gfx.DrawString(text, font, brush, new XRect(x, y, width, 0), format ?? XStringFormats.TopLeft);
        }

        private void DrawSection(XGraphics gfx, string headerKey, string[] lines, XFont headerFont, XFont font, ref double yPoint, double pageWidth)
        {
            DrawSectionHeader(gfx, _resourceManager.GetString(headerKey), headerFont, ref yPoint, pageWidth);
            foreach (var line in lines)
            {
                DrawString(gfx, line, font, XBrushes.Black, 40, yPoint, pageWidth);
                yPoint += 20;
            }
            yPoint += 10;
        }

        private void DrawSectionHeader(XGraphics gfx, string header, XFont font, ref double yPoint, double pageWidth)
        {
            DrawString(gfx, header, font, XBrushes.Black, 40, yPoint, pageWidth);
            yPoint += 20;
            gfx.DrawLine(XPens.Black, 40, yPoint, pageWidth - 40, yPoint);
            yPoint += 10;
        }

        private void ExportAsPDF_Click(object sender, RoutedEventArgs e)
        {
            var selectedLang = (SelezioneLinguaComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            if (!string.IsNullOrEmpty(selectedLang))
            {
                ExportToPDF(selectedLang);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var selectedLang = (SelezioneLinguaComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
            if (!string.IsNullOrEmpty(selectedLang))
            {
                PrintDocument(selectedLang);
            }
        }

        private void PrintDocument(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            _resourceManager = new ResourceManager("GadisItalia.Resources", typeof(Fornitore).Assembly);

            FlowDocument doc = new FlowDocument
            {
                FontFamily = new System.Windows.Media.FontFamily("Verdana"),
                PagePadding = new Thickness(50),
                ColumnWidth = double.PositiveInfinity // Ensure content spans the full width
            };

            // Add header
            AddParagraph(doc, _supplierPreviewModel.RagioneSocialePerDocumenti ?? _supplierPreviewModel.RagioneSociale, 18, FontWeights.Bold, TextAlignment.Center);

            // Add sections
            var sections = PrepareDocumentData();
            foreach (var section in sections)
            {
                AddSection(doc, section.Header, section.Lines.ToArray());
            }

            // Add image if available
            if (_supplierPreviewModel.FavoriteImage != null)
            {
                Image image = new Image
                {
                    Source = _supplierPreviewModel.FavoriteImage,
                    Width = 128,
                    Height = 128,
                    Margin = new Thickness(0, 0, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                BlockUIContainer container = new BlockUIContainer(image);
                doc.Blocks.Add(container);
            }

            // Print the document
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintTicket.PageMediaSize = new System.Printing.PageMediaSize(System.Printing.PageMediaSizeName.ISOA4);
            if (printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Printing FlowDocument");
            }
        }


        private void AddParagraph(FlowDocument doc, string text, double fontSize, FontWeight fontWeight, TextAlignment alignment)
        {
            Paragraph paragraph = new Paragraph(new Run(text))
            {
                FontSize = fontSize,
                FontWeight = fontWeight,
                TextAlignment = alignment,
                Margin = new Thickness(0, 10, 0, 10)
            };
            doc.Blocks.Add(paragraph);
        }

        private void AddSection(FlowDocument doc, string headerKey, string[] lines)
        {
            AddParagraph(doc, _resourceManager.GetString(headerKey), 16, FontWeights.Bold, TextAlignment.Left);
            AddHorizontalDivider(doc);
            foreach (var line in lines)
            {
                AddParagraph(doc, line, 14, FontWeights.Normal, TextAlignment.Left);
            }
        }

        private void AddHorizontalDivider(FlowDocument doc)
        {
            BlockUIContainer container = new BlockUIContainer();
            System.Windows.Shapes.Line line = new System.Windows.Shapes.Line
            {
                X1 = 0,
                X2 = 1,
                Stretch = System.Windows.Media.Stretch.Fill,
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 1,
                Margin = new Thickness(0, 0, 0, 5)
            };
            container.Child = line;
            doc.Blocks.Add(container);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

    }
}