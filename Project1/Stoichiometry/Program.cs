using StoichimetryLibrary;
using StoichiometryLibrary;
using System.Runtime.CompilerServices;

namespace Stoichiometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {

                DisplayHelp();
                string userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Error: No input provided.");
                    DisplayHelp();
                    return;
                }
                if (userInput == "/?")
                {
                    DisplayHelp();
                }
                else if (userInput == "/t")
                {
                    DisplayPeriodicTable();
                }
                //else if (userInput.StartsWith("/f:"))
                //{
                //    string filePath = userInput[0][3..];
                //    ProcessFileInput(filePath);
                //}
                else
                {
                    ProcessFormulas(userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Stoichiometry Carl, Trish, and Cody");
            Console.WriteLine("Usage:");
            Console.WriteLine("/?                  Display help");
            Console.WriteLine("/t                  Display periodic table");
            Console.WriteLine("/f:filepath         Computes the moldecular mass for each formula in the 'filepath'");
            Console.WriteLine("filepath            Specifies a text file containing molecular formulas, one per line");
        }

        private static void DisplayPeriodicTable()
        {
            var elements = PeriodicTable.Elements;
            Console.WriteLine("Atomic Number | Symbol | Name           | Atomic Mass | Period | Group");
            Console.WriteLine("----------------------------------------------------------------------");
            foreach (var element in elements)
            {
                Console.WriteLine($"{element.AtomicNumber,13} | {element.Symbol,6} | {element.Name,-14} | {element.AtomicMass,11:F4} | {element.Period,6} | {element.Group,5}");
            }
        }

        //private static void ProcessFileInput(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        Console.WriteLine($"Error: File '{filePath}' not found.");
        //        return;
        //    }

        //    string[] formulas = File.ReadAllLines(filePath);
        //    ProcessFormulas(formulas);
        //}

        private static void ProcessFormulas(string[] formulas)
        {
            foreach (var formula in formulas)
            {
                Molecule molecule = new(formula);
                if (!molecule.Valid)
                {
                    Console.WriteLine($"{formula} is NOT valid");
                    continue;
                }

                double mass = molecule.CalcMass();
                Console.WriteLine($"{formula} has a mass of {mass:F6}\n");
                foreach (var element in molecule.GetComposition())
                {
                    var periodicElement = PeriodicTable.Elements.FirstOrDefault(e => e.Symbol == element.Symbol);
                    if (periodicElement != null)
                    {
                        Console.WriteLine($"{element.Symbol} ({periodicElement.Name}) {periodicElement.AtomicMass} x {element.Multiplier} = {periodicElement.AtomicMass * element.Multiplier:F6}");
                    }
                }
            }
        }
    }
}
