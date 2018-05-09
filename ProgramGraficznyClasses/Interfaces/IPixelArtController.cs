using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IPixelArtController : IGraphicEditorStandard
    {
        void SetPixel(Color color, int x, int y);
    }
}
