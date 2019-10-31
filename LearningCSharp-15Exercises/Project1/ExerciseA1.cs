using System;

class MainClass
{
    static void Main()
    {
        byte lCount = 0;
        while (true)
        {
            checked { Console.WriteLine((++lCount).ToString()); }
            if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == '\x1B') break;
        }
    }
}