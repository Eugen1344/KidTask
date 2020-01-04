namespace KidTask
{
    public class OperationNode : Node
    {
        public delegate double Operation(double left, double right);

        private readonly Node _left;
        private readonly Node _right;
        private readonly Operation _operationFunction;

        public OperationNode(Node left, Node right, Operation operationFunction)
        {
            _left = left;
            _right = right;
            _operationFunction = operationFunction;
        }

        public override double Evaluate()
        {
            return _operationFunction(_left.Evaluate(), _right.Evaluate());
        }
    }
}