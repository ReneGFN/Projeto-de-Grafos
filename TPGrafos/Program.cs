using System;
using System.IO;
using System.Collections.Generic;

class Aresta
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

    public override string ToString()
    {
        return $"{Inicio} -- {Peso} -> {Fim}";
    }
}
class Grafo
{
    public double quantVertices;
    public double quantArestas;
    public double DensidadeGrafo;

    public double[,] matrizAdjacencia;
    public List<Aresta> ListaArestas = new List<Aresta>();
    public Grafo(double quantVertices, double quantArestas)
    {
        this.quantVertices = quantVertices;
        this.quantArestas = quantArestas;
    }

    public double CalcDensidade()
    {
        double DensidadeGrafo = quantArestas / (quantVertices * (quantVertices - 1));

        return DensidadeGrafo;
    }
    public void ImprimirMatrizAdjacencia()
    {

    }
    public void ImprimirListaAdjacencia()
    {

    }
}
internal class Program
{
    static void Cabecalho()
    {
        Console.Clear();
        Console.WriteLine("Representação de um Grafo");
        Console.WriteLine("=============");
    }
    private static void Main(string[] args)
    {
        double densidade;
        double vertices = 0;
        double arestas = 0;

        Cabecalho();
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

        densidade = grafo.CalcDensidade();
        if (densidade < 0.5)
        {
            Console.WriteLine("Representando por meio de uma Lista de Adjacencia: ");
            grafo.ImprimirListaAdjacencia();
        }
        else
        {
            Console.WriteLine("Representando por meio de uma Matriz de Adjacencia: ");
            grafo.ImprimirMatrizAdjacencia();
        }
    }
}