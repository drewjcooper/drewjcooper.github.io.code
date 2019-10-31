using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningCSharp
{
    public partial class RPNCalculatorEngine
    {
        private Stack<double> _calculationStack;
        private InputLine _input;
        private List<Operation> _operations;
        static private int _baseOperationCount = 30;

        public RPNCalculatorEngine()
        {
            _calculationStack = new Stack<double>();
            _input = new InputLine();
            _input.Clear();
            _operations = new List<Operation>(_baseOperationCount);
            _operations.Add(new Operation("Zero", "0", "0", '0'));
            _operations.Add(new Operation("One", "1", "1", '1'));
            _operations.Add(new Operation("Two", "2", "2", '2'));
            _operations.Add(new Operation("Three", "3", "3", '3'));
            _operations.Add(new Operation("Four", "4", "4", '4'));
            _operations.Add(new Operation("Five", "5", "5", '5'));
            _operations.Add(new Operation("Six", "6", "6", '6'));
            _operations.Add(new Operation("Seven", "7", "7", '7'));
            _operations.Add(new Operation("Eight", "8", "8", '8'));
            _operations.Add(new Operation("Nine", "9", "9", '9'));
            _operations.Add(new Operation("Decimal", ".", ".", '.'));
            _operations.Add(new Operation("Exponent", "Exp", "E", 'E'));
            _operations.Add(new Operation("Back", "Bck", "Back", '\b'));
            _operations.Add(new UnaryOperation("Enter", "Cpy", "Enter", '\r', Copy));
            _operations.Add(new UnaryOperation("Negate", "Neg", "+/-", '_', x => -x));
            _operations.Add(new UnaryOperation("Inverse", "Inv", "1/x", '\\', x => 1 / x));
            _operations.Add(new ConstantOperation("PI", "PI", "\x03c0", 'p', Math.PI));
            _operations.Add(new ConstantOperation("e", "e", "e", 'e', Math.E));
            _operations.Add(new BinaryOperation("Add", "Add", "+", '+', (y, x) => x + y));
            _operations.Add(new BinaryOperation("Subtract", "Sub", "-", '-', (y, x) => x - y));
            _operations.Add(new BinaryOperation("Multiply", "Mul", "x", '*', (y, x) => x * y));
            _operations.Add(new BinaryOperation("Divide", "Div", "\xf7", '/', (y, x) => x / y));
            _operations.Add(new BinaryOperation("Modulus", "Mod", "mod", '%', (y, x) => x % y));
            _operations.Add(new UnaryOperation("Factorial", "Fac", "!", '!', Factorial));
            _operations.Add(new UnaryOperation("Cosine", "Cos", "cos", 'c', Math.Cos));
            _operations.Add(new UnaryOperation("Sine", "Sin", "sin", 's', Math.Sin));
            _operations.Add(new UnaryOperation("Tangent", "Tan", "tan", 't', Math.Tan));
            _operations.Add(new UnaryOperation("Square", "x\xb2", "x\xb2", 'S', x => Math.Pow(x, 2)));
            _operations.Add(new UnaryOperation("Cube", "x\xb3", "x\xb3", 'C', x => Math.Pow(x, 3)));
            _operations.Add(new UnaryOperation("SquareRoot", "RtX", "\x221a", 'R', Math.Sqrt));
        }

        public string InputString
        {
            get
            {
                return this._input.ToString();
            }
        }

        public void PressKey(int keyIndex)
        {
            if (keyIndex < _operations.Count)
            {
                Operation operation = _operations[keyIndex];
                if (_input.AddChar(operation.ShortCut)) return;
                if (operation.ShortCut == '\b' && _calculationStack.Count > 0)
                {
                    _calculationStack.Pop();
                    return;
                }
                if (operation.HasFunction)
                {
                    if (_input.IsActive)
                    {
                        _calculationStack.Push(_input.ToDouble());
                        _input.Clear();
                        if (operation.Name == "Enter") return;
                    }
                    operation.Execute(_calculationStack);
                }
            }
        }
        
        public double? this[int index]
        {
            get
            {
                if (index < _calculationStack.Count)
                    return _calculationStack.ElementAt<double>(index);
                else
                    return null;
            }
        }

        public Operation GetOperation(int index)
        {
            return index < _operations.Count ? _operations[index] : null;
        }

        private double Copy(double operand)
        {
            _calculationStack.Push(operand);
            return operand;
        }

        private double Factorial(double operand)
        {
            if (operand >= 0d && Math.Floor(operand) == operand)    // Check that operand is a positive integer
            {
                double result = 1;
                while (operand > 1)
                {
                    result *= operand--;
                    if (double.IsInfinity(result))
                    {
                        throw new OverflowException("Fac: Result too large");
                    }
                }
                return result;
            }
            else
            {
                _calculationStack.Push(operand);    // Return the operand to the stack
                throw new ArgumentOutOfRangeException("Fac: Must be positive integer");
            }
        }
    }
}
