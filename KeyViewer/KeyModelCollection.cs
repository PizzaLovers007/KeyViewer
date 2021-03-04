using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using KeyViewer.Utils;

namespace KeyViewer
{
    public class KeyModelCollection : INotifyCollectionChanged, IEnumerable<KeyModel>
    {
        private static KeyModelCollection _instance;

        public static KeyModelCollection Instance {
            get {
                if (_instance == null) {
                    _instance = new KeyModelCollection();
                }
                return _instance;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private IDictionary<Key, KeyModel> KeyModels { get; set; }
        private IList<Key> Indices { get; set; }

        public KeyModel this[Key key] => KeyModels[key];
        public KeyModel this[int index] => KeyModels[Indices[index]];
        public int Count => KeyModels.Count;

        private KeyModelCollection() {
            KeyModels = new Dictionary<Key, KeyModel>();
            Indices = new List<Key>();
            LowLevelKeyboardListener.Instance.KeyPressed += KeyPressed;
            LowLevelKeyboardListener.Instance.KeyReleased += KeyReleased;
        }

        ~KeyModelCollection() {
            LowLevelKeyboardListener.Instance.KeyPressed -= KeyPressed;
            LowLevelKeyboardListener.Instance.KeyReleased -= KeyReleased;
        }

        private void KeyPressed(object sender, KeyboardKeyArgs e) {
            if (KeyModels.ContainsKey(e.Key)) {
                KeyModels[e.Key].IsPressed = true;
            }
        }

        private void KeyReleased(object sender, KeyboardKeyArgs e) {
            if (KeyModels.ContainsKey(e.Key)) {
                KeyModels[e.Key].IsPressed = false;
            }
        }

        public bool Add(KeyModel keyModel) {
            if (KeyModels.ContainsKey(keyModel.Key)) {
                return false;
            }
            KeyModels[keyModel.Key] = keyModel;
            Indices.Add(keyModel.Key);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, keyModel, Indices.Count - 1));
            return true;
        }

        public bool Add(Key key) {
            return Add(new KeyModel { Key = key });
        }

        public void Remove(Key key) {
            for (int i = 0; i < KeyModels.Count; i++) {
                if (Indices[i] == key) {
                    KeyModel removed = KeyModels[key];
                    KeyModels.Remove(key);
                    Indices.RemoveAt(i);
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, i));
                    break;
                }
            }
        }

        public void RemoveAt(int index) {
            KeyModel removed = KeyModels[Indices[index]];
            KeyModels.Remove(removed.Key);
            Indices.RemoveAt(index);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, index));
        }

        public void Move(int from, int to) {
            if (to > from) {
                to--;
            }
            KeyModel model = KeyModels[Indices[from]];
            Indices.RemoveAt(from);
            Indices.Insert(to, model.Key);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, model, to, from));
        }

        public void Clear() {
            KeyModels.Clear();
            Indices.Clear();
        }

        public int IndexOf(KeyModel model) {
            for (int i = 0; i < Indices.Count; i++) {
                if (model.Key == Indices[i]) {
                    return i;
                }
            }
            return -1;
        }

        public IEnumerator<KeyModel> GetEnumerator() {
            return new KeyModelEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private class KeyModelEnumerator : IEnumerator<KeyModel>
        {
            private int _index;
            private KeyModelCollection _collection;

            public KeyModel Current => _collection[_index];

            object IEnumerator.Current => _collection[_index];

            public KeyModelEnumerator(KeyModelCollection collection) {
                _collection = collection;
                _index = -1;
            }

            public void Dispose() { }

            public bool MoveNext() {
                _index++;
                return _index < _collection.Count;
            }

            public void Reset() {
                _index = 0;
            }
        }
    }
}
