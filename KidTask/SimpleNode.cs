namespace KidTask
{
    public class SimpleNode : Node
    {
        public double Value;

        public SimpleNode(double value)
        {
            Value = value;
        }

        public override double Evaluate()
        {
            return Value;
        }

        public override string ChildExpressionString()
        {
            return Value.ToString("G29");
        }
    }
}