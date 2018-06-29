using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class PixelArtToolboxForm : Form, IPixelArtToolbox, IProgramGraficznyForm
    {
        public PixelArtToolboxForm(ILog log, INotificator notificator)
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

            log.Write(Factory.GetNewInstanceCreationString("PixelArtToolboxForm"), LogMessagesTypes.Detail);

            // Inicjalizacja komponentów.
            InitializeComponent();

            // Przypisywanie.
            this.notificator = notificator;
            this.log = log;

            Reload();
        }

        private List<Button> colorButtons = new List<Button>();
        private Color selectedColor;
        private int[] colorDialogCustomColors;
        private int selectedButtonIndex;
        private INotificator notificator;
        private ILog log;

        private void DeselectAll()
        {
            Button lastButton = new Button();
            bool isStart = true;
            foreach (Button btn in colorButtons)
            {
                btn.Width = 241;
                btn.Height = 23;
                if (isStart)
                {
                    btn.Location = new Point(12, 12);
                    isStart = false;
                }
                else
                {
                    btn.Location = new Point(12, lastButton.Location.Y + 30);
                }
                lastButton = btn;
            }
        }
        private void SelectButton(Button colorButton)
        {
            // Odznaczanie reszty.
            DeselectAll();

            // Zaznaczanie przycisku.
            colorButton.Width += 6;
            colorButton.Height += 6;
            colorButton.Location = new Point(colorButton.Location.X - 3, colorButton.Location.Y - 3);
            selectedButtonIndex = colorButtons.IndexOf(colorButton);
            selectedColor = colorButton.BackColor;
        }
        private void DeleteButton(Button colorButton)
        {
            int indexOfButtonToDelete = colorButtons.FindIndex(x => x == colorButton);

            colorButtons.Remove(colorButton);
            Controls.Remove(colorButton);
            colorButton.Dispose();

            DeselectAll();

            btnAddColor.Location = new Point(btnAddColor.Location.X, btnAddColor.Location.Y - 30);

            selectedColor = Color.Empty;
        }
        // Zaimplementowane z ISimpleToolbox.
        public Color GetColor()
        {
            return selectedColor != Color.Empty ? selectedColor : ProgramInfo.CurrentTheme.ButtonsColor;
        }
        // Zaimplementowane z IProgramGraficznyForm
        public void Reload()
        {
            BackColor = ProgramInfo.CurrentTheme.BackgroundColor;
            ForeColor = ProgramInfo.CurrentTheme.TextColor;
            btnAddColor.BackColor = ProgramInfo.CurrentTheme.ButtonsColor;
            btnAddColor.ForeColor = ProgramInfo.CurrentTheme.TextColor;
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            // Ustawianie ColorDialog.
            ColorDialog dialog = new ColorDialog();
            if (colorDialogCustomColors != null)
            {
                dialog.CustomColors = colorDialogCustomColors;
            }

            // Wyświetlanie i pobieranie wyników.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                colorDialogCustomColors = dialog.CustomColors;
                Button newButton = new Button()
                {
                    Text = "",
                    Width = 241,
                    Height = 23,
                    BackColor = dialog.Color, 
                };
                if (colorButtons.Count > 0)
                {
                    newButton.Location = new Point(12, colorButtons[colorButtons.Count - 1].Location.Y + 30);
                }
                else
                {
                    newButton.Location = new Point(12, 12);
                }
                btnAddColor.Location = new Point(newButton.Location.X, newButton.Location.Y + 30);
                newButton.MouseDown += ColorButton_MouseDown;
                colorButtons.Add(newButton);
                Controls.Add(newButton);
                SelectButton(newButton);
            }
        }
        private void ColorButton_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;

            if (args.Button == MouseButtons.Left)
            {
                SelectButton(sender as Button);
            }
            else if (args.Button == MouseButtons.Right)
            {
                DeleteButton(sender as Button);
            }
        }
    }
}
