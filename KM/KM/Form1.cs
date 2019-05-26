using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KM.View;
using KM.Services;

namespace KM
{
    public partial class Form1 : Form
    {
        private const int height = 720;
        private const int width = 1280;

        private int counter = 0;

        private bool[] counterCheck;

        private ViewContext vc;
        private ManageService manageService;

        public Form1()
        {
            InitializeComponent();
            FormSettings();
        }

        private void FormSettings()
        {
            Size = new Size(width, height);
            manageService = new ManageService();
            counterCheck = new bool[6];

            vc = new ViewContext(new ViewStartInput(), this, manageService);
        }

        public void AfterInput()
        {
            vc.SetView(new ViewTableParams());
        }

        public void ChangePage()
        {
            switch (counter)
            {
                case 0:
                    vc.SetView(new ViewGenerationChoose());
                    break;
                case 1:
                    vc.SetView(new ViewTableInput(counter));
                    break;
                default:
                    vc.SetView(new ViewTextInfo(counter, manageService.GetNameFromStep(counter)));
                    break;
            }
        }

        public void NextPage()
        {
            if(counter < 6)
                counter++;

            if (!counterCheck[counter])
            {
                counterCheck[counter] = true;
                manageService.ProcessNext();
            }
            else
                manageService.EnableButton(counter - 1);
        }

        public void PrevPage()
        {
            if(counter > 0)
                counter--;

            manageService.EnableButton(counter - 1);
        }

        public void StartAgain()
        {
            counter = 0;
            counterCheck = new bool[6];
            Input.ResearchCount = 0;
            Input.XCount = 0;
            Input.YCount = 0;
            Input.GenerationRatio = 0;
            Input.ZeroAndInterval = null;
            Input.GenerationX = null;
            vc.SetView(new ViewStartInput());
        }
    }
}
