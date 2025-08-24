using System.Collections.Generic;
using System.Linq;
using NameSorterApp.Models;
using NameSorterApp.Services;
using Xunit;

namespace NameSorterTest;

public class NameSortServiceTests
{
    [Theory]
    [InlineData("Janet Parsons", "Parsons", new[] { "Janet" })]
    [InlineData("Hunter Uriah Mathew Clarke", "Clarke", new[] { "Hunter", "Uriah", "Mathew" })]
    [InlineData("  Shelby  Nathaniel  Yoder  ", "Yoder", new[] { "Shelby", "Nathaniel" })]
    public void AddLine_WithValidName_CorrectlyParsesAndAddsName(string line, string expectedSurname, string[] expectedGivenNames)
    {
        // Arrange
        var service = new NameSortService();

        // Act
        service.AddLine(line);

        // Assert
        Assert.Single(service.UnsortedNames);
        var name = service.UnsortedNames.First();
        Assert.Equal(expectedSurname, name.Surname);
        Assert.Equal(expectedGivenNames.ToList(), name.GivenNames);
    }

    [Theory]
    [InlineData("InvalidName")]
    [InlineData("Too Many Name Parts In This Line")]
    [InlineData("")]
    [InlineData("  ")]
    public void AddLine_WithInvalidName_DoesNotAddName(string line)
    {
        // Arrange
        var service = new NameSortService();

        // Act
        service.AddLine(line);

        // Assert
        Assert.Empty(service.UnsortedNames);
    }

    [Fact]
    public void SortNames_WithUnsortedNames_ReturnsNamesSortedBySurnameThenGivenNames()
    {
        // Arrange
        var service = new NameSortService();
        
        service.AddLine("London Lindsey");        
        service.AddLine("Mikayla Lopez Lopez");
        service.AddLine("Aikayla Lopez");
        service.AddLine("Marin Alvarez");

        // Act
        var sortedNames = service.SortNames();

        // Assert
        var sortedNamesAsStrings = sortedNames.Select(n => n.ToString()).ToList();
        var expectedOrder = new List<string>
        {
            "Marin Alvarez",
            "London Lindsey",
            "Aikayla Lopez",
            "Mikayla Lopez Lopez"
        };

        Assert.True(service.IsSorted);
        Assert.Equal(expectedOrder, sortedNamesAsStrings);
        Assert.Same(service.SortedNames, sortedNames);
    }

    [Fact]
    public void SortNames_IsCaseInsensitive()
    {
        // Arrange
        var service = new NameSortService();
        service.AddLine("ab AAA");
        service.AddLine("aa aaa");
        service.AddLine("aa BBB");

        // Act
        var sortedNames = service.SortNames();

        // Assert
        var sortedNamesAsStrings = sortedNames.Select(n => n.ToString()).ToList();
        var expectedOrder = new List<string>
        {
            "aa aaa",
            "ab AAA",  // if case-sensitive, you would expect "ab AAA" to come before "aa aaa"
            "aa BBB"
        };
        Assert.Equal(expectedOrder, sortedNamesAsStrings);
    }

    [Fact]
    public void SortNames_WithEmptyList_ReturnsEmptyListAndSetsIsSorted()
    {
        // Arrange
        var service = new NameSortService();

        // Act
        service.SortNames();

        // Assert
        Assert.True(service.IsSorted);
        Assert.Empty(service.SortedNames);
    }
}

