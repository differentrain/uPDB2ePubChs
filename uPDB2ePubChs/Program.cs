using System;
using System.Threading;
using System.Windows.Forms;

namespace uPDB2ePubChs
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(true, "d021617f-5565-4b2b-9d9e-314195ca3d50", out var createNew))
            {
                if (createNew)
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain());
                }
                else
                {
                    Environment.Exit(1);//It's not required but can send system a non-normal exit signal……
                }
            }
        }
    }
}
