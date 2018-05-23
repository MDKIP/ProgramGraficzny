using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    class BitmapManipulator : IGraphic
    {
        public event EventHandler GraphicChanged;

        public Size Size { get; set; }
        public bool OnlyDrawningMode { get; set; }
        public bool IsConnectedWithProject { get; private set; }

        private Bitmap bitmap;

        public void DrawAllAgain()
        {
            throw new NotImplementedException();
        }
        public void NextState()
        {
            throw new NotImplementedException();
        }
        public void ReturnState()
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public void Save(string path)
        {
            throw new NotImplementedException();
        }
        public void DrawImage(string path, int x, int y)
        {
            throw new NotImplementedException();
        }
        public void DrawImage(Image img, int x, int y)
        {
            throw new NotImplementedException();
        }
        public void DrawLine(Pen p, Point a, Point b, bool onlyEditor)
        {
            throw new NotImplementedException();
        }
        public void UpdateGraphics(Graphics newGraphics)
        {
            throw new NotImplementedException();
        }
        public void Execute(IGraphicOperation operation)
        {
            throw new NotImplementedException();
        }
        public Bitmap CreateBitmap()
        {
            return bitmap;
        }

        protected void OnGraphicChanged()
        {
            GraphicChanged(this, new GraphicChangedEventArgs(bitmap));
        }
    }
}
