using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperHeuristicAnyTimeAlgorithm
{
    class Solution : ICloneable
    {

        public int[] AddresIndexOrder;
        public double distance;
        public Solution(int size)
        {
            this.AddresIndexOrder = new int[size];
            this.distance = -1;

        }
        public void SetSolution(int[] solution)
        {
            solution.CopyTo(this.AddresIndexOrder, 0);
      
        }
        public void SetRandomSolution()
        {
            List<int> aList = new List<int>();
            
            for (int i = 0; i < this.AddresIndexOrder.Length; i++)
            {
                aList.Add(i);
            }
           
            for (int i = 0; i < this.AddresIndexOrder.Length; i++)
            {
                int index = PWP_HyperHeuristic.rnd.Next(aList.Count);
                this.AddresIndexOrder[i] = aList[index];
                aList.RemoveAt(index);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); 
        }
    }
}
