using Newtonsoft.Json;
using System.Text.Json;

namespace StoichimetryLibrary
{
    public class PeriodicTable
    {
        private const string _ELEMENTS_FILE = "PeriodicTableJSON.json";
        private readonly List<IElement> _elements;

        public PeriodicTable()
        {
            // Read PeriodicTable.JSON file
            var jsonString = File.ReadAllText(_ELEMENTS_FILE);

            // Deserialize into a temporary dynamic object to access the "elements" array
            // IMPORTANT!!! this is from chatgpt, I need help doing this in a proper way
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

            // Map JSON objects to the private Element class
            _elements = ((IEnumerable<dynamic>)jsonObject.elements)
                .Select(e => new Element(
                    (string)e.symbol,
                    (string)e.name,
                    (ushort)e.number,
                    (double)e.atomic_mass,
                    (ushort)e.period,
                    (ushort)e.group
                )).Cast<IElement>().ToList();
        }

        public void Print()
        {
            foreach (var element in _elements)
            {
                Console.WriteLine($"{element.Name}");
            }
        }

    }
}
