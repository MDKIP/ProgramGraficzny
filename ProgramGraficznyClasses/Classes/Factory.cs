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
        /// Tworzy nowy ILog z użyciem LogManager.
        /// </summary>
        /// <returns>Ilog.</returns>
        static public ILog CreateLog()
        {
            // Ustawianie listy typów które będą akcpetowane przez LogManager.
            List<LogMessagesTypes> acceptableTypes = new List<LogMessagesTypes>();
            acceptableTypes.Add(LogMessagesTypes.Important);
            acceptableTypes.Add(LogMessagesTypes.Error);
            acceptableTypes.Add(LogMessagesTypes.Warning);
            //acceptableTypes.Add(LogMessagesTypes.Details);

            // Czy LogManager ma zapisywać przed wiadomością typ?
            bool typeBeforeMsg = true;

            // Zwracanie nowego LogManagera.
            return new LogManager(@"C:\Users\Marek\Desktop\Moje Logi\ProgramGraficzny Log.txt", acceptableTypes, typeBeforeMsg);
        }
    }
}
