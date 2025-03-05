using PdfSharp.Drawing;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace GadisItalia.Utils
{
    public static class DocumentUtils
    {
        public static List<string> SplitTextIntoLines(string text, int maxLineLength)
        {
            var lines = new List<string>();
            var words = text.Split(' ');

            var currentLine = new StringBuilder();
            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxLineLength)
                {
                    lines.Add(currentLine.ToString());
                    currentLine.Clear();
                }

                if (currentLine.Length > 0)
                {
                    currentLine.Append(" ");
                }

                currentLine.Append(word);
            }

            if (currentLine.Length > 0)
            {
                lines.Add(currentLine.ToString());
            }

            return lines;
        }

        public static void DrawString(XGraphics gfx, string text, XFont font, XBrush brush, double x, double y, double width, XStringFormat format = null)
        {
            gfx.DrawString(text, font, brush, new XRect(x, y, width, 0), format ?? XStringFormats.TopLeft);
        }

        public static void DrawSection(XGraphics gfx, string header, string[] lines, XFont headerFont, XFont font, ref double yPoint, double pageWidth, ResourceManager resourceManager)
        {
            DrawSectionHeader(gfx, resourceManager.GetString(header), headerFont, ref yPoint, pageWidth);
            foreach (var line in lines)
            {
                var lineHeight = gfx.MeasureString(line, font).Height;
                var wrappedLines = SplitTextIntoLines(line, 105); // Adjust the maxLineLength as needed
                foreach (var wrappedLine in wrappedLines)
                {
                    DrawString(gfx, wrappedLine, font, XBrushes.Black, 40, yPoint, pageWidth - 80);
                    yPoint += lineHeight + 5;
                }
            }
            yPoint += 10;
        }

        public static void DrawSectionHeader(XGraphics gfx, string header, XFont font, ref double yPoint, double pageWidth)
        {
            DrawString(gfx, header, font, XBrushes.Black, 40, yPoint, pageWidth);
            yPoint += 20;
            gfx.DrawLine(XPens.Black, 40, yPoint, pageWidth - 40, yPoint);
            yPoint += 10;
        }

        public static void AddParagraph(FlowDocument doc, string text, double fontSize, FontWeight fontWeight, TextAlignment alignment)
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

        public static void AddSection(FlowDocument doc, string header, string[] lines, ResourceManager resourceManager)
        {
            AddParagraph(doc, resourceManager.GetString(header), 16, FontWeights.Bold, TextAlignment.Left);
            AddHorizontalDivider(doc);
            foreach (var line in lines)
            {
                AddParagraph(doc, line, 14, FontWeights.Normal, TextAlignment.Left);
            }
        }

        public static void AddHorizontalDivider(FlowDocument doc)
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
    }
}
