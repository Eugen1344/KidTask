namespace KidTask
{
    public class Operation
    {
        public delegate double Func(double left, double right);

        public readonly Func Function;
        public readonly string OperationString;

        public Operation(Func function, string operationString)
        {
            Function = function;
            OperationString = operationString;
        }
    }
}