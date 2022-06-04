using System;

namespace KnapsackProblem
{
    class Program
    {
        //objetosc 2,5tys jednostek
            //100 przedmiotow o objetosci 1-10 naturalne
            //objetosc - ma zostac jak najmniej
            //ewolucyjnie - rodzic i dziecko, 1,0, albo bierzemy albo nie
            //operacja mutacji
            //lepsze lub przepełnione idealne
            //oba przepełnione - mniejsze przepełnienie
            //wydrukować przedmioty wzięte do plecaka i wolne miejsce
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

            int emptySpaceLeftParent=0;
            bool perfect = false;
            int failureCounter = 0;

            while (!perfect || failureCounter>10)
            {
                emptySpaceLeftParent = CalculateEmptySpace(items, parent, knapsackVolume);

                //change one number in child
                int toChange = rnd.Next(100);
                if (child[toChange] == 1) child[toChange] = 0; else child[toChange] = 1;
                int emptySpaceLeftChild = CalculateEmptySpace(items, child, knapsackVolume);

                //if the child is better it becomes a parent
                switch (Math.Sign(emptySpaceLeftChild), Math.Sign(emptySpaceLeftParent))
                {
                    case (1, 1):
                        if (emptySpaceLeftParent > emptySpaceLeftChild)
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
                        perfect = true;
                        failureCounter++;
                        break;
                    case (0, 1):
                    case (0, -1):
                        parent = child;
                        emptySpaceLeftParent = emptySpaceLeftChild;
                        failureCounter = 0;
                        perfect = true;
                        break;
                    case (0, 0):
                        failureCounter++;
                        perfect = true;
                        break;
                    default:
                        break;
                }
            }
            //print the answer
            Console.WriteLine("Do plecaka zabrano przedmioty nr: ");
            int sum = 0;
            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i] == 1)
                {
                    Console.Write(i + ", ");
                }
                if (i == parent.Length - 1) Console.Write('\n');
            }
            Console.Write("pozostała wolna przestrzeń: " + emptySpaceLeftParent);
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
