using System;

namespace Set
{
    public static class Program
    {
        private static void Main()
        {
            // We create sets.
            Set<int> set1 = new Set<int>()
            {
                1, 2, 3, 4, 5
            };

            Set<int> set2 = new Set<int>()
            {
                4, 5, 6, 7, 8
            };

            Set<int> set3 = new Set<int>()
            {
                3, 4, 5
            };

            // Perform operations on sets.
            Set<int> union = Set<int>.Union(set1, set2);
            Set<int> difference = Set<int>.Difference(set1, set2);
            Set<int> intersection = Set<int>.Intersection(set1, set2);
            Set<int> symetricDifference = Set<int>.SymmetricDifference(set1, set2);
            bool subset1 = Set<int>.Subset(set3, set1);
            bool subset2 = Set<int>.Subset(set3, set2);

            // We output the initial sets to the console.
            PrintSet(set1, "First set: ");
            PrintSet(set2, "Second set: ");
            PrintSet(set3, "Third set: ");

            // We print the result sets to the console.
            PrintSet(union, "Combining the first and second set: ");
            PrintSet(difference, "The difference between the first and second sets: ");
            PrintSet(intersection, "Intersection of the first and second sets: ");
            PrintSet(symetricDifference, "Symetric difference beetween two sets: ");

            // Print the results of the check for subsets.
            if (subset1)
            {
                Console.WriteLine("The third set is a subset of the first.");
            }
            else
            {
                Console.WriteLine("The third set is not a subset of the first.");
            }

            if (subset2)
            {
                Console.WriteLine("The third set is a subset of the second.");
            }
            else
            {
                Console.WriteLine("The third set is not a subset of the second.");
            }
        }

        /// <summary>
        /// Outputting the set to the console.
        /// </summary>
        /// <param name="set"> Set. </param>
        /// <param name="title"> The header before the output of the set. </param>
        private static void PrintSet(Set<int> set, string title)
        {
            Console.Write(title);

            foreach (int item in set)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }
    }
}