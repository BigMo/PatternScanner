using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Parsing
{
    public static class Parsers
    {
        public static ICodeParser[] All => new ICodeParser[] { HexParser.Instance, IDAParser.Instance, GhidraParser.Instance, CEParser.Instance };
    }
}
