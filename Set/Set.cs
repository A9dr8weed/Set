using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{
    /// <summary>
    /// Set.
    /// </summary>
    /// <typeparam name="T"> The type of data stored in the set. </typeparam>
    public class Set<T> : IEnumerable<T>
    {
        /// <summary>
        /// The collection of stored data.
        /// </summary>
        private List<T> items = new List<T>();

        /// <summary>
        /// Amount of elements.
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// Add data to the set.
        /// </summary>
        /// <param name="item"> Added data. </param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <c>null</c>.</exception>
        public void Add(T item)
        {
            // Check input data for emptiness.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // A set can only contain unique elements, so if the set already contains such a data element, then we do not add it.
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Remove an item from the set.
        /// </summary>
        /// <param name="item"> The item to remove. </param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <c>null</c>.</exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Remove(T item)
        {
            // Check input data for emptiness.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // If the collection does not contain the given element, then we cannot delete it.
            if (!items.Contains(item))
            {
                throw new KeyNotFoundException($"Элемент {item} не найден в множестве.");
            }

            // Remove an item from the collection.
            items.Remove(item);
        }

        /// <summary>
        /// Union of sets.
        /// </summary>
        /// <param name="set1"> First set. </param>
        /// <param name="set2"> Second set. </param>
        /// <returns> A new set containing all the unique elements of the resulting sets. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            // Check input data for emptiness.
            if (set1 == null || set2 == null)
            {
                throw new ArgumentNullException(nameof(set1), nameof(set2));
            }

            // Result set.
            Set<T> resultSet = new Set<T>();

            // The data items of the result set.
            List<T> newItems = new List<T>();

            // If the first input set contains data items, then add them to the result set.
            if (set1.items?.Count > 0)
            {
                // since the list is a reference type, it is necessary not only to transfer data, but to create their duplicates.
                newItems.AddRange(new List<T>(set1.items));
            }

            // If the second input set contains data items, then add from to the resulting set.
            if (set2.items?.Count > 0)
            {
                // since the list is a reference type, it is necessary not only to transfer data, but to create their duplicates.
                newItems.AddRange(new List<T>(set2.items));
            }

            // Remove all duplicates from the resulting set of data items.
            resultSet.items = newItems.Distinct().ToList();

            // Return the resulting set.
            return resultSet;
        }

        /// <summary>
        /// Intersection of many.
        /// </summary>
        /// <param name="set1"> First set. </param>
        /// <param name="set2"> Second set. </param>
        /// <returns> A new set containing matching data items from the resulting sets. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Set<T> Intersection(Set<T> set1, Set<T> set2)
        {
            // Check input data for emptiness.
            if (set1 == null || set2 == null)
            {
                throw new ArgumentNullException(nameof(set1), nameof(set2));
            }

            // Result set.
            Set<T> resultSet = new Set<T>();

            // Select the set containing the least number of elements.
            if (set1.Count < set2.Count)
            {
                // The first set is smaller. We check all the elements of the selected set.
                foreach (T item in set1.items.Where(item => set2.items.Contains(item)))
                {
                    // If an element from the first set is contained in the second set, then add it to the resulting set.
                    resultSet.Add(item);
                }
            }
            else
            {
                // The second set is less than or the sets are equal. We check all the elements of the selected set.
                foreach (T item in set2.items.Where(item => set1.items.Contains(item)))
                {
                    // If an element from the second set is contained in the first set, then add it to the resulting set.
                    resultSet.Add(item);
                }
            }

            // Return the resulting set.
            return resultSet;
        }

        /// <summary>
        /// Difference of sets.
        /// </summary>
        /// <param name="set1"> First set. </param>
        /// <param name="set2"> Second set. </param>
        /// <returns> A new set containing non-matching data items between the first and second sets. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Set<T> Difference(Set<T> set1, Set<T> set2)
        {
            // Check input data for emptiness.
            if (set1 == null || set2 == null)
            {
                throw new ArgumentNullException(nameof(set1), nameof(set2));
            }

            // Result set.
            Set<T> resultSet = new Set<T>();

            // We go through all the elements of the first set.
            foreach (T item in set1.items.Where(item => !set2.items.Contains(item)))
            {
                // If an element from the first set is not contained in the second set, then add it to the resulting set.
                resultSet.Add(item);
            }

            // Remove all duplicates from the resulting set of data items.
            resultSet.items = resultSet.items.Distinct().ToList();

            // Return the resulting set.
            return resultSet;
        }

        /// <summary>
        /// Subset.
        /// </summary>
        /// <param name="set1"> The set to be checked for entering another set. </param>
        /// <param name="set2"> The set in which the entry of another set is checked. </param>
        /// <returns> Whether the first set is a subset of the second. true is, false is not. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Subset(Set<T> set1, Set<T> set2)
        {
            // Check input data for emptiness.
            if (set1 == null || set2 == null)
            {
                throw new ArgumentNullException(nameof(set1), nameof(set2));
            }

            // Go through the elements of the first set.
            // If all the elements of the first set are contained in the second, then this is a subset. Return true, otherwise false.
            return set1.items.All(s => set2.items.Contains(s));
        }

        /// <summary>
        /// Symmetrical difference.
        /// </summary>
        /// <param name="set1"> First set. </param>
        /// <param name="set2"> Second set. </param>
        /// <returns> A new set containing non-matching data items between the resulting sets. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Set<T> SymmetricDifference(Set<T> set1, Set<T> set2)
        {
            // Check input data for emptiness.
            if (set1 == null || set2 == null)
            {
                throw new ArgumentNullException(nameof(set1), nameof(set2));
            }

            Set<T> resultSet = new Set<T>();

            // Unites two difference sets.
            foreach (T item in set1.items.Except(set2.items).Union(set2.items.Except(set1.items)))
            {
                resultSet.Add(item);
            }

            // Remove all duplicates from the resulting set of data items.
            resultSet.items = resultSet.items.Distinct().ToList();

            // Return the resulting set.
            return resultSet;
        }

        /// <summary>
        /// Return an enumerator that iterates over all elements of the set.
        /// </summary>
        /// <returns> An enumerator that can be used to iterate over the collection. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            // We use the enumerator of the list of data items of the set.
            return items.GetEnumerator();
        }

        /// <summary>
        /// Return an enumerator that iterates over the set.
        /// </summary>
        /// <returns> The IEnumerator used to traverse the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            // We use the enumerator of the list of data items of the set.
            return items.GetEnumerator();
        }
    }
}