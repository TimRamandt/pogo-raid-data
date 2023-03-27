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

            var header = csv[0].Split(Delimiter);

            foreach (string row in csv.Skip(1))
            {
                var entries = row.Split(Delimiter);
                var pokemonType = new PokemonType()
                {
                    Name = entries[0],
                    Resistances = ParseResistances(entries, header),

                }; 

                types.Add(pokemonType);
            }

            return types;
        }

        private static Resistance ParseResistances(string[] entries, string[] header)
        {
            var notEffective = new List<PokemonType>();
            var superEffective = new List<PokemonType>();
            var resisted = new List<PokemonType>();

            for (int i = 1; i < entries.Length; i++)
            {
                switch (Convert.ToDouble(entries[i], new CultureInfo("en-US")))
                {
                    case 2:
                        superEffective.Add(new PokemonType() { 
                            Name = header[i]
                        });
                        break;
                    case 0.5:
                        notEffective.Add(new PokemonType() { 
                            Name = header[i]
                        });
                        break;
                    case 0:
                        resisted.Add(new PokemonType() { 
                            Name = header[i]
                        });
                        break;
                    default:
                        break;
                }
            }

            return new Resistance()
            {
                NotEffective = notEffective,
                SuperEffective = superEffective,
                Resisted = resisted
            };
        }
    } 
}
