namespace KidTask
{
    public class Operation
    {
        public delegate double Func(double left, double right);

        public readonly Func Function;
        public readonly string OperationString;
        public readonly int Priority;

        public Operation(Func function, string operationString, int priority)
        {
            Function = function;
            OperationString = operationString;
            Priority = priority;
        }
    }
}