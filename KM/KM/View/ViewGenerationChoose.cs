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
    class ViewGenerationChoose: IViewStrategy
    {
        private Form1 _form;

        private Button next;
        private Button home;
        private Label operationName;
        private Panel mainPanel;

        private Panel inputPanel;

        private Label[] labels;
        private CheckedListBox[] checkedListBoxes;
        private TextBox[] textBoxes;

        private ManageService _manageService;

        private const string _operationName = "Выбор генерирующих соотношений";

        public void MakeView(Form form, List<IDisposable> allFormElements, ManageService manageService)
        {
            _form = form as Form1;
            _manageService = manageService;

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
                Location = new Point(form.Width / 20 + 3 * operationName.Width / 4, 8 * form.Height / 10),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            next.FlatAppearance.BorderSize = 0;
            next.Click += Next_Click;
            mainPanel.Controls.Add(next);
            allFormElements.Add(next);

            home = new Button
            {
                Text = "Начать заново",
                Width = operationName.Width / 4,
                Height = operationName.Height,
                Location = new Point(form.Width / 20, 8 * form.Height / 10),
                BackColor = Color.FromArgb(72, 67, 92), //darker-gray
                Font = new Font("Times New Roman", 14, FontStyle.Regular),
                ForeColor = Color.FromArgb(237, 255, 236),
                FlatStyle = FlatStyle.Flat
            };
            home.FlatAppearance.BorderSize = 0;
            home.Click += Home_Click;
            mainPanel.Controls.Add(home);
            allFormElements.Add(home);

            inputPanel = new Panel
            {
                Size = new Size(9 * form.Width / 10, 3 * form.Height / 5),
                Location = new Point(form.Width / 20, form.Height / 6),
                BackColor = Color.FromArgb(90, 87, 102), //dark-grey
                AutoScroll = true
            };
            mainPanel.Controls.Add(inputPanel);
            allFormElements.Add(inputPanel);

            labels = new Label[Input.GenerationRatio];
            textBoxes = new TextBox[Input.GenerationRatio];
            checkedListBoxes = new CheckedListBox[Input.GenerationRatio];

            for(int i = 0; i < labels.Length; i++)
            {
                AddLabelList(out labels[i], out checkedListBoxes[i], out textBoxes[i], inputPanel, allFormElements, i, "x" + (i + 1 + Input.XCount), i);
            }
        }

        private void Home_Click(object sender, EventArgs e)
        {
            _form.StartAgain();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            Input.GenerationX = new bool[checkedListBoxes.Length][];
            for (int i = 0; i < checkedListBoxes.Length; i++)
            {
                Input.GenerationX[i] = new bool[checkedListBoxes[i].Items.Count + 1];
                for (int j = 0; j < checkedListBoxes[i].Items.Count; j++)
                {
                    if (checkedListBoxes[i].GetItemChecked(j))
                        Input.GenerationX[i][j] = true;
                }
            }

            _manageService.ChangeButtons(next);
            _manageService.ProcessNext();

            _form.NextPage();
           _form.ChangePage();
        }

        private void AddLabelList(out Label l, out CheckedListBox c, out TextBox t, Panel mainPanel, List<IDisposable> allFormElements, int i, string name, int current)
        {
            l = new Label
            {
                Text = name,
                BackColor = Color.FromArgb(97, 231, 134), //green
                Location = new Point(mainPanel.Width / 6 - 10, (i * 6 + 4) * mainPanel.Height / 18),
                Size = new Size(mainPanel.Width / 5, mainPanel.Height / 6),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(l);
            allFormElements.Add(l);

            c = new CheckedListBox
            {
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                Location = new Point(2 * mainPanel.Width / 5 - 10, (i * 2 + 1) * mainPanel.Height / 6),
                Size = new Size(mainPanel.Width / 8, mainPanel.Height / 3),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                MultiColumn = false
            };
            c.ItemCheck += C_ItemCheck;
            mainPanel.Controls.Add(c);
            allFormElements.Add(c);

            if (Input.GenerationX != null)
            {
                for (int j = 0; j < Input.XCount + 1; j++)
                {
                    if (j != Input.XCount)
                    {
                        if (Input.GenerationX[i][j])
                            c.Items.Add("x" + (j + 1), true);
                        else
                            c.Items.Add("x" + (j + 1), false);
                    }
                    else
                    {
                        if (Input.GenerationX[i][j])
                            c.Items.Add("-", true);
                        else
                            c.Items.Add("-", false);
                    }

                }
            }
            else
            {
                for (int j = 0; j < Input.XCount + 1; j++)
                {
                    if (j != Input.XCount)
                    {
                        c.Items.Add("x" + (j + 1));
                    }
                    else
                    {
                        c.Items.Add("-");
                    }
                }
            }

            t = new TextBox
            {
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                Location = new Point(3 * mainPanel.Width / 5 - 10, (i * 6 + 4) * mainPanel.Height / 18),
                Size = new Size(mainPanel.Width / 4, mainPanel.Height / 3),
                ForeColor = Color.FromArgb(72, 67, 92),
                Font = new Font("Times New Roman", 20, FontStyle.Regular),
                Text = "x" + (i + 1 + Input.XCount)
            };
            mainPanel.Controls.Add(t);
            allFormElements.Add(t);
        }

        private void C_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedListBox = sender as CheckedListBox;
            int index = Array.IndexOf(checkedListBoxes, checkedListBox);

            textBoxes[index].Text = "x" + (index + 1 + Input.XCount) + " = ";

            for (int i = 0; i < checkedListBoxes[index].Items.Count; i++)
            {
                if (e.NewValue == CheckState.Unchecked && i == e.Index)
                    continue;

                if(checkedListBoxes[index].GetItemChecked(i))
                {
                    if(i != checkedListBoxes[index].Items.Count - 1)
                        textBoxes[index].Text += "x" + (i + 1);
                    else
                        textBoxes[index].Text = textBoxes[index].Text.Replace("= ", "=  -");
                    continue;
                }

                if (e.NewValue == CheckState.Checked && i == e.Index)
                {
                    if (i != checkedListBoxes[index].Items.Count - 1)
                        textBoxes[index].Text += "x" + (i + 1);
                    else
                        textBoxes[index].Text = textBoxes[index].Text.Replace("= ", "=  -");
                }
            }
        }
    }
}
