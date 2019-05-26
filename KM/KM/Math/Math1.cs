using KM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Distributions.Univariate;
using System.Threading.Tasks;

namespace KM
{
    class Math1 : IMathStrategy
    {
        TableObject[] tableObjects;

        public object[] GetResult()
        {
            return tableObjects;
        }

        public double[] randomchik(double intervalMin, double intervalMax, Random rnd)
        {
            double deltaY = 0.25;
            
            double y = (double)rnd.Next((int)intervalMin, (int)intervalMax) + rnd.NextDouble();
            double[] arrayY = new double[Input.YCount + 1];

            for(int i = 0; i < Input.YCount + 1; ++i)
            {
                if (i % 2 == 0)
                {
                    arrayY[i] = Math.Round(y - deltaY * rnd.NextDouble(), 3);
                }
                else
                {
                    arrayY[i] = Math.Round(y + deltaY * rnd.NextDouble(), 3);
                }

            }
            arrayY[Input.YCount] = Math.Round(arrayY.Average(), 3);

            return arrayY;
        }

        public Status Process()
        {
                      /*var params_pairs = Input.ZeroAndInterval;
                      var num_of_pairs = params_pairs.GetLength(0);
                      var pairs_length = params_pairs.GetLength(1);
                      var y_info_index = num_of_pairs - 1;
                      var y_info = params_pairs
                          .Cast<double>()
                          .Skip(y_info_index * pairs_length)
                          .Take(pairs_length)
                          .ToArray();
                      var y_mean = y_info[0];
                      var y_std_dev = y_info[1];
                      var normal_distrib_generator = new NormalDistribution(y_mean, y_std_dev);*/
            Random rnd = new Random();
            double yMin = Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 0] - Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 1] / 2;
            double yMax = Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 0] + Input.ZeroAndInterval[Input.ZeroAndInterval.GetLength(0) - 1, 1] / 2;

            tableObjects = new TableObject[Input.ResearchCount];
            for (int i = 0; i < Input.ResearchCount; i++)
            {
                 /*var sample = normal_distrib_generator.Generate(Input.YCount).ToList();
                 sample.Add(sample.Average());*/

                tableObjects[i] = new TableObject
                {
                    Number = i + 1,
                    Y = randomchik(yMin, yMax, rnd)/*sample.Select(s => Math.Round(s, 3)).ToArray()*/
                };
            };

            //Mock data
            /*double[] yRandom1 = new double[3];
            yRandom1[0] = 1.09;
            yRandom1[1] = 0.71;
            yRandom1[2] = 0.9;
            tableObjects[0].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 1.34;
            yRandom1[1] = 0.94;
            yRandom1[2] = 1.14;
            tableObjects[1].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 3.07;
            yRandom1[1] = 2.65;
            yRandom1[2] = 2.86;
            tableObjects[2].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 3.42;
            yRandom1[1] = 3.02;
            yRandom1[2] = 3.22;
            tableObjects[3].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 2.9;
            yRandom1[1] = 2.5;
            yRandom1[2] = 2.7;
            tableObjects[4].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 3.01;
            yRandom1[1] = 2.59;
            yRandom1[2] = 2.8;
            tableObjects[5].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 3.74;
            yRandom1[1] = 3.34;
            yRandom1[2] = 3.54;
            tableObjects[6].Y = yRandom1;
            yRandom1 = new double[3];
            yRandom1[0] = 6.64;
            yRandom1[1] = 6.26;
            yRandom1[2] = 6.45;
            tableObjects[7].Y = yRandom1;*/

            return GenerateStatus(true);
    }

    private Status GenerateStatus(bool isSuccess)
    {
        Status status = new Status();
        status.isSuccsess = isSuccess;
        status.messages = new string[] { "Element 1", "ok" };
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
