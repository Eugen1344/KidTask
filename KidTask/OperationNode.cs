namespace KidTask
{
    public class OperationNode : Node
    {
        public Node Left;
        public Node Right;
        public Operation Operation;

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
            return $"{Left.ChildExpressionString()}{Operation.OperationString}{Right.ChildExpressionString()}";
        }
    }
}