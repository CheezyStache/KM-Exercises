using System;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math2 : IMathStrategy
    {
        private ManageService manageService;

        private int[][] planValues; // матрица планирования(+1, -1)

        private bool[][] GenerationRel; // послднее значение в каждом массиве отвечает за минус (минус это true)

        private TableObject[] tableObjects;

        public Math2(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            return planValues;
        }

        private void CalculatePlanValues()
        {
            tableObjects = manageService.GetResultFromStep(0) as TableObject[];

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
                    }
                    else
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

            for (int i = Input.XCount + 1, l = 0; i < planValues.Length; ++i, ++l) //ТОDO протестировать
            {
                planValues[i] = new int[Input.ResearchCount];

                for (int j = 0; j < Input.ResearchCount; ++j)
                {
                    planValues[i][j] = 1;
                    for (int k = 0; k < Input.GenerationX[l].Length - 1; ++k)
                    {
                        if (Input.GenerationX[l][k])
                        {
                            planValues[i][j] *= planValues[k + 1][j];
                        }
                    }

                    if(Input.GenerationX[l][Input.GenerationX[l].Length - 1])
                        planValues[i][j] *= -1;
                }
            }

            
            for(int i = 0; i < tableObjects.Length; i++)
            {
                tableObjects[i].X = new int[planValues.Length];
            }

            for (int i = 0; i < tableObjects.Length; i++)
            {
                for (int j = 0; j < planValues.Length; j++)
                {
                    tableObjects[i].X[j] = planValues[j][i];
                }
            }
        }

        private int AssignValueAndDecrement(int i, int j, int count, int value)
        {
            planValues[i][j] = value;
            return --count;
        }

        public Status Process()
        {
            CalculatePlanValues();

            //Port logic for planValues to this class
            Status status = new Status();
            status.isSuccsess = true;
            status.messages = new string[] { "Element 2", "ok" };
            return status;
        }

        public string GetName()
        {
            return "Таблица исходных данных";
        }

        public string[] GetStringResult()
        {
            return null;
        }
    }
}
