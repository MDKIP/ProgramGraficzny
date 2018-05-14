using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    /// <summary>
    /// Reprezentuje operację graficzną.
    /// </summary>
    public class GraphicOperation : IGraphicOperation
    {
        /// <summary>
        /// Konstruktor dla operacji graficznej która rysuje linię od punktu a do punktu b przy użyciu pędzla p.
        /// </summary>
        /// <param name="p">Pędzel p.</param>
        /// <param name="a">Punkt a.</param>
        /// <param name="b">Punkt b.</param>
        /// <param name="isOnlyEditorOperation">Czy operacja dotyczy tylko edytora?</param>
        /// <param name="log">Log dla tego obiektu.</param>
        public GraphicOperation(Pen p, Point a, Point b, bool isOnlyEditorOperation, ILog log)
        {
            log.Write("Nowa graficzna operacja została utworzona. Jej typ to DrawLine.", LogMessagesTypes.Detail);

            // Przypisywanie.
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
        /// Konstruktor dla operacji graficznej która rysuje obraz.
        /// </summary>
        /// <param name="img">Obraz do narysowania.</param>
        /// <param name="x">Pozycja w osi X.</param>
        /// <param name="y">Pozycja w osi Y.</param>
        /// <param name="isOnlyEditorOperation">Czy operacja dotyczy tylko edytora?</param>
        public GraphicOperation(Image img, int x, int y, bool isOnlyEditorOperation, ILog log)
        {
            log.Write("Nowa graficzna operacja została utworzona. Jej typ to ImagesDrawning.", LogMessagesTypes.Detail);

            // Przypisywanie.
            this.log = log;
            Operation = GraphicOperations.ImagesDrawning;
            IsOnlyEditorOperation = isOnlyEditorOperation;
            parameters = new object[3];
            parameters[0] = img;
            parameters[1] = x;
            parameters[2] = y;
        }

        /// <summary>
        /// Czy operacja dotyczy tylko edytora?
        /// </summary>
        public bool IsOnlyEditorOperation { get; private set; }
        /// <summary>
        /// Typ operacji graficznej.
        /// </summary>
        public GraphicOperations Operation { get; private set; }

        private object[] parameters;
        private ILog log;

        /// <summary>
        /// Zwraca parametry tej operacji graficznej.
        /// </summary>
        /// <returns>Parametry jako objecty.</returns>
        public object[] GetParameters()
        {
            log.Write("Operacja graficzna zwraca parametry.", LogMessagesTypes.Detail);

            return parameters;
        }
    }
}
