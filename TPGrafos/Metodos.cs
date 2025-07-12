using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Grafos
{
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

            if (vertice >= 0 && vertice < grafo.quantVertices)
            {
                List<int> adjacentes = new List<int>();

                foreach (Aresta aresta in grafo.ListaArestas)
                {
                    int adj = -1;
                    if (aresta.Inicio == vertice)
                    {
                        adj = aresta.Fim;
                    }
                    else if (aresta.Fim == vertice)
                    {
                        adj = aresta.Inicio;
                    }

                    if (adj != -1 && !adjacentes.Contains(adj))
                    {
                        adjacentes.Add(adj);
                    }
                }

                if (adjacentes.Count > 0)
                {
                    adjacentes.Sort();
                    Console.WriteLine($"\nVértices adjacentes ao vértice {vertice}:");
                    foreach (int v in adjacentes)
                    {
                        Console.WriteLine(v);
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum vértice adjacente encontrado.");
                }
            }
            else
            {
                Console.WriteLine($"Vértice inválido! Informe um valor entre 0 e {grafo.quantVertices - 1}.");
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
            foreach (Aresta aresta in grafo.ListaArestas)
            {
                if (aresta.Inicio == vertice || aresta.Fim == vertice)
                {
                    Console.WriteLine($"{aresta.Inicio} -- {aresta.Peso} -> {aresta.Fim}");
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
                Console.WriteLine($"\nVértices incidentes a aresta ({inicio} → {fim}):");
                Console.WriteLine($"- Vértice de inicio: {inicio}");
                Console.WriteLine($"- Vértice final: {fim}");
            }
            else
            {
                Console.WriteLine("\nAresta não encontrada no grafo.");
            }
        }

        public void ImprimirGrauDoVertice(Grafo grafo)
        {
            menu.Resultado();
            int grauEntrada = 0;
            int grauSaida = 0;
            Console.Write("Informe o vértice: ");
            int v = int.Parse(Console.ReadLine());

            if (v < 0 || v >= grafo.quantVertices)
            {
                Console.WriteLine($"Vértice inválido! Informe um valor entre 0 e {grafo.quantVertices - 1}.");
                return;
            }

            foreach (Aresta aresta in grafo.ListaArestas)
            {
                if (aresta.Inicio == v)
                {
                    grauSaida++;
                }
                if (aresta.Fim == v)
                {
                    grauEntrada++;
                }
            }
            Console.WriteLine($"\nGrau do vértice {v}:");
            Console.WriteLine($"- Grau de saída: {grauSaida}");
            Console.WriteLine($"- Grau de entrada: {grauEntrada}");
            Console.WriteLine($"- Grau total: {grauEntrada + grauSaida}");
        }

        public bool VerticesSaoAdjacentes(Grafo grafo)
        {
            menu.Resultado();
            Console.WriteLine("Digite o primeiro vértice:");
            int v1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o segundo vértice:");
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

        public void TrocarVertices(Grafo grafo)
        {
            menu.Resultado();

            Console.Write("Digite o primeiro vértice: ");
            int v1 = int.Parse(Console.ReadLine());
            Console.Write("Digite o segundo vértice: ");
            int v2 = int.Parse(Console.ReadLine());

            if (v1 < 0 || v1 >= grafo.quantVertices || v2 < 0 || v2 >= grafo.quantVertices)
            {
                Console.WriteLine("Vértices inválidos!");
                return;
            }

            foreach (Aresta aresta in grafo.ListaArestas)
            {
                if (aresta.Inicio == v1)
                {
                    aresta.Inicio = v2;
                }
                else if (aresta.Inicio == v2)
                {
                    aresta.Inicio = v1;
                }

                if (aresta.Fim == v1)
                {
                    aresta.Fim = v2;
                }
                else if (aresta.Fim == v2)
                {
                    aresta.Fim = v1;
                }
            }
            Console.WriteLine($"\nOs vértices {v1} e {v2} foram trocados com sucesso.");
        }

        public void BuscaEmLargura(Grafo grafo)
        {
            menu.Resultado();
            Console.WriteLine("Digite o vértice inicial:");
            int inicio = int.Parse(Console.ReadLine());

            int[] nivel = new int[grafo.quantVertices];
            int[] pai = new int[grafo.quantVertices];
            int[] L = new int[grafo.quantVertices];
            int tempo = 1;

            Queue<int> fila = new Queue<int>();

            for (int i = 0; i < grafo.quantVertices; i++)
            {
                nivel[i] = -1;
                pai[i] = -1;
                L[i] = 0;
            }

            nivel[inicio] = 0;
            L[inicio] = tempo;
            fila.Enqueue(inicio);

            while (fila.Count > 0)
            {
                int v = fila.Dequeue();

                List<int> vizinhos = new List<int>();
                foreach (Aresta aresta in grafo.ListaArestas)
                {
                    if (aresta.Inicio == v)
                        vizinhos.Add(aresta.Fim);
                }
                vizinhos.Sort();

                foreach (int w in vizinhos)
                {
                    if (nivel[w] == -1)
                    {
                        Console.WriteLine($"Aresta de Árvore: {{{v}, {w}}}");
                        pai[w] = v;
                        nivel[w] = nivel[v] + 1;
                        tempo++;
                        L[w] = tempo;
                        fila.Enqueue(w);
                    }
                    else if (nivel[w] == nivel[v] + 1)
                    {
                        Console.WriteLine($"Aresta Tio: {{{v}, {w}}}");
                    }
                    else if (nivel[w] == nivel[v] && pai[v] == pai[w] && L[w] > L[v])
                    {
                        Console.WriteLine($"Aresta Irmão: {{{v}, {w}}}");
                    }
                    else if (nivel[w] == nivel[v] && pai[v] != pai[w] && L[w] > L[v])
                    {
                        Console.WriteLine($"Aresta Primo: {{{v}, {w}}}");
                    }
                }
                Console.WriteLine("\nResultado Final:");
                Console.WriteLine("Vértice\tNível\tPai");
                for (int i = 0; i < grafo.quantVertices; i++)
                {
                    Console.WriteLine($"{i}\t{nivel[i]}\t{(pai[i] == -1 ? "-" : pai[i].ToString())}");
                }
            }
        }

        public void DFS(int u, int nivelAtual,
                List<List<int>> listaAdj,
                bool[] visitado,
                int[] descoberta,
                int[] finalizacao,
                int[] nivel,
                ref int tempo,
                List<(int, int)> arvoreBusca)
        {
            tempo++;
            descoberta[u] = tempo;
            visitado[u] = true;
            nivel[u] = nivelAtual;

            foreach (int v in listaAdj[u])
            {
                if (!visitado[v])
                {
                    arvoreBusca.Add((u, v));
                    DFS(v, nivelAtual + 1, listaAdj, visitado, descoberta, finalizacao, nivel, ref tempo, arvoreBusca);
                }
            }

            tempo++;
            finalizacao[u] = tempo;
        }

        public void BuscaEmProfundidade(Grafo grafo)
        {
            menu.Resultado();

            Console.Write("Digite o vértice inicial: ");
            int inicio = int.Parse(Console.ReadLine());

            if (inicio >= 0 && inicio < grafo.quantVertices)
            {
                int tempo = 0;
                int[] descoberta = new int[grafo.quantVertices];
                int[] finalizacao = new int[grafo.quantVertices];
                int[] nivel = new int[grafo.quantVertices];
                bool[] visitado = new bool[grafo.quantVertices];
                List<(int, int)> arvoreBusca = new List<(int, int)>();
                List<List<int>> listaAdj = new List<List<int>>();
                for (int i = 0; i < grafo.quantVertices; i++)
                    listaAdj.Add(new List<int>());

                foreach (Aresta aresta in grafo.ListaArestas)
                {
                    listaAdj[aresta.Inicio].Add(aresta.Fim);
                }

                for (int i = 0; i < grafo.quantVertices; i++)
                {
                    listaAdj[i].Sort();
                }

                DFS(inicio, 0, listaAdj, visitado, descoberta, finalizacao, nivel, ref tempo, arvoreBusca);

                Console.WriteLine("\nÁrvore de busca (arestas utilizadas):");
                foreach (var aresta in arvoreBusca)
                {
                    Console.WriteLine($"{aresta.Item1} → {aresta.Item2}");
                }
                Console.WriteLine("\nDescoberta, Finalização e Nível dos vértices:");
                for (int i = 0; i < grafo.quantVertices; i++)
                {
                    Console.WriteLine($"Vértice {i}: Tempo de Descoberta = {descoberta[i]}, Tempo de Finalização = {finalizacao[i]}, Nível = {nivel[i]}");
                }
            }
            else
            {
                Console.WriteLine("Vértice inválido.");
            }
        }

        public void Dijkstra(Grafo grafo)
        {
            menu.Resultado();
            Console.Write("Digite o vértice de origem: ");
            int origem = int.Parse(Console.ReadLine());
            Console.Write("Digite o vértice de destino: ");
            int destino = int.Parse(Console.ReadLine());

            if (origem >= 0 && origem < grafo.quantVertices && destino >= 0 && destino < grafo.quantVertices)
            {
                double[] dist = new double[grafo.quantVertices];
                int[] anterior = new int[grafo.quantVertices];
                bool[] visitado = new bool[grafo.quantVertices];

                for (int i = 0; i < grafo.quantVertices; i++)
                {
                    dist[i] = double.MaxValue;
                    anterior[i] = -1;
                }

                dist[origem] = 0;

                for (int i = 0; i < grafo.quantVertices; i++)
                {
                    int u = -1;
                    double menorDist = double.MaxValue;

                    for (int j = 0; j < grafo.quantVertices; j++)
                    {
                        if (!visitado[j] && dist[j] < menorDist)
                        {
                            menorDist = dist[j];
                            u = j;
                        }
                    }

                    if (u == -1)
                    {
                        i = grafo.quantVertices;
                    }
                    else
                    {
                        visitado[u] = true;

                        foreach (Aresta aresta in grafo.ListaArestas)
                        {
                            if (aresta.Inicio == u)
                            {
                                int v = aresta.Fim;
                                double peso = aresta.Peso;

                                if (dist[u] + peso < dist[v])
                                {
                                    dist[v] = dist[u] + peso;
                                    anterior[v] = u;
                                }
                            }
                        }
                    }
                }

                if (dist[destino] != double.MaxValue)
                {
                    List<int> caminho = new List<int>();
                    int atual = destino;
                    while (atual != -1)
                    {
                        caminho.Insert(0, atual);
                        atual = anterior[atual];
                    }

                    Console.WriteLine($"\nCaminho mínimo de {origem} até {destino}. Distância total: {dist[destino]}");
                    for (int i = 0; i < caminho.Count - 1; i++)
                    {
                        int de = caminho[i];
                        int para = caminho[i + 1];
                        double peso = grafo.ListaArestas.First(a => a.Inicio == de && a.Fim == para).Peso;
                        Console.WriteLine($"{de} → {para} (peso: {peso})");
                    }
                }
                else
                {
                    Console.WriteLine($"\nNão existe caminho de {origem} até {destino}.");
                }
            }
            else
            {
                Console.WriteLine("Vértices inválidos.");
            }
        }

        public void FloydWarshall(Grafo grafo)
        {
            menu.Resultado();
            Console.Write("Digite o vértice de origem: ");
            int origem = int.Parse(Console.ReadLine());

            if (origem >= 0 && origem < grafo.quantVertices)
            {
                int n = grafo.quantVertices;
                double[,] dist = new double[n, n];
                int[,] anterior = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                            dist[i, j] = 0;
                        else
                            dist[i, j] = double.MaxValue;

                        anterior[i, j] = -1;
                    }
                }

                foreach (Aresta aresta in grafo.ListaArestas)
                {
                    dist[aresta.Inicio, aresta.Fim] = aresta.Peso;
                    anterior[aresta.Inicio, aresta.Fim] = aresta.Inicio;
                }

                for (int k = 0; k < n; k++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (dist[i, k] != double.MaxValue && dist[k, j] != double.MaxValue)
                            {
                                double novaDist = dist[i, k] + dist[k, j];
                                if (novaDist < dist[i, j])
                                {
                                    dist[i, j] = novaDist;
                                    anterior[i, j] = anterior[k, j];
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"\nCaminhos mínimos a partir do vértice {origem}:\n");

                for (int destino = 0; destino < n; destino++)
                {
                    if (destino != origem)
                    {
                        Console.Write($"Caminho até {destino}: ");

                        if (dist[origem, destino] == double.MaxValue)
                        {
                            Console.WriteLine("não existe caminho.");
                        }
                        else
                        {
                            List<int> caminho = new List<int>();
                            int atual = destino;

                            while (atual != origem && atual != -1)
                            {
                                caminho.Insert(0, atual);
                                atual = anterior[origem, atual];
                            }

                            if (atual == -1)
                            {
                                Console.WriteLine("erro na reconstrução do caminho.");
                            }
                            else
                            {
                                caminho.Insert(0, origem);
                                for (int i = 0; i < caminho.Count - 1; i++)
                                {
                                    Console.Write($"{caminho[i]} → ");
                                }
                                Console.WriteLine($"{caminho.Last()} (custo: {dist[origem, destino]})");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Vértice inválido.");
            }
        }
    }
}
