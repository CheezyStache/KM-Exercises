using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math4 : IMathStrategy
    {
        private ManageService manageService;
        private double s0;

        public Math4(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            return new Object[] { s0 };
        }

        public Status Process()
        {
            TableObject[] tableAll;
            Dictionary<int, double[]> yCollection = new Dictionary<int, double[]>();
            double[] s;
            double g;
            int m;
            bool success = false;

            tableAll = (TableObject[])manageService.GetResultFromStep(0);
            m = Input.YCount;

            for(int i = 0; i < tableAll.Length; ++i)
            {
                //????????????????????????????????????????????????????????????????????
                double[] yTemp = tableAll[i].Y.Take(tableAll[i].Y.Length - 1).Cast<double>().ToArray();
                yCollection.Add(i, yTemp);
            }

            s = new double[yCollection.Count];

            //count s^2
            for(int i = 0; i < yCollection.Count; ++i)
            {
                double[] currentOne = yCollection[i];
                double ySumAVG = 0;
                for(int j = 0; j < currentOne.Length; ++j)
                {
                    ySumAVG += Math.Pow(currentOne[j] - currentOne.Average(), 2);
                }
                s[i] = 1 / (m - 1) * ySumAVG;
            }

            //count G
            double sMax, sSum;

            sMax = s.Max();
            sSum = s.Sum();
            g = sMax / sSum;

            //place to KOXPEH



            //if yes caclulate s0^2
            s0 = sSum / s.Length;


            return GenerateStatus(success);

        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string[] GetStringResult()
        {
            throw new NotImplementedException();
        }


        private Status GenerateStatus(bool isSuccess)
        {
            Status status = new Status();
            status.isSuccsess = isSuccess;
            status.messages = new string[] { "Element 4", "ok" };
            return status;
        }
    }
}
