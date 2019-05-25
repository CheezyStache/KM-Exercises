using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math6 : IMathStrategy
    {
        private ManageService manageService;
        private double dispAdekvat;
        Status status;

        public Math6(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            throw new NotImplementedException();
        }

        public Status Process()
        {
            double[] coefRegr = manageService.GetResultFromStep(2).Cast<double>().ToArray();
            int[][] planValues = (int[][])manageService.GetResultFromStep(1);
            double[] arrayS0 = manageService.GetResultFromStep(3).Cast<double>().ToArray();
            double s0 = arrayS0[0];

            status = new Status();

            dispAdekvat = 0;

            //TODO вместо иксов planValues
            TableObject[] inputTable = (TableObject[])manageService.GetResultFromStep(0);
            double[] otklikValues = new double[Input.ResearchCount];

            for (int i = 0; i < otklikValues.Length; ++i)
            {
                otklikValues[i] = 0;

                for (int j = 0; j < Input.ResearchCount; ++j)
                {
                    otklikValues[i] += planValues[i][j] * coefRegr[i];
                }
            }

            for (int i = 0; i < inputTable.Length; ++i)
            {
                dispAdekvat += Math.Pow((inputTable[i].Y[inputTable[i].Y.Length - 1] - otklikValues[i]), 2);
            }

            dispAdekvat *= Input.YCount / (Input.ResearchCount - (Input.XCount + Input.GenerationRatio + 1));

            if (dispAdekvat <= s0)
            {
                status.isSuccsess = true;
                status.messages = new string[] { dispAdekvat + "<=" + s0 + "Модель адекватна. s_ад^2 ≤ s_0^2. Тогда вывод об адекватности модели может быть сделан без проверки условия F_p< F_T" };
            } else
            {
                double fisherValue = dispAdekvat / s0;

                //0.003 - znach Ftabl
                if (fisherValue < 0.003)
                {
                    status.isSuccsess = true;
                    status.messages = new string[] { fisherValue + "<=" + 0.003 + "Модель адекватна." };
                } else
                {
                    status.isSuccsess = false;
                    status.messages = new string[] { fisherValue + ">=" + 0.003 + "Модель неадекватна." };
                }
            }

            return status;
        }

        public string GetName()
        {
            return "Проверка модели на адекватность";
        }

        public string[] GetStringResult()
        {
            return status.messages;
        }
    }
}
