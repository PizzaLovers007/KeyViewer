using System;
using System.Windows;
using System.Windows.Input;

namespace KeyViewer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel viewModel;

        public SettingsWindow() {
            InitializeComponent();

            viewModel = new SettingsViewModel();
            DataContext = viewModel;
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.Delete || e.Key == Key.Back) {
                viewModel.RemoveKey();
            }
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e) {
            GetKeyPressWindow dialog = new GetKeyPressWindow();
            dialog.Owner = this;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.ShowDialog();
            if (dialog.Key != Key.None) {
                viewModel.AddKey(dialog.Key);
            }
        }
    }
}
