using System;
using System.Linq;

class MainClass
{
    static ulong Fibonacci1(uint n)
    {
        switch (n)
        {
            case 0:
                return 0;
            case 1:
                return 1;
            default:
                ulong fN = 1, fNMinusTwo, fNMinusOne = 0;
                while (n > 1)
                {
                    fNMinusTwo = fNMinusOne;
                    fNMinusOne = fN;
                    fN = fNMinusTwo + fNMinusOne;
                    n--;
                }
                return fN;
        }
    }
    
    static ulong Fibonacci2(uint n, ulong nMinusOne = 1, ulong nMinusTwo = 0)
    {
        switch (n)
        {
            case 0:
                return nMinusTwo;
            case 1:
                return nMinusOne;
            default:
                return Fibonacci2(n - 1, nMinusOne + nMinusTwo, nMinusOne);
        }
    }

    static void Swap<T>(ref T var1, ref T var2) {
        T tempVar = var1;
        var1 = var2;
        var2 = tempVar;
    }
    
    static void Main()
    {
        Console.WriteLine("Fibonacci series");
        for (uint index = 0; index <= 10; index++)
        {
            Console.WriteLine("F(" + index.ToString() + ")\t" + Fibonacci1(index).ToString() + "\t" + Fibonacci2(index).ToString());
        }

        Console.WriteLine ("\nSwapping int Variables");
        int a = 5;
        int b = 10;
        Console.WriteLine("Before:\tA=" + a.ToString() + "\tB=" + b.ToString());
        Swap(ref a, ref b);
        Console.WriteLine("Before:\tA=" + a.ToString() + "\tB=" + b.ToString());
        
        Console.WriteLine("\nSwapping char Variables");
        char c = 'C';
        char d = 'D';
        Console.WriteLine("Before:\tC=" + c.ToString() + "\tD=" + d.ToString());
        Swap(ref c, ref d);
        Console.WriteLine("Before:\tC=" + c.ToString() + "\tD=" + d.ToString());

        int[] integerArray =  { 15, 4, 76, 53, 25, 63 };
        Console.Write("\nArray = {");
        for (uint index = 0; index < integerArray.Count(); Console.Write(" " + integerArray[index++].ToString() + ","));
        Console.WriteLine("\b }");
        Console.WriteLine("Maximum = " + integerArray.Max().ToString());
        Console.WriteLine("Minimum = " + integerArray.Min().ToString());
    }
}