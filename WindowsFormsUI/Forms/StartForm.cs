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
        public StartForm(ILog log)
        {
            // Przypisywanie.
            this.log = log;

            // Inicjalizacja komponentów (metoda wygenerowana przez desinger'a)
            InitializeComponent();
        }

        private ILog log;

        private void StartForm_Load(object sender, EventArgs e)
        {
            log.Write("StartForm został załadowany.", LogMessagesTypes.Important);

            // Ustawianie tekstu w pasku forma.
            Text = $"{ProgramInfo.ProgramName} ({ProgramInfo.Version})";

            // Ustawianie głównego toolboxa programu.
            if (!ProgramInfo.HasMainToolbox) // Jeżeli program nie ma głównego toolboxa.
            {
                log.Write("Tworzenie nowego toolboxa który będzie domyślny dla całego programu.", LogMessagesTypes.Important);
                // Pokazywanie nowego forma obsugującego toolboxa.
                ToolboxForm toolbox = new ToolboxForm(log);
                toolbox.Show();

                // Ustawianie nowego toolboxa jako toolbox dla całego programu.
                ProgramInfo.MainToolbox = toolbox.MainToolbox;
            }
        }
        private void btnCreateGraphics_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce utworzyć nową grafikę bez projektu.", LogMessagesTypes.Important);

            // Pokazywnie nowego forma w którym użytkownik wybierze właściwości nowej grafiki.
            NewGraphicForm form = new NewGraphicForm(null, log);
            form.Show();
        }
    }
}
