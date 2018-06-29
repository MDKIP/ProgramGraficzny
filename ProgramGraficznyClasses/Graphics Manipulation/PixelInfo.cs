using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    public class PixelInfo
    {
        public List<IPixelArtOperation> ExecutedOperations { get; set; } = new List<IPixelArtOperation>();
        public bool IsColored { get; set; } = false;
    }
}
