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
        private bool next;
        private bool prev;

        public ManageService(/*Button nextButton, Button goBackButon*/)
        {
            /*this.nextButton = nextButton;
            this.goBackButon = goBackButon;*/
            mathStrategies = new IMathStrategy[] { new Math1(), new Math2(this), new Math3(this), new Math4(this), new Math5(this), new Math6(this) };
        }

        public object[] GetResultFromStep(int stepIndex)
        {
            return mathStrategies[stepIndex].GetResult();
        }

        public string[] GetStringResultFromStep(int stepIndex)
        {
            return mathStrategies[stepIndex].GetStringResult();
        }

        public string GetNameFromStep(int stepIndex)
        {
            return mathStrategies[stepIndex].GetName();
        }

        public string[] ProcessNext()
        {
            if (currentStepIndex < 6)
            {
                next = true;
                prev = true;
                Status processStatus = mathStrategies[currentStepIndex].Process();

                if (processStatus.isSuccsess)
                {
                    ++currentStepIndex;
                    next = true;
                    if (goBackButon != null)
                        goBackButon.Enabled = true;
                }
                else
                {
                    next = false;
                }

                return processStatus.messages;
            }
            return null;
        }

        public string[] StartFromTheBeggining()
        {
            if(goBackButon != null)
                goBackButon.Enabled = false;
            currentStepIndex = 0;
            return ProcessNext();
        }

        public void ChangeButtons(Button nextB, Button prevB = null)
        {
            nextButton = nextB;
            goBackButon = prevB;

            nextButton.Enabled = next;
            if(prevB != null)
                goBackButon.Enabled = prev;
        }
    }
}
