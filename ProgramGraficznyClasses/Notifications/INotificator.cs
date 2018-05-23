using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    public interface INotificator
    {
        void Notify(string message);
        void Notify(string message, string title);
        void Notify(Exception error);
    }
}
