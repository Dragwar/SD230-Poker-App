namespace MenuSystem
{
    public interface IMenu
    {
        string InputErrorMessage { get; set; }
        IMenuText MenuHeader { get; set; }
        IMenuOption[] MenusOptions { get; }

        void Invoke();
        void OnInputError(bool reInvokeMenu = true);
        void SetConsoleTitle(string consoleTitle);
    }
}