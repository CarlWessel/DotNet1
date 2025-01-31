using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichimetryLibrary
{
    internal class Element : IElement
    {
        public string Symbol { get; }
        public string Name { get; }
        public ushort AtomicNumber { get; }
        public double AtomicMass { get; }
        public ushort Period { get; }
        public ushort Group { get; }

        public Element(string symbol, string name, ushort atomicNumber, double atomicMass, ushort period, ushort group)
        {
            Symbol = symbol;
            Name = name;
            AtomicNumber = atomicNumber;
            AtomicMass = atomicMass;
            Period = period;
            Group = group;
        }
    }
}
