namespace NameSorterApp.Models;

public class Name
{
    public required List<string> GivenNames { get; init; }
    public required string Surname { get; init; }

    public override string ToString()
    {
        return $"{string.Join(" ", GivenNames)} {Surname}";
    }    
}
