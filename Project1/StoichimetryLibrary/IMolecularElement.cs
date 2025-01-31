using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichimetryLibrary
{
    public interface IMolecularElement : IElement
    {
        ushort Multiplier { get; }
    }
}
