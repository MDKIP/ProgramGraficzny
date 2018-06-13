using System.Drawing;
using System.Windows.Forms;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class GraphicsVisualizer : Form
    {
        public GraphicsVisualizer(Image graphic, ILog log, INotificator notificator)
        {
            log.Write(Factory.GetNewInstanceCreationString("GraphicsVisualizer"), LogMessagesTypes.Detail);

            // Inicjalizacja komponentów.
            InitializeComponent();

            // Przypisywanie.
            this.log = log;
            this.notificator = notificator;
            this.graphic = graphic;

            // Ustawianie rozmiaru okna.
            ClientSize = graphic.Size;
            MinimumSize = Size;
            MaximumSize = Size;
        }

        private Image graphic;
        private INotificator notificator;
        private ILog log;

        private void GraphicsVisualizer_Load(object sender, System.EventArgs e)
        {
            log.Write("GraphicsVisualizer został załadowany.", LogMessagesTypes.Important);

            // Ustawianie grafiki.
            pcbGraphic.Image = graphic;

            // Ustawianie koloru.
            pcbGraphic.BackColor = ProgramInfo.MainSettings.VisualizerBackgroundColor;
        }
    }
}
