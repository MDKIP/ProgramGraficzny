using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class PixelArtEditor : Form, IGraphicEditorStandard, IProgramGraficznyForm
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
            pixelsValue = pixels;
            this.toolbox = toolbox;
            this.notificator = notificator;
            this.project = project;
            this.log = log;
            this.pixels = new PictureBox[pixelsValue, pixelsValue];

            // Tworzenie menu.
            MainMenu menu = new MainMenu();
            Menu = menu;
            MenuItem miFile = new MenuItem("Plik");
            miFile.MenuItems.Add(new MenuItem("Nowy", miNew_Click));
            miFile.MenuItems.Add(new MenuItem("Zapisz", miSave_Click));
            miFile.MenuItems.Add(new MenuItem("Wczytaj", miLoad_Click));
            MenuItem miSettings = new MenuItem("Ustawienia", miSettings_Click);
            miSettings.MenuItems.Add(new MenuItem("Blokuj Rozmiar Siatki", miBlockNetSize_Click));
            Menu.MenuItems.Add(miFile);
            Menu.MenuItems.Add(miSettings);

            Reload();
        }

        public event EventHandler GraphicChanged;

        public bool HasProject { get => project != null; }
        public int RealPixelsPerEditorPixels { get; set; }

        private Dictionary<PictureBox, PixelInfo> pixelsInfo = new Dictionary<PictureBox, PixelInfo>();
        private PictureBox[,] pixels;
        private List<IPixelArtOperation> executedOperations = new List<IPixelArtOperation>();
        private Stack<IPixelArtOperation> futureOperations = new Stack<IPixelArtOperation>();
        private bool fastPaint = false;
        private bool blockNetSize = false;
        private int pixelsValue;
        private int pixelsSpace = 2;
        private IPixelArtToolbox toolbox;
        private INotificator notificator;
        private IProject project;
        private ILog log;

        private void DrawPixels()
        {
            int minAxis = ClientSize.Width > ClientSize.Height ? ClientSize.Height : ClientSize.Width;
            int allSpace = pixelsSpace * pixelsValue;
            int spaceForPixels = minAxis - allSpace;
            int spaceForPixel = spaceForPixels / pixelsValue;

            for (int y = 0; y < pixelsValue; y++)
            {
                for (int x = 0; x < pixelsValue; x++)
                {
                    int xPos = x == 0 ? 0 : pixels[x - 1, y].Location.X + spaceForPixel + pixelsSpace;
                    int yPos = y == 0 ? 0 : pixels[x, y - 1].Location.Y + spaceForPixel + pixelsSpace;

                    PictureBox newPixel = new PictureBox()
                    {
                        Width = spaceForPixel,
                        Height = spaceForPixel,
                        Location = new Point(xPos, yPos),
                        BackColor = ProgramInfo.CurrentTheme.ButtonsColor,
                    };
                    newPixel.MouseDown += pixel_MouseDown;
                    newPixel.MouseEnter += pixel_MouseEnter;
                    Controls.Add(newPixel);
                    pixels[x, y] = newPixel;
                }
            }

            foreach (PictureBox pixel in pixels)
            {
                pixelsInfo.Add(pixel, new PixelInfo());
            }
        }
        private void DeleteAllPixels()
        {
            foreach (PictureBox pixel in pixels)
            {
                Controls.Remove(pixel);
                pixelsInfo.Remove(pixel);
                pixel.Dispose();
            }
        }
        private void PaintPixel(int x, int y, Color c, bool saveOperation)
        {
            PictureBox pixel = pixels[x, y];
            Color prevColor = pixel.BackColor;
            pixel.BackColor = c;

            pixelsInfo[pixel].IsColored = true;

            if (saveOperation)
            {
                futureOperations.Clear();
                executedOperations.Add(new PixelArtOperation(new Point(x, y), pixel.BackColor, prevColor));
            }
        }
        private Point FindCoordiantesOfPixel(PictureBox pixel)
        {
            for (int y = 0; y < pixelsValue; y++)
            {
                for (int x = 0; x < pixelsValue; x++)
                {
                    if (pixel == pixels[x, y]) return new Point(x, y);
                }
            }
            return new Point(-1, -1);
        }
           
        // Zaimplementowane z IGraphicEditorStandard.
        public void Clear()
        {
        }
        public void DrawAllAgain()
        {
            throw new NotImplementedException();
        }
        public void NextState()
        {
            if (futureOperations.Count <= 0) return;
            IPixelArtOperation operation = futureOperations.Pop();
            executedOperations.Add(operation);
            PaintPixel(operation.Coordiantes.X, operation.Coordiantes.Y, operation.AfterColor, false);
        }
        public void ReturnState()
        {
            if (executedOperations.Count <= 0) return;
            IPixelArtOperation operation = executedOperations[executedOperations.Count - 1];
            PixelInfo pixelInfo = pixelsInfo[pixels[operation.Coordiantes.X, operation.Coordiantes.Y]];

            executedOperations.RemoveAt(executedOperations.Count - 1);
            futureOperations.Push(operation);

            PaintPixel(operation.Coordiantes.X, operation.Coordiantes.Y, operation.BeforeColor, false);

            if (pixelInfo.ExecutedOperations.Count >= 1)
            {
                pixelInfo.ExecutedOperations.RemoveAt(pixelInfo.ExecutedOperations.Count - 1);
            }

            if (pixelInfo.ExecutedOperations.Count <= 1)
            {
                pixelInfo.IsColored = false;
            }
        }
        public void Save(string path)
        {
            if (path.EndsWith(".jpg"))
            {
                CreateBitmap().Save(path);
            }
            else if (path.EndsWith(".jpeg"))
            {
                CreateBitmap().Save(path, ImageFormat.Jpeg);
            }
            else if (path.EndsWith(".png"))
            {
                CreateBitmap().Save(path, ImageFormat.Png);
            }
            else if (path.EndsWith(".bmp"))
            {
                CreateBitmap().Save(path, ImageFormat.Bmp);
            }
            else if (path.EndsWith(".emf"))
            {
                CreateBitmap().Save(path, ImageFormat.Emf);
            }
            else if (path.EndsWith(".exif"))
            {
                CreateBitmap().Save(path, ImageFormat.Exif);
            }
            else if (path.EndsWith(".gif"))
            {
                CreateBitmap().Save(path, ImageFormat.Gif);
            }
            else if (path.EndsWith(".ico"))
            {
                CreateBitmap().Save(path, ImageFormat.Icon);
            }
            else if (path.EndsWith(".tiff"))
            {
                CreateBitmap().Save(path, ImageFormat.Tiff);
            }
            else if (path.EndsWith(".wmf"))
            {
                CreateBitmap().Save(path, ImageFormat.Wmf);
            }
            else if (path.EndsWith(".pgpa"))
            {
                List<string> lines = new List<string>();

                for (int y = 0; y < pixelsValue; y++)
                {
                    string line = "";

                    for (int x = 0; x < pixelsValue; x++)
                    {
                        if (x != 0)
                        {
                            line += '@' + pixels[x, y].BackColor.ToArgb().ToString();
                        }
                        else
                        {
                            line = pixels[x, y].BackColor.ToArgb().ToString();
                        }
                    }

                    lines.Add(line);
                }

                File.WriteAllLines(path, lines);
            }
        }
        public void LoadFile(string path)
        {
            if (!path.EndsWith(".pgpa")) return;

            string[] lines = File.ReadAllLines(path);
            pixelsValue = lines.Length;
            DeleteAllPixels();
            DrawPixels();

            int x = 0, y = 0;
            foreach (string line in lines)
            {
                string[] colorsAsStrings = line.Split('@');

                foreach (string colorAsString in colorsAsStrings)
                {
                    Color color = Color.FromArgb(int.Parse(colorAsString));
                    Color pixelColor = color.GetWithAlpha(255);

                    bool isEmpty = (pixelColor == Color.Empty);

                    pixels[x, y].BackColor = isEmpty ? ProgramInfo.CurrentTheme.ButtonsColor : pixelColor;
                    pixelsInfo[pixels[x, y]].ExecutedOperations.Clear();
                    pixelsInfo[pixels[x, y]].IsColored = !isEmpty;

                    x++;
                }
                x = 0;
                y++;
            }
        }
        public Bitmap CreateBitmap()
        {
            Bitmap bitmap = new Bitmap(RealPixelsPerEditorPixels * pixelsValue, RealPixelsPerEditorPixels * pixelsValue);
            Graphics g = Graphics.FromImage(bitmap);

            for (int y = 0, yIndex = 0; y < RealPixelsPerEditorPixels * pixelsValue; y += RealPixelsPerEditorPixels, yIndex++)
            {
                for (int x = 0, xIndex = 0; x < RealPixelsPerEditorPixels * pixelsValue; x += RealPixelsPerEditorPixels, xIndex++)
                {
                    if (pixelsInfo[pixels[xIndex, yIndex]].IsColored)
                    {
                        g.FillRectangle(new SolidBrush(pixels[xIndex, yIndex].BackColor), x, y, RealPixelsPerEditorPixels, RealPixelsPerEditorPixels); 
                    }
                }
            }

            return bitmap;
        }
        // Zaimplementowane z IProgramGraficznyForm
        public void Reload()
        {
            BackColor = ProgramInfo.CurrentTheme.BackgroundColor;

            foreach (PictureBox pixel in pixels)
            {
                if (pixel == null) continue;

                if (!pixelsInfo[pixel].IsColored)
                {
                    pixel.BackColor = ProgramInfo.CurrentTheme.ButtonsColor;
                }
            }
        }
        // Do eventów.
        protected void OnGraphicChanged()
        {
            if (GraphicChanged != null)
            {
                GraphicChanged(this, new EventArgs()); 
            }
        }

        private void PixelArtEditor_Load(object sender, EventArgs e)
        {
            DrawPixels();
        }
        private void PixelArtEditor_ResizeEnd(object sender, EventArgs e)
        {
            if (!blockNetSize)
            {
                for (int y = 0; y < pixelsValue; y++)
                {
                    for (int x = 0; x < pixelsValue; x++)
                    {
                        PictureBox currentPixel = pixels[x, y];
                        int minAxis = ClientSize.Width > ClientSize.Height ? ClientSize.Height : ClientSize.Width;
                        int allSpace = pixelsSpace * pixelsValue;
                        int spaceForPixels = minAxis - allSpace;
                        int spaceForPixel = spaceForPixels / pixelsValue;
                        int xPos = x == 0 ? 0 : pixels[x - 1, y].Location.X + spaceForPixel + pixelsSpace;
                        int yPos = y == 0 ? 0 : pixels[x, y - 1].Location.Y + spaceForPixel + pixelsSpace;
                        currentPixel.Width = spaceForPixel;
                        currentPixel.Height = spaceForPixel;
                        currentPixel.Location = new Point(xPos, yPos);
                    }
                } 
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
                    case Keys.A:
                        {
                            fastPaint = !fastPaint;
                        }
                        break;
                }
            }
        }
        private void pcbImage_Click(object sender, EventArgs e)
        {
        }
        private void pixel_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pixel = sender as PictureBox;
            Point coordinates = FindCoordiantesOfPixel(pixel);

            if (e.Button == MouseButtons.Left)
            {
                PaintPixel(coordinates.X, coordinates.Y, toolbox.GetColor(), true); 
            }
            else if (e.Button == MouseButtons.Right)
            {
                PaintPixel(coordinates.X, coordinates.Y, ProgramInfo.CurrentTheme.ButtonsColor, true);
                pixelsInfo[pixel].IsColored = false;
            }
        }
        private void pixel_MouseEnter(object sender, EventArgs e)
        {
            if (fastPaint)
            {
                Point coordinates = FindCoordiantesOfPixel(sender as PictureBox);
                PaintPixel(coordinates.X, coordinates.Y, toolbox.GetColor(), true);
            }
        }
        private void miSettings_Click(object sneder, EventArgs e)
        {

        }
        private void miSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Program Graficzny Piksel Art (*.pgpa) |.pgpa" +
                "|JPG (*.jpg) |.jpg" +
                "|Joint Photographic Experts Group (*.jpeg) |.jpeg" +
                "|Portable Network Graphics (*.png) |.png" +
                "|Bitmap (*.bmp) |.bmp" +
                "|Enhanced Metafile (*.emf) |.emf" +
                "|Exchangeable File (*.exif) |.exif" +
                "|Graphics Interchange Format (*.gif) |.gif" +
                "|Windows Icon (*.ico) |.ico" +
                "|Tagged Image File Format (*.tiff) |.tiff" +
                "|Windows Metafile (*.wmf) |.wmf";
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
        private void miBlockNetSize_Click(object sender, EventArgs e)
        {
            MenuItem miBlockNetSize = sender as MenuItem;

            blockNetSize = miBlockNetSize.Checked = !miBlockNetSize.Checked;
        }
    }
}
