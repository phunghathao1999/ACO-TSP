﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ACO_TSP
{
    public class AcoTsp
    {
        public static void Main(string[] args)
        {
            
            List<DistModel> distModels = new List<DistModel>();
            distModels = readForder();
            foreach (DistModel distModel in distModels)
            {
                int row = distModel.datas.GetLength(0);
                int colums = distModel.datas.Length / row;
                if (row == colums)
                    for(int i = 0; i<30; i++)
                        CostMatrix(distModel, row);

            }
        }

        public static List<DistModel> readForder()
        {
            List<DistModel> testModels = new List<DistModel>();
            string folderPath = @"C:\Users\phung\Desktop\ACO-TSP\ACO-TSP\test\";
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                string filename = Path.GetFileName(file).Replace(".txt", "");
                var lines = File.ReadAllLines(file);

                string tmp = lines[0].Trim();
                string tmp1 = "";
                for (int i = 0; i < tmp.Length; i++)
                    if (tmp[i] == ' ')
                    {
                        if (tmp[i - 1] != ' ')
                            tmp1 += tmp[i];
                    }
                    else
                        tmp1 += tmp[i];

                int[,] datas = new int[lines.Length, tmp1.Split(" ").Length];

                int k = 0;
                foreach (string line in lines)
                {
                    string result = "";
                    string S = line.Trim();
                    for (int i = 0; i < S.Length; i++)
                        if (S[i] == ' ')
                        {
                            if (S[i - 1] != ' ')
                                result += S[i];
                        }
                        else
                            result += S[i];
                    var a = result.Split(" ");
                    for (int i = 0; i < a.Length; i++)
                        datas[k, i] = Int32.Parse(a[i]);
                    k++;
                }

                testModels.Add(new DistModel(filename, datas));
            }
            return testModels;
        }
        public static void CostMatrix(DistModel distModel, int noNodes)
        {
            int noAnts = 20;
            double alfa = 1.0;
            double beta = 3.0;
            double globalEvapRate = 0.5;
            bool doOpt2 = false;
            bool doOpt3 = false;
            MaxMinAS maxMinAS = new MaxMinAS(noNodes, noAnts, alfa, beta, globalEvapRate, doOpt2, doOpt3, 1000);

            maxMinAS.initData();
            maxMinAS.setDistance(distModel.datas);
            maxMinAS.initPheromones();
            maxMinAS.computeHeuristic();
            maxMinAS.initAnts();

            maxMinAS.constructSolutions();
            maxMinAS.localSearch();
            maxMinAS.updatePheromones();
            maxMinAS.setIteration(maxMinAS.getIteration() + 1);

            int[] bestTour = maxMinAS.getBestSoFarTour();
            for (int i = 0; i < bestTour.Length; i++)
                Console.WriteLine("a: " + bestTour[i]);
            Console.WriteLine("cost: " + maxMinAS.computeTourLength(bestTour));

        }

        public static void OxygenCoordinates(DistModel distModel, int noNodes, int noAnts)
        {

        }
    }
}