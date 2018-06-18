using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ProgramGraficznyClasses
{
    public class ThemesManager : IThemesController
    {
        private Dictionary<string, EditorTheme> themes = new Dictionary<string, EditorTheme>();

        // Zaimplementowane z IThemesController
        public void AddTheme(EditorTheme newTheme)
        {
            themes.Add(newTheme.Name, newTheme);
        }
        public void LoadThemes(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            foreach (string file in Directory.EnumerateFiles(path))
            {
                if (file.EndsWith(".pget"))
                {
                    AddTheme(EditorTheme.FromString(File.ReadAllText(file)));
                }
            }
        }
        public void RemoveTheme(string name)
        {
            themes.Remove(name);
        }
        public string[] GetThemesNames()
        {
            string[] names = new string[themes.Count];

            int index = 0;
            foreach (KeyValuePair<string, EditorTheme> pair in themes)
            {
                names[index++] = pair.Key;
            }

            return names;
        }
        public EditorTheme GetTheme(string name)
        {
            return themes[name];
        }
    }
}
