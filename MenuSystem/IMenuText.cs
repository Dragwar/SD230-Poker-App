using System;
using System.Runtime.InteropServices;

namespace MenuSystem
{
    public interface IMenuText
    {
        bool IsConsoleWriteLine { get; set; }
        ConsoleColor TextColor { get; set; }
        string Title { get; set; }

        void Display([Optional] int? optionNumber);
    }
}