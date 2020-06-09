using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperHeuristicAnyTimeAlgorithm
{
    //public delegate void DistanceUpdatedDelegete();
    partial class PWP_HyperHeuristic
    {
        public static Random rnd = new Random();
        public Point home { get; set; }
        public Point postOffice { get; set; }
        public List<Point> deliveryAddreses { get; set; }
        public List<Solution> solutionList;
        public Solution bestSolution;
        public Thread thread = null;
        public bool isPaused = false;
        public List<double> distances = new List<double>();
        long times;
        private Mutex mutexDrawing = new Mutex();
        private Mutex mutexHeuristic = new Mutex();

        int circleSize = 20;
        int radius = 10;

        public enum heuristics
        {
            mutation_Inversion,
            mutation_AdjacentSwap,
            mutation_Reinsertion,
            localSearch_NextDescent,
            localSearch_HillClimbing,
            crossover_Ordered,
            crosover_Cycle
        }
        //public event DistanceUpdatedDelegete distanceUpdatedEvent;
        //public ShowDistanceDelegete showDistanceDelegete;
        public PWP_HyperHeuristic(int width, int height, int deliveryCount, int solutionCount)
        {
            this.home = new Point(rnd.Next(40, width - 40), rnd.Next(40, height - 40));
            this.postOffice = new Point(rnd.Next(40, width - 40), rnd.Next(40, height - 40));
            this.deliveryAddreses = new List<Point>();
            //this.distanceUpdatedEvent = new DistanceUpdatedDelegete(this.DistanceUpdate);
            // showDistanceDelegete += this.ShowDistance;
            //public event DistanceUpdatedDelegete ProcessCompleted;
            for (int i = 0; i < deliveryCount; i++)
            {
                this.deliveryAddreses.Add(new Point(rnd.Next(40, width - 40), rnd.Next(40, height - 40)));
            }

            this.solutionList = new List<Solution>();
            this.bestSolution = null;



            this.InitializeSolutions(solutionCount);

        }
        /*public void DistanceUpdate()
        {

        }*/

        public void DrawBestSolution(Graphics g)
        {
            mutexHeuristic.WaitOne();

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.FillEllipse(new SolidBrush(Color.Green), this.postOffice.X - radius, this.postOffice.Y - radius, circleSize, circleSize);
            g.DrawEllipse(new Pen(Color.Black), this.postOffice.X - radius, this.postOffice.Y - radius, circleSize, circleSize);

            using (Brush b = new SolidBrush(Color.Blue))
            {
                using (Pen p = new Pen(Color.Black))
                {
                    foreach (Point item in this.deliveryAddreses)
                    {
                        g.FillEllipse(b, item.X - radius, item.Y - radius, circleSize, circleSize);
                        g.DrawEllipse(p, item.X - radius, item.Y - radius, circleSize, circleSize);
                    }
                }
            }
            g.FillEllipse(new SolidBrush(Color.Red), this.home.X - radius, this.home.Y - radius, circleSize, circleSize);
            g.DrawEllipse(new Pen(Color.Black), this.home.X - radius, this.home.Y - radius, circleSize, circleSize);
            if (this.bestSolution != null)
            {
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                using (Pen p = new Pen(Color.Green))
                {
                    p.CustomEndCap = bigArrow;
                    p.Width = 2;
                    g.DrawLine(p, this.postOffice.X, this.postOffice.Y, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[0]].X, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[0]].Y);
                    p.Color = Color.Yellow;
                    for (int i = 0; i < this.deliveryAddreses.Count - 1; i++)
                    {
                        g.DrawLine(p, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[i]].X, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[i]].Y, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[i + 1]].X, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[i + 1]].Y);

                    }
                    p.Color = Color.Blue;
                    g.DrawLine(p, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[this.deliveryAddreses.Count - 1]].X, this.deliveryAddreses[this.bestSolution.AddresIndexOrder[this.deliveryAddreses.Count - 1]].Y, this.home.X, this.home.Y);


                }

            }

            mutexHeuristic.ReleaseMutex();



        }
        private void InitializeSolutions(int count)
        {

            for (int i = 0; i < count; i++)
            {
                Solution newSolution = new Solution(this.deliveryAddreses.Count);
                newSolution.SetRandomSolution();
                this.CalculateSolutionDistance(ref newSolution);
                this.solutionList.Add(newSolution);
            }

            this.OrderSolution();

            this.SetBestSolution(this.solutionList[0]);


        }


        public void UpdateDistance(List<double> distances)
        {

            distances.Add(this.bestSolution.distance);
            if (distances.Count == 100)
            {
                distances.RemoveAt(0);
            }
            //this.distanceUpdatedEvent();
        }

        public void SetBestSolution(Solution solution)
        {

            mutexHeuristic.WaitOne();
            this.bestSolution = new Solution(solution.AddresIndexOrder.Length);

            this.bestSolution.SetSolution(solution.AddresIndexOrder);
            this.CalculateSolutionDistance(ref this.bestSolution);
            this.UpdateDistance(this.distances);
            //this.showDistanceDelegete(distances);

            mutexHeuristic.ReleaseMutex();
            /*  this.bestSolution.AddresIndexOrder[0] = 5;
              Console.WriteLine(this.bestSolution);
              Console.WriteLine(this.solutionList[0]);*/
        }

        public void StartAlgorithm(long times)
        {
            this.times = times;
            if (this.thread != null)
            {
                this.thread.Abort();
            }
            this.thread = new Thread(new ThreadStart(Run));
            this.thread.Start();
        }

        private void Run()
        {

            for (int i = 0; i < times; i++)
            {
                while (this.isPaused)
                {
                   
                    Thread.Sleep(50);
                }
                // mutexHeuristic.WaitOne();
                int level = PWP_HyperHeuristic.rnd.Next(1, this.deliveryAddreses.Count / 5 + 1);
                //level = 1;
                Solution solution1 = null;
                Solution solution2 = null;
                
                //heu = 6;
                
                int mut = PWP_HyperHeuristic.rnd.Next(this.solutionList.Count - 1) + 1;
                int loc = PWP_HyperHeuristic.rnd.Next(this.solutionList.Count - 1) + 1;

                int cros_best = PWP_HyperHeuristic.rnd.Next(2);
                int cros_other = PWP_HyperHeuristic.rnd.Next(cros_best + 1, this.solutionList.Count);
                /* if (cros_best==cros_other)
                 {

                     Console.Write("fdsf");
                 }*/
                int heu = rnd.Next(5)+2;
                Console.WriteLine(i + ". time heu=" + heu);
                switch ((heuristics)heu)
                {
                    case heuristics.mutation_Inversion:
                        level = (level % 3) + 1;
                        solution1 = this.solutionList[mut];
                        this.MutationInversion(ref solution1, level);
                        break;
                    case heuristics.mutation_AdjacentSwap:
                        level = (level % 3) + 1;
                        solution1 = this.solutionList[mut];
                        this.MutationAdjacentSwap(ref solution1, level);
                        break;
                    case heuristics.mutation_Reinsertion:
                        level = (level % 3) + 1;
                        solution1 = this.solutionList[mut];
                        this.MutationReinsertion(ref solution1, level);
                        break;
                    case heuristics.localSearch_NextDescent:
                        solution1 = this.solutionList[loc];
                        this.LocalSearchNextDescent(ref solution1, level);
                        break;
                    case heuristics.localSearch_HillClimbing:
                        solution1 = this.solutionList[loc];
                        this.LocalSearchHillClimbing(ref solution1, level);
                        break;
                    case heuristics.crossover_Ordered:
                        solution1 = this.solutionList[cros_other];
                        solution2 = solutionList[cros_best];
                        this.CrosoverOrdered(ref solution1, ref solution2, level);
                        break;
                    case heuristics.crosover_Cycle:
                        solution1 = this.solutionList[cros_other];
                        solution2 = solutionList[cros_best];
                        this.CrosoverCycle(ref solution1, ref solution2, level);
                        break;
                    default:
                        break;
                }
                if (HasSameSolutionInSolutionList())
                {
                    Console.WriteLine("has");
                }
                /*if (solutionList[0].distance > solutionList[1].distance)
                {
                    this.solutionList.Insert(0, solutionList[1]);
                    this.solutionList.RemoveAt(this.solutionList.Count - 1);
                }*/

                this.OrderSolution();

                this.SetBestSolution(this.solutionList[0]);

                // mutexHeuristic.ReleaseMutex();
                //Thread.Sleep(30);

            }

        }

        public void SwapBetter(int[] solution)
        {
            double distance = this.CalculateSolutionDistance(solution);
            if (this.HasSameSolutionInSolutionList(solution) || this.CheckHasSameGenes(solution))
            {
                return;
            }
            for (int i = this.solutionList.Count - 1; i > 1; i--)
            {

                if (distance < this.solutionList[i].distance)
                {
                    Solution item = this.solutionList[i];
                    item.SetSolution(solution);
                    this.CalculateSolutionDistance(ref item);
                    break;
                }
            }


        }
        public void OrderSolution()
        {


            bool ordered = false;
            while (!ordered)
            {
                ordered = true;
                for (int i = 0; i < solutionList.Count - 1; i++)
                {

                    if (solutionList[i].Equals(solutionList[i + 1]))
                    {
                        Console.WriteLine("equal");
                    }
                    if (solutionList[i].distance > solutionList[i + 1].distance)
                    {
                        ordered = false;
                        this.solutionList.Insert(i, solutionList[i + 1]);
                        this.solutionList.RemoveAt(i + 2);
                    }
                }

            }
        }
        public bool CheckHasSameGenes(int[] solution)
        {
            bool has = false;
            for (int i = 0; i < solution.Length; i++)
            {
                for (int j = i + 1; j < solution.Length; j++)
                {
                    if (solution[i] == solution[j])
                    {
                        has = true;
                        return has;
                    }
                }
            }
            return has;
        }

        public bool CompareSolution(Solution solution1, Solution solution2)
        {
            bool result = true;
            for (int i = 0; i < solution1.AddresIndexOrder.Length; i++)
            {
                if (solution1.AddresIndexOrder[i] != solution2.AddresIndexOrder[i])
                {
                    result = false;
                    return result;
                }
            }
            return result;
        }
        public bool CompareSolution(int[] solution1, int[] solution2)
        {
            bool result = true;
            for (int i = 0; i < solution1.Length; i++)
            {
                if (solution1[i] != solution2[i])
                {
                    result = false;
                    return result;
                }
            }
            return result;
        }

        public bool HasSameSolutionInSolutionList(int[] solution)
        {
            bool has = false;
            for (int i = 0; i < solutionList.Count; i++)
            {

                if (this.CompareSolution(solutionList[i].AddresIndexOrder, solution))
                {
                    has = true;
                    return has;
                }

            }
            return has;

        }

        public bool HasSameSolutionInSolutionList()
        {
            bool has = false;
            for (int i = 0; i < solutionList.Count; i++)
            {
                for (int j = i + 1; j < solutionList.Count; j++)
                {
                    if (this.CompareSolution(solutionList[i], solutionList[j]))
                    {
                        has = true;
                        return has;
                    }
                }
            }
            return has;

        }

        public double CalculateSolutionDistance(ref Solution solution)
        {
            double distance = Math.Sqrt(Math.Pow((this.postOffice.X - this.deliveryAddreses[solution.AddresIndexOrder[0]].X), 2) + Math.Pow((this.postOffice.Y - this.deliveryAddreses[solution.AddresIndexOrder[0]].Y), 2));
            for (int i = 0; i < solution.AddresIndexOrder.Length - 1; i++)
            {
                distance += Math.Sqrt(Math.Pow((this.deliveryAddreses[solution.AddresIndexOrder[i]].X - this.deliveryAddreses[solution.AddresIndexOrder[i + 1]].X), 2) + Math.Pow((this.deliveryAddreses[solution.AddresIndexOrder[i]].Y - this.deliveryAddreses[solution.AddresIndexOrder[i + 1]].Y), 2));

            }
            distance += Math.Sqrt(Math.Pow((this.home.X - this.deliveryAddreses[solution.AddresIndexOrder[solution.AddresIndexOrder.Length - 1]].X), 2) + Math.Pow((this.home.Y - this.deliveryAddreses[solution.AddresIndexOrder[solution.AddresIndexOrder.Length - 1]].Y), 2));
            solution.distance = distance;

            return distance;
        }
        public double CalculateSolutionDistance(int[] solutionIndex)
        {
            double distance = Math.Sqrt(Math.Pow((this.postOffice.X - this.deliveryAddreses[solutionIndex[0]].X), 2) + Math.Pow((this.postOffice.Y - this.deliveryAddreses[solutionIndex[0]].Y), 2));
            for (int i = 0; i < solutionIndex.Length - 1; i++)
            {
                distance += Math.Sqrt(Math.Pow((this.deliveryAddreses[solutionIndex[i]].X - this.deliveryAddreses[solutionIndex[i + 1]].X), 2) + Math.Pow((this.deliveryAddreses[solutionIndex[i]].Y - this.deliveryAddreses[solutionIndex[i + 1]].Y), 2));

            }

            distance += Math.Sqrt(Math.Pow((this.home.X - this.deliveryAddreses[solutionIndex[solutionIndex.Length - 1]].X), 2) + Math.Pow((this.home.Y - this.deliveryAddreses[solutionIndex[solutionIndex.Length - 1]].Y), 2));

            return distance;
        }


        public double GetDistanceHomeAndLocation(int index)
        {
            double distance = Math.Sqrt(Math.Pow((this.home.X - this.deliveryAddreses[index].X), 2) + Math.Pow((this.home.Y - this.deliveryAddreses[index].Y), 2));
            return distance;

        }
        public double GetDistancePostOfficeAndLocation(int index)
        {
            double distance = Math.Sqrt(Math.Pow((this.postOffice.X - this.deliveryAddreses[index].X), 2) + Math.Pow((this.postOffice.Y - this.deliveryAddreses[index].Y), 2));
            return distance;

        }
        public double GetDistanceLocationAndLocation(int index1, int index2)
        {
            double distance = Math.Sqrt(Math.Pow((this.deliveryAddreses[index1].X - this.deliveryAddreses[index2].X), 2) + Math.Pow((this.deliveryAddreses[index1].Y - this.deliveryAddreses[index2].Y), 2));
            return distance;

        }


    }
}
