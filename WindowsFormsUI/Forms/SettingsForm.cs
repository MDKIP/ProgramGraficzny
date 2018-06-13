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
    public partial class SettingsForm : Form
    {
        public SettingsForm(ILog log, INotificator notificator)
        {
            InitializeComponent();

            // Dostosowywanie się do ustawień.
            nudRealPixelsPerEditorPixels.Value = ProgramInfo.MainSettings.StandardRPPEP;
            btnSetColorOfVisualizerBackground.BackColor = ProgramInfo.MainSettings.VisualizerBackgroundColor;
        }

        private SettingsInfo settings = new SettingsInfo();

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
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            ProgramInfo.MainSettings = settings;
        }
    }
}
