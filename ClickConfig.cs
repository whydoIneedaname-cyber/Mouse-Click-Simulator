using System;

namespace MouseClickSimulator
{
    /// <summary>
    /// 点击模拟配置参数
    /// </summary>
    internal class ClickConfig
    {
        /// <summary>目标区域左上角 X 坐标（屏幕像素）</summary>
        public int X { get; set; }

        /// <summary>目标区域左上角 Y 坐标（屏幕像素）</summary>
        public int Y { get; set; }

        /// <summary>目标区域宽度（0 = 单点点击）</summary>
        public int Width { get; set; }

        /// <summary>目标区域高度（0 = 单点点击）</summary>
        public int Height { get; set; }

        /// <summary>每秒点击次数（1 – 1000）</summary>
        public int ClicksPerSecond { get; set; } = 10;

        /// <summary>是否在区域内随机偏移，模拟人类行为</summary>
        public bool UseJitter { get; set; }

        /// <summary>点击按钮类型：Left / Right / Middle</summary>
        public MouseButtonType ButtonType { get; set; } = MouseButtonType.Left;

        /// <summary>最大点击次数（0 = 无限）</summary>
        public int MaxClicks { get; set; }

        /// <summary>随机数生成器</summary>
        public Random Rng { get; set; } = new Random();
    }

    internal enum MouseButtonType
    {
        Left,
        Right,
        Middle
    }
}
