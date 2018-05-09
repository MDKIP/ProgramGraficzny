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
        /// Standard constructor for GraphicsEditor.
        /// </summary>
        /// <param name="projectForThisGraphics">Project for this graphic. Can be null.</param>
        /// <param name="toolboxForThisEditor">Toolbox for this editor.</param>
        /// <param name="type">Type of this graphic.</param>
        /// <param name="log">ILog for this GraphicsEditor.</param>
        public GraphicsEditor(IProject projectForThisGraphics, IToolbox toolboxForThisEditor, GraphicTypes type, ILog log, object[] plusParams)
        {
            log.Write("New instance of GraphicsEditor was created.");
            // Throwing exceptions.
            if (toolboxForThisEditor == null)
            {
                throw new NullReferenceException("Toolbox cannot be null.");
            }

            // Initialazing.
            InitializeComponent();

            // Setting.
            this.log = log;
            standardEditor = Graphic = new Graphic(projectForThisGraphics, pcbWorkSpace.CreateGraphics(), log);
            toolbox = toolboxForThisEditor;
            Project = projectForThisGraphics;
            Type = type;

            // Creating MainMenu.
            MainMenu mainMenu = new MainMenu();
            MenuItem fileItem = new MenuItem("Plik");
            MenuItem newItem = new MenuItem("Nowy", newItem_Click);
            MenuItem saveItem = new MenuItem("Zapisz", saveItem_Click);
            MenuItem graphicItem = new MenuItem("Grafika");
            MenuItem addImageItem = new MenuItem("Dodaj zdjęcie", addImageItem_Click);
            mainMenu.MenuItems.Add(fileItem);
            fileItem.MenuItems.Add(saveItem);
            mainMenu.MenuItems.Add(graphicItem);
            graphicItem.MenuItems.Add(addImageItem);
            Menu = mainMenu;

            // Instructions for PixelArt graphic.
            if (type == GraphicTypes.PixelArt)
            {
                SetWorkSpaceSize(new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height));
                sizeOfPixelArt = (int)plusParams[0];
                DrawLinesForPixelArt();
            }
        }

        /// <summary>
        /// Project of this graphic.
        /// </summary>
        public IProject Project { get; private set; }
        /// <summary>
        /// Graphic of this graphic editor.
        /// </summary>
        public IGraphic Graphic { get; private set; }
        public GraphicTypes Type { get; private set; }

        private int sizeOfPixelArt;
        private IGraphicEditorStandard standardEditor;
        private IPixelArtController pixelArtController;
        private IToolbox toolbox;
        private ILog log;

        private void DrawLinesForPixelArt()
        {
            DrawLinesForPixelArt(sizeOfPixelArt);
        }
        private void DrawLinesForPixelArt(int bytes)
        {
            log.Write($"Rysowanie linii dla piksel artów dla rozmiaru {bytes}.");
            if (bytes <= 0) return;
            int xDistance = Size.Width / bytes - 5;
            int yDistance = Size.Height / bytes - 5;
            Point pos = new Point(0, 0);
            Pen blackPen = new Pen(Color.Black, 2);
            if (xDistance >= yDistance)
            {
                log.Write("Dostosywanie linii do rozmiaru osi Y.");
                int nonDrawBorder = bytes * yDistance;
                for (int i = 0; i < bytes + 1; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(pos.X, Size.Height), true);
                    pos.X += yDistance;
                }
                pos.X = 0;
                for (int i = 0; i < bytes; i++)
                {
                    Graphic.DrawLine(blackPen, pos, new Point(Size.Width, pos.Y), true);
                    pos.Y += yDistance;
                }
            }
            else
            {
                log.Write("Dostosywanie linii do rozmiaru osi X.");
                int nonDrawBorder = bytes * yDistance;
                for (int i = 0; i < bytes; i++)
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
        private Point GetCordinatesForPixelArt(Point click)
        {
            if (Type != GraphicTypes.PixelArt) return new Point();
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sets the work space of editor.
        /// </summary>
        /// <param name="s">Size.</param>
        public void SetWorkSpaceSize(Size s)
        {
            log.Write($"Size of WorkSpace in GraphicsEditor was set as {s.ToString()}.");
            pcbWorkSpace.Size = s;
        }

        private void GraphicsEditor_Load(object sender, EventArgs e)
        {
            log.Write("GraphicsEditor was loaded.");
            Text = "Graphics Editor";
            Size prevSize = Size;
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphic.UpdateGraphics(pcbWorkSpace.CreateGraphics());
            Graphic.Size = pcbWorkSpace.Size;
            Size = prevSize;
            Graphic.DrawAllAgain();
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
            log.Write("Resizing of GraphicsEditor was ended.");
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
                log.Write("Z button with Control on keyboard was clicked by user.");
                Graphic.ReturnState();
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                log.Write("X button with Control on keyboard was clicked by user.");
                Graphic.NextState();
            }
        }
        private void GraphicsEditor_LocationChanged(object sender, EventArgs e)
        {
            if (Graphic != null)
            {
                Graphic.DrawAllAgain();
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
            int x = MousePosition.X - Location.X - 5;
            int y = MousePosition.Y - Location.Y - 50;
            Point location = new Point(x, y);
            log.Write($"pcpWorkSpace was clicked by user at position ({location.ToString()}).");
            if (Type == GraphicTypes.Image || Type == GraphicTypes.Empty)
            {
                switch (toolbox.CurrentTool)
                {
                    case Tools.DrawLine:
                        Graphic.ReturnState();
                        Graphic.DrawLine(toolbox.CurrentPen, location, new Point(0, 0), false);
                        break;
                } 
            }
            else if (Type == GraphicTypes.PixelArt)
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
            log.Write("MenuItem newItem was cliecked by the user.");
            NewGraphicForm dialog = new NewGraphicForm(Project, log);
            dialog.Show();
        }
        private void saveItem_Click(object sender, EventArgs e)
        {
            log.Write("MenuItem saveItem was cliecked by the user.");
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                if (!path.EndsWith(".jpg"))
                {
                    path += ".jpg";
                }
                standardEditor.Save(path);
                log.Write($"Graphic was saved at {path}.");
            }
        }
        private void addImageItem_Click(object sender, EventArgs e)
        {
            log.Write("MenuItem addImageItem was cliecked by the user.");
            if (Type == GraphicTypes.Empty || Type == GraphicTypes.Image)
            {
                OpenFileDialog o = new OpenFileDialog();
                if (o.ShowDialog() == DialogResult.OK)
                {
                    log.Write($"New image has been added to graphic from {o.FileName}.");
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
