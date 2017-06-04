using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class NodoGoogle
    {
        public SearchInformation SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public string TotalResults { get; set; }
    }
}
