using System;
using System.Net.Http;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

namespace pdf
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var html = await FetchHtml("https://www.variant.no");
            var pdfBytes = BuildPdf(html);
            File.WriteAllBytes("./hello.pdf", pdfBytes);
        }

        private static byte[] BuildPdf(string html)
        {
            return pdfConverter.Convert(new HtmlToPdfDocument()
            {
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = html
                    }
                }
            });
        }
 
        private static async Task<string> FetchHtml(string url)
        {
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"FetchHtml failed {response.StatusCode} : {response.ReasonPhrase}");        
            }
            return await response.Content.ReadAsStringAsync();
        }
 
        static HttpClient httpClient = new HttpClient();
        static IConverter pdfConverter = new SynchronizedConverter(new PdfTools());
    }
}
