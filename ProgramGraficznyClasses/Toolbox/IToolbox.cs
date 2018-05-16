using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IToolbox
    {
        Pen CurrentPen { get; set; }
        Color CurrentColor { get; set; }
        Tools CurrentTool { get; set; }
    }
}