using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MotorBing : Motor
    {
        public MotorBing()
        {
            Nombre = "Bing";
            APIkey = "70d28f0ed4f446bc80bbc6e7c10f241c";
            Url = "https://api.cognitive.microsoft.com/bing/v5.0/search?q=";
            HeaderAPIkey = "Ocp-Apim-Subscription-Key";
        }
    }
}
