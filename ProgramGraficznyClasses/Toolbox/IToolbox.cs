using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IToolbox : ISimpleToolbox
    {
        Pen CurrentPen { get; set; }
        Color CurrentColor { get; set; }
        Tools CurrentTool { get; set; }
    }
}