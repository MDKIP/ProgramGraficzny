using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGraficznyClasses
{
    public interface IThemesController
    {
        void RemoveTheme(string name);
        void LoadThemes(string path);
        void AddTheme(EditorTheme newTheme);
        string[] GetThemesNames();
        EditorTheme GetTheme(string name);
    }
}
