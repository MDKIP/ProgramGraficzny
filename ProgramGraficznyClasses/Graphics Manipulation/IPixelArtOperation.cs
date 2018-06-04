using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IPixelArtOperation
    {
        Point Coordiantes { get; }
        Color AfterColor { get; }
        Color BeforeColor { get; }
    }
}
