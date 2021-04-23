using System;
using System.Collections.Generic;
using System.IO;

namespace ACO_TSP
{
    public class Program
    {
        int iLevyFlag = 0;              // 0 or 1, default 0;
        double dLevyThreshold = 1;      //0--1
        double dLevyRatio = 1;          //0.1--5

        double dContribution = 0;       //0--10

        int iGreedyLevyFlag = 0;        // 0 or 1
        double dGreedyEpsilon = 0.9;    // 0--1
        double dGreedyLevyThreshold = 1;//0--1
        double dGreedyLevyRatio = 1;	//0.1--5
        public static void Main(string[] args)
        {
            List<TestModel> testModels = new List<TestModel>();
            readForder(testModels: ref testModels);

            foreach (TestModel testModel in testModels)
            {
                int row = testModel.datas.GetLength(0);
                int colums = testModel.datas.Length / row;
                if (row == colums)
                    CostMatrix(testModel);
                else
                    OxyCoordinates(testModel);
            }
        }

        public static void readForder(ref List<TestModel> testModels)
        {
            string folderPath = @"C:\Users\phung\Desktop\OneDrive\Seminar\ACO-TSP\ACO-TSP\test\";
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
                foreach(string line in lines)
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
                
                testModels.Add(new TestModel(filename, datas));
            }
        }

        public static void OxyCoordinates(TestModel testModel)
        {
            //int point = testModel.datas.GetLength(0);
            //int[,] T = new int[point, point]; // Nồng độ vết mùi trên các cạnh
            //int[,] Delta = new int[point, point]; // Lưu sự cập nhật mùi
            //int[] W = new int[point]; // Hành trình mỗi con kiến
            //bool[] Mark = new bool[point]; // Các đỉnh đã thăm
            //int[] UV = new int[point]; // Các đỉnh chưa thăm của con kiến

            //for (int i = 0; i < point; i++)
            //    for (int j = 0; j < 2; j++)
            //        Console.WriteLine("row[" + i + "," + j + "]: " + testModel.datas[i, j]);
        }
        public static void CostMatrix(TestModel testModel)
        {
            int point = testModel.datas.GetLength(0);
            int[,] T = new int[point, point]; // Nồng độ vết mùi trên các cạnh
            int[,] Delta = new int[point, point]; // Lưu sự cập nhật mùi
            int[] W = new int[point]; // Hành trình mỗi con kiến
            bool[] Mark = new bool[point]; // Các đỉnh đã thăm
            int[] UV = new int[point]; // Các đỉnh chưa thăm của con kiến

            for (int i = 0; i < point; i++)
                for (int j = 0; j < point; j++)
                    Console.WriteLine("row[" + i + "," + j + "]: " + testModel.datas[i, j]);


        }
        public bool termination_condition(int n_tours, int max_tours)
        {
            if (((n_tours >= max_tours) && (elapsed_time(VIRTUAL) >= max_time)) || (best_so_far_ant->tour_length <= optimal))
                return true;
            return false;
        }

        public void construct_solutions()
        {

        }
    }
    
}
