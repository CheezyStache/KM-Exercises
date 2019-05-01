﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM
{
    interface IMathStrategy
    {
        string ShowInfo();
    }

    class MathContext
    {
        private IMathStrategy _strategy;

        public MathContext(IMathStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IMathStrategy strategy)
        {
            _strategy = strategy;
        }

        public string ShowInfo()
        {
            return _strategy.ShowInfo();
        }
    }
}