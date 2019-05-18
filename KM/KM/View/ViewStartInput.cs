using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KM.View
{
    class ViewStartInput : IViewStrategy
    {
        private Form1 _form;

        private Button next;
        private Label operationName;
        private Panel mainPanel;

        private Label researchLabel;
        private Label xLabel;
        private Label yLabel;
        private Label generationLabel;

        private TextBox researchTextBox;
        private TextBox xTextBox;
        private TextBox yTextBox;
        private TextBox generationTextBox;

        private const string _operationName = "Ввод входных данных";

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

            AddLabelText(out researchLabel, out researchTextBox, mainPanel, allFormElements, 1, "Количество экспериментов");
            AddLabelText(out xLabel, out xTextBox, mainPanel, allFormElements, 2, "Количество факторов");
            AddLabelText(out yLabel, out yTextBox, mainPanel, allFormElements, 3, "Количество результатов опытов");
            AddLabelText(out generationLabel, out generationTextBox, mainPanel, allFormElements, 4, "Количество линейных переменных");
        }

        private void Next_Click(object sender, EventArgs e)
        {
            Input.ResearchCount = Convert.ToInt32(researchTextBox.Text);
            Input.XCount = Convert.ToInt32(xTextBox.Text);
            Input.YCount = Convert.ToInt32(yTextBox.Text);
            Input.GenerationRatio = Convert.ToInt32(generationTextBox.Text);

            _form.AfterInput();
        }

        private void AddLabelText(out Label l, out TextBox t, Panel mainPanel, List<IDisposable> allFormElements, int i, string name)
        {
            l = new Label
            {
                Text = name,
                BackColor = Color.FromArgb(97, 231, 134), //green
                Location = new Point(_form.Width / 4 - 10, (3 * i + 1) * _form.Height / 20),
                Size = new Size(operationName.Width / 4, _form.Height / 10),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(l);
            allFormElements.Add(l);

            t = new TextBox
            {
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                Location = new Point(_form.Width / 2 + 10, (i * 6 + 3) * _form.Height / 40),
                Size = new Size(operationName.Width / 4, _form.Height / 10),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = HorizontalAlignment.Center
            };
            mainPanel.Controls.Add(t);
            allFormElements.Add(t);
        }
    }
}
