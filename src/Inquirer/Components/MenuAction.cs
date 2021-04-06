using System;

namespace InquirerCS.Components
{
    public class MenuAction
    {
        public MenuAction(string description, Action action)
        {
            Description = description;
            Action = action;
        }

        public Action Action { get; set; }

        public string Description { get; set; }
    }
}
