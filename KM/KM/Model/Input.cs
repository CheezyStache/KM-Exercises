using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM
{
    public static class Input
    {
        private static int k; //количество факторов
        private static int l; //количество линейных переменных

        private static int N;

        public static int ResearchCount
        {
            get { return N; }
            set { N = value; }
        }

        public static int XCount
        {
            get { return k; }
            set { k = value; }
        }

        public static int YCount { get; set; }

        public static int GenerationRatio
        {
            get { return l; }
            set { l = value; }
        }

        public static double[,] ZeroAndInterval { get; set; }

        public static bool[][] GenerationX { get; set; }
    }

    public class TableObject
    {
        public int Number { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
    }
}
