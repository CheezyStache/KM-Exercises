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

namespace KM
{
    public partial class Form1 : Form
    {
        private const int height = 720;
        private const int width = 1280;

        private ViewContext vc;

        public Form1()
        {
            InitializeComponent();
            FormSettings();
        }

        private void FormSettings()
        {
            Size = new Size(width, height);

            vc = new ViewContext(new ViewStartInput(), this);
            //vc = new ViewContext(new ViewTextInfo(new string[] { "" }, "Test"), this);
        }

        public void AfterInput()
        {
            vc.SetView(new ViewTableInput());
        }
    }
}
