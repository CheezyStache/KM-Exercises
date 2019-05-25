using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;
using Accord.Statistics.Distributions.Univariate;

namespace KM
{
    class Math4 : IMathStrategy
    {
        private ManageService manageService;
        private double s0 = 0;
        Status status;

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

            for (int i = 0; i < tableAll.Length; ++i)
            {
                //???
                double[] yTemp = tableAll[i].Y.Take(tableAll[i].Y.Length - 1).Cast<double>().ToArray();
                yCollection.Add(i, yTemp);
            }

            s = new double[yCollection.Count];

            //count s^2
            for (int i = 0; i < yCollection.Count; ++i)
            {
                double[] currentOne = yCollection[i];
                double ySumAVG = 0;
                for (int j = 0; j < currentOne.Length; ++j)
                {
                    ySumAVG += Math.Pow(currentOne[j] - currentOne.Average(), 2);
                }
                s[i] = ySumAVG / (m - 1);
            }

            //count G
            double sMax, sSum;

            sMax = s.Max();
            sSum = s.Sum();
            g = sMax / sSum;

            //KOXPEH
            var g_tabular = Cochran(0.05, Input.ResearchCount, Input.YCount);

            //if yes caclulate s0^2
            if(g < g_tabular)
            {
                success = true;
                s0 = sSum / s.Length;
            }
        

            return GenerateStatus(success, g, g_tabular);

        }

        public string GetName()
        {
            return "Однородность дисперсии";
        }

        public string[] GetStringResult()
        {
            return status.messages;
        }


        private Status GenerateStatus(bool isSuccess, double g, double g_tabular)
        {
            status = new Status();
            status.isSuccsess = isSuccess;
            if (isSuccess)
            {
                status.messages = new string[] { g + " < " + g_tabular + Environment.NewLine + "Ошибка опыта: " + s0 };
            } else
            {
                status.messages = new string[] { g + " > " + g_tabular + Environment.NewLine + "Условие не выполняется. Попробуйте увеличить число параллельных опытов." };
            }
            return status;
        }

        public static double Cochran(double significance_level, int groups_number, int observations_number)
        {
            var fst_degree = (groups_number - 1) * (observations_number - 1);
            var snd_degree = (observations_number - 1);
            var fisher_value = Fisher(significance_level / groups_number, fst_degree, snd_degree);
            var result = 1 / (1 + (groups_number - 1) / fisher_value);
            return result;
        }

        public static double Fisher(double significance_level, int fst_degree, int snd_degree)
        {
            var fd = new FDistribution(fst_degree, snd_degree);
            var fisher_value = 1 / fd.InverseDistributionFunction(significance_level);
            return fisher_value;
        }
    }
}
