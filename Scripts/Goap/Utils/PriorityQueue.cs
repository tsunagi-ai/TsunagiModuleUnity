// This entire file is written by GitHub Copilot
using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap.Utils
{
    /// <summary>
    /// Represents a generic priority queue that organizes elements based on their priority.
    /// </summary>
    /// <typeparam name="T">The type of elements in the priority queue. Must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <seealso cref="https://www.geeksforgeeks.org/priority-queue-set-1-introduction/"/>
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The list of elements in the priority queue.
        /// </summary>
        private List<T> elements = new List<T>();

        /// <summary>
        /// Gets the number of elements in the priority queue.
        /// </summary>
        public int Count => elements.Count;

        /// <summary>
        /// Adds an element to the priority queue.
        /// </summary>
        /// <param name="item">The element to add.</param>
        public void Enqueue(T item)
        {
            elements.Add(item);
            int childIndex = elements.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (elements[childIndex].CompareTo(elements[parentIndex]) >= 0)
                {
                    break;
                }

                T temp = elements[childIndex];
                elements[childIndex] = elements[parentIndex];
                elements[parentIndex] = temp;

                childIndex = parentIndex;
            }
        }

        /// <summary>
        /// Removes and returns the element with the highest priority from the priority queue.
        /// </summary>
        /// <returns>The element with the highest priority.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the priority queue is empty.</exception>
        public T Dequeue()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            T result = elements[0];
            int lastIndex = elements.Count - 1;
            elements[0] = elements[lastIndex];
            elements.RemoveAt(lastIndex);

            int parentIndex = 0;
            while (true)
            {
                int leftChildIndex = 2 * parentIndex + 1;
                int rightChildIndex = 2 * parentIndex + 2;
                int smallestIndex = parentIndex;

                if (
                    leftChildIndex < elements.Count
                    && elements[leftChildIndex].CompareTo(elements[smallestIndex]) < 0
                )
                {
                    smallestIndex = leftChildIndex;
                }

                if (
                    rightChildIndex < elements.Count
                    && elements[rightChildIndex].CompareTo(elements[smallestIndex]) < 0
                )
                {
                    smallestIndex = rightChildIndex;
                }

                if (smallestIndex == parentIndex)
                {
                    break;
                }

                T temp = elements[parentIndex];
                elements[parentIndex] = elements[smallestIndex];
                elements[smallestIndex] = temp;

                parentIndex = smallestIndex;
            }

            return result;
        }
    }
}
