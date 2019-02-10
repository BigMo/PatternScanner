using PatternScanner.DTO;
using PatternScanner.DTO.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Parsing
{
    public interface ICodeParser
    {
        CodeText Parse(string text);
    }
}
