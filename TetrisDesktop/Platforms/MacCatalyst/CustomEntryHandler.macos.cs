using Microsoft.Maui.Handlers;
using UIKit;

namespace TetrisDesktop.Platforms.MacCatalyst
{
    public class CustomEntryHandler : EntryHandler
    {
        protected override UITextField CreatePlatformView()
        {
            var nativeEntry = (UITextField)base.CreatePlatformView();

            nativeEntry.KeyboardType = UIKeyboardType.Default;
            nativeEntry.ReturnKeyType = UIReturnKeyType.Done;
            nativeEntry.AutocorrectionType = UITextAutocorrectionType.No;
            nativeEntry.SpellCheckingType = UITextSpellCheckingType.No;
            nativeEntry.AutocapitalizationType = UITextAutocapitalizationType.None;

            nativeEntry.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var virtualView = VirtualView as CustomEntry;
                var keyArgs = new KeyEventArgs(GetKeyFromString(replacementString));
                return !(virtualView?.OnKeyPressed(keyArgs) ?? false);
            };

            return nativeEntry;
        }

        private static Keys GetKeyFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return Keys.Other;

            switch (str)
            {
                case "\u001b[D": return Keys.LeftArrow;
                case "\u001b[C": return Keys.RightArrow;
                case "\u001b[A": return Keys.UpArrow;
                case "\u001b[B": return Keys.DownArrow;
                default: return Keys.Other;
            }
        }
    }
}