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
    class Math5 : IMathStrategy
    {
        private ManageService manageService;
        private int[] rghtInd;
        private int[] errInd;
        Status status;

        public Math5(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            return new Object[] {rghtInd};
        }

        public Status Process()
        {
            double[] arrayB = manageService.GetResultFromStep(2).Cast<double>().ToArray();
            double[] arrayS0 = manageService.GetResultFromStep(3).Cast<double>().ToArray();
            double s0 = arrayS0[0];
            double sbi, deltaB;
            double[] tip = new double[arrayB.Length];
            bool success = false;
            List<int> rightIndexes = new List<int>();
            List<int> errIndexes = new List<int>();

            //calculate sbi^2
            sbi = s0 / arrayB.Length;

            //calculate tip
            double rootS0 = Math.Pow(s0, (1 / 2));
            for (int i = 0; i < tip.Length; ++i)
            {
                tip[i] = Math.Abs(arrayB[i]) / rootS0;
            }

            //place to STUDENT

            var t_tabular = Student(0.05, Input.ResearchCount * (Input.YCount - 1));

            //calculate deltaB
            deltaB = Math.Abs(t_tabular * sbi);

            //calculate absolute value
            for(int i = 0; i < tip.Length; ++i)
                {
                    if((Math.Abs(arrayB[i]) > deltaB) && (tip[i] > t_tabular))
                    {
                        rightIndexes.Add(i);
                    }
                    else
                    {
                        errIndexes.Add(i);
                    }
                }
            rghtInd = rightIndexes.ToArray();
            errInd = errIndexes.ToArray();

            if (!errInd.Any())
            {
                success = true;
            }

            return GenerateStatus(success, arrayB, deltaB, tip, t_tabular);
        }

        public string GetName()
        {
            return "Проверка значимости коэффициентов";
        }

        public string[] GetStringResult()
        {
            return status.messages;
        }

        private Status GenerateStatus(bool isSuccess, double[] arrayB, double deltaB, double[] tip, double t_tabular)
        {
            status = new Status();
            status.isSuccsess = isSuccess;

            status.messages = new string[] { "Проверка значимости коэффициентов:" + Environment.NewLine };

            status.messages[0] += String.Join(";" + Environment.NewLine, rghtInd.Select(b => "Коэффициент "  + arrayB[b] + " значим, поскольку " +
            Math.Abs(arrayB[b]) + " > " + deltaB + " и " + tip[b] + " > " + t_tabular));
            status.messages[0] += ";" + Environment.NewLine;

            if (errInd.Any())
            {
                status.messages[0] += String.Join(";" + Environment.NewLine, errInd.Select(b => "Коэффициент " + arrayB[b] + " не значим по заданным условиям"));
            }


            return status;
        }

        public static double Student(double significance_level, int degrees_of_freedom)
        {
            var td = new TDistribution(degrees_of_freedom);
            return Math.Abs(td.InverseDistributionFunction(significance_level));
        }

    }
}
