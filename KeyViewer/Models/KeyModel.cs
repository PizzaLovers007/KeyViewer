using System.Windows.Input;
using System.Windows.Media;

namespace KeyViewer
{
    public class KeyModel : BaseNotifyPropertyChanged
    {
        #region Properties

        private Key key;

        public Key Key {
            get { return key; }
            set {
                key = value;
                OnPropertyChanged();
            }
        }

        private bool isPressed;

        public bool IsPressed {
            get { return isPressed; }
            set {
                isPressed = value;
                OnPropertyChanged();
                OnPropertyChanged("Fill");
                OnPropertyChanged("Outline");
                OnPropertyChanged("FontForeground");
            }
        }

        private Brush pressedFill;

        public Brush PressedFill {
            get { return pressedFill; }
            set {
                pressedFill = value;
                OnPropertyChanged();
                OnPropertyChanged("Fill");
            }
        }

        private Brush releasedFill;

        public Brush ReleasedFill {
            get { return releasedFill; }
            set {
                releasedFill = value;
                OnPropertyChanged();
                OnPropertyChanged("Fill");
            }
        }

        private Brush pressedOutline;

        public Brush PressedOutline {
            get { return pressedOutline; }
            set {
                pressedOutline = value;
                OnPropertyChanged();
                OnPropertyChanged("Outline");
            }
        }

        private Brush releasedOutline;

        public Brush ReleasedOutline {
            get { return releasedOutline; }
            set {
                releasedOutline = value;
                OnPropertyChanged();
                OnPropertyChanged("Outline");
            }
        }

        private Brush pressedFontForeground;

        public Brush PressedFontForeground {
            get { return pressedFontForeground; }
            set {
                pressedFontForeground = value;
                OnPropertyChanged();
                OnPropertyChanged("FontForeground");
            }
        }

        private Brush releasedFontForeground;

        public Brush ReleasedFontForeground {
            get { return releasedFontForeground; }
            set {
                releasedFontForeground = value;
                OnPropertyChanged();
                OnPropertyChanged("FontForeground");
            }
        }

        public Brush Fill { get { return IsPressed ? PressedFill : ReleasedFill; } }
        public Brush Outline { get { return IsPressed ? PressedOutline : ReleasedOutline; } }
        public Brush FontForeground { get { return IsPressed ? PressedFontForeground : ReleasedFontForeground; } }

        public string KeyString {
            get {
                if (Constants.KEY_TO_STRING.ContainsKey(key)) {
                    return Constants.KEY_TO_STRING[key];
                }
                return key.ToString();
            }
        }

        #endregion Properties

        public KeyModel() {
            PressedFill = Constants.DefaultPressedFill;
            ReleasedFill = Constants.DefaultReleasedFill;
            PressedOutline = Constants.DefaultPressedOutline;
            ReleasedOutline = Constants.DefaultReleasedOutline;
            PressedFontForeground = Constants.DefaultPressedFontForeground;
            ReleasedFontForeground = Constants.DefaultReleasedFontForeground;
        }

        public override bool Equals(object obj) {
            KeyModel other = obj as KeyModel;
            return other?.Key == Key;
        }

        public override int GetHashCode() {
            return Key.GetHashCode();
        }
    }
}
