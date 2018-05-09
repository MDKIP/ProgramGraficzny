using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Class containts info about program.
    /// </summary>
    static public class ProgramInfo
    {
        /// <summary>
        /// Main toolbox for all Graphics Editors.
        /// </summary>
        static public IToolbox MainToolbox { get; set; }
        /// <summary>
        /// Has program have a main toolbox.
        /// </summary>
        static public bool HasMainToolbox { get => (MainToolbox != null); }
        /// <summary>
        /// Name of program.
        /// </summary>
        static public string ProgramName { get; } = "Program Graficzny";
        /// <summary>
        /// Version of program.
        /// </summary>
        static public string Version { get; } = "Alpha 0.1";
    }
}
