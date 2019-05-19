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

        private int counter = -1;

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

            vc = new ViewContext(new ViewStartInput(), this, manageService);
            //vc = new ViewContext(new ViewTextInfo(new string[] { "" }, "Test"), this);
        }

        public void AfterInput()
        {
            vc.SetView(new ViewTableParams());
        }

        public void ChangePage()
        {
            if (counter == -1)
                vc.SetView(new ViewTableInput());
            else
                vc.SetView(new ViewTextInfo(manageService.GetStringResultFromStep(counter), manageService.GetNameFromStep(counter)));
        }

        public void NextPage()
        {
            if(counter < 5)
                counter++;
        }

        public void PrevPage()
        {
            if(counter > -1)
                counter--;
        }
    }
}
