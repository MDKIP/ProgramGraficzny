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
    public partial class StartForm : Form
    {
        /// <summary>
        /// Standardowy konstruktor dla StartForm.
        /// </summary>
        /// <param name="log">Log do którego będą zapisywane wiadomości.</param>
        public StartForm(ILog log, INotificator notificator)
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

            log.Write(Factory.GetNewInstanceCreationString("StartForm"), LogMessagesTypes.Detail);

            // Przypisywanie.
            this.notificator = notificator;
            this.log = log;

            // Inicjalizacja komponentów.
            InitializeComponent();
        }

        private INotificator notificator;
        private ILog log;

        private void StartForm_Load(object sender, EventArgs e)
        {
            log.Write("StartForm został załadowany.", LogMessagesTypes.Important);

            // Ustawianie tekstu w pasku forma.
            Text = $"{ProgramInfo.ProgramName} ({ProgramInfo.Version})";
        }
        private void btnCreateGraphics_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce utworzyć nową grafikę bez projektu.", LogMessagesTypes.Important);

            FormsManager.ShowNewGraphicForm(null);
        }
        private void btnLoadGraphic_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "Program Graficzny Piksel Art (*.pgpa) |.pgpa";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.EndsWith(".pgpa"))
                {
                    FormsManager.ShowPixelArtEditor(32, 50, null).LoadFile(dialog.FileName);
                }
                else
                {
                    notificator.Notify((new Exception("Plik z którego próbujesz wczytać grafikę ma nieprawidołowy format. Dostępne formaty to .pgpa")));
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormsManager.ShowSettingsForm();
        }
    }
}
