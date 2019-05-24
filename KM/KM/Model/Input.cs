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

        public static int ResearchCount //количество экспериментов
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

        public static int GenerationRatio // параметры
        {
            get { return l; }
            set { l = value; }
        }

        public static double[,] ZeroAndInterval { get; set; }

        public static bool[][] GenerationX { get; set; }

        private static Dictionary<int, Dictionary<int, double>> fisherTable;

        static Input()
        {
            fisherTable = new Dictionary<int, Dictionary<int, double>>();

            Dictionary<int, double> fisherRow = new Dictionary<int, double>();
            fisherRow.Add(1, 161);
            fisherRow.Add(2, 200);

            fisherTable.Add(1, fisherRow);

            fisherRow = new Dictionary<int, double>();
            fisherTable.Add(2, fisherRow);
        }

        public static double GetFisherTableValue(int f1, int f2)
        {
            return fisherTable[f2][f1];
        }
    }

    public class TableObject
    {
        public int Number { get; set; }
        public int[] X { get; set; }
        public double[] Y { get; set; }
    }

    public struct Generation
    {
        public int XNumber { get; set; }
        public int[] ReferenceX { get; set; }
    }
}
