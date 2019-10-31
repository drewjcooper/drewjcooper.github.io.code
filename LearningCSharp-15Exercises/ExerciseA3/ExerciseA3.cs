using System;
using System.Collections.Generic;
using System.Linq;

class MainClass
{
    static void Main()
    {
        string inputLine;

        List<string> strings = new List<string>();
        Console.WriteLine("Enter a series of strings, one per line.  Finish with a blank line"); 
        while ((inputLine = Console.ReadLine()) != "") {
            strings.Add(inputLine);
        }
        strings.Sort();
        Console.WriteLine("Ascending list of strings:");
        foreach (string item in strings)
        {
            Console.WriteLine("   " + item);
        }
        strings.Reverse();
        Console.WriteLine("Descending list of strings:");
        foreach (string item in strings)
        {
            Console.WriteLine("   " + item);
        }

        Console.WriteLine();

        List<double> numbers = new List<double>();
        double inputNumber;
        Console.WriteLine("Enter a series of numbers, one per line.  Finish with a blank line");
        while ((inputLine = Console.ReadLine()) != "")
        {
            if (double.TryParse(inputLine, out inputNumber))
            {
                numbers.Add(inputNumber);
            }
        }
        numbers.Sort();
        Console.WriteLine("Ascending list of numbers:");
        foreach (double item in numbers)
        {
            Console.WriteLine("   " + item.ToString());
        }
        numbers.Reverse();
        Console.WriteLine("Descending list of numbers:");
        foreach (double item in numbers)
        {
            Console.WriteLine("   " + item.ToString());
        }
    }
}
