using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int caloriasMinimas = 50;
        int pesoMaximo = 10;

        List<Elemento> elementos = new List<Elemento>
        {
            new Elemento("E1", 5, 3),
            new Elemento("E2", 3, 5),
            new Elemento("E3", 5, 2),
            new Elemento("E4", 1, 8),
            new Elemento("E5", 2, 3)
        };

        List<Elemento> elementosViables = EncontrarElementosViables(elementos, caloriasMinimas, pesoMaximo);
        Console.WriteLine("Elementos viables:");
        foreach (Elemento elemento in elementosViables)
        {
            Console.WriteLine($"Elemento {elemento.Nombre} - Peso: {elemento.Peso} - Calorías: {elemento.Calorias}");
        }
    }
    static List<Elemento> EncontrarElementosViables(List<Elemento> elementos, int caloriasMinimas, int pesoMaximo)
    {
        List<Elemento> elementosViables = new List<Elemento>();

        for (int i = 1; i <= elementos.Count; i++)
        {
            foreach (List<Elemento> combinacion in Combinaciones(elementos, i))
            {
                int totalCalorias = combinacion.Sum(e => e.Calorias);
                int totalPeso = combinacion.Sum(e => e.Peso);

                if (totalCalorias >= caloriasMinimas && totalPeso <= pesoMaximo)
                {
                    elementosViables = combinacion;
                    break;
                }
            }
            if (elementosViables.Count > 0)
            {
                break;
            }
        }
        return elementosViables;
    }
    static IEnumerable<List<Elemento>> Combinaciones(List<Elemento> elementos, int r)
    {
        if (r == 0)
        {
            yield return new List<Elemento>();
        }
        else
        {
            for (int i = 0; i < elementos.Count; i++)
            {
                foreach (List<Elemento> combination in Combinaciones(elementos.Skip(i + 1).ToList(), r - 1))
                {
                    combination.Insert(0, elementos[i]);
                    yield return combination;
                }
            }
        }
    }
}
class Elemento
{
    public string Nombre { get; }
    public int Peso { get; }
    public int Calorias { get; }
    public Elemento(string nombre, int peso, int calorias)
    {
        Nombre = nombre;
        Peso = peso;
        Calorias = calorias;
    }
}

