using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.Services
{
    interface IManageService
    {
        string[] ProcessNext();
        string[] StartFromTheBeggining();
        object[] GetResultFromStep(int stepIndex);
    }
}
