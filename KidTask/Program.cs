using System;
using System.Collections.Generic;

namespace KidTask
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            TaskTree tree = new TaskTree(123456789);

            int i = 0;
            foreach (Node combination in tree.SetGraphOperationCombinations())
            {
                //if (i < 10)
                {
                    Console.WriteLine($"x = {combination.ChildExpressionString()}");
                    Console.WriteLine($"x = {combination.Evaluate()}");
                }
                //else
                {
                   // break;
                }

                i++;
            }

            Console.ReadKey(true);
        }
    }
}