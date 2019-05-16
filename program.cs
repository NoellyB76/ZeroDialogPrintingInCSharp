...
using System.Drawing.Printing;

public bool PrintPDF(string printer, string paperName, string filename, int copies)
{
    try {
        var printerSettings = new PrinterSettings {
            PrinterName = printer,
            Copies = (short)copies,
        };

        var pageSettings = new PageSettings(printerSettings) {
            Margins = new Margins(0, 0, 0, 0),
        };
        foreach (PaperSize paperSize in printerSettings.PaperSizes) {
            if (paperSize.PaperName == paperName) {
                pageSettings.PaperSize = paperSize;
                break;
            }
        }

        using (var document = PdfDocument.Load(filename)) {
            using (var printDocument = document.CreatePrintDocument()) {
                printDocument.PrinterSettings = printerSettings;
                printDocument.DefaultPageSettings = pageSettings;
                printDocument.PrintController = new StandardPrintController();
                printDocument.Print();
            }
        }
        return true;
    } catch {
        return false;
    }
}
