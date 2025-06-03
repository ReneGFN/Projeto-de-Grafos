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
    int quantVertices;
    int quantArestas;

    public double[,] matrizAdjacencia;
    public List<Aresta>;
}
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}