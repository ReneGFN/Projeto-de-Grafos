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
    public double DensidadeGrafo = 0;

    public double[,] matrizAdjacencia;
    public List<Aresta> Arestas = new List<Aresta>();
    public Grafo(int quantVertices, int quantArestas)
    {
        this.quantVertices = quantVertices;
        this.quantArestas = quantArestas;
    }

    public double CalcDensidade()
    {
        DensidadeGrafo = quantArestas/ (quantVertices * (quantVertices - 1));
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
        double op = 0;
        int vertices = 0;
        int arestas = 0;

        Cabecalho();
        Console.WriteLine("Digite o número de vértices: ");
        vertices = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite o número de arestas: ");
        arestas = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite o peso de cada aresta no seguinte formato separado por espaço: {Inicio Fim Peso}");


        Grafo grafo = new Grafo(vertices, arestas);

        op = grafo.CalcDensidade();
        if (op < 0.5)
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