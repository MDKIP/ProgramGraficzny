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
    public partial class SettingsForm : Form, IProgramGraficznyForm
    {
        public SettingsForm(ILog log, INotificator notificator)
        {
            InitializeComponent();

            // Przypisywanie.
            this.notificator = notificator;
            this.log = log;

            // Dostosowywanie się do ustawień.
            btnSetColorOfVisualizerBackground.BackColor = ProgramInfo.MainSettings.VisualizerBackgroundColor;

            Reload();
        }

        private SettingsInfo settings = new SettingsInfo();
        private INotificator notificator;
        private ILog log;

        // Zaimplementowane z IProgramGraficznyForm
        public void Reload()
        {
            // Kolory.
            BackColor = ProgramInfo.CurrentTheme.BackgroundColor;
            ForeColor = ProgramInfo.CurrentTheme.TextColor;
            btnSaveChanges.BackColor = ProgramInfo.CurrentTheme.ButtonsColor;
            btnSaveChanges.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            lblColorOfVisualizerBackground.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            lblEditor.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            lblPixelArt.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            lblRealPixelsPerEditorPixels.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            lblVisualizer.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            cmbThemes.BackColor = ProgramInfo.CurrentTheme.ButtonsColor;
            cmbThemes.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            nudRealPixelsPerEditorPixels.BackColor = ProgramInfo.CurrentTheme.ButtonsColor;
            nudRealPixelsPerEditorPixels.ForeColor = ProgramInfo.CurrentTheme.TextColor;
            btnSetColorOfVisualizerBackground.BackColor = ProgramInfo.MainSettings.VisualizerBackgroundColor;

            // Wczytywanie stylów.
            cmbThemes.Items.RemoveAll();
            cmbThemes.Items.AddRange(ProgramInfo.MainThemesController.GetThemesNames());

            // Ustawienia.
            cmbThemes.Text = ProgramInfo.CurrentTheme.Name;
            nudRealPixelsPerEditorPixels.Value = ProgramInfo.MainSettings.StandardRPPEP;
        }

        private void btnSetColorOfVisualizerBackground_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color outputColor = dialog.Color;
                btnSetColorOfVisualizerBackground.BackColor = outputColor;
            }
        }
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            settings.StandardRPPEP = (int)nudRealPixelsPerEditorPixels.Value;
            settings.VisualizerBackgroundColor = btnSetColorOfVisualizerBackground.BackColor;
            ProgramInfo.CurrentTheme = ProgramInfo.MainThemesController.GetTheme(cmbThemes.Text);

            ProgramInfo.MainSettings = settings;
            FormsManager.ReloadAllForms();
            Program.SaveSettings();

            Close();
        }
    }
}
