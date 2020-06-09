using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperHeuristicAnyTimeAlgorithm
{
    partial class PWP_HyperHeuristic
    {
        public void MutationInversion(ref Solution solution, int level)
        {
            // for random pick number
            List<int> aList = new List<int>();

            int rnd1 = 0;
            int rnd2 = 0;
            for (int i = 0; i < solution.AddresIndexOrder.Length; i++)
            {
                aList.Add(i);
            }
            // **********************/
            int temp = 0;
            for (int i = 0; i < level; i++)
            {
                //pick random 2 number. numbers can not be same
                rnd1 = PWP_HyperHeuristic.rnd.Next(aList.Count);
                aList.RemoveAt(rnd1);
                rnd2 = PWP_HyperHeuristic.rnd.Next(aList.Count);
                aList.Add(rnd1);
                //**************
                //swap
                temp = solution.AddresIndexOrder[rnd1];
                solution.AddresIndexOrder[rnd1] = solution.AddresIndexOrder[rnd2];
                solution.AddresIndexOrder[rnd2] = temp;
                //**************
            }

            this.CalculateSolutionDistance(ref solution);

        }
        public void MutationAdjacentSwap(ref Solution solution, int level)
        {
            int temp = 0;
            int rnd = 0;
            int next = 0;
            for (int i = 0; i < level; i++)
            {
                rnd = PWP_HyperHeuristic.rnd.Next(solution.AddresIndexOrder.Length);
                // swap
                next = rnd + 1;
                next = next % solution.AddresIndexOrder.Length;

                //swap
                temp = solution.AddresIndexOrder[rnd];
                solution.AddresIndexOrder[rnd] = solution.AddresIndexOrder[next];
                solution.AddresIndexOrder[next] = temp;
                //**************
            }
            this.CalculateSolutionDistance(ref solution);
        }
        public void MutationReinsertion(ref Solution solution, int level)
        {
            List<int> mutatedList = new List<int>();
            for (int i = 0; i < solution.AddresIndexOrder.Length; i++)
            {
                mutatedList.Add(solution.AddresIndexOrder[i]);
            }
            int pick = 0;
            int rnd1 = 0;
            int rnd2 = 0;
            for (int i = 0; i < level; i++)
            {
                // pick random 2 number. numbers can not be same
                rnd1 = PWP_HyperHeuristic.rnd.Next(mutatedList.Count);// select an index
                pick = mutatedList[rnd1];
                mutatedList.RemoveAt(rnd1);//get and remove item from list

                rnd2 = PWP_HyperHeuristic.rnd.Next(mutatedList.Count); // select another index
                mutatedList.Insert(rnd2, pick);// add picked item to index 

            }
            solution.SetSolution(mutatedList.ToArray());

            this.CalculateSolutionDistance(ref solution);
        }
        public void LocalSearchNextDescent(ref Solution solution, int level)
        {


            int[] list = new int[solution.AddresIndexOrder.Length];
            solution.AddresIndexOrder.CopyTo(list, 0);

            int rnd = 0;
            int indexStart = rnd;
            int index = rnd;
            int next1 = index + 1;
            int next2 = index + 2;
            int temp = 0;
            for (int i = 0; i < level; i++)
            {
                rnd = PWP_HyperHeuristic.rnd.Next(solution.AddresIndexOrder.Length - 2);
                indexStart = rnd;
                index = rnd;
                next1 = index + 1;
                next2 = index + 2;
                double next1Distance = 0;
                double next2Distance = 0;
                while (next2 != indexStart)
                {
                    next1Distance = 0;
                    next2Distance = 0;
                    /*if (index == 0)
                    {
                        next1Distance = this.GetDistancePostOfficeAndLocation(list[index]);
                        next2Distance = this.GetDistancePostOfficeAndLocation(list[next1]);
                        if (next1Distance > next2Distance)
                        {
                            temp = solution.AddresIndexOrder[index];
                            list[index] = solution.AddresIndexOrder[next1];
                            list[next1] = temp;
                            break;
                        }
                    }
                    else if (index == solution.AddresIndexOrder.Length - 1)
                    {
                        next1Distance = this.GetDistanceHomeAndLocation(list[index]);
                        next2Distance = this.GetDistanceHomeAndLocation(list[index - 1]);

                        if (next1Distance > next2Distance)
                        {
                            temp = solution.AddresIndexOrder[index - 1];
                            list[index - 1] = list[index];
                            list[index] = temp;
                            break;
                        }
                    }
                    else
                    {*/

                    next1Distance = this.GetDistanceLocationAndLocation(list[index], list[next1]);
                    next2Distance = this.GetDistanceLocationAndLocation(list[index], list[next2]);
                    if (next1Distance > next2Distance)
                    {
                        temp = list[next1];
                        list[next1] = list[next2];
                        list[next2] = temp;
                        break;
                    }
                    //}
                    index = (index + 1) % list.Length;
                    next1 = (next1 + 1) % list.Length;
                    next2 = (next2 + 1) % list.Length;

                }

                /*if (this.CheckHasSameGenes(list))
                {
                    Console.WriteLine("has");
                }*/

            }
            double child1Distance = this.CalculateSolutionDistance(list);
            if ( !this.HasSameSolutionInSolutionList(list))
            {
                solution.SetSolution(list);
                this.CalculateSolutionDistance(ref solution);
            }

            /* solution.SetSolution(list);
             this.CalculateSolutionDistance(ref solution);*/


        }
        public void LocalSearchHillClimbing(ref Solution solution, int level)
        {

            int rnd = 0;
            Tuple<int, int> pair;
            int[] list = new int[solution.AddresIndexOrder.Length];
            solution.AddresIndexOrder.CopyTo(list, 0);
            double nextDistance = 0;
            int temp = 0;
            for (int i = 0; i < level; i++)
            {
                //for randomize
                List<Tuple<int, int>> aList = new List<Tuple<int, int>>();
                for (int k = 0; k < solution.AddresIndexOrder.Length; k++)
                {
                    for (int j = 0; j < solution.AddresIndexOrder.Length; j++)
                    {
                        if (k != j)
                            aList.Add(new Tuple<int, int>(k, j));
                    }

                }
                double distance = CalculateSolutionDistance(list);
                do
                {
                    rnd = PWP_HyperHeuristic.rnd.Next(aList.Count);
                    pair = aList[rnd];
                    aList.RemoveAt(rnd);

                    temp = list[pair.Item1];
                    list[pair.Item1] = list[pair.Item2];
                    list[pair.Item2] = temp;

                    nextDistance = this.CalculateSolutionDistance(list);
                    if (nextDistance > solution.distance)
                    {
                        temp = list[pair.Item2];
                        list[pair.Item2] = list[pair.Item1];
                        list[pair.Item1] = temp;
                    }

                } while (nextDistance > distance && aList.Count != 0);



            }
            nextDistance = CalculateSolutionDistance(list);
            if (!this.HasSameSolutionInSolutionList(list))
            {
                solution.SetSolution(list);
                this.CalculateSolutionDistance(ref solution);
            }
            /*solution.SetSolution(list);
            this.CalculateSolutionDistance(ref solution);*/

        }
        public void CrosoverOrdered(ref Solution solution1, ref Solution solution2, int level)
        {
           
            int cut1 = 0;
            int cut2 = 0;



            int[] child1 = null;
            int[] child2 = null;
            level = 1;
            /*for (int i = 0; i < level; i++)
            {*/
            //for limiting cut size
            cut1 = PWP_HyperHeuristic.rnd.Next(solution1.AddresIndexOrder.Length / 2) + 1;
            cut2 = PWP_HyperHeuristic.rnd.Next(cut1) + 1 + cut1;

            child1 = new int[solution1.AddresIndexOrder.Length];
            child2 = new int[solution1.AddresIndexOrder.Length];

            for (int j = cut1; j < cut2; j++)
            {
                child1[j] = solution2.AddresIndexOrder[j]; //[0 0 1 3 5 6 0]
                child2[j] = solution1.AddresIndexOrder[j];// [0 0 6 7 9 1 0]
            }
            int complater1 = cut2;
            int complater2 = cut2;

            while (complater2 != cut1)
            {
                complater1 = complater1 % solution1.AddresIndexOrder.Length;
                complater2 = complater2 % solution1.AddresIndexOrder.Length;

                //comparision 
                bool isFind = false;
                for (int j = cut1; j < cut2; j++)
                {
                    if (child1[j] == solution1.AddresIndexOrder[complater1])
                    {
                        isFind = true;
                        break;
                    }
                }
                // if child not has that gen you can put to index
                if (!isFind)
                {
                    child1[complater2] = solution1.AddresIndexOrder[complater1];
                    complater2++;
                }
                complater1++;

            }
            complater1 = cut2;
            complater2 = cut2;
            while (complater2 != cut1)
            {
                complater1 = complater1 % solution2.AddresIndexOrder.Length;
                complater2 = complater2 % solution2.AddresIndexOrder.Length;

                bool isFind = false;
                for (int j = cut1; j < cut2; j++)
                {
                    if (child2[j] == solution2.AddresIndexOrder[complater1])
                    {
                        isFind = true;
                        break;
                    }
                }
                if (!isFind)
                {
                    child2[complater2] = solution2.AddresIndexOrder[complater1];
                    complater2++;
                }
                complater1++;
            }


            this.SwapBetter(child2);
            this.SwapBetter(child1);
           


        }
        public void CrosoverCycle(ref Solution solution1, ref Solution solution2, int level)
        {
            List<int> cycle = new List<int>();
            List<int> cycleTotal = new List<int>();
            //int[] child1 = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            int[] parent1Genes = new int[solution1.AddresIndexOrder.Length];
            solution1.AddresIndexOrder.CopyTo(parent1Genes, 0);
            int[] child1 = new int[solution1.AddresIndexOrder.Length];


            int[] parent2Genes = new int[solution1.AddresIndexOrder.Length];
            solution2.AddresIndexOrder.CopyTo(parent2Genes, 0);
            int[] child2 = new int[solution1.AddresIndexOrder.Length];
            /*int[] child1 = new int[5] {1,5,3,4,2 };
            int[] child2 = new int[5] { 3, 4, 2, 5, 1 };*/

            List<int> aList = new List<int>();
            for (int m = 0; m < child1.Length; m++)
            {
                aList.Add(m);
            }
            int rnd = 0;
            int startIndex = 0;
            int nextIndex = 0;
            int i = 0;
            level = 1;
            /*while (i < level && aList.Count>0)
            {*/
            while (aList.Count > 0)
            {
                cycle = new List<int>();
                rnd = PWP_HyperHeuristic.rnd.Next(aList.Count);
                startIndex = aList[rnd];
                //Console.WriteLine(startIndex);
                aList.RemoveAt(rnd);
                //Console.WriteLine(startIndex);
                //startIndex = 0;
                nextIndex = startIndex;
                cycle.Add(startIndex);
                while (parent1Genes[startIndex] != parent2Genes[nextIndex] && cycle.Count < parent1Genes.Length)
                {
                    //nextIndex = child1[nextIndex];
                    for (int j = 0; j < parent1Genes.Length; j++)
                    {
                        if (parent1Genes[j] == parent2Genes[nextIndex])
                        {
                            nextIndex = j;
                            break;
                        }
                    }
                    cycle.Add(nextIndex);

                }
                /*Console.Write("cycle ");
                foreach (int item in cycle)
                {
                    Console.Write(item + "  ");
                }*/
                //Console.WriteLine();
                i++;
                if (cycle.Count == 1)
                {
                    /* int swp= PWP_HyperHeuristic.rnd.Next(child1.Length);
                     int swp2 = (swp + 1) % child1.Length;
                     int temp = child1[swp];
                     child1[swp] = child1[swp2];
                     child1[swp2] = temp;*/
                    aList.Remove(cycle[0]);
                    continue;
                }

                /* if (this.CompareSolution(solution1.AddresIndexOrder, child1))
                 {
                     Console.WriteLine("same");
                 }*/
                for (int k = 0; k < solution1.AddresIndexOrder.Length; k++)
                {
                    if (cycle.Contains(k))
                    {
                        child2[k] = parent1Genes[k];
                        child1[k] = parent2Genes[k];
                    }
                    else
                    {
                        child2[k] = parent2Genes[k];
                        child1[k] = parent1Genes[k];
                    }
                }

                child1.CopyTo(parent2Genes, 0);
                child2.CopyTo(parent1Genes, 0);
                break;

            }
            /*double child1Distance = this.CalculateSolutionDistance(child1);
            double child2Distance = this.CalculateSolutionDistance(child2);
            if (child1Distance<child2Distance)
            {
                this.SwapBetter(child1);
            }
            else
            {
                this.SwapBetter(child2);
            }*/
            this.SwapBetter(child2);
            this.SwapBetter(child1);
            /*if (this.CompareSolution(parent1Genes, parent2Genes))
            {
                Console.WriteLine("dsad");
            }*/
            /*
            double child1Distance = this.CalculateSolutionDistance(child1);
            double child2Distance = this.CalculateSolutionDistance(child2);
            if (child1Distance< solution2.distance || child2Distance<solution1.distance)
            {
                solution2.SetSolution(child1);
                this.CalculateSolutionDistance(ref solution2);
                solution1.SetSolution(child2);
                this.CalculateSolutionDistance(ref solution1);
            }*/

            /*
            double child1Distance = this.CalculateSolutionDistance(parent1Genes);
            double child2Distance = this.CalculateSolutionDistance(parent2Genes);

            if (child1Distance < solution2.distance)
            {
                if (!this.CompareSolution(solution1.AddresIndexOrder, parent1Genes))
                {
                    solution2.SetSolution(parent1Genes);
                    this.CalculateSolutionDistance(ref solution2);
                }
            }
            else if (child2Distance < solution1.distance)
            {
                if (!this.CompareSolution(solution2.AddresIndexOrder, parent2Genes))
                {
                    solution1.SetSolution(parent2Genes);
                    this.CalculateSolutionDistance(ref solution1);
                }
            }*/


        }

    }
}
