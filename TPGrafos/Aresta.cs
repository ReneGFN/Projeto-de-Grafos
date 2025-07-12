using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Aresta
    {
        public int Inicio;
        public int Fim;
        public double Peso;

        public Aresta(int Inicio, int Fim, double Peso)
        {
            this.Inicio = Inicio;
            this.Fim = Fim;
            this.Peso = Peso;
        }
    }
}