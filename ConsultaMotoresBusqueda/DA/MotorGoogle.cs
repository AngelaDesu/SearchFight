using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MotorGoogle : Motor
    {
        public MotorGoogle()
        {
            Nombre = "Google";
            APIkey = "AIzaSyCAW5mGYpmN6EvQCpYU3z2dbY8MAXbA-ks";
            Url = "https://www.googleapis.com/customsearch/v1?cx=002043362819668786901:f5gnziv9a24&key="+APIkey+"&output=xml_no_dtd&client=google-csbe&q=";
            UrlFraseExacta = "&exactTerms=";
        }
        
    }
}
