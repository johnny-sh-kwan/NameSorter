using System;
using NameSorterApp.Models;

namespace NameSorterApp.Services;

public class NameSortService : INameSortService
{
    public List<Name> Names { get; init; } = [];

    public void AddLine(string line)
    {
        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2 && parts.Length <= 4) // 1-3 given names + 1 surname
        {
            string surname = parts.Last();
            List<string> givenNames = parts.Take(parts.Length - 1).ToList();
            Names.Add(new Name { GivenNames = givenNames, Surname = surname });
        }
        else
        {
            Console.WriteLine($"Parse error: line must have 2-4 name parts, skipping invalid line: {line}");
        }
    }

    public List<Name> SortNames()
    {
        return Names.OrderBy(n => n.Surname, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(n => string.Join(" ", n.GivenNames), StringComparer.OrdinalIgnoreCase)
                    .ToList();
    }
}
