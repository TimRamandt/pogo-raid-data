
namespace DataThingy
{
    public class Pokemon {
        public List<PokemonType> Types { get; private set; }
        public string Name { get; private set; }

        public bool IsMega { get; private set; }
        public bool IsUltraBeast { get; private set; }
        public Pokemon(List<PokemonType> types, string name, bool isMega = false, bool isUB = false)
        {
            if (types.Count > 2)
            {
                throw new ArgumentException("Pokemon can only have a max of two types");
            }
            Types = types;
            Name = name;
            IsMega = isMega;
            IsUltraBeast = isUB;
        }

        public List<PokemonType> GetWeaknesses()
        {
            var superEffective = new List<PokemonType>();
            var NotEffective = new List<string>();

            foreach (var pokemonType in Types)
            {
                foreach (var se in pokemonType.Resistances.IsWeakAgainst)
                {
                    superEffective.Add(se);
                }

                foreach (var re in pokemonType.Resistances.Resists)
                {
                    NotEffective.Add(re.Name);
                }

                foreach (var ne in pokemonType.Resistances.NotEffective)
                {
                    NotEffective.Add(ne.Name);
                }
            }
            
            foreach (var ne in NotEffective)
            {
                for(int i = 0; i < superEffective.Count; i++)
                {
                    if (superEffective[i].Name == ne)
                    {
                        superEffective.RemoveAt(i);
                        break;
                    }
                }
            }

            var doubleWeakness = FindDoubleWeakness(superEffective);
            


            return doubleWeakness == null ? superEffective : new List<PokemonType>() { doubleWeakness };
        }

        public override string ToString()
        {
            return Name;
        }

        private PokemonType? FindDoubleWeakness(List<PokemonType> superEffective)
        {
            Dictionary<string, int> entries = new Dictionary<string, int>();
            for (int i = 0; i < superEffective.Count; i++)
            {
                var name = superEffective[i].Name;
                if (!entries.ContainsKey(name))
                {
                    entries.Add(name, 1);
                    continue;
                }
                entries[name]++;
            }

            var doubleWeakness = entries.Aggregate((x, y) => x.Value > y.Value ? x : y);
            if (doubleWeakness.Value > 1)
            {
                return superEffective.FirstOrDefault(pt => pt.Name == doubleWeakness.Key);
            }

            return null;

        }

    }
}
