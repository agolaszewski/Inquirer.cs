using System;
using System.Reflection;

namespace ConsoleWizard
{
    public class AppAction
    {
        public PropertyInfo PropertyInfo { get; set; }
        public Action<PropertyInfo> Action { get; set; }
    }
}