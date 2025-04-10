using System;
using System.Collections.Generic;

namespace KidTask
{
    public class TaskTree
    {
        public static List<Operation> Operations = new List<Operation>
        {
            new Operation(Composition, "", 3) { NeverOverride = true },
            new Operation(Power, "^", 2),
            new Operation(Multiplication, "*", 1),
            new Operation(Division, "/", 1),
            new Operation(Addition, "+", 0),
            new Operation(Subtraction, "-", 0)
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

        public static double Division(double x, double y)
        {
            return x / y;
        }

        public static double Addition(double x, double y)
        {
            return x + y;
        }

        public static double Subtraction(double x, double y)
        {
            return x - y;
        }

        public static double Power(double x, double y)
        {
            return Math.Pow(x, y);
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
                double divNumber = number / Math.Pow(10, i + 1);
                uint currentDigit = (uint)((divNumber - (uint)divNumber) * 10.0);

                if (i == 0)
                {
                    currentNode = new OperationNode(null, new SimpleNode(currentDigit), Operations[0]);
                    RootNode = currentNode;

                    continue;
                }

                if (currentNode != null)
                {
                    if (i == digitCount - 1)
                    {
                        currentNode.Left = new SimpleNode(currentDigit);
                    }
                    else
                    {
                        OperationNode newNode = new OperationNode(null, new SimpleNode(currentDigit), Operations[0]);
                        currentNode.Left = newNode;

                        currentNode = newNode;
                    }
                }
            }
        }

        public IEnumerable<Node> GetGraphOperationCombinations()
        {
            foreach (Node node in SetNodeOperation(RootNode))
            {
                yield return node;
            }
        }

        public const int MaxParenthesisDepth = 1;

        private IEnumerable<Node> SetNodeOperation(Node node)
        {
            if (node is OperationNode operationNode)
            {
                if (operationNode.Left is OperationNode leftOperationNode)
                {
                    for (int i = 0; i <= MaxParenthesisDepth; i++)
                    {
                        operationNode.OverridePriority = i;

                        foreach (Operation operation in Operations)
                        {
                            operationNode.Operation = operation;

                            foreach (Node childNode in SetNodeOperation(leftOperationNode))
                            {
                                yield return childNode;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= MaxParenthesisDepth; i++)
                    {
                        operationNode.OverridePriority = i;

                        foreach (Operation operation in Operations)
                        {
                            operationNode.Operation = operation;

                            Node copiedRootNode = CopyGraph(RootNode);

                            while (!BalanceGraphByOperationsOrder(copiedRootNode))
                            {
                            }

                            yield return copiedRootNode;
                        }
                    }
                }
            }
        }

        private bool BalanceGraphByOperationsOrder(Node node)
        {
            bool isBalanced = true;

            if (node is OperationNode operationNode)
            {
                if (operationNode.Left is OperationNode leftOperationNode)
                {
                    if (!leftOperationNode.Operation.NeverOverride)
                    {
                        if (operationNode.Operation.NeverOverride)
                        {
                            SwapNodeAndLeftSubNode(operationNode, leftOperationNode);
                            isBalanced = false;
                        }
                        else if (leftOperationNode.OverridePriority != operationNode.OverridePriority)
                        {
                            if (leftOperationNode.OverridePriority < operationNode.OverridePriority)
                            {
                                SwapNodeAndLeftSubNode(operationNode, leftOperationNode);
                                isBalanced = false;
                            }
                        }
                        else if (leftOperationNode.Operation.Priority < operationNode.Operation.Priority)
                        {
                            SwapNodeAndLeftSubNode(operationNode, leftOperationNode);
                            isBalanced = false;
                        }
                    }

                    if (!BalanceGraphByOperationsOrder(leftOperationNode))
                        isBalanced = false;
                }

                if (operationNode.Right is OperationNode rightOperationNode)
                {
                    if (!rightOperationNode.Operation.NeverOverride)
                    {
                        if (operationNode.Operation.NeverOverride)
                        {
                            SwapNodeAndRightSubNode(operationNode, rightOperationNode);
                            isBalanced = false;
                        }
                        else if (rightOperationNode.OverridePriority != operationNode.OverridePriority)
                        {
                            if (rightOperationNode.OverridePriority < operationNode.OverridePriority)
                            {
                                SwapNodeAndRightSubNode(operationNode, rightOperationNode);
                                isBalanced = false;
                            }
                        }
                        else if (rightOperationNode.Operation.Priority < operationNode.Operation.Priority)
                        {
                            SwapNodeAndRightSubNode(operationNode, rightOperationNode);
                            isBalanced = false;
                        }
                    }

                    if (!BalanceGraphByOperationsOrder(rightOperationNode))
                        isBalanced = false;
                }
            }

            return isBalanced;
        }

        private static void SwapNodeAndLeftSubNode(OperationNode operationNode, OperationNode leftOperationNode)
        {
            OperationNode operationNodeCopy = new OperationNode(operationNode.Left, operationNode.Right, operationNode.Operation);
            operationNodeCopy.OverridePriority = operationNode.OverridePriority;

            operationNode.Operation = leftOperationNode.Operation;
            operationNode.OverridePriority = leftOperationNode.OverridePriority;
            leftOperationNode.Operation = operationNodeCopy.Operation;
            leftOperationNode.OverridePriority = operationNodeCopy.OverridePriority;

            operationNode.Left = leftOperationNode.Left;
            operationNode.Right = leftOperationNode;

            leftOperationNode.Left = leftOperationNode.Right;
            leftOperationNode.Right = operationNodeCopy.Right;
        }

        private static void SwapNodeAndRightSubNode(OperationNode operationNode, OperationNode rightOperationNode)
        {
            OperationNode operationNodeCopy = new OperationNode(operationNode.Left, operationNode.Right, operationNode.Operation);
            operationNodeCopy.OverridePriority = operationNode.OverridePriority;

            operationNode.Operation = rightOperationNode.Operation;
            operationNode.OverridePriority = rightOperationNode.OverridePriority;
            rightOperationNode.Operation = operationNodeCopy.Operation;
            rightOperationNode.OverridePriority = operationNodeCopy.OverridePriority;

            operationNode.Left = rightOperationNode;
            operationNode.Right = rightOperationNode.Right;

            rightOperationNode.Right = rightOperationNode.Left;
            rightOperationNode.Left = operationNodeCopy.Left;
        }

        private Node CopyGraph(Node node)
        {
            if (node is OperationNode operationNode)
            {
                Node copiedLeftNode = CopyGraph(operationNode.Left);
                Node copiedRightNode = CopyGraph(operationNode.Right);

                OperationNode newNode = new OperationNode(copiedLeftNode, copiedRightNode, operationNode.Operation);
                newNode.OverridePriority = operationNode.OverridePriority;
                return newNode;
            }

            if (node is SimpleNode simpleNode)
            {
                return new SimpleNode(simpleNode.Value);
            }

            throw new NotImplementedException("Wrong graph type");
        }
    }
}