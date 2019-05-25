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

        public Status Process()
        {
            Random rand = new Random();
            tableObjects = new TableObject[Input.ResearchCount];

            var params_pairs = Input.ZeroAndInterval;
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
            var normal_distrib_generator = new NormalDistribution(y_mean, y_std_dev);

            for (int i = 0; i < Input.ResearchCount; i++)
            {
                tableObjects[i] = new TableObject
                {
                    Number = i + 1,
                    Y = normal_distrib_generator.Generate(Input.YCount)
                };
            };

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
