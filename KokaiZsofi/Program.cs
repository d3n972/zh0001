// See https://aka.ms/new-console-template for more information
using KokaiZsofi;

string[] lines = new string[] {
"s1 ABV-043 1220 2102 D",
"s2 GLS-031 2220 6705 V",
"s3 AHR-140 3230 3992 V",
"s4 MBS-204 4160 5806 V",
"s5 ABV-043 5220 6102 D",
"s6 MSV-146 5610 6723 M",
};
List<Jarmu> j = new List<Jarmu>();
foreach (var item in lines)
{
    j.Add(new Jarmu(item));
}

j.ForEach(x => Console.WriteLine(x));