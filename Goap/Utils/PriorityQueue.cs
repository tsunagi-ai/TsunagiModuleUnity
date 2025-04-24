using System;
using System.Collections.Generic;

namespace TsunagiModule.Goap.Utils
{
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        private List<T> elements = new List<T>();

        public int Count => elements.Count;

        public void Enqueue(T item)
        {
            elements.Add(item);
            int childIndex = elements.Count - 1;
            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (elements[childIndex].CompareTo(elements[parentIndex]) >= 0)
                    break;

                T temp = elements[childIndex];
                elements[childIndex] = elements[parentIndex];
                elements[parentIndex] = temp;

                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            if (elements.Count == 0)
                throw new InvalidOperationException("The priority queue is empty.");

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
                    smallestIndex = leftChildIndex;

                if (
                    rightChildIndex < elements.Count
                    && elements[rightChildIndex].CompareTo(elements[smallestIndex]) < 0
                )
                    smallestIndex = rightChildIndex;

                if (smallestIndex == parentIndex)
                    break;

                T temp = elements[parentIndex];
                elements[parentIndex] = elements[smallestIndex];
                elements[smallestIndex] = temp;

                parentIndex = smallestIndex;
            }

            return result;
        }
    }
}
