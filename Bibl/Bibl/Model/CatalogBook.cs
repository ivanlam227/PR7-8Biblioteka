using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibl
{
    internal class CatalogBook
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public uint Year { get; set; }
        public string Publisher { get; set; }
        public uint PageCount { get; set; }
        public uint ID { get; set; }
        public uint Quantity { get; set; }
        public uint Instances { get; set; }
        public List<Reader> Readers { get; set; }
    }
}
