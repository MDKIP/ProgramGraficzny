using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    public class PixelArtOperation : IPixelArtOperation
    {
        public PixelArtOperation(Point coordinates, Color afterColor, Color beforeColor)
        {
            // Przypisywanie.
            Coordiantes = coordinates;
            AfterColor = afterColor;
            BeforeColor = beforeColor;
        }

        public Point Coordiantes { get; private set; }
        public Color AfterColor { get; private set; }
        public Color BeforeColor { get; private set; }
    }
}
