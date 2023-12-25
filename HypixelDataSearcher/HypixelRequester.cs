using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text;
using System.Text.Unicode;

namespace HypixelDataSearcher
{
    internal static class HypixelRequester
    {
        public static async Task<string> GetBodyAsync(HttpRequestMessage request)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            var content = string.Empty;
            try
            {
                var response = await client.SendAsync(request);

                using (var stream = new BrotliStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                using (var reader = new StreamReader(stream))
                {
                    content = await reader.ReadToEndAsync();
                }
            }
            catch
            {
            }
            return content;
        }
    }
}
