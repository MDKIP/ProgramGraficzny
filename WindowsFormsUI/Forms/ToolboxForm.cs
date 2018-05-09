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
            // Przypisywanie.
            MainToolbox = new Toolbox();
            currentColor = Color.White;
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
        public IToolbox MainToolbox { get; private set; }

        private Color currentColor;
        private Size prevSize;
        private int[] prevousCustomsColorOfColorDialog;
        private ILog log;

        public void CreatePen()
        {
            MainToolbox.CurrentPen = new Pen(currentColor, (float)nudPenSize.Value);
            log.Write($"Nowy Pen został utworzony w ToolboxForm używając koloru ({currentColor.ToString()}) i szerokości wynoszącej ({(float)nudPenSize.Value}).", LogMessagesTypes.Detail);
        }
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
            log.Write("ToolboxForm was loaded.");
            Text = "Toolbox";
            prevSize = Size;
        }
        private void ToolboxForm_SizeChanged(object sender, EventArgs e)
        {
            Size difference = Size - prevSize;
            cmbTools.Size = new Size(cmbTools.Size.Width + difference.Width, cmbTools.Size.Height);
            nudPenSize.Size = new Size(nudPenSize.Size.Width + difference.Width, nudPenSize.Size.Height);
            prevSize = Size;
        }
        private void cmbTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Write("Selected Index of cmbTools in ToolboxForm was changed.");
            MainToolbox.CurrentTool = GetCurrentToolFromInput();
            SetLayout(MainToolbox.CurrentTool);
        }
        private void nudPenSize_ValueChanged(object sender, EventArgs e)
        {
            log.Write("Value of nudPenSize in ToolboxForm was changed.");
            CreatePen();
        }
        private void btnSetColorForPen_Click(object sender, EventArgs e)
        {
            log.Write("Button btnSetColorForPen in ToolboxForm was clicked.");
            ColorDialog dialog = new ColorDialog();
            dialog.CustomColors = prevousCustomsColorOfColorDialog;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                prevousCustomsColorOfColorDialog = dialog.CustomColors;
                currentColor = dialog.Color;
                log.Write($"User select color ({currentColor.ToString()}) in ToolboxForm.");
                CreatePen();
            }
        }
    }
}
