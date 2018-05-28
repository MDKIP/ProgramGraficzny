using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class PixelArtEditor : Form, IGraphicEditorStandard
    {
        public PixelArtEditor(int pixels, IProject project, ILog log, INotificator notificator)
        {
            // Wywalanie wyjątków.
            if (log == null && notificator == null)
            {
                throw new NullReferenceException("log i notificator są puste");
            }
            else if (log == null)
            {
                Exception error = new NullReferenceException(Factory.GetProgrammerErrorString("log nie może być pusty.", true));
                notificator.Notify(error);
                throw error;
            }
            else if (notificator == null)
            {
                Exception error = new NullReferenceException(Factory.GetProgrammerErrorString("notificator nie może być pusty.", true));
                log.Write(error.Message);
                throw error;
            }

            // Inicjalizacja komponentów.
            InitializeComponent();

            // Przypisywanie.
            this.pixels = pixels;
            this.notificator = notificator;
            this.project = project;
            this.log = log;
            bitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            topsPositions = new Point[pixels + 1, pixels + 1];
            colorMap = new int[pixels, pixels];
        }

        public event EventHandler GraphicChanged;

        public bool HasProject { get => project != null; }

        private List<Point> coloredPixels = new List<Point>();
        private Bitmap bitmap;
        private Point[,] topsPositions;
        private int[,] colorMap;
        private int pixels;
        private INotificator notificator;
        private IProject project;
        private ILog log;

        private void DrawNet()
        {
            // Oblicznie odległości między liniami w osiach X i Y.
            int minusConst = 50;
            int xDistance = (Size.Width - minusConst) / pixels;
            int yDistance = (Size.Height - minusConst) / pixels;
            int distance = (xDistance >= yDistance) ? yDistance : xDistance;

            // Oblicznie granicy rysowania.
            int nonDrawBorder = (xDistance < yDistance) ? xDistance * pixels : yDistance * pixels;

            // Rysowanie pojedynczych linni pionowych.
            for (int l = 0, nl = 0; nl < pixels + 1; l += distance, nl++)
            {
                DrawStraightLine(new Point(l, 0), new Point(l, nonDrawBorder));
            }

            // Rysowanie pojedynczych linni poziomych.
            for (int l = 0, nl = 0; nl < pixels + 1; l += distance, nl++)
            {
                DrawStraightLine(new Point(0, l), new Point(nonDrawBorder, l));
            }

            // Obliczanie pozycji wierzchołków i zapisywanie tych danych do topsPositions.
            for (int yPos = 0, npY = 0; npY < pixels + 1; yPos -+= distance, npY++)
            {
                for (int xPos = 0, npX = 0; npX < pixels + 1; xPos += distance, npX++)
                {
                    topsPositions[npX, npY] = new Point(xPos, yPos);
                }
            }

            // Informowanie o zmianie grafiki.
            OnGraphicChanged();

            // Metody lokalne.
            void DrawStraightLine(Point a, Point b)
            {
                // Wybieranie osi w której linnia ma być prosta.
                if (a.X == b.X)
                {
                    // Szukanie mniejszej osi Y w punktach.
                    if (a.Y <= b.Y)
                    {
                        // Ustawianie pojedynczych pikseli.
                        for (int i = a.Y; i < b.Y; i++)
                        {
                            bitmap.SetPixel(a.X, i, Color.Black);
                            coloredPixels.Add(new Point(a.X, i));
                        }
                    }
                    else
                    {
                        // Ustawianie pojedynczych pikseli.
                        for (int i = b.Y; i < a.Y; i++)
                        {
                            bitmap.SetPixel(a.X, i, Color.Black);
                            coloredPixels.Add(new Point(a.X, i));
                        }
                    }
                }
                else if (a.Y == b.Y)
                {
                    // Szukanie mniejszej osi X w punktach.
                    if (a.X <= b.X)
                    {
                        // Ustawianie pojedynczych pikseli.
                        for (int i = a.X; i < b.X; i++)
                        {
                            bitmap.SetPixel(i, a.Y, Color.Black);
                            coloredPixels.Add(new Point(i, a.Y));
                        }
                    }
                    else
                    {
                        // Ustawianie pojedynczych pikseli.
                        for (int i = b.X; i < a.X; i++)
                        {
                            bitmap.SetPixel(i, a.Y, Color.Black);
                            coloredPixels.Add(new Point(i, a.Y));
                        }
                    }
                }
            }
        }
        private void DrawSquare(Point topA, Point topB, Color color)
        {
            // Dobieranie większych i mniejszych wartości osi X i Y.
            int minX = (topA.X <= topB.X) ? topA.X : topB.X;
            int minY = (topA.Y <= topB.Y) ? topA.Y : topB.Y;
            int maxX = (topA.X >= topB.X) ? topA.X : topB.X;
            int maxY = (topA.Y >= topB.Y) ? topA.Y : topB.Y;

            // Iterowanie przez każdy piksel w kwadracie.
            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    // Ustawianie koloru.
                    bitmap.SetPixel(x, y, color);
                    coloredPixels.Add(new Point(x, y));
                }
            }
        }
        private Point GetClickCordinates(Point clickPos)
        {
            notificator.Notify(clickPos.ToString());
            //notificator.Notify(topsPositions.Length.ToString());
            for (int yIndex = 0; yIndex < pixels; yIndex++)
            {
                for (int xIndex = 0; xIndex < pixels; xIndex++)
                {
                    //notificator.Notify($"yIndex: {yIndex}   xIndex: {xIndex}");
                    notificator.Notify($"current point: {topsPositions[xIndex, yIndex]}{Environment.NewLine}next point: {topsPositions[xIndex + 1, yIndex + 1]}");
                    if (topsPositions[xIndex, yIndex].IsGratherThen(clickPos) && topsPositions[xIndex + 1, yIndex + 1].IsSmallerThen(clickPos))
                    {
                        Point output = new Point(xIndex, yIndex);
                        notificator.Notify($"OUTPUT: {output}");
                        return output;
                    }
                }
            }
            return new Point(-1, -1);
        }
        // Zaimplementowane z IGraphicEditorStandard.
        public void Clear()
        {
            // Wyczyszczanie każdego pokolorowaengo piksela. (robię to w while bo w foreach nie mogę modyfikować kolekcji)
            while (coloredPixels.Count != 0)
            {
                Point pixel = coloredPixels[0];
                bitmap.SetPixel(pixel.X, pixel.Y, Color.White);
                coloredPixels.RemoveAt(0);
            }
        }
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
        public void Save(string path)
        {
            throw new NotImplementedException();
        }
        public Bitmap CreateBitmap()
        {
            return bitmap;
        }
        // Do eventów.
        private void OnGraphicChanged()
        {
            if (GraphicChanged != null)
            {
                GraphicChanged(this, new EventArgs()); 
            }

            pcbImage.Image = bitmap;
        }

        private void PixelArtEditor_Load(object sender, EventArgs e)
        {
            Text = "Edytor Piksel Artów";

            DrawNet();
        }
        private void PixelArtEditor_ResizeEnd(object sender, EventArgs e)
        {
            try
            {
                Clear();
                DrawNet();
            }
            catch (Exception error)
            {
                notificator.Notify(error);
            }
        }
        private void pcbImage_Click(object sender, EventArgs e)
        {
            int xOfNewClick = MousePosition.X - Location.X - 5;
            int yOfNewClick = MousePosition.Y - Location.Y - 50;
            Point locationOfClick = new Point(xOfNewClick, yOfNewClick);
            notificator.Notify(GetClickCordinates(locationOfClick).ToString());
        }
    }
}
