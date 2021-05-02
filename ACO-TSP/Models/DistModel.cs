using System;
using System.Collections.Generic;
using System.Text;

namespace ACO_TSP
{
    public class DistModel
    {
        public string namefile { get; set; }
        //public List<List<int>> datas { get; set; }
        public int[,] datas { get; set; }

        public DistModel(string namefile, int[,] datas)
        {
            this.namefile = namefile;
            this.datas = datas;
        }
    }
}
