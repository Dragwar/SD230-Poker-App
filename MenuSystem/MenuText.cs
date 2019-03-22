using System;
using System.Runtime.InteropServices;

namespace MenuSystem
{
    public struct MenuText : IMenuText
    {
        public string Title { get; set; }
        public ConsoleColor TextColor { get; set; }
        public bool IsConsoleWriteLine { get; set; }

        public MenuText(string title, ConsoleColor textColor = ConsoleColor.Gray, bool isConsoleWriteLine = true)
        {
            Title = title;
            TextColor = textColor;
            IsConsoleWriteLine = isConsoleWriteLine;

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Text can't be null or whitespace");
            }
        }

        public void Display([Optional] int? optionNumber)
        {
            Console.ForegroundColor = TextColor;

            string output = optionNumber.HasValue ? $"{optionNumber} - {Title}" : $"{Title}";

            if (IsConsoleWriteLine)
            {
                Console.WriteLine(output);
            }
            else
            {
                Console.Write(output);
            }
            Console.ResetColor();
        }
    }
}
