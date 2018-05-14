using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Szybko tworzy potrzebny i ważny typ. 
    /// </summary>
    static public class Factory
    {
        /// <summary>
        /// Tworzy nowy ILog z użyciem LogManagera.
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

            // Zwracanie nowego LogManagera.
            return new LogManager(path, acceptableTypes, typeBeforeMsg);
        }
    }
}
