using System.Globalization;

namespace DataThingy
{
    public class CsvParser
    {
        private const char Delimiter = ',';
        public static TypeChart ParseTypeChart()
        {
            var chart = new TypeChart();
            var csv = File.ReadAllLines("csv/typechart.csv");

            for(int row = 1; row < csv.Length; row++)
            {
                var entries = csv[row].Split(Delimiter);
                var name = entries[0];
                chart.Add(ParseResistances(row, csv, name));
            }

            return chart;
        }
        public static List<Pokemon> ParseLegendaries(TypeChart typeChart)
        {
            var legendaries = new List<Pokemon>();
            var csv = File.ReadAllLines("csv/legendaries.csv");
            var header = csv[0].Split(Delimiter);

            for(int row = 1; row < csv.Length; row++)
            {
                var entry = csv[row].Split(Delimiter);
                legendaries.Add(ParsePokemon(typeChart, header ,entry));
            }

            return legendaries;
        }

        private static Pokemon ParsePokemon(TypeChart typeChart, string[] header, string[] entry)
        {
            var types = new List<PokemonType>();
            bool isUB = false;
            bool isMega = false;

            for (int i = 1; i < entry.Length; i++)
            {
                if (entry[i] == string.Empty)
                {
                    continue;
                }

                if (header[i] == "Mega")
                {
                    isMega = true;
                    continue;
                }

                if (header[i] == "UB")
                {
                    isUB = true;
                    continue;
                }


                types.Add(typeChart.LookUp(header[i]));

            } 

            return new Pokemon(types, entry[0], isMega, isUB); 
        }

        private static PokemonType ParseResistances(int columnIndex, string[] csv, string name)
        {
            var notEffective = new List<PokemonType>();
            var weaknesses = new List<PokemonType>();
            var resists = new List<PokemonType>();

            for (int row = 1; row < csv.Length; row++)
            {
                var entries = csv[row].Split(Delimiter);
                string qName = entries[0].ToLower();
                switch (Convert.ToDouble(entries[columnIndex], new CultureInfo("en-US")))
                {
                    case 2:
                        weaknesses.Add(new PokemonType()
                        {
                            Name = qName
                        });
                        break;
                    case 0.5:
                        resists.Add(new PokemonType()
                        {
                            Name = qName
                        });
                        break;
                    case 0:
                        notEffective.Add(new PokemonType()
                        {
                            Name = qName
                        });
                        break;
                    default:
                        break;
                }
            }

            return new PokemonType()
            {
                Name = name.ToLower(),
                Resistances = new Resistance()
                {
                    NotEffective = notEffective,
                    IsWeakAgainst = weaknesses,
                    Resists = resists
                }
            };
        }
    } 
}
