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
            throw new NotImplementedException();
        }

        public Status Process()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string[] GetStringResult()
        {
            throw new NotImplementedException();
        }
    }
}
