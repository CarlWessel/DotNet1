using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public class Molecule
    {
        // Regex pattern to match valid chemical formulas
        private static readonly Regex FormulaRegex = new(@"^([A-Z][a-z]*\d*|\(([^()]+)\)\d*)+$");

        // Store chemical formula for get and set access
        public string Formula { get; set; }

        // Read-only to indicate if formula is valid
        public bool Valid
        {
            get
            {
                if (string.IsNullOrEmpty(Formula) || !FormulaRegex.IsMatch(Formula))
                    return false;
                try
                {
                    //valid formula
                    GetComposition();
                    return true;
                }
                catch (InvalidOperationException)
                {
                    //Not valid formula
                    return false;
                }
            }
        }

        // Default constructor; initializes to empty string
        public Molecule() => Formula = string.Empty;

        // Initialize Formula with the provided string argument
        public Molecule(string formula) => Formula = formula ?? string.Empty;

        // Method to calculate total mass of the molecule based on the formula
        public double CalcMass()
        {
            if (!Valid)
                throw new InvalidOperationException("Invalid chemical formula.");

            double totalMass = 0.0;
            foreach (var element in GetComposition())
            {
                var periodicElement = PeriodicTable.Elements.FirstOrDefault(e => e.Symbol == element.Symbol);
                if (periodicElement != null)
                {
                    totalMass += periodicElement.AtomicMass * element.Multiplier;
                }
            }
            return totalMass;
        }

        // Method to get composition of the molecule as an array of IMolecularElement
        public IMolecularElement[] GetComposition()
        {
            var elementCounts = new Dictionary<string, ushort>();
            var matches = Regex.Matches(Formula, @"([A-Z][a-z]*)(\d*)|\(([^()]+)\)(\d*)");

            foreach (Match match in matches)
            {
                string symbol = match.Groups[1].Value;
                ushort multiplier = (match.Groups[2].Success && ushort.TryParse(match.Groups[2].Value, out ushort parsedValue))
                    ? parsedValue
                    : (ushort)1;

                if (!string.IsNullOrEmpty(symbol))
                {
                    if (!PeriodicTable.Elements.Any(e => e.Symbol == symbol))
                        throw new InvalidOperationException($"Invalid element found: {symbol}");

                    if (elementCounts.ContainsKey(symbol))
                        elementCounts[symbol] += multiplier;
                    else
                        elementCounts[symbol] = multiplier;
                }
                else
                {
                    string subFormula = match.Groups[3].Value;
                    ushort groupMultiplier = match.Groups[4].Success ? ushort.Parse(match.Groups[4].Value) : (ushort)1;
                    var subMolecule = new Molecule(subFormula);

                    foreach (var element in subMolecule.GetComposition())
                    {
                        if (elementCounts.ContainsKey(element.Symbol))
                            elementCounts[element.Symbol] += (ushort)(element.Multiplier * groupMultiplier);
                        else
                            elementCounts[element.Symbol] = (ushort)(element.Multiplier * groupMultiplier);
                    }
                }
            }

            return elementCounts.Select(pair =>
                new Element(pair.Key, pair.Key, 0, 0.0, 0, 0, pair.Value)
            ).ToArray();
        }

    }
}
