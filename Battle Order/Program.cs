using System;
using System.Windows.Forms;

namespace BattleOrder
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BattleOrderForm());
        }
    }
}