using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KM.Model;
using KM.Services;

namespace KM
{
    class Math2 : IMathStrategy
    {
        private ManageService manageService;

        public Math2(ManageService manageService)
        {
            this.manageService = manageService;
        }

        public object[] GetResult()
        {
            throw new NotImplementedException();
        }

        public Status Process()
        {
            Status status = new Status();
            status.isSuccsess = true;
            status.messages = new string[] { "Element 2", "ok" };
            return status;
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
