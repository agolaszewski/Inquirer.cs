﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class StringOrKeyInputComponent : IWaitForInputComponent<StringOrKey>
    {
        private ConsoleKey[] _intteruptedKeys;

        public StringOrKeyInputComponent(params ConsoleKey[] intteruptedKeys)
        {
            _intteruptedKeys = intteruptedKeys;
        }

        public StringOrKey WaitForInput()
        {
            ConsoleKey? intteruptedKey;
            string result = ConsoleHelper.Read(out intteruptedKey, _intteruptedKeys);
            return new StringOrKey(result, intteruptedKey);
        }
    }
}
