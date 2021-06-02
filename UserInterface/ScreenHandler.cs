using System;

namespace UserInterface
{
    public class ScreenHandler : IScreenHandler
    {
        private Screen _screen = null;
        public Screen Screen { get => _screen; set => _screen = value; }
        private ConsoleHelper _consoleHelper;
        public ConsoleHelper ConsoleHelper { get => _consoleHelper; set => _consoleHelper = value; }
        public void TransitionTo(Screen screen)
        {
            Console.Clear();
            _screen = screen;
            _screen.SetScreen(this);
            DisplayScreen();
        }
        public void DisplayScreen()
        {
            _screen.DrawScreen();
        }
    }
}