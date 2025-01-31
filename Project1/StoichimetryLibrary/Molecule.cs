using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichimetryLibrary
{
    public class Molecule
    {
        public bool valid { get; }
        public string formula { get; set; }

        public Molecule(string formula)
        {
            this.formula = formula;
            valid = true;
        }

        public double CalcMass()
        {
            return 0.0;
        }

        //public IMolecularElement GetComposition() 
        //{
        //    return null;
        //}

    }
}
