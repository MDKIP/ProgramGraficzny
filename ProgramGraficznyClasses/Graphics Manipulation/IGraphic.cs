using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IGraphic : IGraphicEditorStandard
    {
        Size Size { get; set; }
        bool OnlyDrawningMode { get; set; }
        bool IsConnectedWithProject { get; }

        void DrawLine(Pen p, Point a, Point b, bool onlyEditor);
        void DrawImage(string path, int x, int y);
        void DrawImage(Image img, int x, int y);
        void UpdateGraphics(Graphics newGraphics);
        void Execute(IGraphicOperation operation);
    }
}
