using System;

namespace KidTask
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            TaskTree tree = new TaskTree(123456);

            foreach (Node combination in tree.GetGraphOperationCombinations())
            {
                double value = combination.Evaluate();

                Console.WriteLine($"Attempted: {combination.ChildExpressionString()} = {value}");

                if (Math.Abs(value - 61.0) != 0)
                    continue;

                Console.WriteLine($"\n\n\nFinished: {combination.ChildExpressionString()} = {value}");

                Console.WriteLine("Done");
                Console.ReadKey(true);

                return;
            }

            Console.WriteLine("Nothing found");
            Console.ReadKey(true);
        }
    }
}