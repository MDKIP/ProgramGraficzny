using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public class GraphicChangedEventArgs : EventArgs
    {
        public GraphicChangedEventArgs(Image img)
        {
            Image = img;
        }

        public Image Image { get; private set; }
    }
}
