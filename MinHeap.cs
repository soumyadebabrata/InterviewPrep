using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class MinHeap
    {
        private int[] set;
        public int count;
        private int size;

        private int LeftChildIndex(int i) { return i * 2 + 1; }
        private int RightChildIndex(int i) { return i * 2 + 2; }
        private int ParentIndex(int i) { return (i -1) / 2; }

        private int GetLeftChild(int i) { return set[LeftChildIndex(i)]; }
        private int GetRightChild(int i) { return set[RightChildIndex(i)]; }

        private int GetParent(int i)
        {
            int pi = ParentIndex(i);
            return set[pi];
        }

        private bool HasLeftChild(int i) { return LeftChildIndex(i) >= 0 && LeftChildIndex(i) < count; }
        private bool HasRightChild(int i) { return RightChildIndex(i) >= 0 && RightChildIndex(i) < count; }

        private bool HasParent(int i)
        {
            if (i == 0)
            {
                return false;
            }
            return ParentIndex(i) >= 0;

        }

        private void swap(int i, int j)
        {
            int temp = set[i];
            set[i] = set[j];
            set[j] = temp;
        }

        public MinHeap(List<int> data)
        {
            count = 0;
            size = data.Count;
            set = new int[size];
            foreach (int i in data)
            {
                Insert(i);
            }
        }

        public void Insert(int v)
        {
            if (count == size)
            {
                throw new InvalidOperationException();
            }

            set[count] = v;
            count += 1;
            HeapifyUp();
        }

        public int Peek()
        {
            if (count > 0)
            {
                return set[0];
            }

            throw new InvalidOperationException();
        }

        public int Pop()
        {
            if (count > 0)
            {
                int result = set[0];
                set[0] = set[count - 1];
                count = count - 1;
                HeapifyDown();
                return result;
            }

            throw new InvalidOperationException();
        }

        public void HeapifyUp()
        {
            int i = count - 1;

            while (HasParent(i))
            {
                if (GetParent(i) < set[i])
                {
                    break;
                }

                swap(i, ParentIndex(i));
                i = ParentIndex(i);
            }
        }

        public void HeapifyDown()
        {
            int i = 0;
            while (HasLeftChild(i))
            {
                int minChildIndex = LeftChildIndex(i);
                if (HasRightChild(i) && GetRightChild(i) < GetLeftChild(i))
                {
                    minChildIndex = RightChildIndex(i);
                }

                if (set[minChildIndex] > set[i])
                {
                    break;
                }

                swap(i, minChildIndex);
                i = minChildIndex;
            }
        }
    }
}
