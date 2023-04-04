using DataThingy;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private List<PokemonType> _typeChart;
        [SetUp]
        public void Setup()
        {
            _typeChart = CsvParser.ParseTypeChart(); 
        }


        [Test]
        public void TestMonoType()
        {
            var types = new List<PokemonType>()
            {
                _typeChart.Single(x => x.Name == "water"),
            };
            var kyogre = new Pokemon(types, "kyogre");
            var weaknesses = kyogre.GetWeaknesses();
            Assert.That(weaknesses, Has.Count.EqualTo(2));
            Assert.That(PrintWeaknesses(weaknesses), Is.EqualTo("electric, grass"));
        }

        [Test]
        public void TestDualType()
        {
            var types = new List<PokemonType>()
            {
                _typeChart.Single(x => x.Name == "ice"),
                _typeChart.Single(x => x.Name == "dragon"),
            };
            var kyurem = new Pokemon(types, "kyurem");
            var weaknesses = kyurem.GetWeaknesses();
            Assert.That(weaknesses, Has.Count.EqualTo(5));
            Assert.That(PrintWeaknesses(weaknesses), Is.EqualTo("fighting, rock, steel, dragon, fairy"));
        }

        [Test]
        public void TestWithNotEffective()
        {
            var types = new List<PokemonType>()
            {
                _typeChart.Single(x => x.Name == "flying"),
                _typeChart.Single(x => x.Name == "electric"),
            };
            var zapdos = new Pokemon(types, "zapdos");
            var weaknesses = zapdos.GetWeaknesses();
            Assert.That(weaknesses, Has.Count.EqualTo(2));
            Assert.That(PrintWeaknesses(weaknesses), Is.EqualTo("ice, rock"));
        }

        [Test]
        public void TestSETimes4()
        {
            var types = new List<PokemonType>()
            {
                _typeChart.Single(x => x.Name == "flying"),
                _typeChart.Single(x => x.Name == "dragon"),
            };
            var rayqauza = new Pokemon(types, "articuno");
            var weaknesses = rayqauza.GetWeaknesses();
            Assert.That(weaknesses, Has.Count.EqualTo(1));
            Assert.That(PrintWeaknesses(weaknesses), Is.EqualTo("ice"));
        }

        private string PrintWeaknesses(List<PokemonType> weaknesses)
        {
            string output = string.Empty;
            foreach(var weakness in weaknesses)
            {
                output += $"{weakness.Name}, ";
            }

            return output.TrimEnd(new char[] {',',' '});
        }
    }
}
