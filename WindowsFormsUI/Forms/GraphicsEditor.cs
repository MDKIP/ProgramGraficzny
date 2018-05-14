using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class GraphicsEditor : Form
    {
        /// <summary>
        /// Standardowy konstruktor dla GraphicsEditor.
        /// </summary>
        /// <param name="projectForThisGraphics">Projekt dla tego edytora graficznego. Może być pusty.</param>
        /// <param name="toolboxForThisEditor">Toolbox z którego ten edytor będzie pobierać informacje.</param>
        /// <param name="type">Typ tej grafiki.</param>
        /// <param name="log">Log dla tego edytora graficznego.</param>
        /// <param name="plusParams">Dodatkowe parametry zależne od typu grafiki.</param>
        public GraphicsEditor(IProject projectForThisGraphics, IToolbox toolboxForThisEditor, GraphicTypes type, ILog log, object[] plusParams)
        {
            // Wywalanie wyjątków.
            if (log == null) // Jeżeli log jest pusty.
            {
                throw new NullReferenceException("Log nie może być pusty.");
            }
            else if (toolboxForThisEditor == null) // Jeżeli toolbox jest pusty.
            {
                throw new NullReferenceException("Toolbox nie może być pusty.");
            }

            log.Write("Nowa instancja edytora graficznego została utworzona pomyślnie.", LogMessagesTypes.Detail);

            // Inicjalizacja komponentów.
            InitializeComponent();

            // Przypisywanie.
            this.log = log;
            standardEditor = Graphic = new Graphic(projectForThisGraphics, pcbWorkSpace.CreateGraphics(), log);
            toolbox = toolboxForThisEditor;
            Project = projectForThisGraphics;
            Type = type;

            // Tworzenie Menu Głównego.
            MainMenu mainMenu = new MainMenu();
            MenuItem fileItem = new MenuItem("Plik");
            MenuItem newItem = new MenuItem("Nowy", newItem_Click);
            MenuItem saveItem = new MenuItem("Zapisz", saveItem_Click);
            MenuItem graphicItem = new MenuItem("Grafika");
            MenuItem addImageItem = new MenuItem("Dodaj zdjęcie", addImageItem_Click);
            mainMenu.MenuItems.Add(fileItem);
            fileItem.MenuItems.Add(newItem);
            fileItem.MenuItems.Add(saveItem);
            mainMenu.MenuItems.Add(graphicItem);
            graphicItem.MenuItems.Add(addImageItem);
            Menu = mainMenu;

            // Instrukcje dla PixelArt'ów.
            if (type == GraphicTypes.PixelArt) // Jeżeli grafika jest pixel artem.
            {
                SetWorkSpaceSize(new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height));
                sizeOfPixelArt = (int)plusParams[0];
                DrawLinesForPixelArt();
            }
        }

        /// <summary>
        /// Projekt w którym znajduje się ta grafika. Może być pusty.
        /// </summary>
        public IProject Project { get; private set; }
        /// <summary>
        /// IGraphic tego graficznego edytora.
        /// </summary>
        public IGraphic Graphic { get; private set; }
        /// <summary>
        /// Typ tej grafiki.
        /// </summary>
        public GraphicTypes Type { get; private set; }

        private int sizeOfPixelArt;
        private IGraphicEditorStandard standardEditor;
        private IPixelArtController pixelArtController;
        private IToolbox toolbox;
        private ILog log;

        /// <summary>
        /// Rysuje siatkę dla PixelArtu.
        /// </summary>
        private void DrawLinesForPixelArt()
        {
            DrawLinesForPixelArt(sizeOfPixelArt);
        }
        /// <summary>
        /// Rysuje siatkę dla PixelArtu z określoną ilością bajtów (w budowie).
        /// </summary>
        /// <param name="bytes">Bajty tego PixelArtu. Np 16, 32, 64</param>
        private void DrawLinesForPixelArt(int bytes)
        {
            log.Write($"Rysowanie linii dla piksel artów dla rozmiaru {bytes}.", LogMessagesTypes.Important);
            if (bytes <= 0) return;
            int xDistance = Size.Width / bytes - 5;
            int yDistance = Size.Height / bytes - 5;
            Point pos = new Point(0, 0);
            Pen blackPen = new Pen(Color.Black, 2);
            if (xDistance >= yDistance)
            {
                log.Write("Dostosywanie linii do rozmiaru osi Y.", LogMessagesTypes.Detail);
                int nonDrawBorder = bytes * yDistance;
                for (int i = 0; i < bytes + 1; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(pos.X, Size.Height), true);
                    pos.X += yDistance;
                }
                pos.X = 0;
                for (int i = 0; i < bytes + 1; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(Size.Width, pos.Y), true);
                    pos.Y += yDistance;
                }
            }
            else
            {
                log.Write("Dostosywanie linii do rozmiaru osi X.", LogMessagesTypes.Detail);
                int nonDrawBorder = bytes * yDistance;
                for (int i = 0; i < bytes + 1; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(pos.X, Size.Height), true);
                    pos.X += xDistance;
                }
                pos.X = 0;
                for (int i = 0; i < bytes + 1; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(Size.Width, pos.Y), true);
                    pos.Y += xDistance;
                }
            }
        }
        /// <summary>
        /// Zwraca koordynaty na siatce PixelArtu z pozycji kliknięcia. (w budowie)
        /// </summary>
        /// <param name="click">Punkt kliknięcia.</param>
        /// <returns>Koordynaty.</returns>
        private Point GetCordinatesForPixelArt(Point click)
        {
            if (Type != GraphicTypes.PixelArt) return new Point();
            throw new NotImplementedException();
        }
        /// <summary>
        /// Ustawia rozmiar pola roboczego.
        /// </summary>
        /// <param name="s">Nowa wielokość pola roboczego.</param>
        public void SetWorkSpaceSize(Size s)
        {
            log.Write($"Rozmiar pola roboczego został ustawiony na {s.ToString()}.");
            pcbWorkSpace.Size = s;
            Graphic.UpdateGraphics(pcbWorkSpace.CreateGraphics());
            Graphic.Size = s;
        }

        private void GraphicsEditor_Load(object sender, EventArgs e)
        {
            log.Write("GraphicsEditor został załadowny.", LogMessagesTypes.Important);

            // Ustawianie tekstu nagłówka.
            Text = "Edytor";
        }
        private void GraphicsEditor_SizeChanged(object sender, EventArgs e)
        {
            if (Type == GraphicTypes.Empty || Type == GraphicTypes.Image)
            {
                //Graphic.DrawAllAgain(); 
            }
            else if (Type == GraphicTypes.PixelArt)
            {
                
            }
        }
        private void GraphicsEditor_ResizeEnd(object sender, EventArgs e)
        {
            log.Write("Zmiana rozmiaru okna GraphicsEditor została zakończona.", LogMessagesTypes.Important);
            if (Type == GraphicTypes.Empty || Type == GraphicTypes.Image)
            {
                Graphic.DrawAllAgain(); 
            }
            else if (Type == GraphicTypes.PixelArt)
            {
                standardEditor.Clear();
                DrawLinesForPixelArt();
            }
        }
        private void GraphicsEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                log.Write("Użytkownik nacisnął przycisk Z.", LogMessagesTypes.Important);
                Graphic.ReturnState();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                log.Write("Użytkownik nacisnął przycisk X.", LogMessagesTypes.Important);
                Graphic.NextState();
            }
        }
        private void GraphicsEditor_LocationChanged(object sender, EventArgs e)
        {
            if (Graphic != null) // Jeżeli grafika nie jest pusta.
            {
                Graphic.DrawAllAgain(); // (doskonale wiem jak bardzo jest to nieoptymalne rozwiązanie, zostanie przeze mnie zmodyfikowane w przyszłości)
            }
        }
        private void GraphicsEditor_Shown(object sender, EventArgs e)
        {
            Graphic.DrawAllAgain();
        }
        private void GraphicsEditor_Click(object sender, EventArgs e)
        {
        }
        private void pcbWorkSpace_Paint(object sender, PaintEventArgs e)
        {
            Graphic.DrawAllAgain();
        }
        private void pcbWorkSpace_Click(object sender, EventArgs e)
        {
            // Pobieranie lokalizacji kursora (muszę to regulować tymi stałymi z jeszcze nieznanego mi powodu)
            int x = MousePosition.X - Location.X - 5;
            int y = MousePosition.Y - Location.Y - 50;
            Point location = new Point(x, y);

            log.Write($"Pole robocze zostało klinkniętę na pozycji ({location.ToString()}).");

            if (Type == GraphicTypes.Image || Type == GraphicTypes.Empty) // Jeżeli typ grafki to Image lub Empty.
            {
                switch (toolbox.CurrentTool) // Różne zachowania dla różnych narzędzi.
                {
                    case Tools.DrawLine:
                        Graphic.ReturnState();
                        Graphic.DrawLine(toolbox.CurrentPen, location, new Point(0, 0), false);
                        break;
                } 
            }
            else if (Type == GraphicTypes.PixelArt) // Jeżeli typ grafki to PixelArt.
            {
                
            }
        }
        private void pcbWorkSpace_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (isInDrawningMode)
            {
                int x = Cursor.Position.X - Location.X;
                int y = Cursor.Position.Y - Location.Y;
                Point location = new Point(x, y);

                switch (toolbox.CurrentTool)
                {
                    case Tools.DrawLine:
                        Graphic.DrawLine(toolbox.CurrentPen, location, new Point(0, 0), false);
                        break;
                } 
            }
            */
        }
        private void newItem_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce utworzyć nową grafikę.", LogMessagesTypes.Important);
            NewGraphicForm dialog = new NewGraphicForm(Project, log);
            dialog.Show();
        }
        private void saveItem_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce zapisać grafikę.", LogMessagesTypes.Important);
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                if (!path.EndsWith(".jpg"))
                {
                    path += ".jpg";
                }
                standardEditor.Save(path);
                log.Write($"Grafika została zapisana na ścieżce ({path}).");
            }
        }
        private void addImageItem_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce dodać obraz do grafiki.");
            if (Type == GraphicTypes.Empty || Type == GraphicTypes.Image)
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() == DialogResult.OK)
                {
                    log.Write($"Nowy obraz został dodany z ({o.FileName}).");
                    Graphic.DrawImage(Image.FromFile(o.FileName), 0, 0);
                }   
            }
            else
            {
                MessageBox.Show("Wklejanie zdjęć nie jest dostępne w trybie PixelArt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
