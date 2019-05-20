using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math5 : IMathStrategy
    {
        private ManageService manageService;

        public Math5(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            return new Object[] { };
        }

        public Status Process()
        {
            double[] arrayB = manageService.GetResultFromStep(2).Cast<double>().ToArray();
            double[] arrayS0 = manageService.GetResultFromStep(3).Cast<double>().ToArray();
            double s0 = arrayS0[0];
            double sbi;
            double[] tip = new double[arrayB.Length];
            bool success = false;

            //calculate sbi^2
            sbi = s0 / arrayB.Length;

            //calculate tip
            double rootS0 = Math.Pow(s0, (1 / 2));
            for (int i = 0; i < tip.Length; ++i)
            {
                tip[i] = Math.Abs(arrayB[i]) / rootS0;
            }

            //place to STUDENT

            //calculate deltaB

            //calculate absolute value
            


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
            status.messages = new string[] { "Element 5", "ok" };
            return status;
        }

    }
}
