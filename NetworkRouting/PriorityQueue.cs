using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NetworkRouting
{
    abstract class PriorityQueue
    {
        public PriorityQueue(int size)
        {
            initializeArray(size);
        }
        public List<double> distances = new List<double>();
        public List<int> formerIndexes = new List<int>();
        public abstract void initializeArray(int size);
        public abstract void changeDistance(int index, double value);
        //returns the delted
        public abstract int deleteMinimum();
    }
}
