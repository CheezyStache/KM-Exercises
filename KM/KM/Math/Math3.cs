using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math3 : IMathStrategy
    {
        private ManageService manageService;

        private double[] coefsRegr;

        private int[][] planValues; // матрица планирования(+1, -1)

        public Math3(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            return new object[] { coefsRegr };
        }

        public Status Process()
        {
            planValues = manageService.GetResultFromStep(1) as int[][];

            CalculateCoefs();

            return GenerateStatus(true);
        }

        private void CalculateCoefs()
        {
            //TODO вместо иксов planValues
            TableObject[] inputTable =  manageService.GetResultFromStep(1) as TableObject[];
            coefsRegr = new double[Input.XCount + Input.GenerationRatio + 1];

            //TODO протестировать
            /*for (int i = 0; i < coefsRegr.Length; ++i)
            {
                coefsRegr[i] = 0;

                for (int j = 0; j < Input.ResearchCount; ++j)
                {
                    coefsRegr[i] += planValues[i][j] * inputTable[j].Y[inputTable[j].Y.Length - 1];
                }

                coefsRegr[i] /= Input.ResearchCount;
            }*/
        }

        private Status GenerateStatus(bool isSuccess)
        {
            Status status = new Status();
            status.isSuccsess = isSuccess;
            status.messages = new string[] { "Element 3", "ok" };
            return status;
        }

        public string GetName()
        {
            return "Матрица планирования";
        }

        public string[] GetStringResult()
        {
            throw new NotImplementedException();
            //return new string[]{ " " };
        }
    }
}
