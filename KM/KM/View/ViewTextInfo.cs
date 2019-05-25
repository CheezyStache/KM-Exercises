using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using KM.Services;

namespace KM.View
{
    class ViewTextInfo: IViewStrategy
    {
        private RichTextBox[] richTextBox;
        private Button home;
        private Button prev;
        private Button next;
        private Label operationName;
        private Panel mainPanel;

        private string _operationName;
        private string[] _info;
        private int currentStepIndex;

        private ManageService _manageService;
        private Form1 _form;

        public ViewTextInfo(int currentStepIndex, string operation)
        {
            this.currentStepIndex = currentStepIndex;
            _operationName = operation;
        }

        public void MakeView(Form form, List<IDisposable> allFormElements, ManageService manageService)
        {
            _manageService = manageService;
            _manageService.ProcessNext();
            _info = _manageService.GetStringResultFromStep(currentStepIndex);

            _form = form as Form1;

            mainPanel = new Panel
            {
                Width = form.Width,
                Height = form.Height,
                BackColor = Color.FromArgb(90, 87, 102) //dark-grey
            };
            form.Controls.Add(mainPanel);
            allFormElements.Add(mainPanel);

            operationName = new Label
            {
                Text = _operationName,
                BackColor = Color.FromArgb(97, 231, 134), //green
                Location = new Point(form.Width / 20, form.Height / 20),
                Size = new Size(form.Width / 20 * 18, form.Height / 10),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(operationName);
            allFormElements.Add(operationName);

            home = new Button
            {
                Text = "Начать заново",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 20, form.Height / 5),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            home.FlatAppearance.BorderSize = 0;
            home.Click += Home_Click;
            mainPanel.Controls.Add(home);
            allFormElements.Add(home);

            prev = new Button
            {
                Text = "Предыдущая страница",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 20 + 3 * operationName.Width / 8, form.Height / 5),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            prev.FlatAppearance.BorderSize = 0;
            prev.Click += Prev_Click;
            mainPanel.Controls.Add(prev);
            allFormElements.Add(prev);

            next = new Button
            {
                Text = "Следующая страница",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 20 + 3 * operationName.Width / 4, form.Height / 5),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            next.FlatAppearance.BorderSize = 0;
            next.Click += Next_Click;
            mainPanel.Controls.Add(next);
            allFormElements.Add(next);

            richTextBox = new RichTextBox[_info.Length];

            for (int i = 0; i < _info.Length; i++)
            {
                int offset = 0;
                if (i != 0)
                    offset = form.Height / 40;

                int shrink = 0;
                if (_info.Length != 1)
                    shrink = form.Height / 40;

                if (i != 0 && i != _info.Length - 1)
                    shrink = form.Height / 20;

                richTextBox[i] = new RichTextBox
                {
                    BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                    Location = new Point(form.Width / 20 + i * operationName.Width / _info.Length + offset, 7 * form.Height / 20),
                    Size = new Size(operationName.Width / _info.Length - shrink, 11 * form.Height / 20),
                    BorderStyle = BorderStyle.None,
                    ForeColor = Color.FromArgb(72, 67, 92),
                    Font = new Font("Times New Roman", 14, FontStyle.Regular),
                    ReadOnly = true,
                    Text = _info[i]
                };
                mainPanel.Controls.Add(richTextBox[i]);
                allFormElements.Add(richTextBox[i]);
            }

            _manageService.ChangeButtons(next, prev);
        }

        private void Home_Click(object sender, EventArgs e)
        {
            _form.StartAgain();
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            _form.PrevPage();
            _form.ChangePage();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            _form.NextPage();
            _form.ChangePage();
        }
    }
}
