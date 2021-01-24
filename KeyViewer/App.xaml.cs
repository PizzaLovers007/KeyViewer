using KeyViewer.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeyViewer");
        private static readonly string settingsPath = Path.Combine(folderPath, "settings.txt");

        /// <summary>
        /// Perform some setup before the app starts
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LowLevelKeyboardListener.Instance.HookKeyboard();

            LoadData();
        }

        /// <summary>
        /// Perform some teardown after the app ends
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            LowLevelKeyboardListener.Instance.UnHookKeyboard();

            SaveData();

            base.OnExit(e);
        }

        private void LoadData()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    if (!Directory.Exists(folderPath) || !File.Exists(settingsPath))
                    {
                        return;
                    }
                    using (StreamReader sr = new StreamReader(settingsPath))
                    {
                        BrushConverter brushConverter = new BrushConverter();
                        int numKeys = int.Parse(sr.ReadLine().Trim());
                        for (int i = 0; i < numKeys; i++)
                        {
                            string line = sr.ReadLine();
                            string[] split = line.Split(',');
                            KeyModelCollection.Instance.Add(new KeyModel
                            {
                                Key = KeyInterop.KeyFromVirtualKey(int.Parse(split[0])),
                                PressedFill = (Brush)brushConverter.ConvertFrom(split[1]),
                                ReleasedFill = (Brush)brushConverter.ConvertFrom(split[2]),
                                PressedOutline = (Brush)brushConverter.ConvertFrom(split[3]),
                                ReleasedOutline = (Brush)brushConverter.ConvertFrom(split[4]),
                                PressedFontForeground = (Brush)brushConverter.ConvertFrom(split[5]),
                                ReleasedFontForeground = (Brush)brushConverter.ConvertFrom(split[6]),
                            });
                        }
                        double[] position = Array.ConvertAll(sr.ReadLine().Trim().Split(' '), double.Parse);
                        Config.WindowX = position[0];
                        Config.WindowY = position[1];
                    }
                });
            }
            catch (Exception ex)
            {
                KeyModelCollection.Instance.Clear();
                Config.WindowX = 50;
                Config.WindowY = 50;
                Console.Error.WriteLine(ex);
                Console.Error.WriteLine("Error loading file");
                File.Delete(settingsPath);
            }
        }

        private void SaveData()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    using (StreamWriter sw = new StreamWriter(settingsPath))
                    {
                        sw.WriteLine(KeyModelCollection.Instance.Count);
                        foreach (KeyModel model in KeyModelCollection.Instance)
                        {
                            sw.WriteLine(string.Format(
                                "{0},{1},{2},{3},{4},{5},{6}",
                                KeyInterop.VirtualKeyFromKey(model.Key),
                                model.PressedFill,
                                model.ReleasedFill,
                                model.PressedOutline,
                                model.ReleasedOutline,
                                model.PressedFontForeground,
                                model.ReleasedFontForeground));
                        }
                        sw.WriteLine("{0} {1}", Config.WindowX, Config.WindowY);
                    }
                });
            }
            catch (Exception ex)
            {
                KeyModelCollection.Instance.Clear();
                Console.Error.WriteLine(ex);
                Console.Error.WriteLine("Error saving file");
            }
        }
    }
}
