using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public class GraphicOperation : IGraphicOperation
    {
        /// <summary>
        /// Constructor for graphic operation which draws line form point a to point b.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="isOnlyEditorOperation"></param>
        /// <param name="log"></param>
        public GraphicOperation(Pen p, Point a, Point b, bool isOnlyEditorOperation, ILog log)
        {
            log.Write("New instance of GraphicOperation was created and it's type is DrawLine.");
            this.log = log;
            Operation = GraphicOperations.DrawLine;
            IsOnlyEditorOperation = isOnlyEditorOperation;
            parameters = new object[4];
            parameters[0] = p;
            parameters[1] = a;
            parameters[2] = b;
            parameters[3] = isOnlyEditorOperation;
        }
        /// <summary>
        /// Constructor for graphic operation which draws image.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isOnlyEditorOperation"></param>
        public GraphicOperation(Image img, int x, int y, bool isOnlyEditorOperation, ILog log)
        {
            log.Write("New instance of GraphicOperation was created and it's type is ImagesDrawning.");
            this.log = log;
            Operation = GraphicOperations.ImagesDrawning;
            IsOnlyEditorOperation = isOnlyEditorOperation;
            parameters = new object[3];
            parameters[0] = img;
            parameters[1] = x;
            parameters[2] = y;
        }

        public bool IsOnlyEditorOperation { get; private set; }
        public GraphicOperations Operation { get; private set; }

        private object[] parameters;
        private ILog log;

        /// <summary>
        /// Returns the paramters used to this graphic operation.
        /// </summary>
        /// <returns></returns>
        public object[] GetParameters()
        {
            return parameters;
        }
    }
}
