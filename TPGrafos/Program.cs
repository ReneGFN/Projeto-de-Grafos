using System;
using System.IO;
using System.Collections.Generic;
using TP_Grafos;

class Program
{
    public static void Main(string[] args)
    {
        double densidade;
        int vertices = 0;
        int arestas = 0;
        int op = 1;
        Metodos metodos = new Metodos();
        Menu menu = new Menu();
        menu.Cabecalho();
        Console.WriteLine("Digite o número de vértices: ");
        vertices = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite o número de arestas: ");
        arestas = int.Parse(Console.ReadLine());
        Grafo grafo = new Grafo(vertices, arestas);
        for (int i = 0; i < grafo.quantArestas; i++)
        {
            Console.WriteLine("Digite o peso de cada aresta no seguinte formato separado por espaço: {Inicio,Fim,Peso}"); ;
            string[] partes = Console.ReadLine().Split(',');
            int inicio = int.Parse(partes[0]);
            int fim = int.Parse(partes[1]);
            double peso = double.Parse(partes[2]);
            grafo.ListaArestas.Add(new Aresta(inicio, fim, peso));
        }
        Console.WriteLine();
        densidade = metodos.CalcDensidade(grafo);

        while (op != 0)
        {
            menu.Corpo();
            Console.WriteLine("Digite a opção");
            op = int.Parse(Console.ReadLine());

            switch (op)
            {
                case 0:
                    break;
                case 1:
                    menu.Resultado();

                    if (densidade < 0.5)
                    {
                        metodos.ImprimirListaAdjacencia(grafo);
                    }
                    else
                    {
                        Console.WriteLine("Não é possivel pois a densidade é maior que 0.5");
                    }
                    break;
                case 2:
                    menu.Resultado();

                    if (densidade > 0.5)
                    {
                        metodos.ImprimirMatrizAdjacencia(grafo);
                    }
                    else
                    {
                        Console.WriteLine("Não é possivel pois a densidade é menor que 0.5");
                    }
                    break;
                case 3:
                    menu.Resultado();
                    metodos.ImpressaoGrafoArquivo();
                    break;
                case 4:
                    metodos.ImprimirArestasAdjacentes(grafo);
                    break;
                case 5:
                    metodos.ImprimirVerticesAdjacentesVertice(grafo);
                    break;
                case 6:
                    metodos.ImprimirArestasIncidentesVertice(grafo);
                    break;
                case 7:
                    metodos.ImprimirVerticesIncidentesArestas(grafo);
                    break;
                case 8:
                    metodos.ImprimirGrauDoVertice(grafo);
                    break;
                case 9:
                    metodos.VerticesSaoAdjacentes(grafo);
                    break;
                case 10:
                    metodos.SubstituirPesoAresta(grafo);
                    break;
                case 11:
                    metodos.TrocarVertices(grafo);
                    break;
                case 12:
                    metodos.BuscaEmLargura(grafo);
                    break;
                case 13:
                    metodos.BuscaEmProfundidade(grafo);
                    break;
                case 14:
                    metodos.Dijkstra(grafo);
                    break;
                case 15:
                    metodos.FloydWarshall(grafo);
                    break;
            }
        }
    }
}