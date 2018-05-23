using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.MessageBox;

namespace ProgramGraficznyClasses
{
    public class MessageBoxNotificator : INotificator
    {
        public void Notify(string message)
        {
            Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Notify(string message, string title)
        {
            Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Notify(Exception error)
        {
            Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
