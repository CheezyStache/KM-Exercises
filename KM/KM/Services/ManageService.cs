using KM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KM.Services
{
    class ManageService : IManageService
    {
        private Button nextButton;
        private Button goBackButon;
        private IMathStrategy[] mathStrategies;
        private int currentStepIndex;

        public ManageService(Button nextButton, Button goBackButon)
        {
            this.nextButton = nextButton;
            this.goBackButon = goBackButon;
            mathStrategies = new IMathStrategy[] { new Math1(), new Math2(this), new Math3(this), new Math4(this), new Math5(this), new Math6(this) };
        }

        public object[] GetResultFromStep(int stepIndex)
        {
            return mathStrategies[stepIndex].GetResult();
        }

        public string[] ProcessNext()
        {
            Status processStatus = mathStrategies[currentStepIndex].Process();

           if (processStatus.isSuccsess)
            {
                ++currentStepIndex;
                nextButton.Enabled = true;
                goBackButon.Enabled = true;
            } else
            {
                nextButton.Enabled = false;
            }

            return processStatus.messages;                    
        }

        public string[] StartFromTheBeggining()
        {
            goBackButon.Enabled = false;
            currentStepIndex = 0;
            return ProcessNext();
        }
    }
}
