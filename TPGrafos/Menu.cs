using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
    internal class Menu
    {
        public Menu() { }

        public void Cabecalho()
        {
            Console.Clear();
            Console.WriteLine("Representação de um Grafo");
            Console.WriteLine("=============");
        }

        public void Corpo()
        {
            Console.WriteLine();
            Console.WriteLine("Menu");
            Console.WriteLine("=============");
            Console.WriteLine($"0.Sair\n1.Lista de Adjacência\n2.Matriz de Adjacência\n3.Leitura e impressão de um grafo já pronto\n4.Imprimir todas as arestas adjacentes a uma aresta a\n5.Imprimir todos os vértices adjacentes a um vértice v\n6.Imprimir todas as arestas incidentes a um vértice v\n7.Imprimir todos os vértices incidentes a uma aresta a\n8.Imprimir o grau do vértice v\n9.Determinar se dois vértices são adjacentes\n10.Substituir o peso de uma aresta a\n11.Trocar dois vértices\n12.Busca em Largura\n13.Busca em Profundidade\n14.Algoritmo de Dijkstra\n15.Algoritmo de Floyd Warshal");
            Console.WriteLine();
        }

        public void Resultado()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Solução Abaixo");
            Console.WriteLine("=============");
        }

    }
}
