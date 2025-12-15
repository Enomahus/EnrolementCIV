namespace Infrastructure.Configurations
{
    public class PdfPrinterConfiguration
    {
        public string? ApiBaseUrl { get; set; }
        public string HtmlToPdfEndpoint { get; set; } = "pdf";
    }
}
