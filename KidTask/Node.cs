namespace KidTask
{
    public abstract class Node
    {
        public abstract double Evaluate();
        public abstract double EvaluateDebug();
        public abstract string ChildExpressionString();
    }
}