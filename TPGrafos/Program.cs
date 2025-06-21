using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
}
internal class Metodos
{
    public Metodos() { }

    Menu menu = new Menu();

    public double CalcDensidade(Grafo grafo)
    {
        double DensidadeGrafo = (double)grafo.quantArestas / (grafo.quantVertices * (grafo.quantVertices - 1.0));

        return DensidadeGrafo;
    }

    public void ImprimirListaAdjacencia(Grafo grafo)
    {
        List<List<int>> listaAdj = new List<List<int>>();

        for (int i = 0; i < grafo.quantVertices; i++)
        {
            listaAdj.Add(new List<int>());
        }

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            listaAdj[aresta.Inicio].Add(aresta.Fim);
        }

        for (int i = 0; i < grafo.quantVertices; i++)
        {
            Console.Write($"Vértice {i}");
            foreach (int proximo in listaAdj[i])
            {
                Console.Write($" -> {proximo}");
            }
            Console.WriteLine();
        }
    }

    public void ImprimirMatrizAdjacencia(Grafo grafo)
    {
        grafo.matrizAdjacencia = new int[grafo.quantVertices, grafo.quantVertices];

        for (int i = 0; i < grafo.quantVertices; i++)
        {
            for (int j = 0; j < grafo.quantVertices; j++)
            {
                grafo.matrizAdjacencia[i, j] = 0;
            }
        }

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            grafo.matrizAdjacencia[aresta.Inicio, aresta.Fim] = 1;
        }

        Console.WriteLine("Matriz de Adjacência:");
        for (int i = 0; i < grafo.quantVertices; i++)
        {
            for (int j = 0; j < grafo.quantVertices; j++)
            {
                Console.Write($"{grafo.matrizAdjacencia[i, j]}");
            }
            Console.WriteLine();
        }
    }

    public void ImpressaoGrafoArquivo()
    {
        int count = 0;
        Grafo grafoArq = null;
        try
        {
            StreamReader arquivo = new StreamReader("GrafoEntrada.txt", Encoding.UTF8);
            string linha = arquivo.ReadLine();
            while (linha != null)
            {
                count++;

                if (count == 1)
                {
                    string[] VerArestas = linha.Split(' ');
                    int vertices = int.Parse(VerArestas[0]);
                    int arestas = int.Parse(VerArestas[1]);
                    grafoArq = new Grafo(vertices, arestas);
                }
                linha = arquivo.ReadLine();
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] partes = linha.Split(' ');
                    int inicio = int.Parse(partes[0]);
                    int fim = int.Parse(partes[1]);
                    double peso = double.Parse(partes[2]);

                    grafoArq.ListaArestas.Add(new Aresta(inicio, fim, peso));
                }
            }
            arquivo.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        double densidade = CalcDensidade(grafoArq);

        if (densidade < 0.5)
        {
            ImprimirListaAdjacencia(grafoArq);
        }
        else
        {
            ImprimirMatrizAdjacencia(grafoArq);
        }
    }

    public void ImprimirArestasAdjacentes(Grafo grafo)
    {
        menu.Resultado();
        Console.Write("Informe o vértice de início da aresta:");
        int inicio = int.Parse(Console.ReadLine());
        Console.Write("Informe o vértice de fim da aresta:");
        int fim = int.Parse(Console.ReadLine());

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            if (!((aresta.Inicio == inicio) && (aresta.Fim == fim)) && (aresta.Inicio == inicio || aresta.Fim == inicio || aresta.Inicio == fim || aresta.Fim == fim))
            {
                Console.WriteLine($"Aresta de {aresta.Inicio} para {aresta.Fim} com peso {aresta.Peso}");
            }
        }
    }
    public void ImprimirVerticesAdjacentesVertice(Grafo grafo)
    {
        menu.Resultado();
        Console.WriteLine("Informe o vértice:");
        int vertice = int.Parse(Console.ReadLine());
        if (vertice < 0 || vertice >= grafo.quantVertices)
        {
            Console.WriteLine($"Vértice inválido! Informe um valor entre 0 e {grafo.quantVertices - 1}.");
            return;
        }
        bool encontrou = false;
        Console.WriteLine($"\nVértices adjacentes ao vértice {vertice}:");
        foreach(Aresta aresta in grafo.ListaArestas)
        {
            if (aresta.Inicio == vertice)
            {
                Console.WriteLine(aresta.Fim);
                encontrou = true;
            }
            else if (aresta.Fim == vertice)
            {
                Console.WriteLine(aresta.Inicio);
                encontrou = true;
            }
        }
        if (!encontrou)
        {
            Console.WriteLine("Nenhum vértice adjacente encontrado.");
        }
    }
    public void ImprimirArestasIncidentesVertice(Grafo grafo)
    {
        menu.Resultado();
        Console.WriteLine("Informe o vértice:");
        int vertice = int.Parse(Console.ReadLine());
        if (vertice < 0 || vertice >= grafo.quantVertices)
        {
            Console.WriteLine($"Vértice inválido! Informe um valor entre 0 e {grafo.quantVertices - 1}.");
            return;
        }
        bool encontrou = false;
        Console.WriteLine($"\nArestas incidentes ao vértice {vertice}:");
        foreach(Aresta aresta in grafo.ListaArestas)
        {
            if( aresta.Inicio == vertice || aresta.Fim == vertice)
            {
                aresta.ToString();
                encontrou = true;
            }
        }
        if (!encontrou)
        {
            Console.WriteLine("Nenhuma aresta incidente detectada.");
        }
    }
    public void ImprimirVerticesIncidentesArestas(Grafo grafo)
    {
        menu.Resultado();
        bool encontrou = false;
        Console.Write("Informe o vértice de início da aresta: ");
        int inicio = int.Parse(Console.ReadLine());
        Console.Write("Informe o vértice de fim da aresta: ");
        int fim = int.Parse(Console.ReadLine());

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            if (aresta.Inicio == inicio && aresta.Fim == fim)
            {
                encontrou = true;
                break;
            }
        }

        if (encontrou)
        {
            Console.WriteLine($"\nVértices incidentes à aresta ({inicio} → {fim}):");
            Console.WriteLine($"- Vértice de origem: {inicio}");
            Console.WriteLine($"- Vértice de destino: {fim}");
        }
        else
        {
            Console.WriteLine("\nAresta não encontrada no grafo.");
        }
    }
    public bool VerticesSaoAdjacentes(Grafo grafo)
    {
        Console.WriteLine("Informe o primeiro vértice:");
        int v1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe o segundo vértice:");
        int v2 = int.Parse(Console.ReadLine());

        if (v1 < 0 || v1 >= grafo.quantVertices || v2 < 0 || v2 >= grafo.quantVertices)
        {
            Console.WriteLine("Vértices inválidos! Informe vértices dentro do intervalo válido.");
            return false;
        }

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            if (aresta.Inicio == v1 && aresta.Fim == v2)
            {
                Console.WriteLine($"Os vértices {v1} e {v2} são adjacentes.");
                return true;
            }
        }

        Console.WriteLine($"Os vértices {v1} e {v2} não são adjacentes.");
        return false;
    }
    public void SubstituirPesoAresta(Grafo grafo)
    {
        menu.Resultado();
        bool encontrou = false;
        Console.Write("Informe o vértice de início da aresta: ");
        int inicio = int.Parse(Console.ReadLine());
        Console.Write("Informe o vértice de fim da aresta: ");
        int fim = int.Parse(Console.ReadLine());
        Console.Write("Informe o novo peso da aresta: ");
        double novoPeso = double.Parse(Console.ReadLine());

        foreach (Aresta aresta in grafo.ListaArestas)
        {
            if (aresta.Inicio == inicio && aresta.Fim == fim)
            {
                aresta.Peso = novoPeso;
                encontrou = true;
                Console.WriteLine($"\nPeso da aresta ({inicio} → {fim}) atualizado para {novoPeso}.");
                break;
            }
        }

        if (encontrou == false)
        {
            Console.WriteLine("\nAresta não encontrada no grafo.");
        }
    }

    public void BuscaEmProfundidade()
    {
        menu.Resultado();

    }

    internal class Program
    {

        private static void Main(string[] args)
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
                        break;
                    case 9:
                        metodos.VerticesSaoAdjacentes(grafo);
                        break;
                    case 10:
                        metodos.SubstituirPesoAresta(grafo);
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                }

            }
        }
    }
}