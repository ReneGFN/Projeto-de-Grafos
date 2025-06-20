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

    public double CalcDensidade()
    {
        double DensidadeGrafo = (double)quantArestas / (quantVertices * (quantVertices - 1.0));

        return DensidadeGrafo;
    }

    public void ImprimirMatrizAdjacencia()
    {
        matrizAdjacencia = new int[quantVertices, quantVertices];

        for (int i = 0; i < quantVertices; i++)
        {
            for (int j = 0; j < quantVertices; j++)
            {
                matrizAdjacencia[i, j] = 0;
            }
        }

        foreach (Aresta aresta in ListaArestas)
        {
            matrizAdjacencia[aresta.Inicio, aresta.Fim] = 1;
        }

        Console.WriteLine("Matriz de Adjacência:");
        for (int i = 0; i < quantVertices; i++)
        {
            for (int j = 0; j < quantVertices; j++)
            {
                Console.Write($"{matrizAdjacencia[i, j]}");
            }
            Console.WriteLine();
        }
    }
    public void ImprimirListaAdjacencia()
    {
        List<LinkedList<int>> listaAdjacencia = new List<LinkedList<int>>();

        for (int i = 0; i < quantVertices; i++)
        {
            listaAdjacencia.Add(new LinkedList<int>());
        }

        foreach (Aresta aresta in ListaArestas)
        {
            listaAdjacencia[aresta.Inicio].AddLast(aresta.Fim);
        }

        for (int i = 0; i < quantVertices; i++)
        {
            Console.Write($"Vértice {i}");
            foreach (int destino in listaAdjacencia[i])
            {
                Console.Write($" -> {destino}");
            }
            Console.WriteLine();
        }
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
        int vertices = 0;
        int arestas = 0;

        Cabecalho();
        Console.WriteLine("Digite o número de vértices e arestas: ");
        string[] VertArestas = Console.ReadLine().Split(' ');
        vertices = int.Parse(VertArestas[0]);
        arestas = int.Parse(VertArestas[1]);

        Grafo grafo = new Grafo(vertices, arestas);
        for (int i = 0; i < grafo.quantArestas; i++)
        {
            Console.WriteLine("Digite cada aresta no formato com um espaço entre: Inicio Fim Peso");
            string[] partes = Console.ReadLine().Split(' ');
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