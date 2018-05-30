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
    public partial class PixelArtToolboxForm : Form, ISimpleToolbox
    {
        public PixelArtToolboxForm()
        {
            InitializeComponent();
        }
        // Zaimplementowane z ISimpleToolbox.
        public Color GetColor()
        {
            throw new NotImplementedException();
        }
    }
}
