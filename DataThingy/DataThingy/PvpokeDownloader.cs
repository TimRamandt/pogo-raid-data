using System.Runtime.CompilerServices;

namespace DataThingy
{
    public class PvpokeDownloader
    {
        private HttpClient _httpClient;

        public async Task UpdateDataAsync()
        {
            Console.WriteLine("Fetching GL data...");
            File.WriteAllText("gl.json", await MakeRequestAsync(League.Gl.DownloadUrl())); 
            Console.WriteLine("Fetching UL data...");
            File.WriteAllText("ul.json", await MakeRequestAsync(League.Ul.DownloadUrl())); 
            Console.WriteLine("Fetching ML data...");
            File.WriteAllText("ml.json", await MakeRequestAsync(League.Ml.DownloadUrl())); 
        }

        public bool CheckDataFiles()
        {
            return File.Exists("gl.json") && File.Exists("ul.json") && File.Exists("ml.json");
        }

        public async Task DownloadMissingFilesAsync()
        {
            if (!File.Exists("gl.json"))
            {
               Console.WriteLine("Fetching GL data...");
               File.WriteAllText("gl.json", await MakeRequestAsync(League.Gl.DownloadUrl())); 
            }

            if (!File.Exists("ul.json"))
            {
               Console.WriteLine("Fetching UL data...");
               File.WriteAllText("ul.json", await MakeRequestAsync(League.Ul.DownloadUrl())); 
            }

            if (!File.Exists("ml.json"))
            {
               Console.WriteLine("Fetching ML data...");
               File.WriteAllText("ml.json", await MakeRequestAsync(League.Ml.DownloadUrl())); 
            }
        }

        private async Task<string> MakeRequestAsync(string url)
        {
            using (_httpClient = new HttpClient()) {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                using (response) {
                    using HttpContent content = response.Content;
                    return await content.ReadAsStringAsync();
                }
            }
        }
    }

}
