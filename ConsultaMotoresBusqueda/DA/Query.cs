using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class Query
    {
        public string Texto { get; set; }
        public List<Resultado> Resultados { get; set; }

        public Query(string texto)
        {
            Texto = texto;
            Resultados = new List<DA.Resultado>();
        }
        
    }
}
