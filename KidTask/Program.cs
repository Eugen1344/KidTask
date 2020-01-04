using System;

namespace KidTask
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            OperationNode x = new OperationNode(new SimpleNode(2.0), new OperationNode(new SimpleNode(2.0), new SimpleNode(2.0), Addition), Multiplication);

            Console.WriteLine(x.Evaluate());

            Console.ReadKey(true);
        }

        public static double Multiplication(double x, double y)
        {
            return x * y;
        }

        public static double Addition(double x, double y)
        {
            return x + y;
        }
    }
}