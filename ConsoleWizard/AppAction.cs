using System;
using System.Reflection;

namespace ConsoleWizard
{
    public class AppAction
    {
        public Action<PropertyInfo> Action { get; set; }

        public PropertyInfo PropertyInfo { get; set; }
    }
}
