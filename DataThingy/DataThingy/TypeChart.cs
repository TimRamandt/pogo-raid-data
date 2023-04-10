using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataThingy
{
    public class TypeChart : List<PokemonType>
    {
        public PokemonType LookUp(string name)
        {
            return this.Single(pt => pt.Name == name.ToLower());
        }
    }
}
