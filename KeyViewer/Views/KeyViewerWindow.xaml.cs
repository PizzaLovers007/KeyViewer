using System;
using System.Windows;
using System.Windows.Input;

namespace KeyViewer
{
    /// <summary>
    /// Interaction logic for KeyViewerWindow.xaml
    /// </summary>
    public partial class KeyViewerWindow : Window
    {
        public KeyModelCollection KeyModels { get; set; }

        private SettingsWindow settingsWindow;

        public KeyViewerWindow() {
            InitializeComponent();

            DataContext = this;

            KeyModels = KeyModelCollection.Instance;

            Left = Config.WindowX;
            Top = Config.WindowY;

            settingsWindow = new SettingsWindow();

            Activated += CreateSettingsWindow;
        }

        private void CreateSettingsWindow(object sender, EventArgs e) {
            Activated -= CreateSettingsWindow;

            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            settingsWindow.Show();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            DragMove();
        }

        protected override void OnLocationChanged(EventArgs e) {
            base.OnLocationChanged(e);

            Config.WindowX = Left;
            Config.WindowY = Top;
        }
    }
}
