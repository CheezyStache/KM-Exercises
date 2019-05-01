using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KM
{
    class ViewTextInfo: IViewStrategy
    {
        private RichTextBox richTextBox;
        private Button home;
        private Button prev;
        private Button next;
        private Label operationName;
        private Panel mainPanel;

        private string _operationName;
        private string _info;

        public ViewTextInfo(string info, string operation)
        {
            _info = info;
            _operationName = operation;
        }

        public void MakeView(Form form, List<IDisposable> allFormElements)
        {
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
            mainPanel.Controls.Add(next);
            allFormElements.Add(next);

            richTextBox = new RichTextBox
            {
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                Location = new Point(form.Width / 20, 7 * form.Height / 20),
                Size = new Size(operationName.Width, 11 * form.Height / 20),
                BorderStyle = BorderStyle.None,
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 14, FontStyle.Regular)
            };
            mainPanel.Controls.Add(richTextBox);
            allFormElements.Add(richTextBox);
        }
    }
}
