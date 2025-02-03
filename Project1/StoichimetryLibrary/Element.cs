using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichimetryLibrary
{
    internal class Element : IMolecularElement
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public ushort AtomicNumber { get; set; }
        public double AtomicMass { get; set; }
        public ushort Period { get; set; }
        public ushort Group { get; set; }

        public ushort Multiplier { get; set; }

        //public Element(string symbol, string name, ushort atomicNumber, double atomicMass, ushort period, ushort group)
        //{
        //    Symbol = symbol;
        //    Name = name;
        //    AtomicNumber = atomicNumber;
        //    AtomicMass = atomicMass;
        //    Period = period;
        //    Group = group;
        //}
    }
}
