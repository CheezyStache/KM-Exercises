using KM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM
{
    class Math1: IMathStrategy
    {
        TableObject[] tableObjects;

        public object[] GetResult()
        {
            return tableObjects;
        }

        public Status Process()
        {
            Random rand = new Random();
            tableObjects = new TableObject[Input.ResearchCount];

            for (int i = 0; i < Input.ResearchCount; i++)
            {
                double[] xRandom = new double[Input.XCount];
                for (int j = 0; j < xRandom.Length; j++)
                {
                    xRandom[j] = rand.NextDouble() * Input.ZeroAndInterval[j, 1] * 2 + Input.ZeroAndInterval[j, 0] - Input.ZeroAndInterval[j, 1];
                    xRandom[j] = Math.Round(xRandom[j], 3);
                }
                double[] yRandom = new double[Input.YCount + 1];
                for (int j = 0; j < yRandom.Length; j++)
                {
                    if (j != yRandom.Length - 1)
                    {
                        yRandom[j] = rand.NextDouble() * Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 1] * 2 + Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 0] - Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 1];
                        yRandom[j] = Math.Round(yRandom[j], 3);
                    }
                    else
                    {
                        yRandom[j] = yRandom.Where((arr, k) => k != yRandom.Length - 1).Average();
                        yRandom[j] = Math.Round(yRandom[j], 3);
                    }
                }
                tableObjects[i] = new TableObject
                {
                    Number = i + 1,
                    Y = yRandom
                };
            }
            return GenerateStatus(true);
        }

        private Status GenerateStatus(bool isSuccess)
        {
            Status status = new Status();
            status.isSuccsess = isSuccess;
            status.messages = new string[]{"Element 1", "ok"};
            return status;
        }

        public string GetName()
        {
            return "Выбор генерирующих соотношений";
        }

        public string[] GetStringResult()
        {
            return null;
        }
    }
}
