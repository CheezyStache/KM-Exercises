using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM
{
    class Math1: IMathStrategy
    {
        public string[] ShowInfo()
        {
            return null;
        }

        public object[] GetResult()
        {
            Random rand = new Random();
            TableObject[] tableObjects = new TableObject[Input.ResearchCount];

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
                    }
                }
                tableObjects[i] = new TableObject
                {
                    Number = i + 1,
                    X = xRandom,
                    Y = yRandom
                };
            }

            return tableObjects;
        }
    }
}
