using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NzEscapeHtmlListingExtractor
{
    class Listing
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Island { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
    }
}
