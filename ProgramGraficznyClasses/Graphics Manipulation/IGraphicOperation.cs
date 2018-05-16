namespace ProgramGraficznyClasses
{
    public interface IGraphicOperation
    {
        bool IsOnlyEditorOperation { get; }
        GraphicOperations Operation { get; }

        object[] GetParameters();
    }
}