using System;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;

namespace KeyViewer
{
    public class SettingsViewModel : BaseNotifyPropertyChanged, IDropTarget
    {
        public KeyModelCollection KeyModels { get; set; }

        private int selectedIndex;

        public int SelectedIndex {
            get { return selectedIndex; }
            set {
                selectedIndex = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrModel");
                OnPropertyChanged("IsSelecting");
            }
        }

        private double _keySize;
        public double KeySize {
            get => _keySize;
            set {
                _keySize = value;
                OnPropertyChanged();
                OnPropertyChanged("KeySizeText");
                SizeChanged();
            }
        }

        public KeyModel CurrModel => SelectedIndex != -1 ? KeyModels[SelectedIndex] : null;
        public bool IsSelecting => selectedIndex != -1;
        public string KeySizeText => KeySize + "";

        public ICommand RemoveKeyCommand { get; set; }
        public ICommand SizeChangedCommand { get; set; }

        public SettingsViewModel() {
            KeyModels = KeyModelCollection.Instance;

            RemoveKeyCommand = new Command(RemoveKey);
            SizeChangedCommand = new Command(SizeChanged);

            SelectedIndex = -1;
            KeySize = Config.Instance.KeySize;
        }

        public void AddKey(Key key) {
            KeyModels.Add(key);
            OnPropertyChanged("KeyModels");
        }

        public void RemoveKey() {
            if (SelectedIndex != -1) {
                KeyModels.RemoveAt(SelectedIndex);
                SelectedIndex = -1;
                OnPropertyChanged("KeyModels");
            }
        }

        public void DragOver(IDropInfo dropInfo) {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            dropInfo.Effects = System.Windows.DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo) {
            KeyModels.Move(KeyModels.IndexOf((KeyModel)dropInfo.Data), Math.Max(dropInfo.InsertIndex, 0));
        }

        public void SizeChanged() {
            KeyModels.Size = KeySize;
        }
    }
}
