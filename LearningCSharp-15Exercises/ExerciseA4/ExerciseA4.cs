using System;

class MainClass
{
    static double ReadDouble ( string inputPrompt = "Enter a number")
    {
        double inputValue;

        do
        {
            Console.Write(inputPrompt + ":  ");
        }
        while (!double.TryParse(Console.ReadLine(), out inputValue));
        return inputValue;
    }

    static void Main()
    {
        Reynolds reynolds = new Reynolds();
        Console.WriteLine("Calculate Reynold's Number and flow characteristic");
        reynolds.density = ReadDouble("Enter the Density (\x03c1)");
        reynolds.diameter = ReadDouble("Enter the Diameter (D)");
        reynolds.velocity = ReadDouble("Enter the Velocity (v)");
        reynolds.viscosity = ReadDouble("Enter the Viscosity (\x03bc)");
        Console.WriteLine("Reynold's Number = " + reynolds.Number.ToString() + " (" + reynolds.FlowType + " flow)");
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
            if (Number < 2100)
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
