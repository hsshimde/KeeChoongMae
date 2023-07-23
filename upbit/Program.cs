using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.View;
using upbit.View.KeySaveForm;

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
            KeySaveForm keySaveForm = new KeySaveForm();
            Application.Run(keySaveForm);

            if (keySaveForm.IsLoggedIn)
            {
                MainForm main = new MainForm(keySaveForm.mAPI);
                main.Init();
                Application.Run(main);
            }
        }


    }
}
