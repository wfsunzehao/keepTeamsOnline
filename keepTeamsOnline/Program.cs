using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("kernel32.dll")]
    static extern uint SetThreadExecutionState(uint esFlags);

    private const uint ES_CONTINUOUS = 0x80000000;
    private const uint ES_SYSTEM_REQUIRED = 0x00000001;
    private const uint ES_DISPLAY_REQUIRED = 0x00000002;
    private const uint MOUSEEVENTF_MOVE = 0x0001;

    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("启动保持在线 + 防止休眠 脚本...");
        Console.CancelKeyPress += (s, e) =>
        {
            // 程序退出时释放
            SetThreadExecutionState(ES_CONTINUOUS);
        };

        // 程序运行期间，阻止系统休眠/关屏
        SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED | ES_DISPLAY_REQUIRED);

        while (true)
        {
            // 模拟鼠标轻微移动
            int dx = random.Next(-3, 4);
            int dy = random.Next(-3, 4);
            mouse_event(MOUSEEVENTF_MOVE, (uint)dx, (uint)dy, 0, UIntPtr.Zero);

            Console.WriteLine($"模拟鼠标移动: dx={dx}, dy={dy}");
            Thread.Sleep(60000); // 每 60 秒一次
        }
    }
}