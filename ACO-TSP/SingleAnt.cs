
using System;

namespace ACO_TSP
{
    public class SingleAnt
    {
        public SingleAnt(int n)
        {
            tour = new int[n + 1];
            visited = new bool[n];
            tourLength = 0;
            noNodes = n;
        }
        private int noNodes;
        private int tourLength;
        private int[] tour;
        private bool[] visited;

        public int getTourLength()
        {
            return tourLength;
        }
        public void setTourLength(int len)
        {
            tourLength = len;
        }
        public int getNoNodes()
        {
            return tourLength;
        }
        public void setNoNodes(int len)
        {
            tourLength = len;
        }
        public bool getVisited(int idx)
        {
            return visited[idx];
        }
        public void setVisited(int idx, bool val)
        {
            visited[idx] = val;

        }
        public int getTour(int idx)
        {
            return tour[idx];
        }
        public void setTour(int idx, int val)
        {
            //Console.WriteLine("val: " + val);
            tour[idx] = val;

        }
        public int[] getTour()
        {
            return tour;
        }
    }
}
