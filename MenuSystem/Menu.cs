using System;

namespace MenuSystem
{
    public class Menu : IMenu
    {
        public string InputErrorMessage { get; set; }
        public IMenuText MenuHeader { get; set; }
        public IMenuOption[] MenusOptions { get; private set; }

        private const string ErrorMessageSeperator = "\n-----------------------------------(Error)------------------------------------\n";
        private string GetDefaultErrorMessage() => (
            ErrorMessageSeperator +
            $"\n\t\tPlease input a integer between 1 - {MenusOptions.Length}\n" +
            ErrorMessageSeperator
        );

        public Menu(IMenuText menuHeader, IMenuOption[] menusOptions, string inputErrorMessage = null)
        {
            MenuHeader = menuHeader;
            MenusOptions = menusOptions.Length > 9 ? throw new Exception("Array needs to be less then 9 options") : menusOptions;
            InputErrorMessage = string.IsNullOrWhiteSpace(inputErrorMessage) ? GetDefaultErrorMessage() : inputErrorMessage;
        }

        public void SetConsoleTitle(string consoleTitle) => Console.Title = consoleTitle;

        public void OnInputError(bool reInvokeMenu = true)
        {
            if (reInvokeMenu)
            {
                new MenuText(InputErrorMessage, ConsoleColor.Red).Display();
                Invoke();
            }
            else
            {
                new MenuText(InputErrorMessage, ConsoleColor.Red).Display();
            }
        }

        public void Invoke()
        {
            MenuHeader.Display();
            DisplayMenuOptions();
            HandleUserInput();
        }

        private void DisplayMenuOptions()
        {
            for (int i = 0; i < MenusOptions.Length; i++)
            {
                IMenuOption menuOption = MenusOptions[i];
                menuOption.Header.Display(i + 1);
            }
        }

        private void HandleUserInput()
        {
            Console.WriteLine();
            int offsetMenuOptionNumber = 1;

            ConsoleKey key = Console.ReadKey(true).Key;
            int? input = null;

            switch (key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    input = 1 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    input = 2 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    input = 3 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    input = 4 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    input = 5 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    input = 6 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    input = 7 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad8:
                case ConsoleKey.D8:
                    input = 8 - offsetMenuOptionNumber;
                    break;

                case ConsoleKey.NumPad9:
                case ConsoleKey.D9:
                    input = 9 - offsetMenuOptionNumber;
                    break;
            }

            if (input.HasValue)
            {
                if (input.Value >= MenusOptions.Length)
                {
                    OnInputError();
                }
                else if (input.Value < 0)
                {
                    OnInputError();
                }
                else
                {
                    MenusOptions[input.Value].Action();
                }
            }
            else
            {
                OnInputError();
            }
        }
    }
}
