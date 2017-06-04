using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DA;

namespace ConsultaMotoresBusqueda
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://www.googleapis.com/customsearch/v1?cx=002043362819668786901:f5gnziv9a24&key=AIzaSyCAW5mGYpmN6EvQCpYU3z2dbY8MAXbA-ks&q=java
            string consulta = Console.ReadLine();

            BL.SearchFight s = new BL.SearchFight(consulta);
            
            // Haciendo las consultas a todos los motores de búsqueda
            s.Search();

            // Imprimiendo resultados
            foreach (DA.Query q in s.Querys)
            {
                Console.Write(q.Texto+": ");
                foreach(DA.Resultado r in q.Resultados)
                {
                    Console.Write(r.MotorBusqueda.Nombre + ": " + r.Total + " ");
                }
                Console.WriteLine();
            }

            // Calculando los ganadores de todos los motores de búqueda
            s.CalcularGanadores();

            // Imprimiendo ganadores
            foreach(DA.Winner w in s.Winners)
            {
                Console.WriteLine(w.Motor.Nombre + " winner: " + w.Query.Texto);
            }

            Console.WriteLine("Total winner: " + s.WinnerTotal.Texto);
            
            Console.Write("Click enter para terminar...");
            Console.ReadLine();
        }
    }
}
