using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public interface IGraphicEditorStandard
    {
        event EventHandler GraphicChanged;

        void DrawAllAgain();
        void NextState();
        void ReturnState();
        void Clear();
        void Save(string path);
        Bitmap CreateBitmap();
    }
}
