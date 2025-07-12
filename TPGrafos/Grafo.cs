using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Grafo
    {
        public int quantVertices;
        public int quantArestas;
        public double DensidadeGrafo;
        public int[,] matrizAdjacencia;
        public List<Aresta> ListaArestas = new List<Aresta>();

        public Grafo(int quantVertices, int quantArestas)
        {
            this.quantVertices = quantVertices;
            this.quantArestas = quantArestas;
        }
    }
}
