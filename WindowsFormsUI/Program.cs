using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    static class Program
    {
        /// <summary>
        /// Punkt wejściowy dla programu.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Tworzenie rzeczy z fabryki.
            ILog log = Factory.CreateLog();
            FormsManager.StandardLog = log;
            FormsManager.StandardNotificator = Factory.CreateINotificator();

            log.Write("Start programu.", LogMessagesTypes.Important);

            // Wygenerowane przez Visual Studio.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Odpala aplikację.
            Application.Run(FormsManager.ShowStartForm());
        }
    }
}
