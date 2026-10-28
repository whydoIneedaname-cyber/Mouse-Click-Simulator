using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseClickSimulator
{
    /// <summary>
    /// 全局低级键盘钩子，即使窗口不在前台也能捕获指定按键。
    /// </summary>
    internal sealed class KeyboardHook : IDisposable
    {
        // ── Win32 ────────────────────────────────────────────────────
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN     = 0x0100;

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // ── 实例字段 ─────────────────────────────────────────────────
        private readonly LowLevelKeyboardProc _proc;
        private readonly IntPtr _hookId;
        private readonly Keys _targetKey;
        private readonly Action _onPress;

        public KeyboardHook(Keys key, Action onPress)
        {
            _targetKey = key;
            _onPress   = onPress;
            _proc      = HookCallback;   // prevent GC of delegate
            _hookId    = InstallHook(_proc);
        }

        private IntPtr InstallHook(LowLevelKeyboardProc proc)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule  = curProcess.MainModule!;
            return SetWindowsHookEx(
                WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName!), 0);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == _targetKey)
                    _onPress?.Invoke();
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookId);
        }
    }
}
