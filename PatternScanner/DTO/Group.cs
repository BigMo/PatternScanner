using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.DTO
{
    public class Group : TransparentContainer<Pattern>
    {
        public string Name { get; set; }
    }
}
