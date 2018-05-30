using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Klasa przetrzymująca toolboxowe elementy.
    /// </summary>
    public class Toolbox : IToolbox
    {
        // Zaimplementowane z ISimpleToolbox.
        public Color GetColor()
        {
            return CurrentColor;
        }
        // Zaimplementowane z IToolbox
        /// <summary>
        /// Obecny pędzel.
        /// </summary>
        public Pen CurrentPen { get; set; }
        /// <summary>
        /// Obecny kolor.
        /// </summary>
        public Color CurrentColor { get; set; }
        /// <summary>
        /// Obecne narzędzie.
        /// </summary>
        public Tools CurrentTool { get; set; }
    }
}
