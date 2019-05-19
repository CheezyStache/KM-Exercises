using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KM
{
    interface IViewStrategy
    {
        void MakeView(Form form, List<IDisposable> allFormElements);
    }

    class ViewContext
    {
        private IViewStrategy _strategy;
        private List<IDisposable> allFormElements;
        private Form _form;

        public ViewContext(IViewStrategy strategy, Form form)
        {
            _strategy = strategy;
            _form = form;
            allFormElements = new List<IDisposable>();
            MakeView(_form, allFormElements);
        }

        public void SetView(IViewStrategy strategy)
        {
            DisposeAll();
            _strategy = strategy;
            MakeView(_form, allFormElements);
        }

        private void MakeView(Form form, List<IDisposable> allFormElements)
        {
            _strategy.MakeView(form, allFormElements);
        }

        private void DisposeAll()
        {
            foreach(var obj in allFormElements)
            {
                obj.Dispose();
            }

            allFormElements.Clear();
        }
    }
}
