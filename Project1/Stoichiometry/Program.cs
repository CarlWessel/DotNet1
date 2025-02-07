using StoichiometryLibrary;
using System.Runtime.CompilerServices;

namespace Stoichiometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            if (args.Length <=0 || args == null)
            {
                Console.WriteLine("Error: No input provided.");
                DisplayHelp();
            }
            else if (args[0] == "/?")
            {
                DisplayHelp();
            }
            else if (args[0] == "/t")
            {
                DisplayPeriodicTable();
            }
            else if (args[0].StartsWith("/f:"))
            {
                string filePath = args[0];
                ProcessFileInput(filePath);
            }
            else
            {
                ProcessFormulas(args);
            }
        }

        //Displays help info
        private static void DisplayHelp()
        {
            Console.WriteLine("Stoichiometry Carl Wessel, Trishia Salamangkit, and Cody Sykes");
            Console.WriteLine("Usage:");
            Console.WriteLine("/?                  Display help");
            Console.WriteLine("/t                  Display periodic table");
            Console.WriteLine("/f:filepath         Computes the moldecular mass for each formula in the 'filepath'");
            Console.WriteLine("filepath            Specifies a text file containing molecular formulas, one per line");
        }

        //Displays the periodic table
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

        //Proccesses file and formulas and outputs results
        private static void ProcessFileInput(string filePath)
        {
            filePath = filePath.Remove(0, 3);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' not found.");
                return;
            }

            string[] formulas = File.ReadAllLines(filePath);
            ProcessFormulas(formulas);
        }

        //Processes formula and calculates it
        private static void ProcessFormulas(string[] formulas)
        {
            Console.WriteLine("Stoichiometry Carl Wessel, Trishia Salamangkit, and Cody Sykes\n");
            foreach (var formula in formulas)
            {
                Molecule molecule = new(formula);
                if (!molecule.Valid)
                {
                    Console.WriteLine($"  {formula} is NOT valid\n");
                    continue;
                }

                double mass = molecule.CalcMass();
                Console.WriteLine($"  {formula} has a mass of {mass:F6}\n");

                foreach (var element in molecule.GetComposition())
                {
                    var periodicElement = PeriodicTable.Elements.FirstOrDefault(e => e.Symbol == element.Symbol);
                    if (periodicElement != null)
                    {
                        Console.WriteLine($"\t{element.Symbol} ({periodicElement.Name}) {periodicElement.AtomicMass} x {element.Multiplier} = {periodicElement.AtomicMass * element.Multiplier:F6}");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
