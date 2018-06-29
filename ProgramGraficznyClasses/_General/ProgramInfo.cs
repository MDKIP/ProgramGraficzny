using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Zawiera podstawowe informacje o programie.
    /// </summary>
    static public class ProgramInfo
    {
        static public EditorTheme CurrentTheme { get; set; }
        static public SettingsInfo MainSettings { get; set; }
        static public string ProgramName { get; } = "Program Graficzny";
        static public string Version { get; } = "Alpha 0.2";
        static public IThemesController MainThemesController { get; set; }
    }
}
