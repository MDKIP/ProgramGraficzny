using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public class Toolbox : IToolbox
    {
        /// <summary>
        /// The current pen in toolbox.
        /// </summary>
        public Pen CurrentPen { get; set; }
        /// <summary>
        /// Current tool.
        /// </summary>
        public Tools CurrentTool { get; set; }
        public Color CurrentColor { get; set; }
    }
}
