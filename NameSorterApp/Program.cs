using NameSorterApp.Models;
using NameSorterApp.Services;

string unsortedNamesFilePath = "unsorted-names-list.txt";
string sortedNamesFilePath = "sorted-names-list.txt";

try
{
    // optionally pass in unsorted name list filePath, otherwise, use default
    if (args.Length > 0)
    {
        unsortedNamesFilePath = args[0];
    }
    Console.WriteLine($"Reading names from {unsortedNamesFilePath}...\n");
    using StreamReader sr = new(unsortedNamesFilePath);

    NameSortService nameSortService = new();

    string? line;
    while ((line = sr.ReadLine()) != null)
    {
        nameSortService.AddLine(line);
    }

    nameSortService.SortNames();
    nameSortService.SortedResultsToConsole();
    nameSortService.SortedResultsToFile(sortedNamesFilePath);
}
catch (FileNotFoundException)
{
    Console.WriteLine($"Error: {unsortedNamesFilePath} not found.");
    return;
}
catch (DirectoryNotFoundException)
{
    Console.WriteLine($"Error: {Path.GetDirectoryName(unsortedNamesFilePath)} not found.");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    return;
}
