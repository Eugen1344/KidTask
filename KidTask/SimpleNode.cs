using System;

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

        public override double EvaluateDebug()
        {
            Console.WriteLine("Returned: " + Value);

            return Value;
        }

        public override string ChildExpressionString()
        {
            return Value.ToString();
        }
    }
}