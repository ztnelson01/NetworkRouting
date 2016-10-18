using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkRouting
{
    class ArrayQueue : PriorityQueue
    {
        public ArrayQueue(int size) : base(size) { }

        List<bool> deleted = new List<bool>();

        public override void changeDistance(int index, double value)
        {
            distances[index] = value;
        }

        //returns removed index
        public override int deleteMinimum()
        {
            int index = -1;
            double minimum = double.MaxValue;
            for (int i = 0; i < distances.Count; i++)
            {
                if (!deleted[i])
                {
                    if (index == -1)
                    {
                        index = i;
                    }
                    if (distances[i] < minimum)
                    {
                        index = i;
                        minimum = distances[i];
                    }
                }
               
            }
            
            deleted[index] = true;
            return index;
        }

        public override void initializeArray(int size)
        {
            for (int i = 0; i < size; i++)
            {
                distances.Add(Double.MaxValue);
                formerIndexes.Add(-1);
                deleted.Add(false);
            }
        }

    }
}
