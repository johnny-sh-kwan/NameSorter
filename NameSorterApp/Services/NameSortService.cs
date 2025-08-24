using System;
using System.Text;
using NameSorterApp.Models;

namespace NameSorterApp.Services;

public class NameSortService : INameSortService
{
    public List<Name> UnsortedNames { get; init; } = [];
    public List<Name> SortedNames { get; private set; } = [];
    public bool IsSorted { get; private set; } = false;

    public void AddLine(string line)
    {
        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2 && parts.Length <= 4) // 1-3 given names + 1 surname
        {
            string surname = parts.Last();
            List<string> givenNames = parts.Take(parts.Length - 1).ToList();
            UnsortedNames.Add(new Name { GivenNames = givenNames, Surname = surname });
        }
        else
        {
            Console.WriteLine($"Parse error: line must have 2-4 name parts, skipping invalid line: {line}");
        }
    }

    public List<Name> SortNames()
    {
        SortedNames = UnsortedNames.OrderBy(n => n.Surname, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(n => string.Join(" ", n.GivenNames), StringComparer.OrdinalIgnoreCase)
                    .ToList();

        IsSorted = true;
        return SortedNames;
    }

    public void SortedResultsToConsole()
    {
        if (!IsSorted)
            throw new InvalidOperationException("Names must be sorted before displaying results.");

        foreach (Name name in SortedNames)
        {
            Console.WriteLine($"{name}");
        }
    }

    public void SortedResultsToFile(string filePath)
    {
        if (!IsSorted)
            throw new InvalidOperationException("Names must be sorted before writing results to file.");

        StringBuilder sb = new();
        foreach (Name name in SortedNames)
        {
            sb.AppendLine(name.ToString());
        }

        File.WriteAllText(filePath, sb.ToString());

        Console.WriteLine($"\nSorted names written to {filePath}");
    }
}
