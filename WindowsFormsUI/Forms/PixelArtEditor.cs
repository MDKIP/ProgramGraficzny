using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
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

            // Tworzenie menu.
            MainMenu menu = new MainMenu();
            Menu = menu;
            MenuItem miFile = new MenuItem("Plik");
            miFile.MenuItems.Add(new MenuItem("Nowy", miNew_Click));
            miFile.MenuItems.Add(new MenuItem("Zapisz", miSave_Click));
            miFile.MenuItems.Add(new MenuItem("Wczytaj", miLoad_Click));
            MenuItem miSettings = new MenuItem("Ustawienia", miSettings_Click);
            Menu.MenuItems.Add(miFile);
            Menu.MenuItems.Add(miSettings);
        }

        public event EventHandler GraphicChanged;

        public bool HasProject { get => project != null; }
        public int RealPixelsPerEditorPixels { get; set; }

        private List<Point> coloredPixelsCoordinates = new List<Point>();
        private List<Point> coloredPixels = new List<Point>();
        private List<IPixelArtOperation> executedOperations = new List<IPixelArtOperation>();
        private Stack<IPixelArtOperation> futureOperations = new Stack<IPixelArtOperation>();
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
            // Zapisywanie danych.
            if (saveOperations)
            {
                coloredPixelsCoordinates.Add(coordinates);
                executedOperations.Add(new PixelArtOperation(coordinates, color, GetColorOfPixel(coordinates)));
                futureOperations.Clear();
            }

            // Malowanie.
            Point topA = new Point(topsPositions[coordinates.X, coordinates.Y].X + 1, topsPositions[coordinates.X, coordinates.Y].Y + 1);
            Point topB = topsPositions[coordinates.X + 1, coordinates.Y + 1];
            DrawSquare(topA, topB, color);
            colorMap[coordinates.X, coordinates.Y] = color;
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
        private Color GetColorOfPixel(Point coordinates)
        {
            Point p = new Point(topsPositions[coordinates.X, coordinates.Y].X + 1, topsPositions[coordinates.X, coordinates.Y].Y + 1);
            return bitmap.GetPixel(p.X, p.Y);
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
            // Zapobieganie błędom.
            //notificator.Notify(futureOperations.Count.ToString());
            if (futureOperations.Count == 0) return;
            

            // Zapisywanie danych.
            IPixelArtOperation operation = futureOperations.Pop();
            executedOperations.Add(operation);

            // Zmiany w edytorze.
            saveOperations = false;
            PaintPixel(operation.Coordiantes, operation.AfterColor);
            saveOperations = true;
            OnGraphicChanged();
        }
        public void ReturnState()
        {
            // Przypisywanie zmiennych i zapobieganie błędom.
            int lastIndex = executedOperations.Count - 1;
            if (lastIndex < 0) return;
            //notificator.Notify(futureOperations.Count.ToString());

            // Zapisywanie danych.
            IPixelArtOperation operation = executedOperations[lastIndex];
            executedOperations.RemoveAt(lastIndex);
            futureOperations.Push(operation);

            // Zmiany w edytorze.
            saveOperations = false;
            PaintPixel(operation.Coordiantes, operation.BeforeColor);
            saveOperations = true;
            OnGraphicChanged();
        }
        public void Save(string path)
        {
            if (path.EndsWith(".jpg"))
            {
                CreateBitmap().Save(path);
            }
            else if (path.EndsWith(".pgpa"))
            {
                List<string> lines = new List<string>();

                for (int y = 0; y < pixels; y++)
                {
                    string line = "";

                    for (int x = 0; x < pixels; x++)
                    {
                        if (x != 0)
                        {
                            line += "@" + colorMap[x, y].ToArgb(); 
                        }
                        else
                        {
                            line += colorMap[x, y].ToArgb();
                        }
                    }

                    lines.Add(line);
                }

                File.WriteAllLines(path, lines);
            }
        }
        public void LoadFile(string path)
        {
            if (path.EndsWith(".pgpa"))
            {
                string[] lines = File.ReadAllLines(path);
                pixels = lines.Length;
                int x = 0, y = 0;
                colorMap = new Color[pixels, pixels];
                topsPositions = new Point[pixels + 1, pixels + 1];

                foreach (string line in lines)
                {
                    //notificator.Notify("d");
                    string[] colors = line.Split('@');

                    foreach (string color in colors)
                    {
                        colorMap[x, y] = Color.FromArgb(int.Parse(color));
                        if (!colorMap[x, y].IsEmpty)
                        {
                            coloredPixelsCoordinates.Add(new Point(x, y));
                        }

                        x++;
                    }
                    x = 0;
                    y++;
                }

                Clear();
                DrawNet();
                DrawPixels();
                OnGraphicChanged();
            }
        }
        public Bitmap CreateBitmap()
        {
            Bitmap image = new Bitmap(RealPixelsPerEditorPixels * pixels, RealPixelsPerEditorPixels * pixels);
            Graphics graphics = Graphics.FromImage(image);

            for (int yIndex = 0, yPos = 0; yIndex < pixels; yIndex++, yPos += RealPixelsPerEditorPixels)
            {
                for (int xIndex = 0, xPos = 0; xIndex < pixels; xIndex++, xPos += RealPixelsPerEditorPixels)
                {
                    Brush b = new SolidBrush(colorMap[xIndex, yIndex]);
                    Size s = new Size(RealPixelsPerEditorPixels, RealPixelsPerEditorPixels);
                    Rectangle r = new Rectangle(new Point(xPos, yPos), s);
                    graphics.FillRectangle(b, r);
                }
            }

            return image;
        }
        // Do eventów.
        protected void OnGraphicChanged()
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
        private void PixelArtEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
               
                switch (e.KeyCode)
                {
                    case Keys.Z:
                        {
                            ReturnState();
                        }
                        break;
                    case Keys.X:
                        {
                            NextState();
                        }
                        break;
                    case Keys.S:
                        {
                            FormsManager.ShowGraphicsVisualizer(CreateBitmap());
                        }
                        break;
                }
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
                if (args.Button == MouseButtons.Left)
                {
                    PaintPixel(coordinates, toolbox.GetColor());
                }
                else if (args.Button == MouseButtons.Right)
                {
                    PaintPixel(coordinates, Color.Empty);
                }
            }
        }
        private void miSettings_Click(object sneder, EventArgs e)
        {

        }
        private void miSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Program Graficzny Piksel Art (*.pgpa) |.pgpa|JPG files (*.jpg) |.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Save(dialog.FileName);
            }
        }
        private void miLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "Program Graficzny Piksel Art (*.pgpa) |.pgpa";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadFile(dialog.FileName);
            }
        }
        private void miNew_Click(object sender, EventArgs e)
        {
            FormsManager.ShowNewGraphicForm(null);
        }
    }
}
