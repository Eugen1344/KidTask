using System;
using System.Collections.Generic;

namespace KidTask
{
    public class TaskTree
    {
        public static List<Operation> Operations = new List<Operation>
        {
            new Operation(Composition, ""),
            new Operation(Multiplication, "*"),
            new Operation(Addition, "+")
        };

        public static double Composition(double x, double y)
        {
            uint digitCount = DigitCount((uint)y);

            return x * Math.Pow(10, digitCount) + y;
        }

        public static double Multiplication(double x, double y)
        {
            return x * y;
        }

        public static double Addition(double x, double y)
        {
            return x + y;
        }

        public static uint DigitCount(uint number)
        {
            uint division = number;
            uint i = 0;

            while (division >= 1)
            {
                i++;
                division /= 10;
            }

            return i;
        }

        public Node RootNode;

        public TaskTree(uint number)
        {
            GenerateNodes(number);
        }

        public void GenerateNodes(uint number)
        {
            uint digitCount = DigitCount(number);

            if (digitCount <= 1)
            {
                RootNode = new SimpleNode(number);
                return;
            }

            OperationNode currentNode = null;
            for (int i = 0; i < digitCount; i++)
            {
                double divNumber = number / Math.Pow(10, digitCount - i);
                uint currentDigit = (uint)((divNumber - (uint)divNumber) * 10.0);

                if (i == 0)
                {
                    currentNode = new OperationNode(new SimpleNode(currentDigit), null, Operations[0]);
                    RootNode = currentNode;

                    continue;
                }

                if (currentNode != null)
                {
                    if (i == digitCount - 1)
                    {
                        currentNode.Right = new SimpleNode(currentDigit);
                    }
                    else
                    {
                        OperationNode newNode = new OperationNode(new SimpleNode(currentDigit), null, Operations[0]);
                        currentNode.Right = newNode;

                        currentNode = newNode;
                    }
                }
            }
        }

        public IEnumerable<Node> SetGraphOperationCombinations()
        {
            foreach (OperationNode node in SetNodeOperation((OperationNode)RootNode))
            {
                yield return RootNode;
            }
        }

        private IEnumerable<OperationNode> SetNodeOperation(OperationNode node)
        {
            if (node.Right is OperationNode operationNode)
            {
                foreach (OperationNode childNode in SetNodeOperation(operationNode))
                {
                    yield return childNode;
                }
            }

            foreach (Operation operation in Operations)
            {
                node.Operation = operation;

                yield return node;
            }
        }
    }
}