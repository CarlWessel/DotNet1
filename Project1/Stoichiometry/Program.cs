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
                else
                {
                    Console.WriteLine("Error, command unknown");
                    DisplayHelp();
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
    }
}
