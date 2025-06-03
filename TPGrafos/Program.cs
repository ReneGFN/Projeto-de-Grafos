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
    public List<Aresta>;

    public double CalcDensidade(int quantVertices, int quantArestas)
    {
        DensidadeGrafo = quantArestas/ (quantVertices * (quantVertices - 1));
        return DensidadeGrafo;
    }
}
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}