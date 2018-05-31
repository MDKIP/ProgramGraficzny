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
        public PixelArtEditor(int pixels, IPixelArtToolbox toolbox, IProject project, ILog log, INotificator notificator)
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
            else if (toolbox == null)
            {
                Exception error = new NullReferenceException(Factory.GetProgrammerErrorString("toolbox nie może być pusty.", true));
                log.Write(error.Message);
                throw error;
            }

            // Inicjalizacja komponentów.
            InitializeComponent();

            // Przypisywanie.
            this.pixels = pixels;
            this.toolbox = toolbox;
            this.notificator = notificator;
            this.project = project;
            this.log = log;
            bitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            topsPositions = new Point[pixels + 1, pixels + 1];
            colorMap = new Color[pixels, pixels];
        }

        public event EventHandler GraphicChanged;

        public bool HasProject { get => project != null; }
        public int RealPixelsPerEditorPixels { get; set; }

        private List<Point> coloredPixelsCoordinates = new List<Point>();
        private List<Point> coloredPixels = new List<Point>();
        private Bitmap bitmap;
        private Point[,] topsPositions;
        private Color[,] colorMap;
        private bool saveOperations = true;
        private int pixels;
        private IPixelArtToolbox toolbox;
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
            for (int yPos = 0, npY = 0; npY < pixels + 1; yPos += distance, npY++)
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
        private void DrawPixels()
        {
            saveOperations = false;
            foreach (Point pixelCoordiante in coloredPixelsCoordinates)
            {
                PaintPixel(pixelCoordiante, colorMap[pixelCoordiante.X, pixelCoordiante.Y]);
            }
            saveOperations = true;
        }
        private void PaintPixel(Point coordinates, Color color)
        {
            // Malowanie.
            Point topA = new Point(topsPositions[coordinates.X, coordinates.Y].X + 1, topsPositions[coordinates.X, coordinates.Y].Y + 1);
            Point topB = topsPositions[coordinates.X + 1, coordinates.Y + 1];
            DrawSquare(topA, topB, color);

            // Zapisywanie danych.
            if (saveOperations)
            {
                colorMap[coordinates.X, coordinates.Y] = color;
                coloredPixelsCoordinates.Add(coordinates); 
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

            OnGraphicChanged();
        }
        private Point GetClickCordinates(Point clickPos)
        {
            for (int yIndex = 0; yIndex < pixels; yIndex++)
            {
                for (int xIndex = 0; xIndex < pixels; xIndex++)
                {
                    if (topsPositions[xIndex, yIndex].IsSmallerThen(clickPos) && topsPositions[xIndex + 1, yIndex + 1].IsGratherThen(clickPos))
                    {
                        return new Point(xIndex, yIndex);
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
            if (path.EndsWith(".jpg"))
            {
                Bitmap image = new Bitmap(RealPixelsPerEditorPixels * pixels, RealPixelsPerEditorPixels * pixels);
                Graphics graphics = Graphics.FromImage(image);
                for (int y = 0, yIndex = 0; y < RealPixelsPerEditorPixels * pixels; y += RealPixelsPerEditorPixels, yIndex++)
                {
                    for (int x = 0, xIndex = 0; x < RealPixelsPerEditorPixels * pixels; x += RealPixelsPerEditorPixels, xIndex++)
                    {
                        if (coloredPixelsCoordinates.Contains(new Point(xIndex, yIndex)))
                        {
                            
                        }
                    }
                }
            }
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
                DrawPixels();
            }
            catch (Exception error)
            {
                notificator.Notify(error);
            }
        }
        private void pcbImage_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            // Pobieranie koordynatów kliknięcia.
            Point coordinates = GetClickCordinates(args.Location);

            log.Write($"Użytkownik kliknął pcbImage na pozycji {args.Location}. Koordynaty to {coordinates}.");

            // Zamalowywanie piksela.
            if (coordinates != new Point(-1, -1))
            {
                PaintPixel(coordinates, toolbox.GetColor()); 
            }
        }
    }
}
