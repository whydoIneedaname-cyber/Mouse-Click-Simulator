namespace MouseClickSimulator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── 控件声明 ─────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpTarget;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Button btnPickPoint;

        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.NumericUpDown numFrequency;
        private System.Windows.Forms.Label lblMaxClicks;
        private System.Windows.Forms.NumericUpDown numMaxClicks;
        private System.Windows.Forms.CheckBox chkJitter;
        private System.Windows.Forms.Label lblButton;
        private System.Windows.Forms.ComboBox cmbButton;

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblHotkeys;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── 实例化 ───────────────────────────────────────────────
            this.grpTarget    = new System.Windows.Forms.GroupBox();
            this.lblX         = new System.Windows.Forms.Label();
            this.numX         = new System.Windows.Forms.NumericUpDown();
            this.lblY         = new System.Windows.Forms.Label();
            this.numY         = new System.Windows.Forms.NumericUpDown();
            this.lblWidth     = new System.Windows.Forms.Label();
            this.numWidth     = new System.Windows.Forms.NumericUpDown();
            this.lblHeight    = new System.Windows.Forms.Label();
            this.numHeight    = new System.Windows.Forms.NumericUpDown();
            this.btnPickPoint = new System.Windows.Forms.Button();

            this.grpOptions   = new System.Windows.Forms.GroupBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.numFrequency = new System.Windows.Forms.NumericUpDown();
            this.lblMaxClicks = new System.Windows.Forms.Label();
            this.numMaxClicks = new System.Windows.Forms.NumericUpDown();
            this.chkJitter    = new System.Windows.Forms.CheckBox();
            this.lblButton    = new System.Windows.Forms.Label();
            this.cmbButton    = new System.Windows.Forms.ComboBox();

            this.btnStart     = new System.Windows.Forms.Button();
            this.btnStop      = new System.Windows.Forms.Button();
            this.lblStatus    = new System.Windows.Forms.Label();
            this.lblHotkeys   = new System.Windows.Forms.Label();

            // BeginInit
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxClicks)).BeginInit();
            this.grpTarget.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();

            // ═══════════════════════════════════════════════════════════
            // grpTarget — 目标区域
            // ═══════════════════════════════════════════════════════════
            this.grpTarget.Text     = "目标区域（屏幕坐标）";
            this.grpTarget.Location = new System.Drawing.Point(12, 12);
            this.grpTarget.Size     = new System.Drawing.Size(370, 110);

            // lblX / numX
            this.lblX.Text     = "X:";
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(15, 28);
            this.numX.Location = new System.Drawing.Point(50, 25);
            this.numX.Size     = new System.Drawing.Size(80, 23);
            this.numX.Maximum  = 32767;

            // lblY / numY
            this.lblY.Text     = "Y:";
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(145, 28);
            this.numY.Location = new System.Drawing.Point(175, 25);
            this.numY.Size     = new System.Drawing.Size(80, 23);
            this.numY.Maximum  = 32767;

            // lblWidth / numWidth
            this.lblWidth.Text     = "宽度:";
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(15, 65);
            this.numWidth.Location = new System.Drawing.Point(50, 62);
            this.numWidth.Size     = new System.Drawing.Size(80, 23);
            this.numWidth.Maximum  = 32767;

            // lblHeight / numHeight
            this.lblHeight.Text     = "高度:";
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(145, 65);
            this.numHeight.Location = new System.Drawing.Point(175, 62);
            this.numHeight.Size     = new System.Drawing.Size(80, 23);
            this.numHeight.Maximum  = 32767;

            // btnPickPoint — 拾取坐标
            this.btnPickPoint.Text     = "🎯 拾取坐标";
            this.btnPickPoint.Location = new System.Drawing.Point(270, 25);
            this.btnPickPoint.Size     = new System.Drawing.Size(88, 60);
            this.btnPickPoint.Click   += new System.EventHandler(this.btnPickPoint_Click);

            this.grpTarget.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblX, this.numX, this.lblY, this.numY,
                this.lblWidth, this.numWidth, this.lblHeight, this.numHeight,
                this.btnPickPoint
            });

            // ═══════════════════════════════════════════════════════════
            // grpOptions — 点击选项
            // ═══════════════════════════════════════════════════════════
            this.grpOptions.Text     = "点击选项";
            this.grpOptions.Location = new System.Drawing.Point(12, 130);
            this.grpOptions.Size     = new System.Drawing.Size(370, 110);

            // lblFrequency / numFrequency
            this.lblFrequency.Text     = "频率 (次/秒):";
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(15, 28);
            this.numFrequency.Location = new System.Drawing.Point(105, 25);
            this.numFrequency.Size     = new System.Drawing.Size(70, 23);
            this.numFrequency.Minimum  = 1;
            this.numFrequency.Maximum  = 1000;
            this.numFrequency.Value    = 10;

            // lblMaxClicks / numMaxClicks
            this.lblMaxClicks.Text     = "最大次数:";
            this.lblMaxClicks.AutoSize = true;
            this.lblMaxClicks.Location = new System.Drawing.Point(190, 28);
            this.numMaxClicks.Location = new System.Drawing.Point(260, 25);
            this.numMaxClicks.Size     = new System.Drawing.Size(90, 23);
            this.numMaxClicks.Maximum  = 9999999;
            this.numMaxClicks.Value    = 0; // 0 = 无限

            // chkJitter
            this.chkJitter.Text     = "随机抖动";
            this.chkJitter.AutoSize = true;
            this.chkJitter.Location = new System.Drawing.Point(15, 65);

            // lblButton / cmbButton
            this.lblButton.Text     = "鼠标按键:";
            this.lblButton.AutoSize = true;
            this.lblButton.Location = new System.Drawing.Point(130, 65);
            this.cmbButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbButton.Items.AddRange(new object[] { "左键", "右键", "中键" });
            this.cmbButton.SelectedIndex = 0;
            this.cmbButton.Location = new System.Drawing.Point(200, 62);
            this.cmbButton.Size     = new System.Drawing.Size(80, 23);

            this.grpOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFrequency, this.numFrequency,
                this.lblMaxClicks, this.numMaxClicks,
                this.chkJitter, this.lblButton, this.cmbButton
            });

            // ═══════════════════════════════════════════════════════════
            // 按钮 & 状态栏
            // ═══════════════════════════════════════════════════════════
            // btnStart
            this.btnStart.Text     = "▶  开始";
            this.btnStart.Location = new System.Drawing.Point(12, 250);
            this.btnStart.Size     = new System.Drawing.Size(120, 35);
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Click    += new System.EventHandler(this.btnStart_Click);

            // btnStop
            this.btnStop.Text     = "■  停止";
            this.btnStop.Location = new System.Drawing.Point(145, 250);
            this.btnStop.Size     = new System.Drawing.Size(120, 35);
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Enabled   = false;
            this.btnStop.Click    += new System.EventHandler(this.btnStop_Click);

            // lblStatus
            this.lblStatus.Text     = "就绪";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 298);
            this.lblStatus.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // lblHotkeys
            this.lblHotkeys.Text     = "快捷键：F9 = 暂停/继续 | Esc = 停止";
            this.lblHotkeys.AutoSize = true;
            this.lblHotkeys.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblHotkeys.Location  = new System.Drawing.Point(12, 320);

            // ═══════════════════════════════════════════════════════════
            // MainForm
            // ═══════════════════════════════════════════════════════════
            this.ClientSize = new System.Drawing.Size(394, 348);
            this.Controls.Add(this.grpTarget);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblHotkeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "鼠标点击模拟器 — Mouse Click Simulator";
            this.TopMost         = true;

            // EndInit
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxClicks)).EndInit();
            this.grpTarget.ResumeLayout(false);
            this.grpTarget.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
