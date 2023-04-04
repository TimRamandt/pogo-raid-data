namespace DataThingy
{
    public class PokemonType
    {
        public string Name { get; set; }
        public Resistance Resistances { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Resistance
    {
        public List<PokemonType> NotEffective { get; set; }
        public List<PokemonType> IsWeakAgainst { get; set; }
        public List<PokemonType> Resists { get; set; }
    }
}
