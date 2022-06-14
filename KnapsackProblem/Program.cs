using System;
using System.Collections.Generic;

namespace KnapsackProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int knapsackVolume = 2500;
            int[] items = new int[100];
            Random rnd = new Random();
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = rnd.Next(100);
            }

            int[] parent = new int[100];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = rnd.Next(2);
            }

            //create child, populate with parent
            int[] child = new int[100];
            for (int i = 0; i < parent.Length; i++)
            {
                child[i] = parent[i];
            }

            int emptySpaceLeftParent=CalculateEmptySpace(items, parent, knapsackVolume);
            int emptySpaceLeftChild = emptySpaceLeftParent;
            
            bool perfect = false;
            int failureCounter = 0;
            List<int> emptySpace = new List<int>();

            while (!perfect && failureCounter<1000)
            {
                //change one number in child
                int toChange = rnd.Next(100);
                if (child[toChange] == 0) child[toChange] = 1; else child[toChange] = 0;
                emptySpaceLeftChild = CalculateEmptySpace(items, child, knapsackVolume);
                
                //if the child is better it becomes a parent
                switch (Math.Sign(emptySpaceLeftChild), Math.Sign(emptySpaceLeftParent))
                {
                    case (1, 1):
                        if (emptySpaceLeftParent >= emptySpaceLeftChild)
                        {
                            parent = child;
                            emptySpaceLeftParent = emptySpaceLeftChild;
                            failureCounter = 0;
                        }

                        break;
                    case (-1, -1):
                        if (emptySpaceLeftParent < emptySpaceLeftChild)
                        {
                            parent = child;
                            emptySpaceLeftParent = emptySpaceLeftChild;
                            failureCounter = 0;
                        }
                        break;
                    case (-1, 1):
                        failureCounter++;
                        break;
                    case (1, -1):
                        parent = child;
                        emptySpaceLeftParent = emptySpaceLeftChild;
                        failureCounter = 0;
                        break;
                    case (-1, 0):
                    case (1, 0):
                        failureCounter++;
                        perfect = true;
                        break;
                    case (0, 1):
                    case (0, -1):
                        parent = child;
                        emptySpaceLeftParent = emptySpaceLeftChild;
                        perfect = true;
                        break;
                    case (0, 0):
                        failureCounter++;
                        perfect = true;
                        break;
                    default:
                        break;
                }
                
                emptySpace.Add(emptySpaceLeftParent);
            }

            Console.WriteLine();
            Console.WriteLine("Do plecaka zabrano przedmioty nr: ");
            int sum = 0;
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i] == 1)
                {
                    Console.Write(i + ", ");
                }
            }
            Console.WriteLine("pozostała wolna przestrzeń: " + emptySpaceLeftParent);

            Console.WriteLine();
            Console.WriteLine("Zmiany pozostałej wolnej przestrzeni z interacji na iterację: ");
            for (int i = 0; i < emptySpace.Count; i++)
            {
                if (i == emptySpace.Count-1) Console.Write(emptySpace[i]);
                else Console.Write(emptySpace[i] + " >> ");
            }
        }

        private static int CalculateEmptySpace(int[] items, int[] parent, int knapsackVolume )
        {
            int volumeTaken = 0;
            for (int i = 0; i < items.Length; i++)
            {
                if (parent[i] == 1) volumeTaken += items[i];
            }
            return knapsackVolume - volumeTaken;
        }
    }
}

