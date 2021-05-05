using System;
using System.Collections.Generic;
using System.Text;

namespace ACO_TSP
{
    public class OptimizationTSP
    {
        int[,] dist;
        public OptimizationTSP(int[,] dist)
        {
            this.dist = dist;
        }
        public void opt2(int[] tour)
        {
            int i, j, k;
            int a1, a2, a3, b1, b2, b3, swap;
            for (i = 1; i < tour.Length - 2; i++)
            {
                a1 = dist[tour[i - 1], tour[i]];
                a2 = dist[tour[i], tour[i + 1]];
                a3 = dist[tour[i + 1], tour[i + 2]];
                b1 = dist[tour[i - 1], tour[i + 1]];
                b2 = dist[tour[i + 1], tour[i]];
                b3 = dist[tour[i], tour[i + 2]];
                if (a1 + a2 + a3 > b1 + b2 + b3)
                {
                    //Console.WriteLine("opt2: optimization done!");
                    swap = tour[i];
                    tour[i] = tour[i + 1];
                    tour[i + 1] = swap;
                }
            }
        }

        public void opt3(int[] tour)
        {
            int i, j, k;
            int[] distances = { 0, 0, 0, 0, 0, 0 };
            for (i = 1; i < tour.Length - 3; i++)
            {
                // ABC
                distances[0] = dist[tour[i - 1], tour[i]] + dist[tour[i], tour[i + 1]] + dist[tour[i + 1], tour[i + 2]] + dist[tour[i + 2], tour[i + 3]];
                // ACB
                distances[1] = dist[tour[i - 1], tour[i]] + dist[tour[i], tour[i + 2]] + dist[tour[i + 2], tour[i + 1]] + dist[tour[i + 1], tour[i + 3]];
                // BAC
                distances[2] = dist[tour[i - 1], tour[i + 1]] + dist[tour[i + 1], tour[i]] + dist[tour[i], tour[i + 2]] + dist[tour[i + 2], tour[i + 3]];
                // BCA
                distances[3] = dist[tour[i - 1], tour[i + 1]] + dist[tour[i + 1], tour[i + 2]] + dist[tour[i + 2], tour[i]] + dist[tour[i], tour[i + 3]];
                // CAB
                distances[4] = dist[tour[i - 1], tour[i + 2]] + dist[tour[i + 2], tour[i]] + dist[tour[i], tour[i + 1]] + dist[tour[i + 1], tour[i + 3]];
                // CBA
                distances[5] = dist[tour[i - 1], tour[i + 2]] + dist[tour[i + 2], tour[i + 1]] + dist[tour[i + 1], tour[i]] + dist[tour[i], tour[i + 3]];
                // caut min
                int min = Int32.MaxValue;
                int minIdx = -1;
                for (j = 0; j < 6; j++)
                {
                    if (min > distances[j])
                    {
                        min = distances[j];
                        minIdx = j;
                    }
                }
                int swap;
                switch (minIdx)
                {
                    case 0:     // ABC
                        break;
                    case 1:     // ACB
                        //Console.WriteLine("opt3: optimization done!");
                        this.swap(tour, i + 1, i + 2);
                        break;
                    case 2:     // BAC
                        //Console.WriteLine("opt3: optimization done!");
                        this.swap(tour, i, i + 1);
                        break;
                    case 3:     // BCA
                                //Console.WriteLine("opt3: optimization done!");
                                // ABC -> BAC
                        this.swap(tour, i, i + 1);
                        // BAC -> BCA
                        this.swap(tour, i + 1, i + 2);
                        break;
                    case 4:     // CAB
                                //Console.WriteLine("opt3: optimization done!");
                                // ABC -> CBA
                        this.swap(tour, i, i + 2);
                        // CBA -> CAB
                        this.swap(tour, i + 1, i + 2);
                        break;
                    case 5:     // CBA
                                //Console.WriteLine("opt3: optimization done!");
                        this.swap(tour, i, i + 2);
                        break;

                }

            }

        }
        public void swap(int[] tour, int i, int j)
        {
            int swap = tour[i];
            tour[i] = tour[j];
            tour[j] = swap;
        }
    }

}
