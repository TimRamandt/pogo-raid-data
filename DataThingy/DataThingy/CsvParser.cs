using System.Globalization;

namespace DataThingy
{
    public class CsvParser
    {
        private const char Delimiter = ',';
        public static List<PokemonType> ParseTypeChart()
        {
            var types = new List<PokemonType>();
            var csv = File.ReadAllLines("typechart.csv");

            for(int row = 1; row < csv.Length; row++)
            {
                var entries = csv[row].Split(Delimiter);
                var name = entries[0];
                types.Add(ParseResistances(row, csv, name));
            }

            return types;
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
