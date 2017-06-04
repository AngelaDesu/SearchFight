//using System;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Net;
using System.Web.Script.Serialization;
//using System.Threading.Tasks;
using DA;

namespace BL
{
    public class SearchFight
    {
        public List<DA.Query> Querys { get; set; }
        public List<DA.Winner> Winners { get; set; }
        public DA.Query WinnerTotal { get; set; }
                
        public SearchFight(string consulta)
        {
            consulta = consulta.Trim();
            Querys = new List<DA.Query>();
            Winners = new List<DA.Winner>();

            if(consulta == "")
            {
                return;
            }

            string[] consulta2 = consulta.Split(' ');
            bool comillas = false;
            string t1 = "";
            foreach (string t in consulta2)
            {
                if(t.IndexOf('"') == -1)
                {
                    if (comillas)
                    {
                        t1 = t1 + " " + t;
                    }
                    else
                    {
                        DA.Query q = new DA.Query(t);
                        Querys.Add(q);
                    }
                }
                if(t.IndexOf('"') == 0)
                {
                    comillas = true;
                    t1 = t;
                    if (t.IndexOf('"', 1) == t.Length - 1)
                    {
                        comillas = false;
                        DA.Query q = new DA.Query(t);
                        Querys.Add(q);
                    }
                }
                if(t.IndexOf('"') == t.Length - 1)
                {
                    comillas = false;
                    t1 = t1 + " " + t;
                    DA.Query q = new DA.Query(t1);
                    Querys.Add(q);
                }
                if(t.IndexOf('"') > 0 && t.IndexOf('"') < t.Length - 1)
                {
                    if (comillas)
                    {
                        t1 = t1 + " " + t;
                    }
                    else
                    {
                        DA.Query q = new DA.Query(t);
                        Querys.Add(q);
                    }
                }
            }
        }
        
        private void SearchGoogle()
        {
            if(Querys.Count() == 0)
            {
                return;
            }

            DA.MotorGoogle goo = new MotorGoogle();
            DA.NodoGoogle resultadoDeserializado = new NodoGoogle();
            string url = "";

            foreach(DA.Query q in Querys)
            {
                q.Texto = q.Texto.Replace("\"", "");
                string[] temp = q.Texto.Split(' ');
                if (temp.Count() == 1)
                {
                    url = goo.Url + WebUtility.UrlEncode(q.Texto);
                }
                else
                {
                    //string temp2 = q.Texto.Replace(" ", "+");
                    string temp2 = WebUtility.UrlEncode(q.Texto);
                    url = goo.Url + temp2 + goo.UrlFraseExacta + temp2;
                }
                
                using (WebClient wc = new WebClient())
                {
                    string resultJSON = wc.DownloadString(url);
                    resultJSON = resultJSON.Replace("\n", "");

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    resultadoDeserializado = jss.Deserialize<DA.NodoGoogle>(resultJSON);

                    DA.Resultado res = new DA.Resultado();
                    res.MotorBusqueda = goo;
                    res.Total = long.Parse(resultadoDeserializado.SearchInformation.TotalResults);

                    q.Resultados.Add(res);
                }
            }
        }

        private void SearchBing()
        {
            if (Querys.Count() == 0)
            {
                return;
            }

            DA.MotorBing bin = new MotorBing();
            DA.NodoBing resultadoDeserializado = new NodoBing();
            string url = "";

            foreach (DA.Query q in Querys)
            {
                q.Texto = q.Texto.Replace("\"", "");
                string[] temp = q.Texto.Split(' ');
                url = bin.Url + WebUtility.UrlEncode(q.Texto);
                
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add(bin.HeaderAPIkey, bin.APIkey);
                    string resultJSON = wc.DownloadString(url);
                    resultJSON = resultJSON.Replace("\n", "");

                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    resultadoDeserializado = jss.Deserialize<DA.NodoBing>(resultJSON);

                    DA.Resultado res = new DA.Resultado();
                    res.MotorBusqueda = bin;
                    res.Total = long.Parse(resultadoDeserializado.WebPages.TotalEstimatedMatches);

                    q.Resultados.Add(res);
                }
            }
        }

        public void Search()
        {
            SearchGoogle();
            SearchBing();
        }

        public void CalcularGanadores()
        {
            CalcularGanador(new DA.MotorGoogle());
            CalcularGanador(new DA.MotorBing());
            CalcularGanadorTotal();
        }

        private void CalcularGanadorTotal()
        {
            if (Querys.Count() == 0)
            {
                return;
            }

            long mayor = 0;
            long suma = 0;
            DA.Query mayorQuery = new DA.Query("");
            List<DA.Resultado> resxquery = new List<DA.Resultado>();

            foreach (DA.Query q in Querys)
            {
                resxquery = q.Resultados;
                suma = 0;
                foreach(DA.Resultado r in resxquery)
                {
                    suma += r.Total;
                }
                if(suma > mayor)
                {
                    mayor = suma;
                    mayorQuery = q;
                }
            }

            WinnerTotal = mayorQuery;
        }

        private void CalcularGanador(DA.Motor motor)
        {
            if (Querys.Count() == 0)
            {
                return;
            }

            long mayor = 0;
            DA.Winner mayorQuery = new DA.Winner();
            mayorQuery.Motor = motor;
            long total = 0;
            List<DA.Resultado> resxquery = new List<DA.Resultado>();

            foreach(DA.Query q in Querys)
            {
                resxquery = q.Resultados;
                foreach(DA.Resultado r in resxquery)
                {
                    if(motor.Nombre == r.MotorBusqueda.Nombre)
                    {
                        total = r.Total;
                    }
                }
                if(total > mayor)
                {
                    mayor = total;
                    mayorQuery.Query = q;
                }
            }

            Winners.Add(mayorQuery);
        }
    }
}
