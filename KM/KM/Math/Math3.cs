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
        private int[][] planValues; // матрица планирования(+1, -1)

        private bool[][] GenerationRel;
        private double[] coefsRegr;

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
            int changeRange = 0;
            int count = 0;
            int value = 1;
            planValues = new int[Input.XCount + Input.GenerationRatio + 1][];

            GenerationRel = Input.GenerationX;

            for (int i = 0; i < Input.XCount + 1; ++i)
            {
                planValues[i] = new int[Input.ResearchCount];
                value = 1;

                if (i != 0)
                {
                    changeRange = (int)Math.Pow(2, i - 1);
                    count = changeRange;
                }

                for (int j = 0; j < planValues[i].Length; ++j)
                {
                    if (i == 0)
                    {
                        planValues[i][j] = value;
                    } else
                    {
                        if (count == 0)
                        {
                            count = changeRange;
                            value *= -1;
                        }

                        count = AssignValueAndDecrement(i, j, count, value);

                    }
                }
            }

            for (int i = Input.XCount + 1, l = 0; i < planValues.Length; ++i, ++l)
            {
                planValues[i] = new int[Input.ResearchCount];

                for (int j = 0; j < Input.ResearchCount; ++j)
                {
                    planValues[i][j] = 1;
                    for (int k = 0; k < Input.GenerationX.Length; ++k)
                    {
                        if (Input.GenerationX[l][k])
                        {
                            planValues[i][j] *= planValues[k + 1][j];
                        }
                    }
                }
            }

            CalculateCoefs();

            return GenerateStatus(true);
        }

        private void CalculateCoefs()
        {
            //TODO вместо иксов planValues
            TableObject[] inputTable =  (TableObject[])manageService.GetResultFromStep(0);
            coefsRegr = new double[Input.XCount + Input.GenerationRatio + 1];

            for (int i = 0; i < coefsRegr.Length; ++i)
            {
                coefsRegr[i] = 0;

                for (int j = 0; j < Input.ResearchCount; ++j)
                {
                    coefsRegr[i] += planValues[i][j] * inputTable[j].Y[inputTable[j].Y.Length - 1];
                }

                coefsRegr[i] /= Input.ResearchCount;
            }
        }

        private int AssignValueAndDecrement(int i, int j, int count,int value)
        {
            planValues[i][j] = value;
            return --count;
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
        }
    }
}
