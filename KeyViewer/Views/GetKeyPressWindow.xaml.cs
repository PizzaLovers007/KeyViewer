using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KeyViewer.Utils;

namespace KeyViewer
{
    /// <summary>
    /// Interaction logic for GetKeyPressWindow.xaml
    /// </summary>
    public partial class GetKeyPressWindow : Window
    {
        public Key Key { get; set; }

        public GetKeyPressWindow() {
            InitializeComponent();

            LowLevelKeyboardListener.Instance.KeyPressed += KeyPressed;

            Key = Key.None;
        }

        private async void KeyPressed(object sender, KeyboardKeyArgs e) {
            Key = e.Key;
            await Task.Delay(1); // Wait 1 ms so the enter key does not press the add button again
            Close();
        }

        protected override void OnClosed(EventArgs e) {
            LowLevelKeyboardListener.Instance.KeyPressed -= KeyPressed;

            base.OnClosed(e);
        }
    }
}
