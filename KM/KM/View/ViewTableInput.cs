using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KM.View
{
    class ViewTableInput : IViewStrategy
    {
        private DataGridView mainTable;
        private BindingSource bindingSource;

        private TableObject[] tableObjects;

        private Form1 _form;

        private Button next;
        private Label operationName;
        private Panel mainPanel;

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

            mainTable = new DataGridView
            {
                Size = new Size(9 * form.Width / 10, 3 * form.Height / 5),
                Location = new Point(form.Width / 20, form.Height / 6),
                BackColor = Color.FromArgb(237, 255, 236), //white-bit green
                ForeColor = Color.FromArgb(72, 67, 92)//,
                //Font = new Font("Times New Roman", 20, FontStyle.Regular)
            };
            mainTable.AutoSize = true;
            mainPanel.Controls.Add(mainTable);
            allFormElements.Add(mainTable);

            TableCreate();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TableCreate()
        {
            bindingSource = new BindingSource();

            mainTable.ColumnCount = Input.XCount + Input.YCount + 1;

            mainTable.Columns[0].Name = "N";
            for (int i = 1; i <= Input.XCount; i++)
                mainTable.Columns[i].Name = "x" + i;
            for (int i = 1; i <= Input.YCount; i++)
                mainTable.Columns[i + Input.XCount].Name = "y" + i;

            tableObjects = new TableObject[Input.ResearchCount];

            for (int i = 0; i < Input.ResearchCount; i++)
            {
                tableObjects[i] = new TableObject
                {
                    Number = i + 1,
                    X = new double[Input.XCount],
                    Y = new double[Input.YCount]
                };

                bindingSource.Add(tableObjects[i]);
            }

            mainTable.DataSource = bindingSource;
        }
    }
}
