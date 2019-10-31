using System;
using System.Collections.Generic;

using LearningCSharp;

class ConsoleRPNCalculator
{
    static RPNCalculatorEngine rpnCalcEngine;
    static Dictionary<char, int> buttons;

    static private void DrawCalculator()
    {
        for (int stackIndex = 3; stackIndex >= 0; stackIndex--)
        {
            Console.SetCursorPosition(0, 5 - stackIndex);
            Console.Write("                         ");
            Console.CursorLeft = 0;
            Console.WriteLine((stackIndex + 1) + ": " + rpnCalcEngine[stackIndex]);
        }
        Console.SetCursorPosition(0, 7);
        Console.Write("                         ");
        Console.SetCursorPosition(0, 7);
        Console.Write(rpnCalcEngine.InputString);
    }

    static void DisplayButtons()
    {
        Console.Clear();
        for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
        {
            RPNCalculatorEngine.Operation operation = rpnCalcEngine.GetOperation(buttonIndex);
            Console.SetCursorPosition(40,buttonIndex);
            Console.CursorTop = buttonIndex;
            string keyName = operation.ShortCut == '\b' ? "BkSpc" : operation.ShortCut == '\r' ? "Enter" : operation.ShortCut.ToString();
            Console.Write("<" + keyName + ">\t" + operation.KeyText + "\t(" + operation.Name + ")");
        }
    }

    static void WriteErrorLine(string errorString)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(errorString);
    }

    public static void Main()
    {
        rpnCalcEngine = new RPNCalculatorEngine();
        buttons = new Dictionary<char, int>();
        RPNCalculatorEngine.Operation operation;

        int i = 0;
        bool errorDisplayed = false;
        while ((operation = rpnCalcEngine.GetOperation(i)) != null)
        {
            buttons.Add(operation.ShortCut, i++);
        }
        DisplayButtons();
        do
        {
            DrawCalculator();
            int button;
            char keyChar = Console.ReadKey(true).KeyChar;
            if (keyChar == '\x1b') break;    // exit the loop if Escape key pressed
            if (buttons.TryGetValue(keyChar, out button))
            {
                try
                {
                    if (errorDisplayed)
                    {
                        Console.SetCursorPosition(0, 0);
                        WriteErrorLine("                                   ");
                        WriteErrorLine("                                   ");
                        DisplayButtons();
                        errorDisplayed = false;
                    }
                    rpnCalcEngine.PressKey(button);
                }
                catch (FormatException)
                {
                    WriteErrorLine("Invalid number format");
                    errorDisplayed = true;
                }
                catch (Exception e)
                {
                    WriteErrorLine(e.Message);
                    errorDisplayed = true;
                }
            }
        } while (true);
    }
}