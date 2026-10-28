using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MouseClickSimulator
{
    /// <summary>
    /// 后台线程，执行鼠标移动 + 点击循环。
    /// </summary>
    internal class ClickWorker
    {
        // ── Win32 P/Invoke ───────────────────────────────────────────
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(
            uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN   = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP     = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN  = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP    = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP   = 0x0040;

        // ── 状态 ─────────────────────────────────────────────────────
        private Thread? _thread;
        private volatile bool _running;
        private volatile bool _paused;
        private long _totalClicks;

        private readonly Action<string> _statusCallback;

        public bool IsRunning => _running;
        public bool IsPaused  => _paused;
        public long TotalClicks => Interlocked.Read(ref _totalClicks);

        public ClickWorker(Action<string> statusCallback)
        {
            _statusCallback = statusCallback;
        }

        // ── 公共控制 ─────────────────────────────────────────────────
        public void Start(ClickConfig cfg)
        {
            if (_running) return;
            _running = true;
            _paused  = false;
            Interlocked.Exchange(ref _totalClicks, 0);
            _thread = new Thread(() => RunLoop(cfg)) { IsBackground = true };
            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
            _thread?.Join(2000);
            _statusCallback("已停止");
        }

        public void TogglePause()
        {
            if (!_running) return;
            _paused = !_paused;
            _statusCallback(_paused ? "已暂停" : "运行中…");
        }

        // ── 核心循环 ─────────────────────────────────────────────────
        private void RunLoop(ClickConfig cfg)
        {
            double intervalMs = 1000.0 / cfg.ClicksPerSecond;
            var sw = Stopwatch.StartNew();

            GetMouseEventFlags(cfg.ButtonType, out uint downFlag, out uint upFlag);

            while (_running)
            {
                if (_paused)
                {
                    Thread.Sleep(50);
                    continue;
                }

                // 计算目标坐标
                int tx = cfg.X;
                int ty = cfg.Y;
                if (cfg.UseJitter && (cfg.Width > 0 || cfg.Height > 0))
                {
                    tx += cfg.Width  > 0 ? cfg.Rng.Next(cfg.Width)  : 0;
                    ty += cfg.Height > 0 ? cfg.Rng.Next(cfg.Height) : 0;
                }

                // 移动 + 点击
                SetCursorPos(tx, ty);
                mouse_event(downFlag, 0, 0, 0, UIntPtr.Zero);
                mouse_event(upFlag,   0, 0, 0, UIntPtr.Zero);

                long count = Interlocked.Increment(ref _totalClicks);

                // 状态回调（每 20 次刷新一次 UI 以减少跨线程调用）
                if (count % 20 == 0 || count == 1)
                    _statusCallback($"运行中… 已点击 {count} 次");

                // 达到上限则停止
                if (cfg.MaxClicks > 0 && count >= cfg.MaxClicks)
                {
                    _running = false;
                    _statusCallback($"完成！共点击 {count} 次");
                    break;
                }

                // 节流
                double elapsed = sw.Elapsed.TotalMilliseconds;
                int sleepMs = (int)Math.Max(0, intervalMs - elapsed);
                if (sleepMs > 0) Thread.Sleep(sleepMs);
                sw.Restart();
            }
        }

        // ── 辅助 ─────────────────────────────────────────────────────
        private static void GetMouseEventFlags(
            MouseButtonType btn, out uint down, out uint up)
        {
            switch (btn)
            {
                case MouseButtonType.Right:
                    down = MOUSEEVENTF_RIGHTDOWN;
                    up   = MOUSEEVENTF_RIGHTUP;
                    break;
                case MouseButtonType.Middle:
                    down = MOUSEEVENTF_MIDDLEDOWN;
                    up   = MOUSEEVENTF_MIDDLEUP;
                    break;
                default:
                    down = MOUSEEVENTF_LEFTDOWN;
                    up   = MOUSEEVENTF_LEFTUP;
                    break;
            }
        }
    }
}
