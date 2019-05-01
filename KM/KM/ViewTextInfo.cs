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
                BackColor = Color.FromArgb(75, 95, 70) //lime-yellow
            };
            form.Controls.Add(mainPanel);
            allFormElements.Add(mainPanel);

            operationName = new Label
            {
                Text = _operationName,
                BackColor = Color.FromArgb(35, 90, 40), //brown-yellow
                ForeColor = Color.LightGray,
                Location = new Point(form.Width / 10, form.Height / 10),
                Size = new Size(form.Width / 10 * 8, form.Height / 10)
            };
            mainPanel.Controls.Add(operationName);
            allFormElements.Add(operationName);

            home = new Button
            {
                Text = "Начать заново",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 10, form.Height / 4),
                BackColor = Color.FromArgb(55, 100, 80), //yellow
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(home);
            allFormElements.Add(home);

            prev = new Button
            {
                Text = "Предыдущая страница",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 10 + 3 * operationName.Width / 8, form.Height / 4),
                BackColor = Color.FromArgb(55, 100, 80), //yellow
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(prev);
            allFormElements.Add(prev);

            next = new Button
            {
                Text = "Следующая страница",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 10 + 3 * operationName.Width / 4, form.Height / 4),
                BackColor = Color.FromArgb(55, 100, 80), //yellow
                ForeColor = Color.Black
            };
            mainPanel.Controls.Add(next);
            allFormElements.Add(next);

            richTextBox = new RichTextBox
            {
                BackColor = Color.LightGray,
                Location = new Point(form.Width / 10, 2 * form.Height / 5),
                Size = new Size(operationName.Width, form.Height / 2)
            };
            mainPanel.Controls.Add(richTextBox);
            allFormElements.Add(richTextBox);
        }
    }
}
