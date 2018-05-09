using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProgramGraficznyClasses
{
    public class LogManager : ILog
    {
        /// <summary>
        /// Standardowy konstruktor dla LogManager.
        /// </summary>
        /// <param name="pathToLog">Ścieżka do pliku .txt do którego będą zapisywane wiadomości. Jeżeli plik nie istnieje to zostanie utworzony.</param>
        /// <param name="acceptableTypes">Akceptowalne typy widomości. Inne wiadomości nie będą wypisywane.</param>
        /// <param name="typeBeforeMsg">Czy przed wiadomością ma być jej typ (type: msg). Domyślnie false.</param>
        public LogManager(string pathToLog, List<LogMessagesTypes> acceptableTypes, bool typeBeforeMsg = false)
        {
            // Przypisywanie.
            this.pathToLog = pathToLog;
            this.acceptableTypes = acceptableTypes;
            this.typeBeforeMsg = typeBeforeMsg;

            // Sprawdzanie poprawności pliku .txt.
            if (!pathToLog.EndsWith(".txt"))
            {
                pathToLog += ".txt";
            }
            if (!File.Exists(pathToLog))
            {
                File.Create(pathToLog);
            }

            // Operacje na pliku.
            File.WriteAllText(pathToLog, String.Empty);
        }

        private List<LogMessagesTypes> acceptableTypes;
        private string pathToLog;
        private bool typeBeforeMsg;

        /// <summary>
        /// Zapisuje wiadomość do logu.
        /// </summary>
        /// <param name="msg">Wiadomość.</param>
        public void Write(string msg)
        {
            // Dodaje wiadomość do pliku i daje nową linię.
            File.AppendAllText(pathToLog, msg + Environment.NewLine);
        }
        /// <summary>
        /// Zapisuje wiadomość do logu i określa jej typ.
        /// </summary>
        /// <param name="msg">Wiadomość.</param>
        /// <param name="type">Typ wiadomości.</param>
        public void Write(string msg, LogMessagesTypes type)
        {
            if (acceptableTypes.Contains(type)) // Czy typ jest dozwolony?
            {
                if (typeBeforeMsg) // Czy log ma zapisać typ wiadomości przed wiadomością?
                {
                    // Zapisywanie zmodyfikowanej wiadomości do logu.
                    Write($"{type.ToString().ToUpper()}: {msg}");
                }
                else
                {
                    // Zapisywanie wiadomości do logu.
                    Write(msg);
                }
            }
        }
    }
}
