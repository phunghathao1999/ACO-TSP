using System;
using System.Collections.Generic;
using System.IO;

namespace ACO_TSP
{
    public class AcoTsp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start !!!");
            List<DistModel> distModels = new List<DistModel>();
            distModels = readFolder();
            foreach (DistModel distModel in distModels)
            {
                int row = distModel.datas.GetLength(0);
                int colums = distModel.datas.Length / row;
                if (distModel.namefile.Split("_")[1] == "d")
                    for (int i = 0; i < 30; i++)
                        CostMatrix(distModel, row);
                else
                    if (distModel.namefile.Split("_")[1] == "e")
                        for (int i = 0; i < 30; i++)
                            EuclidDistance(distModel, row);
            }

            Console.WriteLine("Complete !!!");
        }

        public static List<DistModel> readFolder()
        {
            List<DistModel> distModels = new List<DistModel>();
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

                distModels.Add(new DistModel(filename, datas));
            }
            return distModels;
        }

        public static void writeFolder(string nameFile, int[] bestTour, int cost)
        {
            string folder = @"C:\Users\phung\Desktop\ACO-TSP\ACO-TSP\result\";
            string fileName = nameFile.Split("_")[0] + "_s.txt";
            string fullPath = folder + fileName;

            string[] tourString = new string[bestTour.Length + 1];
            tourString[0] = cost.ToString();
            for (int i = 1; i < bestTour.Length + 1; i++)
                tourString[i] = bestTour[i - 1].ToString();

            try
            {
                string[] readLine = File.ReadAllLines(fullPath);
                if (cost < Int32.Parse(readLine[0]))
                    File.WriteAllLines(fullPath, tourString);
            }
            catch (FileNotFoundException e)
            {
                File.WriteAllLines(fullPath, tourString);
            }
        }

        public static void CostMatrix(DistModel distModel, int noNodes)
        {
            int noAnts = Convert.ToInt32(noNodes / 2);
            double alfa = 0.1;
            double beta = 0.3;
            double globalEvapRate = 0.05;
            bool doOpt2 = true;
            bool doOpt3 = true;
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

            //int[] a = { 0, 14, 50, 53, 107, 223, 222, 224, 245, 293, 363, 168, 190, 292, 294, 371, 71, 86, 108, 178, 295, 373, 106, 220, 221, 296, 307, 362, 365, 374, 225, 291, 361, 69, 83, 264, 290, 105, 226, 234, 366, 372, 219, 15, 263, 109, 297, 218, 104, 110, 298, 103, 217, 360, 370, 100, 167, 179, 273, 299, 323, 379, 19, 80, 87, 111, 169, 177, 216, 300, 352, 35, 262, 265, 272, 308, 353, 359, 20, 153, 321, 327, 114, 261, 302, 369, 1, 21, 92, 215, 303, 2, 3, 214, 145, 329, 154, 213, 271, 326, 358, 4, 5, 304, 305, 320, 176, 212, 260, 274, 306, 325, 328, 330, 350, 211, 22, 36, 112, 130, 159, 170, 6, 113, 259, 233, 319, 349, 23, 155, 171, 210, 258, 277, 209, 227, 275, 289, 339, 341, 378, 7, 81, 88, 172, 228, 278, 288, 24, 156, 180, 309, 287, 348, 115, 181, 324, 340, 342, 286, 93, 116, 141, 257, 270, 310, 182, 194, 347, 142, 191, 256, 311, 343, 354, 143, 229, 248, 285, 230, 269, 284, 25, 37, 183, 236, 249, 266, 231, 235, 255, 312, 283, 357, 26, 184, 192, 241, 246, 377, 8, 82, 117, 331, 337, 344, 27, 118, 313, 94, 119, 244, 247, 332, 345, 120, 185, 237, 346, 131, 186, 279, 280, 333, 355, 195, 121, 314, 196, 356, 122, 187, 197, 315, 9, 28, 38, 198, 65, 123, 124, 188, 301, 316, 368, 125, 66, 317, 173, 335, 376, 242, 29, 334, 89, 174, 189, 322, 126, 48, 239, 90, 250, 281, 367, 10, 127, 267, 49, 91, 128, 95, 175, 251, 338, 51, 129, 364, 96, 252, 268, 351, 67, 97, 336, 30, 59, 375, 98, 151, 99, 199, 31, 101, 200, 253, 232, 32, 282, 84, 152, 254, 33, 318, 201, 34, 102, 202, 157, 160, 203, 204, 60, 132, 205, 243, 240, 11, 68, 72, 158, 161, 276, 133, 12, 206, 73, 13, 207, 39, 134, 162, 52, 208, 74, 40, 135, 163, 41, 75, 144, 136, 164, 238, 54, 42, 57, 76, 137, 165, 55, 58, 43, 146, 149, 166, 56, 61, 138, 147, 150, 44, 193, 77, 139, 148, 62, 78, 79, 63, 140, 85, 16, 17, 45, 46, 47, 64, 18, 70, 0 };
            //Console.WriteLine("a: " + maxMinAS.computeTourLength(a));
            writeFolder(distModel.namefile, maxMinAS.getBestSoFarTour(), maxMinAS.computeTourLength(maxMinAS.getBestSoFarTour()));
        }

        public static void EuclidDistance(DistModel distModel, int noNodes)
        {
            int[,] costMatrix = new int[noNodes, noNodes];
            for (int i = 0; i < noNodes; i++)
                for (int j = 0; j < noNodes; j++)
                {
                    //Console.WriteLine("distModel.datas[" + i + "][1]: " + distModel.datas[i, 1]);
                    //Console.WriteLine("distModel.datas[" + i + "][2]: " + distModel.datas[i, 2]);
                    //Console.WriteLine("distModel.datas[" + j + "][1]: " + distModel.datas[j, 1]);
                    //Console.WriteLine("distModel.datas[" + j + "][2]: " + distModel.datas[j, 2]);
                    if (i == j)
                        costMatrix[i, j] = 0;
                    else
                        costMatrix[i, j] = Convert.ToInt32(Math.Sqrt(Math.Pow(distModel.datas[i, 1] - distModel.datas[j, 1], 2) + Math.Pow(distModel.datas[i, 2] - distModel.datas[j, 2], 2)));
                }


            //Console.WriteLine("distModel.datas[" + i + "][" + j + "]: " + Convert.ToInt32(Math.Sqrt(Math.Pow(distModel.datas[i, 1] - distModel.datas[i, 2], 2) + Math.Pow(distModel.datas[j, 1] - distModel.datas[j, 2], 2))));
            CostMatrix(new DistModel(distModel.namefile, costMatrix), noNodes);
        }

    }
}
