using System;
using System.Collections.Generic;

namespace LearningCSharp
{
    public partial class RPNCalculatorEngine
    {
        public delegate double BinaryOp(double operand1, double operand2);
        public delegate double UnaryOp(double operand);

        public class Operation
        {
            protected readonly string _name;
            protected readonly string _abbreviation;
            protected readonly string _keyText;
            protected readonly char _keyboardShortCut;
            protected readonly int _operandsRequired;

            public  Operation(string name, string abbr, string keyText, char keyboardShortCut, int operandsRequired = 0)
            {
                _name = name;
                _abbreviation = abbr;
                _keyText = keyText;
                _keyboardShortCut = keyboardShortCut;
                _operandsRequired = operandsRequired;
            }

            public string Name { get { return _name; } }
            public string Abbreviation { get { return _abbreviation; } }
            public string KeyText { get { return _keyText; } }
            public char ShortCut { get { return _keyboardShortCut; } }
            public virtual bool HasFunction { get { return false; } }

            public virtual void Execute(Stack<double> operands) { }
        }

        public class BinaryOperation : Operation
        {

            private readonly BinaryOp _function;

            public BinaryOperation(string name, string abbr, string keyText, char keyBoardShortCut, BinaryOp func)
                : base(name, abbr, keyText, keyBoardShortCut, 2)
            {
                _function = func;
            }

            public override bool HasFunction { get { return true; } }
            
            public override void Execute(Stack<double> operands)
            {
                if (operands.Count < _operandsRequired)
                {
                    throw new ArgumentException(_abbreviation + ": Insufficient operands");
                }
                operands.Push(_function(operands.Pop(), operands.Pop()));
            }
        }

        public class UnaryOperation : Operation
        {

            private readonly UnaryOp _function;

            public UnaryOperation(string name, string abbr, string keyText, char keyBoardShortCut, UnaryOp func)
                : base(name, abbr, keyText, keyBoardShortCut, 1)
            {
                _function = func;
            }

            public override bool HasFunction { get { return true; } }

            public override void Execute(Stack<double> operands)
            {
                if (operands.Count < _operandsRequired)
                {
                    throw new ArgumentException(_abbreviation + ": Insufficient operands");
                }
                operands.Push(_function(operands.Pop()));
            }
        }

        public class ConstantOperation : Operation
        {

            private readonly double _value;

            public ConstantOperation(string name, string abbr, string keyText, char keyBoardShortCut, double value)
                : base(name, abbr, keyText, keyBoardShortCut, 0)
            {
                _value = value;
            }

            public override bool HasFunction { get { return true; } }

            public override void Execute(Stack<double> operands)
            {
                operands.Push(_value);
            }
        }
    }
}