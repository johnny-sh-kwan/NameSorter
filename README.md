## Name sorting console app in .net 9

dotnet build
dotnet run

By default, will read from "unsorted-names-list.txt"  
Can optionally pass in file name of unsorted names.  

Will ignore any invalid names, eg all lines must contain both given and surname.  

There is a NameSorterTest xUnit test project containing unit tests.

Github actions CI pipeline in .github\workflows