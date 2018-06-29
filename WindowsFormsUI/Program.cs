using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramGraficznyClasses;
using System.IO;
using System.Drawing;
using System.Threading;

namespace WindowsFormsUI
{
    static class Program
    {
        static private string settingsFilePath = "Settings.pgsi";
        static private INotificator notificator;
        static private ILog log;

        /// <summary>
        /// Punkt wejściowy dla programu.
        /// </summary>
        [STAThread]
        static private void Main()
        {
            // Tworzenie rzeczy z fabryki.
            log = Factory.CreateLog();
            FormsManager.StandardLog = log;
            notificator = FormsManager.StandardNotificator = Factory.CreateINotificator();
            ProgramInfo.MainThemesController = Factory.CreateIThemesController();
            LoadSettings();

            log.Write("Start programu.", LogMessagesTypes.Important);

            // Wygenerowane przez Visual Studio.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.ThreadException += Application_ThreadException;

            // Odpala aplikację.
            Application.Run(FormsManager.ShowStartForm());
        }
        static public void SaveSettings()
        {
            string fileContent = $"{ProgramInfo.CurrentTheme.Name}|" +
                $"{ProgramInfo.MainSettings.StandardRPPEP}|" +
                $"{ProgramInfo.MainSettings.VisualizerBackgroundColor.ToArgb()}";

            File.WriteAllText(settingsFilePath, fileContent);
        }
        static public void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
            {
                ProgramInfo.MainSettings = SettingsInfo.Default;
                ProgramInfo.CurrentTheme = ProgramInfo.MainThemesController.GetTheme("Standard");
                return;
            }

            string fileContent = File.ReadAllText(settingsFilePath);

            string[] dataParts = fileContent.Split('|');

            ProgramInfo.CurrentTheme = ProgramInfo.MainThemesController.GetTheme(dataParts[0]);
            FormsManager.ReloadAllForms();
            ProgramInfo.MainSettings = new SettingsInfo()
            {
                StandardRPPEP = int.Parse(dataParts[1]),
                VisualizerBackgroundColor = Color.FromArgb(int.Parse(dataParts[2])),
            };
        }
        static private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //notificator.Notify(e.Exception);
        }
    }
}
