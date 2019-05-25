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
            counterCheck = new bool[7];

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
        }

        public void PrevPage()
        {
            if(counter > 0)
                counter--;
        }

        public void StartAgain()
        {
            counter = 0;
            vc.SetView(new ViewStartInput());
        }
    }
}
