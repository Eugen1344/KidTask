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

        public override string ChildExpressionString()
        {
            string operationName = Operation.OperationString;
            string operationString = new StringBuilder(operationName.Length * OverridePriority).Insert(0, operationName, OverridePriority + 1).ToString();

            return $"{Left.ChildExpressionString()}{operationString}{Right.ChildExpressionString()}";
        }
    }
}