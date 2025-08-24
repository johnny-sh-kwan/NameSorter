using System;
using System.Text;

namespace NameSorterApp.Models;

public class Name
{
    public required List<string> GivenNames { get; init; }
    public required string Surname { get; init; }

    public override string ToString()
    {
        return $"{string.Join(" ", GivenNames)} {Surname}";
    }

    public static void WriteToConsole(List<Name> names)
    {
        foreach (Name name in names)
        {
            Console.WriteLine($"{name}");
        }
    }

    public static void WriteToFile(List<Name> names, string filePath)
    {
        StringBuilder sb = new();
        foreach (Name name in names)
        {
            sb.AppendLine(name.ToString());
        }

        File.WriteAllText(filePath, sb.ToString());

        Console.WriteLine($"\nSorted names written to {filePath}");
    }
}
