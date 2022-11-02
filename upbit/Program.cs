using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.View;

namespace upbit
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogInForm logInForm = new LogInForm();
            Application.Run(logInForm);

            if (logInForm.BIsLoggedIn)
            {
                MainForm main = new MainForm(logInForm.GetUpbitAPI());
                main.Init();
                //await main.Init();
                Application.Run(main);
            }
        }


    }
}
