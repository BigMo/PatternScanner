using PeNet;
using PeNet.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.PeExtension
{
    public class ImageSection
    {
        public IMAGE_SECTION_HEADER SectionHeader { get; private set; }
        public string Name { get; private set; }
        public long Address => SectionHeader.PointerToRawData;
        public long Size => SectionHeader.SizeOfRawData;
        private PeFile file;

        public ImageSection(PeFile file, IMAGE_SECTION_HEADER header)
        {
            this.file = file;
            SectionHeader = header;
            Name = Encoding.ASCII.GetString(header.Name);
            if (Name.IndexOf('\0') != -1)
                Name = Name.Substring(0, Name.IndexOf('\0'));
        }

        public override string ToString()
        {
            return $"{Name} (@{Address.ToString("X8")}, {SizeString(Size)})";
        }

        private static string[] sizes = new string[]{"B","KB","MB","GB"};
        private string SizeString(double size)
        {
            int idx = 0;
            while(size >= 1024 && idx < sizes.Length - 1)
            {
                size /= 1024;
                idx++;
            }
            return $"{size.ToString("0.###")} {sizes[idx]}";
        }
    }
}
