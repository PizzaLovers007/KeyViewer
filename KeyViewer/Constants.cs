using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyViewer
{
    public static class Constants
    {
        public static readonly Brush DefaultPressedFill = Brushes.White;
        public static readonly Brush DefaultReleasedFill = (Brush)new BrushConverter().ConvertFrom("#80808080");
        public static readonly Brush DefaultPressedOutline = Brushes.White;
        public static readonly Brush DefaultReleasedOutline = Brushes.White;
        public static readonly Brush DefaultPressedFontForeground = Brushes.Black;
        public static readonly Brush DefaultReleasedFontForeground = Brushes.White;
		public static readonly Dictionary<Key, string> KEY_TO_STRING = new Dictionary<Key, string>() {
			{ Key.D0, "0" },
			{ Key.D1, "1" },
			{ Key.D2, "2" },
			{ Key.D3, "3" },
			{ Key.D4, "4" },
			{ Key.D5, "5" },
			{ Key.D6, "6" },
			{ Key.D7, "7" },
			{ Key.D8, "8" },
			{ Key.D9, "9" },
			{ Key.NumPad0, "0" },
			{ Key.NumPad1, "1" },
			{ Key.NumPad2, "2" },
			{ Key.NumPad3, "3" },
			{ Key.NumPad4, "4" },
			{ Key.NumPad5, "5" },
			{ Key.NumPad6, "6" },
			{ Key.NumPad7, "7" },
			{ Key.NumPad8, "8" },
			{ Key.NumPad9, "9" },
			{ Key.Add, "+" },
			{ Key.Subtract, "-" },
			{ Key.Multiply, "*" },
			{ Key.Divide, "/" },
			{ Key.Return, "↵" },
			{ Key.Decimal, "." },
			{ Key.None, " " },
			{ Key.OemBackslash, "\\" },
			{ Key.OemMinus, "-" },
			{ Key.OemPlus, "=" },
			{ Key.OemOpenBrackets, "[" },
			{ Key.OemCloseBrackets, "]" },
			{ Key.OemSemicolon, ";" },
			{ Key.OemQuestion, "/" },
			{ Key.OemComma, "," },
			{ Key.OemPeriod, "." },
			{ Key.OemQuotes, "'" },
			{ Key.Up, "↑" },
			{ Key.Down, "↓" },
			{ Key.Left, "←" },
			{ Key.Right, "→" },
		};
    }
}
