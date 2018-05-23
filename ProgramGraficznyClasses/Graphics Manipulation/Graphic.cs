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
    /// <summary>
    /// Klasa do kontroli nad grafiką czyli do rysowania, zapisywania itd.
    /// </summary>
    public class Graphic : IGraphic
    {
        /// <summary>
        /// Standardowy konstruktor dla klasy Graphic.
        /// </summary>
        /// <param name="project">Projekt dla tej grafiki. Może być pusty.</param>
        /// <param name="graphics">Na tej Graphics będzie odbywać się rysowanie. Nie może być pusta.</param>
        /// <param name="log">ILog dla tego obiektu. Nie może być pusty.</param>
        public Graphic(IProject project, Graphics graphics, ILog log)
        {
            // Wywalanie wyjątków.
            if (log == null) // Jeżeli log jest pusty.
            {
                throw new NullReferenceException("ILog nie może być pusty.");
            }
            else if (graphics == null) // Jeżeli graphics jest puste.
            {
                log.Write("Graphics jest puste.", LogMessagesTypes.Error);
                throw new NullReferenceException("Graphics nie może być puste.");
            }

            log.Write("Nowa instancja Graphic została utworzona pomyślnie.", LogMessagesTypes.Detail);

            // Przypisywanie.
            this.log = log;
            this.graphics = graphics;
            IsConnectedWithProject = (project != null);
        }

        public event EventHandler GraphicChanged;

        /// <summary>
        /// Wielkość powierzchni rysowania.
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// Jeżeli jest true to Graphic nie będzie zapamiętywać operacji rysowania.
        /// </summary>
        public bool OnlyDrawningMode { get; set; }
        /// <summary>
        /// Czy ta grafika jest połączona z projektem?.
        /// </summary>
        public bool IsConnectedWithProject { get; private set; }

        private List<IGraphicOperation> operations = new List<IGraphicOperation>();
        private List<IGraphicOperation> futureOperations = new List<IGraphicOperation>();
        private Graphics graphics;
        private ILog log;

        /// <summary>
        /// Rysuje wszystko od nowa.
        /// </summary>
        public void DrawAllAgain()
        {
            log.Write("Rysowanie wszystkiego od nowa.", LogMessagesTypes.Important);

            // Wyczyszczenie grafiki.
            graphics.Clear(Color.White);

            OnlyDrawningMode = true; // Aby operacje rysowania nie były zapamiętywane.

            foreach (GraphicOperation operation in operations) // Wykonywanie wszystkich operacji rysowania.
            {
                Execute(operation);
            }

            OnlyDrawningMode = false;
        }
        /// <summary>
        /// Przywraca następny stan grafiki. Oczywiście tylko wtedy gdy jest następny stan.
        /// </summary>
        public void NextState()
        {
            log.Write("Przywracanie następnego stanu grafiki.", LogMessagesTypes.Important);

            if (futureOperations.Count > 0) // Jeżeli cokolwiek jest w przyszłych operacjach.
            {
                IGraphicOperation nextOperation = futureOperations[0]; // pierwsza przyszła operacja

                // Ustawianie i dorysowywanie operacji.
                operations.Add(nextOperation);
                futureOperations.RemoveAt(0);
                Execute(nextOperation);
            }
        }
        /// <summary>
        /// Przywraca grafikę do poprzedniego stanu. Oczywiście tylko wtedy gdy jest jakiś poprzedni stan.
        /// </summary>
        public void ReturnState()
        {
            log.Write("Przywracanie poprzedniego stanu grafki.", LogMessagesTypes.Important);

            if (operations.Count > 0) // Jeżeli jest jakiś poprzedni stan.
            {
                // Wyczyszczenie grafiki.
                graphics.Clear(Color.White);

                // Ustawianie i usuwanie operacji.
                futureOperations.Insert(0, operations[operations.Count - 1]); // wpisuje obecny operację na indeks 0 przyszłych operacji
                operations.RemoveAt(operations.Count - 1); // usuwa obecną operację z listy operacji przeprowadzonych
                DrawAllAgain();
            }
        }
        /// <summary>
        /// Wyczyszcza całą grafikę. Po tym nie działają metody NextState oraz ReturnState.
        /// </summary>
        public void Clear()
        {
            log.Write("Wyczyszczanie grafiki.", LogMessagesTypes.Important);

            // Wyczyszcznie przeszłych i przyszłych operacji.
            operations.RemoveRange(0, operations.Count);
            futureOperations.RemoveRange(0, futureOperations.Count);

            // Wyczyszcznie grafiki.
            graphics.Clear(Color.White);
        }
        /// <summary>
        /// Zapisuje grafikę na podanej ścieżce. Dostępne rozszerzenia to jpg.
        /// </summary>
        /// <param name="path">Ścieżka zapisu.</param>
        public void Save(string path)
        {
            log.Write($"Zapisywanie grafki na ścieżce ({path}).", LogMessagesTypes.Important);
            
            if (path.EndsWith(".jpg")) // Jeżeli format pliku ma być JPG.
            {
                // Zapisywanie Bitmapy jako JPG.
                CreateBitmap().Save(path);
            }
            else if (path.EndsWith(".prgrg")) // Jeżeli format pliku ma być PRGRG.
            {
                // Tu w przyszłości będzie skrypt zapisujący Graphic do pliku PRGRG z którego będzie można powrócić do edycji grafiki. 
            }
            else // Jeżeli żaden format pliku nie jest odpowiedni.
            {
                // Zapisywanie jako JPG.
                Save(path += ".jpg"); 
            }
        }
        /// <summary>
        /// Rysuje linię pędzlem p od punktu a do punktu b.
        /// </summary>
        /// <param name="p">Pędzel p.</param>
        /// <param name="a">Punkt a.</param>
        /// <param name="b">Punkt b.</param>
        /// <param name="onlyEditor">Czy rysować tylko w edytorze? (jeżeli tak to linie zostaną zignorowane podczas tworzenia bitmapy i zapisu pliku)</param>
        public void DrawLine(Pen p, Point a, Point b, bool onlyEditor)
        {
            // Rysowanie linii na graphics.
            graphics.DrawLine(p, a, b);

            if (!OnlyDrawningMode) // Jeżeli ma zapisać operację.
            {
                string onlyEditorString = onlyEditor ? "wyłącznie w edytorze" : "na grafice";
                log.Write($"Rysowanie nowej linni od punktu {a} do punktu {b} pędzlem o szerokości {p.Width} i kolorze {p.Color} {onlyEditorString}.", LogMessagesTypes.Detail);

                operations.Add(new GraphicOperation(p, a, b, onlyEditor, log)); // dodawanie operacji do istniejących operacji
            }
        }
        /// <summary>
        /// Rysuje obraz w określonej pozycji.
        /// </summary>
        /// <param name="path">Ścieżka do obrazu.</param>
        /// <param name="x">Pozycja w osi X.</param>
        /// <param name="y">Pozycja w osi Y.</param>
        public void DrawImage(string path, int x, int y)
        {
            DrawImage(Image.FromFile(path), x, y);
        }
        /// <summary>
        /// Rysuje obraz w określonej pozycji.
        /// </summary>
        /// <param name="img">Obraz do narysowania (Image).</param>
        /// <param name="x">Pozycja w osi X.</param>
        /// <param name="y">Pozycja w osi Y.</param>
        public void DrawImage(Image img, int x, int y)
        {
            // Rysowanie obrazu.
            graphics.DrawImage(img, 0, 0, img.Width, img.Height); 

            if (!OnlyDrawningMode) // Jeżeli ma zapisać operację.
            {
                operations.Add(new GraphicOperation(img, x, y, false, log));

                log.Write($"Nowy obraz został narysowany na pozycji {new Point(x, y)}.", LogMessagesTypes.Detail);
            }
        }
        /// <summary>
        /// Przeprowadza update graphics. Od teraz na tym graphics będzie odbywać się rysowanie.
        /// </summary>
        /// <param name="newGraphics">Nowe graphics.</param>
        public void UpdateGraphics(Graphics newGraphics)
        {
            log.Write("Został przeprowadzony update graphics.", LogMessagesTypes.Detail);

            // Przypisanie.
            graphics = newGraphics;
        }
        /// <summary>
        /// Wykonuje operację graficzną.
        /// </summary>
        /// <param name="operation">Operacja do wykonania.</param>
        public void Execute(IGraphicOperation operation)
        {
            log.Write("Operacja graficzna została wykonana.", LogMessagesTypes.Detail);

            // Pobieranie paramterów operacji graficznej.
            object[] parameters = operation.GetParameters();

            switch (operation.Operation) // Tu dla różnych typów operacji graficznych różnie interpretowne są argumenty.
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
        /// Tworzy bitmapę tej grafiki z narysowanymi jej elementami. Potrzebny Size.
        /// </summary>
        /// <returns>Bitmapa z narysowanymi elemtami.</returns>
        public Bitmap CreateBitmap()
        {
            log.Write("Tworzenie bitmapy.", LogMessagesTypes.Detail);

            // Tworzenie nowej bitmapy.
            Bitmap bitmap = new Bitmap(Size.Width, Size.Height);

            // Tworzenie graphics z bitmapy i rysowanie na niej elemtów tej grafiki.
            Graphics graphicsOfBitmap = Graphics.FromImage(bitmap);
            foreach (IGraphicOperation operation in operations)
            {
                if (operation.IsOnlyEditorOperation) // Jeżeli operacja dotyczy tylko edytora.
                {
                    continue; // następna operacja
                }

                // Pobieranie paramterów operacji graficznej.
                object[] parameters = operation.GetParameters(); 

                switch (operation.Operation) // Tu dla różnych typów operacji graficznych różnie interpretowne są argumenty.
                {
                    case GraphicOperations.ImagesDrawning:
                        graphicsOfBitmap.DrawImage(parameters[0] as Image, (int)parameters[1], (int)parameters[2]);
                        break;
                    case GraphicOperations.DrawLine:
                        graphicsOfBitmap.DrawLine(parameters[0] as Pen, (Point)parameters[1], (Point)parameters[2]);
                        break;
                }
            }

            // Zwracanie gotowej bitmapy.
            return bitmap;
        }
    }
}
