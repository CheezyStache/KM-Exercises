using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KM.View
{
    class ViewTableParams : IViewStrategy
    {
        private Form1 _form;

        private Button next;
        private Label operationName;
        private Panel mainPanel;

        private Panel inputPanel;

        private Label[,] labels;
        private TextBox[,] textBoxes;

        private const string _operationName = "Таблица результатов";

        public void MakeView(Form form, List<IDisposable> allFormElements)
        {
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

            next = new Button
            {
                Text = "Следующая страница",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 2 - operationName.Width / 8, 8 * form.Height / 10),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            next.FlatAppearance.BorderSize = 0;
            next.Click += Next_Click;
            mainPanel.Controls.Add(next);
            allFormElements.Add(next);

            inputPanel = new Panel
            {
                Size = new Size(9 * form.Width / 10, 3 * form.Height / 5),
                Location = new Point(form.Width / 20, form.Height / 6),
                BackColor = Color.FromArgb(90, 87, 102), //dark-grey
                AutoScroll = true
            };
            mainPanel.Controls.Add(inputPanel);
            allFormElements.Add(inputPanel);

            labels = new Label[Input.XCount + 1, 2];
            textBoxes = new TextBox[Input.XCount + 1, 2];

            for(int i = 0; i < labels.GetLength(0); i++)
            {
                if (i != labels.GetLength(0) - 1)
                {
                    AddLabelText(out labels[i, 0], out textBoxes[i, 0], inputPanel, allFormElements, i, "Нулевой уровень: х" + (i + 1), 0);
                    AddLabelText(out labels[i, 1], out textBoxes[i, 1], inputPanel, allFormElements, i, "Интервал варьирования: х" + (i + 1), 1);
                }
                else
                {
                    AddLabelText(out labels[i, 0], out textBoxes[i, 0], inputPanel, allFormElements, i, "Нулевой уровень: y", 0);
                    AddLabelText(out labels[i, 1], out textBoxes[i, 1], inputPanel, allFormElements, i, "Интервал варьирования: y", 1);
                }
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            double[,] temp = new double[labels.GetLength(0), 2];

            for(int i = 0; i < labels.GetLength(0); i++)
            {
                temp[i, 0] = Convert.ToInt32(textBoxes[i, 0].Text);
                temp[i, 1] = Convert.ToInt32(textBoxes[i, 1].Text);
            }

            Input.ZeroAndInterval = temp;

            _form.ChangePage();
        }

        private void AddLabelText(out Label l, out TextBox t, Panel mainPanel, List<IDisposable> allFormElements, int i, string name, int j)
        {
            l = new Label
            {
                Text = name,
                BackColor = Color.FromArgb(97, 231, 134), //green
                Location = new Point((j * 8 + 1) * inputPanel.Width / 16 - 10, (3 * i + 1) * _form.Height / 20),
                Size = new Size(inputPanel.Width / 5, inputPanel.Height / 6),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(l);
            allFormElements.Add(l);

            t = new TextBox
            {
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                Location = new Point((j * 8 + 4) * inputPanel.Width / 16 + 20, (i * 6 + 3) * _form.Height / 40),
                Size = new Size(inputPanel.Width / 5, inputPanel.Height / 6),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = HorizontalAlignment.Center
            };
            mainPanel.Controls.Add(t);
            allFormElements.Add(t);
        }
    }
}
