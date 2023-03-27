// See https://aka.ms/new-console-template for more information
using DataThingy;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
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