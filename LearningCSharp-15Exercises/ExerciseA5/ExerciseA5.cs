using System;

class MainClass
{
    static double ReadNonNegativeDouble(string inputPrompt = "Enter a number", bool zeroAllowed = false)
    {
        double inputValue;

        do
        {
            Console.Write(inputPrompt + ":  ");
            if (double.TryParse(Console.ReadLine(), out inputValue))
            {
                if (inputValue > 0 || inputValue == 0 && zeroAllowed) break;
            }
        }
        while (true);
        return inputValue;
    }

    static void Main()
    {
        Reynolds reynolds = new Reynolds();
        Console.WriteLine("\nCalculate Reynold's Number and flow characteristic\n");
        do
        {
            reynolds.density = ReadNonNegativeDouble("Enter the Density (\x03c1)");
            reynolds.diameter = ReadNonNegativeDouble("Enter the Diameter (D)");
            reynolds.velocity = ReadNonNegativeDouble("Enter the Velocity (v)", true);
            reynolds.viscosity = ReadNonNegativeDouble("Enter the Viscosity (\x03bc)");
            if (double.IsInfinity(reynolds.Number)) 
            {
                Console.WriteLine("Calculation overflow!");
            }
            else if (double.IsNaN(reynolds.Number))
            {
                Console.WriteLine("Invalid result!");
            }
            else
            {
                Console.WriteLine("Reynold's Number = " + reynolds.Number.ToString() + " (" + reynolds.FlowType + " flow)");
            }
            Console.Write("\nDo you want to calculate again? (y/n) ");
            while (true) {
                char keyPress = Console.ReadKey(true).KeyChar;
                if (keyPress == 'y') break;
                if (keyPress == 'n') return;
            }
            Console.Write("\n\n");
        } while (true);
    }
}

class Reynolds
{
    public double density;
    public double diameter;
    public double velocity;
    public double viscosity;

    public double Number
    {
        get
        {
            return density * diameter * velocity / viscosity;
        }
    }

    public string FlowType
    {
        get
        {
            if (double.IsInfinity(Number) || double.IsNaN(Number)) 
            {
                return "Invalid result";
            }
            else if (Number < 2100)
            {
                return "Laminar";
            }
            else if (Number < 4000)
            {
                return "Transient";
            }
            else
            {
                return "Turbulent";
            }
        }
    }
}
