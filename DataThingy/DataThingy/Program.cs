// See https://aka.ms/new-console-template for more information
using DataThingy;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var scraper = new PvpokeDownloader();
        if (scraper.CheckDataFiles())
        {
           Console.WriteLine("Would you like to update the PvP data? Y/N");
           var answer = Console.ReadKey();
           if (answer.Key == ConsoleKey.Y)
           {
               Console.WriteLine("started fetching the latest rankings, this can take a while");
               await scraper.UpdateDataAsync();
           }
        } else {

            Console.WriteLine("downloading missing data");
            await scraper.DownloadMissingFilesAsync();
        }

        var glRanking = League.Gl.ReadData();

        var typeChart = CsvParser.ParseTypeChart();

        var fighting = typeChart.FirstOrDefault(x => x.Name == "Fighting");
        Console.WriteLine(fighting?.Name);
        Console.WriteLine("Not effective!");
        foreach (var pokemonType in fighting?.Resistances?.NotEffective)
        {
            Console.WriteLine(pokemonType.Name);
        }
    }
}