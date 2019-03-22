using System;

namespace MenuSystem
{
    public interface IMenuOption
    {
        Action Action { get; set; }
        IMenuText Header { get; set; }
    }
}