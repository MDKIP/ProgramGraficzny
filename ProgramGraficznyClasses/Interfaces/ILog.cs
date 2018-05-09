namespace ProgramGraficznyClasses
{
    public interface ILog
    {
        void Write(string msg);
        void Write(string msg, LogMessagesTypes type);
    }
}