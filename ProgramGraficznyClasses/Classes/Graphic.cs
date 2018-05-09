using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProgramGraficznyClasses
{
    public class Graphic : IGraphic
    {
        /// <summary>
        /// Standard constructor for graphic class.
        /// </summary>
        /// <param name="project">Project of this graphic. Can be null.</param>
        /// <param name="graphics">On this graphics will be draw.</param>
        /// <param name="log">Log for this object.</param>
        public Graphic(IProject project, Graphics graphics, ILog log)
        {
            // Throwing exceptions.
            if (graphics == null)
            {
                throw new NullReferenceException("Graphics cannot be null.");
            }

            // Setting.
            operations = new List<IGraphicOperation>();
            futureOperations = new List<IGraphicOperation>();
            this.log = log;
            IsConnectedWithProject = (project != null);
            this.graphics = graphics;
        }

        /// <summary>
        /// Size of this graphic.
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// If is on, the graphic can only draw and can't remebered operations.
        /// </summary>
        public bool OnlyDrawningMode { get; set; }
        /// <summary>
        /// Is graphic connected with project.
        /// </summary>
        public bool IsConnectedWithProject { get; private set; }

        private List<IGraphicOperation> operations;
        private List<IGraphicOperation> futureOperations;
        private Graphics graphics;
        private ILog log;

        /// <summary>
        /// Draws all again.
        /// </summary>
        public void DrawAllAgain()
        {
            OnlyDrawningMode = true;
            foreach (GraphicOperation operation in operations)
            {
                Execute(operation);
            }
            OnlyDrawningMode = false;
        }
        /// <summary>
        /// Next state of graphic, of course only if exsit.
        /// </summary>
        public void NextState()
        {
            if (futureOperations.Count > 0)
            {
                IGraphicOperation nextOperation = futureOperations[0];
                operations.Add(nextOperation);
                futureOperations.RemoveAt(0);
                DrawAllAgain();
            }
        }
        /// <summary>
        /// Returns graphics to the last state.
        /// </summary>
        public void ReturnState()
        {
            if (operations.Count > 0)
            {
                graphics.Clear(Color.White);
                futureOperations.Insert(0, operations[operations.Count - 1]);
                operations.RemoveAt(operations.Count - 1);
                DrawAllAgain();
            }
        }
        /// <summary>
        /// Clear the graphic content.
        /// </summary>
        public void Clear()
        {
            operations.RemoveRange(0, operations.Count);
            graphics.Clear(Color.White);
        }
        /// <summary>
        /// Saves the graphic as image at the path.
        /// </summary>
        /// <param name="path">Path of save.</param>
        public void Save(string path)
        {
            if (path.EndsWith(".jpg"))
            {
                CreateBitmap().Save(path);
            }
            else if (path.EndsWith(".prgrgp"))
            {
            
            }
        }
        /// <summary>
        /// Draws a line from point a to point b.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="onlyEditor">Draw only at editor?</param>
        public void DrawLine(Pen p, Point a, Point b, bool onlyEditor)
        {
            graphics.DrawLine(p, a, b);
            if (!OnlyDrawningMode)
            {
                operations.Add(new GraphicOperation(p, a, b, onlyEditor, log));
                log.Write($"New line has been created from point {a.ToString()} to point {b.ToString()}.");
            }
        }
        /// <summary>
        /// Draws image at the specified position.
        /// </summary>
        /// <param name="path">Path to image.</param>
        /// <param name="x">X axis of position.</param>
        /// <param name="y">Y axis of position.</param>
        public void DrawImage(string path, int x, int y)
        {
            DrawImage(Image.FromFile(path), x, y);
        }
        /// <summary>
        /// Draws image at the specified position.
        /// </summary>
        /// <param name="img">Image to draw.</param>
        /// <param name="x">X axis of position.</param>
        /// <param name="y">Y axis of position.</param>
        public void DrawImage(Image img, int x, int y)
        {
            graphics.DrawImage(img, 0, 0, img.Width, img.Height);
            if (!OnlyDrawningMode)
            {
                operations.Add(new GraphicOperation(img, x, y, false, log));
                log.Write($"New image was created at location {x}, {y}.");
            }
        }
        /// <summary>
        /// Updates graphics using by this Graphic.
        /// </summary>
        /// <param name="newGraphics">New graphics.</param>
        public void UpdateGraphics(Graphics newGraphics)
        {
            graphics = newGraphics;
        }
        /// <summary>
        /// Exectues the graphic operation.
        /// </summary>
        /// <param name="operation">Operation to execute.</param>
        public void Execute(IGraphicOperation operation)
        {
            object[] parameters = operation.GetParameters();
            switch (operation.Operation)
            {
                case GraphicOperations.ImagesDrawning:
                    DrawImage(parameters[0] as Image, (int)parameters[1], (int)parameters[2]);
                    break;
                case GraphicOperations.DrawLine:
                    DrawLine(parameters[0] as Pen, (Point)parameters[1], (Point)parameters[2], (bool)parameters[3]);
                    break;
            }
        }
        /// <summary>
        /// Creates bitmap from this Graphic.
        /// </summary>
        public Bitmap CreateBitmap()
        {
            Bitmap bitmap = new Bitmap(Size.Width, Size.Height);
            Graphics graphicsOfBitmap = Graphics.FromImage(bitmap);
            foreach (GraphicOperation operation in operations)
            {
                if (operation.IsOnlyEditorOperation)
                {
                    continue;
                }
                object[] parameters = operation.GetParameters();
                switch (operation.Operation)
                {
                    case GraphicOperations.ImagesDrawning:
                        graphicsOfBitmap.DrawImage(parameters[0] as Image, (int)parameters[1], (int)parameters[2]);
                        break;
                    case GraphicOperations.DrawLine:
                        graphicsOfBitmap.DrawLine(parameters[0] as Pen, (Point)parameters[1], (Point)parameters[2]);
                        break;
                }
            }
            return bitmap;
        }
    }
}
