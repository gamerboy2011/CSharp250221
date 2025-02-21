using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public char Gender { get; set; }
    public int Payment { get; set; }
    public List<int> Scores { get; set; }
}

class Program
{
    static void Main()
    {
        const string filePath = "C:\\Users\\ny20FaludiB\\Desktop\\Course\\course.txt";
        List<Student> students = new List<Student>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(';');
            students.Add(new Student
            {
                Name = parts[0],
                Gender = parts[1][0],
                Payment = int.Parse(parts[2]),
                Scores = parts.Skip(3).Select(int.Parse).ToList()
            });
        }

        Console.WriteLine($"1. Hallgatók száma: {students.Count}");
        Console.WriteLine($"2. Átlag a backend modulból: {students.Average(s => s.Scores[3]):F2}");
        Console.WriteLine($"3. Legjobb hallgató: {students.OrderByDescending(s => s.Scores.Sum()).First().Name}");
        Console.WriteLine($"4. Férfiak aránya: {(double)students.Count(s => s.Gender == 'm') / students.Count * 100:F2}%");
        Console.WriteLine($"5. Legjobb webfejlesztő: {students.OrderByDescending(s => s.Scores[2] + s.Scores[3]).First().Name}");
        Console.WriteLine($"6. Teljes összeget befizetők: {students.Count(s => s.Payment == 2600)}");
        Console.WriteLine($"7. Javítóvizsgázók: {students.Count(s => s.Scores.Average() < 51)}");
        Console.WriteLine($"8. Minden modulból 100%-ot teljesítők: {students.Count(s => s.Scores.All(score => score == 100))}");

        Console.WriteLine("9. Javítóvizsgázók listája:");
        foreach (var student in students.Where(s => s.Scores.Average() < 51))
        {
            Console.WriteLine(student.Name);
        }

        Console.WriteLine("10. ABC sorrendben, átlageredménnyel:");
        foreach (var student in students.OrderBy(s => s.Name.Split(' ').Last()))
        {
            Console.WriteLine($"{student.Name}: {student.Scores.Average():F2}");
        }
    }
}

