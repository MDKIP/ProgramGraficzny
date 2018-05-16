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
    public partial class ToolboxForm : Form
    {
        /// <summary>
        /// Standardowy konstruktor dla ToolboxForm.
        /// </summary>
        /// <param name="log">Log do którego będą zapisywane wiadomości.</param>
        public ToolboxForm(ILog log)
        {
            // Wywalanie wyjątków.
            if (log == null)
            {
                throw new NullReferenceException("log nie może być pusty.");
            }

            // Przypisywanie.
            this.log = log;
            MainToolbox.CurrentPen = new Pen(Color.White);
            MainToolbox.CurrentTool = Tools.DrawLine;

            // Inicjalizacja komponentów (metoda wygenerowana przez designer'a).
            InitializeComponent();
            
            // Operacje przygotowywujące ToolboxForm do wyświetlenia.
            SetLayout(Tools.DrawLine);
        }

        /// <summary>
        /// IToolbox który jest kontrolowany przez ten ToolboxForm.
        /// </summary>
        public IToolbox MainToolbox { get; private set; } = new Toolbox();

        private Color currentColor = Color.White;
        private Size prevSize;
        private int[] previousCustomsColorOfColorDialog;
        private ILog log;

        /// <summary>
        /// Tworzy nowy pędzel i ustawia go w toolboxie.
        /// </summary>
        public void CreatePen()
        {
            MainToolbox.CurrentPen = new Pen(currentColor, (float)nudPenSize.Value);
            log.Write($"Nowy Pen został utworzony w ToolboxForm używając koloru ({currentColor.ToString()}) i szerokości wynoszącej ({(float)nudPenSize.Value}).", LogMessagesTypes.Detail);
        }
        /// <summary>
        /// Ustawia layout dla typu.
        /// </summary>
        /// <param name="t">Typ.</param>
        public void SetLayout(Tools t)
        {
            switch (t)
            {
                case Tools.DrawLine:
                    ShowDrawLine(true);
                    log.Write("Obecny layout dla ToolboxForma to DrawLine.", LogMessagesTypes.Detail);
                    break;
                default:
                    ShowDrawLine(false);
                    log.Write("Obecny layout dla ToolboxForma jest nieznany.", LogMessagesTypes.Detail);
                    break;
            }
            void ShowDrawLine(bool show)
            {
                lblPenSize.Visible = show;
                nudPenSize.Visible = show;
            }
        }
        /// <summary>
        /// Zwraca obecny tool z inputu.
        /// </summary>
        /// <returns></returns>
        public Tools GetCurrentToolFromInput()
        {
            switch (cmbTools.Text)
            {
                case "Linia":
                    log.Write("Obecne narzędnie używane przez ToolboxForm to DrawLine.", LogMessagesTypes.Important);
                    return Tools.DrawLine;
                default:
                    log.Write("Obecne narzędnie używane przez ToolboxForm nie zostało wykryte więc ustawiono DrawLine.", LogMessagesTypes.Important);
                    return Tools.DrawLine;
            }
        }

        private void ToolboxForm_Load(object sender, EventArgs e)
        {
            log.Write("ToolboxForm został załadowany.", LogMessagesTypes.Important);
            Text = "Toolbox";
            prevSize = Size;
        }
        private void ToolboxForm_SizeChanged(object sender, EventArgs e)
        {
            Size difference = Size - prevSize;
            cmbTools.Size = new Size(cmbTools.Size.Width + difference.Width, cmbTools.Size.Height);
            nudPenSize.Size = new Size(nudPenSize.Size.Width + difference.Width, nudPenSize.Size.Height);
            btnSetColorForPen.Size = new Size(btnSetColorForPen.Size.Width + difference.Width, btnSetColorForPen.Size.Height);
            prevSize = Size;
        }
        private void cmbTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainToolbox.CurrentTool = GetCurrentToolFromInput();
            SetLayout(MainToolbox.CurrentTool);
        }
        private void nudPenSize_ValueChanged(object sender, EventArgs e)
        {
            CreatePen();
        }
        private void btnSetColorForPen_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce wybrać kolor.", LogMessagesTypes.Important);
            ColorDialog dialog = new ColorDialog();
            dialog.CustomColors = previousCustomsColorOfColorDialog;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                previousCustomsColorOfColorDialog = dialog.CustomColors;
                currentColor = dialog.Color;
                log.Write($"Użytkownik wybrał kolor ({currentColor.ToString()}).", LogMessagesTypes.Important);
                CreatePen();
            }
        }
    }
}
