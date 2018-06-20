using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    class EmptyLog : ILog
    {
        public void Write(string msg)
        {
            
        }

        public void Write(string msg, LogMessagesTypes type)
        {
            
        }
    }
}
