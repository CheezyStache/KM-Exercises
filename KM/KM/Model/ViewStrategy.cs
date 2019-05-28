using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KM.Services;

namespace KM
{
    interface IViewStrategy
    {
        void MakeView(Form form, List<IDisposable> allFormElements, ManageService manageService);
    }

    class ViewContext
    {
        private IViewStrategy _strategy;
        private List<IDisposable> allFormElements;
        private Form _form;
        private ManageService _manageService;

        public ViewContext(IViewStrategy strategy, Form form, ManageService manageService)
        {
            _strategy = strategy;
            _form = form;
            _manageService = manageService;
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
            _strategy.MakeView(form, allFormElements, _manageService);
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
