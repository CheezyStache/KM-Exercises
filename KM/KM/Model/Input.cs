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

        /*static Input()
        {
            fisherTable = new Dictionary<int, Dictionary<int, double>>();

            Dictionary<int, double> fisherRow = new Dictionary<int, double>();
            fisherRow.Add(1, 161.4);
            fisherRow.Add(2, 199.5);
            fisherRow.Add(3, 215.7);
            fisherRow.Add(4, 224.6);
            fisherRow.Add(5, 230.2);
            fisherRow.Add(6, 234);
            fisherRow.Add(7, 236.8);
            fisherRow.Add(8, 238.9);
            fisherRow.Add(9, 240.54);
            fisherRow.Add(10, 241.88);
            fisherRow.Add(11, 243.91);
            fisherRow.Add(12, 245.95);
            fisherRow.Add(15, 248.01);
            fisherRow.Add(16, 248.6);
            fisherRow.Add(20, 249.05);
            for(int i = 21; i < 1000; i++)
                fisherRow.Add(i, 254.3);
            fisherTable.Add(1, fisherRow);

            fisherRow = new Dictionary<int, double>();
            fisherRow.Add(1, 18.51);
            fisherRow.Add(2, 19);
            fisherRow.Add(3, 19.16);
            fisherRow.Add(4, 19.25);
            fisherRow.Add(5, 19.3);
            fisherRow.Add(6, 19.33);
            fisherRow.Add(7, 19.35);
            fisherRow.Add(8, 19.37);
            fisherRow.Add(9, 19.385);
            fisherRow.Add(10, 19.396);
            fisherRow.Add(11, 19.413);
            fisherRow.Add(12, 19.429);
            for(int i = 13; i < 1000; i++)
                fisherRow.Add(i, 19.45);
            fisherTable.Add(2, fisherRow);

            fisherRow = new Dictionary<int, double>();
            fisherRow.Add(1, 10.128);
            fisherRow.Add(2, 9.55);
            fisherRow.Add(3, 9.277);
            fisherRow.Add(4, 9.1172);
            fisherRow.Add(5, 9.0135);
            fisherRow.Add(6, 8.94);
            fisherRow.Add(7, 8.887);
            fisherRow.Add(8, 8.85);
            fisherRow.Add(9, 8.81);
            fisherRow.Add(10, 8.785);
            fisherRow.Add(11, 8.75);
            fisherRow.Add(12, 8.7);
            for(int i = 13; i < 1000; i++)
                fisherRow.Add(i, 8.65);
            fisherTable.Add(3, fisherRow);

            fisherRow = new Dictionary<int, double>();
            fisherRow.Add(1, 0);
            fisherRow.Add(2, 0);
            fisherRow.Add(3, 0);
            fisherRow.Add(4, 0);
            fisherRow.Add(5, 0);
            fisherRow.Add(6, 0);
            fisherRow.Add(7, 0);
            fisherRow.Add(8, 0);
            fisherRow.Add(9, 0);
            fisherRow.Add(10, 0);
            fisherRow.Add(11, 0);
            fisherRow.Add(12, 0);
            for(int i = 13; i < 1000; i++)
                fisherRow.Add(i, 0);
            fisherTable.Add(4, fisherRow);
        }*/

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
