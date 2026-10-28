using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseClickSimulator
{
    public partial class MainForm : Form
    {
        // ── Win32 — 获取当前鼠标位置 ────────────────────────────────
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT { public int X; public int Y; }

        // ── 状态 ─────────────────────────────────────────────────────
        private ClickWorker _worker;
        private KeyboardHook? _hookToggle;
        private KeyboardHook? _hookStop;
        private bool _pickingCoord;

        public MainForm()
        {
            InitializeComponent();
            _worker = new ClickWorker(UpdateStatus);
            RegisterHotkeys();
        }

        // ═══════════════════════════════════════════════════════════════
        //  按钮事件
        // ═══════════════════════════════════════════════════════════════
        private void btnStart_Click(object? sender, EventArgs e)
        {
            var cfg = BuildConfig();
            _worker.Start(cfg);
            btnStart.Enabled = false;
            btnStop.Enabled  = true;
            lblStatus.Text   = "运行中…";
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            _worker.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled  = false;
            lblStatus.Text   = "已停止";
        }

        /// <summary>
        /// "拾取坐标" 按钮：进入拾取模式，3 秒后捕获当前鼠标位置。
        /// </summary>
        private async void btnPickPoint_Click(object? sender, EventArgs e)
        {
            if (_pickingCoord) return;
            _pickingCoord = true;
            btnPickPoint.Enabled = false;

            for (int i = 3; i > 0; i--)
            {
                lblStatus.Text = $"请将鼠标移到目标位置… {i}s";
                await System.Threading.Tasks.Task.Delay(1000);
            }

            GetCursorPos(out POINT pt);
            numX.Value = pt.X;
            numY.Value = pt.Y;
            lblStatus.Text = $"已拾取坐标 ({pt.X}, {pt.Y})";

            btnPickPoint.Enabled = true;
            _pickingCoord = false;
        }

        // ═══════════════════════════════════════════════════════════════
        //  配置构建
        // ═══════════════════════════════════════════════════════════════
        private ClickConfig BuildConfig()
        {
            MouseButtonType btn = cmbButton.SelectedIndex switch
            {
                1 => MouseButtonType.Right,
                2 => MouseButtonType.Middle,
                _ => MouseButtonType.Left,
            };

            return new ClickConfig
            {
                X               = (int)numX.Value,
                Y               = (int)numY.Value,
                Width           = (int)numWidth.Value,
                Height          = (int)numHeight.Value,
                ClicksPerSecond = (int)numFrequency.Value,
                UseJitter       = chkJitter.Checked,
                ButtonType      = btn,
                MaxClicks       = (int)numMaxClicks.Value,
            };
        }

        // ═══════════════════════════════════════════════════════════════
        //  状态回调 & 全局热键
        // ═══════════════════════════════════════════════════════════════
        private void UpdateStatus(string text)
        {
            if (InvokeRequired)
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = text;
                    // 如果 worker 停了，恢复按钮状态
                    if (!_worker.IsRunning)
                    {
                        btnStart.Enabled = true;
                        btnStop.Enabled  = false;
                    }
                }));
            else
            {
                lblStatus.Text = text;
                if (!_worker.IsRunning)
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled  = false;
                }
            }
        }

        private void RegisterHotkeys()
        {
            // F9 → 暂停/继续（如果未运行则启动）
            _hookToggle = new KeyboardHook(Keys.F9, () =>
            {
                if (_worker.IsRunning)
                    _worker.TogglePause();
                else
                    BeginInvoke(new Action(() => btnStart.PerformClick()));
            });

            // Esc → 停止
            _hookStop = new KeyboardHook(Keys.Escape, () =>
            {
                if (_worker.IsRunning)
                    BeginInvoke(new Action(() => btnStop.PerformClick()));
            });
        }

        // ═══════════════════════════════════════════════════════════════
        //  清理
        // ═══════════════════════════════════════════════════════════════
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _worker.Stop();
            _hookToggle?.Dispose();
            _hookStop?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
