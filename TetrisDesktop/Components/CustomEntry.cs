namespace TetrisDesktop
{
    public class CustomEntry : Entry
    {
        public event Func<KeyEventArgs, bool> KeyPressed;

        public bool OnKeyPressed(KeyEventArgs e)
        {
            if (KeyPressed != null)
            {
                return KeyPressed(e);
            }
            return false;
        }

    }

    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; }
    }

    public enum Keys
    {
        LeftArrow,
        RightArrow,
        UpArrow,
        DownArrow,
        Other
    }
}