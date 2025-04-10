using System;
using System.Text;

namespace KidTask
{
    public class OperationNode : Node
    {
        public Node Left;
        public Node Right;
        public Operation Operation;
        public int OverridePriority = 0;

        public OperationNode(Node left, Node right, Operation operationFunction)
        {
            Left = left;
            Right = right;
            Operation = operationFunction;
        }

        public override double Evaluate()
        {
            return Operation.Function(Left.Evaluate(), Right.Evaluate());
        }

        public override double EvaluateDebug()
        {
            double left = Left.EvaluateDebug();
            double right = Right.EvaluateDebug();
            double value = Operation.Function(left, right);

            Console.WriteLine($"Evaluated: {left}{Operation.OperationString}{right} = {value}. Priority = {Operation.Priority}, override = {OverridePriority}, always override = {Operation.NeverOverride}");

            return value;
        }

        public override string ChildExpressionString()
        {
            string operationName = Operation.OperationString;

            return $"{new string('(', OverridePriority)}{Left.ChildExpressionString()}{operationName}{Right.ChildExpressionString()}{new string(')', OverridePriority)}";
        }
    }
}