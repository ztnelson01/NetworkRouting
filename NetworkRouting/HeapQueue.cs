using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    class HeapQueue : PriorityQueue
    {
        List<int> tree = new List<int>();
        List<int> pointToTree = new List<int>();

        public HeapQueue(int size) : base(size)
        {
        }

        public int getParent(int index)
        {
            return (index + 1) / 2 - 1;
        }

        public void swap(int index1, int index2)
        {
            pointToTree[tree[index1]] = index2;
            pointToTree[tree[index2]] = index1;

            int temp = tree[index1];
            tree[index1] = tree[index2];
            tree[index2] = temp;
        }
        public void bubbleUp(int index)
        {
            while (true)
            {
                int parentIndex = getParent(index);
                double distance = distances[tree[index]];
                if (parentIndex < 0) return;
                double parentDistance = distances[tree[parentIndex]];
                if (distance < parentDistance)
                {
                    swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public override void changeDistance(int index, double value)
        {
            distances[index] = value;
            index = pointToTree[index];
            bubbleUp(index);
        }

        private int getLeftChild(int index)
        {
            int childIndex = 2 * (index + 1) - 1;
            if (childIndex > (tree.Count - 1)) return -1;
            return childIndex;
        }

        private int getRightChild(int index)
        {
            int childIndex = 2 * (index + 1);
            if (childIndex > (tree.Count - 1)) return -1;
            return childIndex;
        }

        private void   trickleDown(int currentNode)
        {
            while (true)
            {
                int leftChild = getLeftChild(currentNode);
                int rightChild = getRightChild(currentNode);
                double leftChildDistance;
                if (leftChild == -1) leftChildDistance = Double.MaxValue;
                else leftChildDistance = distances[tree[leftChild]];
                double rightChildDistance;
                if (rightChild == -1) rightChildDistance = Double.MaxValue;
                else rightChildDistance = distances[tree[rightChild]];
                double currentNodeDistance = distances[tree[currentNode]];
                //break if the child elements are greater than parent
                if (currentNodeDistance <= leftChildDistance && currentNodeDistance <= rightChildDistance) return;

                if (leftChildDistance < rightChildDistance)
                {
                    swap(currentNode, leftChild);
                    currentNode = leftChild;
                }
                else if (rightChildDistance < leftChildDistance)
                {
                    swap(currentNode, rightChild);
                    currentNode = rightChild;
                }
            }
        }

        public override int deleteMinimum()
        {
            int indexToRemove = tree[0];
            //remove top of tree
            tree.RemoveAt(0);
            if (tree.Count == 0) return indexToRemove;
            //move last element to first
            tree.Insert(0, tree[tree.Count - 1]);
            pointToTree[tree[0]] = 0;
            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            tree.RemoveAt(tree.Count - 1);
          
            trickleDown(0);
            
            //printTree();
            return indexToRemove;
        }

        public override void initializeArray(int size)
        {
            for (int i = 0; i < size; i++)
            {
                formerIndexes.Add(-1);
                tree.Add(i);
                pointToTree.Add(i);
                distances.Add(double.MaxValue);
            }
        }

        private void printTree()
        {
            Console.WriteLine();
            for (int i = 0; i < tree.Count; i++)
            {
                Console.Write(tree[i] + " " + distances[tree[i]] + ",");
            }
            Console.WriteLine();
            
        }
    }
}
