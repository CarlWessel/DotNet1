using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StoichimetryLibrary;

namespace StoichiometryLibrary
{
    public static class PeriodicTable
    {
        private static readonly List<IElement> _elements;

        static PeriodicTable()
        {
            var json = File.ReadAllText("PeriodicTableJSON.json");
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

            _elements = new List<IElement>();
            foreach (var item in data["elements"])
            {
                _elements.Add(new Element
                {
                    Symbol = item["symbol"],
                    Name = item["name"],
                    AtomicNumber = item["number"],
                    AtomicMass = item["atomic_mass"],
                    Period = item["period"],
                    Group = item["group"]
                });
            }
        }

        public static IElement[] Elements => _elements.ToArray();
    }
}