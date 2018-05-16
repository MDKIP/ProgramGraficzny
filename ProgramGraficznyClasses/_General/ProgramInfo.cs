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
        /// <summary>
        /// Czy program posiada domyślny toolbox.
        /// </summary>
        static public bool HasMainToolbox { get => (MainToolbox != null); }
        /// <summary>
        /// Nazwa programu.
        /// </summary>
        static public string ProgramName { get; } = "Program Graficzny";
        /// <summary>
        /// Wersja programu.
        /// </summary>
        static public string Version { get; } = "Alpha 0.1";
        /// <summary>
        /// Domyślny toolbox.
        /// </summary>
        static public IToolbox MainToolbox { get; set; }
    }
}
