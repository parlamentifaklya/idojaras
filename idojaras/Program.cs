using System;
using System.Collections.Generic;
using System.Linq;

class Idojaras
{
    private int _maxHo;
    private int _minHo;

    public Idojaras(int maxHo, int minHo)
    {
        _maxHo = maxHo;
        _minHo = minHo;
    }

    public int MaxHo { get { return _maxHo; } }
    public int MinHo { get { return _minHo; } }
}

class Program
{
    static void Main(string[] args)
    {
        int napszam;
        int.TryParse(Console.ReadLine(), out napszam);
        var idojarasAdatok = Enumerable.Range(1, napszam)
            .Select(index =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    var parts = input.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int minHo) && int.TryParse(parts[1], out int maxHo))
                    {
                        return new { Index = index, Idojaras = new Idojaras(maxHo, minHo) };
                    }
                    else
                    {
                        throw new Exception("Hibás formátum! Adja meg újra (maxHo minHo):");
                    }
                }
            })
            .ToDictionary(x => x.Index, x => x.Idojaras);

        //1 feladat
        Console.WriteLine("#");
        Console.WriteLine($"{idojarasAdatok.Count(x => x.Value.MinHo <= 0)}");

        //2 feladat
        Console.WriteLine("#");
        Console.WriteLine($"{idojarasAdatok.OrderByDescending(x => x.Value.MaxHo - x.Value.MinHo).ThenBy(x => x.Key).First().Key}");

        //3 feladat
        Console.WriteLine("#");
        int MaximumKisebb(Dictionary<int, Idojaras> input) => idojarasAdatok.Where(x => x.Key > 1).FirstOrDefault(x => idojarasAdatok[x.Key - 1].MinHo > x.Value.MaxHo).Key;
        Console.WriteLine($"{(MaximumKisebb(idojarasAdatok) == 0 ? -1 : MaximumKisebb(idojarasAdatok))}");

        //4 feladat
        Console.WriteLine("#");
        List<int> FagyasOlvadas(Dictionary<int, Idojaras> input) => input.Where(x => x.Value.MinHo <= 0 && x.Value.MaxHo > 0).Select(x => x.Key).OrderBy(x => x).ToList();
        Console.WriteLine(string.Join(" ", FagyasOlvadas(idojarasAdatok).Count, string.Join(" ", FagyasOlvadas(idojarasAdatok))));
    }
}