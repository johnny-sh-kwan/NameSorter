using NameSorterApp.Models;

namespace NameSorterApp.Services;

public interface INameSortService
{
    List<Name> Names { get; init; }

    void AddLine(string line);
    List<Name> SortNames();
}
