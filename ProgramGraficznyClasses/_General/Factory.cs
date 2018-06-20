using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Szybko tworzy potrzebny i ważny typ. 
    /// </summary>
    static public class Factory
    {
        /// <summary>
        /// Zwraca tekst reprezentujący pojawienie się nowej instancji klasy. Do użycia w ILog.
        /// </summary>
        /// <param name="name">Nazwa klasy.</param>
        /// <returns>Zwraca tekst reprezentujący pojawienie się nowej instancji klasy.</returns>
        static public string GetNewInstanceCreationString(string name)
        {
            return $"Nowa instancja {name} została utworzona pomyślnie.";
        }
        /// <summary>
        /// Zwraca tekst reprezentujący błąd programisty z użyciem informacji o błędzie.
        /// </summary>
        /// <param name="info">Informacje o błędzie.</param>
        /// <param name="applicationExit">Jeżeli true to na końcu tekstu pojawi się inforamcja o zamnknięciu aplikacji.</param>
        /// <returns>Tekst reprezentujący błąd programisty.</returns>
        static public string GetProgrammerErrorString(string info, bool applicationExit)
        {
            string afterInfo = applicationExit ? "Nastąpi teraz zamknięcie aplikacji. Przepraszamy za utrudnienia." : "";
            return $"Wystąpił błąd (wina programistów).{Environment.NewLine}INFO: {info}{Environment.NewLine}{afterInfo}";
        }
        /// <summary>
        /// Tworzy nowy ILog.
        /// </summary>
        /// <returns>ILog.</returns>
        static public ILog CreateLog()
        {
            // Ścieżka do pliku txt z logiem.
            string path = @"C:\Users\Marek\Desktop\Moje Logi\ProgramGraficzny Log.txt";

            // Ustawianie listy typów które będą akcpetowane przez LogManager.
            List<LogMessagesTypes> acceptableTypes = new List<LogMessagesTypes>();
            acceptableTypes.Add(LogMessagesTypes.Important);
            acceptableTypes.Add(LogMessagesTypes.Error);
            acceptableTypes.Add(LogMessagesTypes.Warning);
            acceptableTypes.Add(LogMessagesTypes.Detail);

            // Czy LogManager ma zapisywać typ przed wiadomością?
            bool typeBeforeMsg = true;

            // Zwracanie nowego ILog.
            return new EmptyLog();
        }
        /// <summary>
        /// Tworzy nowy INotificator.
        /// </summary>
        /// <returns>iNotificator.</returns>
        static public INotificator CreateINotificator()
        {
            return new MessageBoxNotificator();
        }
        static public IThemesController CreateIThemesController()
        {
            IThemesController output = new ThemesManager();

            output.AddTheme(new EditorTheme("Standard", Color.FromKnownColor(KnownColor.Control), Color.FromKnownColor(KnownColor.Control), Color.Black));
            output.LoadThemes("Themes");

            return output;
        }
    }
}
