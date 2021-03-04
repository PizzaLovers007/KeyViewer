using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace KeyViewer.Utils
{
    public class LowLevelKeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyboardKeyArgs> KeyPressed;

        public event EventHandler<KeyboardKeyArgs> KeyReleased;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private readonly bool[] isDown;

        public bool IsHooked { get; private set; }

        private static LowLevelKeyboardListener listener;

        public static LowLevelKeyboardListener Instance {
            get {
                if (listener == null) {
                    listener = new LowLevelKeyboardListener();
                }
                return listener;
            }
        }

        private LowLevelKeyboardListener() {
            _proc = HookCallback;
            isDown = new bool[1000];
        }

        public void HookKeyboard() {
            if (IsHooked) {
                return;
            }
            _hookID = SetHook(_proc);
            IsHooked = true;
        }

        public void UnHookKeyboard() {
            if (!IsHooked) {
                return;
            }
            UnhookWindowsHookEx(_hookID);
            IsHooked = false;
        }

        public bool KeyIsPressed(Key key) {
            return isDown[KeyInterop.VirtualKeyFromKey(key)];
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode >= 0) {
                int vkCode = Marshal.ReadInt32(lParam);

                if (wParam == (IntPtr)WM_KEYDOWN && !isDown[vkCode]) {
                    isDown[vkCode] = true;
                    KeyPressed?.Invoke(this, new KeyboardKeyArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
                } else if (wParam == (IntPtr)WM_KEYUP && isDown[vkCode]) {
                    isDown[vkCode] = false;
                    KeyReleased?.Invoke(this, new KeyboardKeyArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }

    public class KeyboardKeyArgs : EventArgs
    {
        public Key Key { get; private set; }

        public KeyboardKeyArgs(Key key) {
            Key = key;
        }
    }
}
