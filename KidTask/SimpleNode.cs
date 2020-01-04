namespace KidTask
{
    public class SimpleNode : Node
    {
        private readonly double _value;

        public SimpleNode(double value)
        {
            _value = value;
        }

        public override double Evaluate()
        {
            return _value;
        }
    }
}