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

            // Dostosowywanie się do ustawień.
            nudRealPixelsPerEditorPixels.Value = ProgramInfo.MainSettings.StandardRPPEP;
            btnSetColorOfVisualizerBackground.BackColor = ProgramInfo.MainSettings.VisualizerBackgroundColor;

            Reload();
        }

        private SettingsInfo settings = new SettingsInfo();

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

            // Wczytywanie stylów.
            cmbThemes.Items.AddRange(ProgramInfo.MainThemesController.GetThemesNames());

            // Ustawienia.
            cmbThemes.Text = ProgramInfo.CurrentTheme.Name;
        }

        private void btnSetColorOfVisualizerBackground_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color outputColor = dialog.Color;
                btnSetColorOfVisualizerBackground.BackColor = outputColor;
                settings.VisualizerBackgroundColor = outputColor;
            }
        }
        private void nudRealPixelsPerEditorPixels_ValueChanged(object sender, EventArgs e)
        {
            settings.StandardRPPEP = (int)nudRealPixelsPerEditorPixels.Value;
        }
        private void cmbThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramInfo.CurrentTheme = ProgramInfo.MainThemesController.GetTheme(cmbThemes.Text);
            Reload();
            FormsManager.ReloadAllForms();
        }
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            ProgramInfo.MainSettings = settings;
        }
    }
}
