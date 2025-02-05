using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
                _elements.Add(new Element(
                    symbol: (string)item["symbol"],
                    name: (string)item["name"],
                    atomicNumber: Convert.ToUInt16(item["number"]),
                    atomicMass: Convert.ToDouble(item["atomic_mass"]),
                    period: Convert.ToUInt16(item["period"]),
                    group: Convert.ToUInt16(item["group"])
                ));
            }
        }

        public static IElement[] Elements => _elements.ToArray();
    }
}