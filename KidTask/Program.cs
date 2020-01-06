using System;

namespace KidTask
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            TaskTree tree = new TaskTree(123456789);

            foreach (Node combination in tree.GetGraphOperationCombinations())
            {
                double value = combination.Evaluate();

                if (value == 7415.0)
                {
                    Console.WriteLine($"{combination.ChildExpressionString()} = {value}");
                }
            }

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }
    }
}