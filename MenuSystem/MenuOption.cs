using System;

namespace MenuSystem
{
    public class MenuOption : IMenuOption
    {
        public IMenuText Header { get; set; }
        public Action Action { get; set; }

        public MenuOption(IMenuText header, Action action)
        {
            Header = header;
            Action = action;
        }
    }
}
