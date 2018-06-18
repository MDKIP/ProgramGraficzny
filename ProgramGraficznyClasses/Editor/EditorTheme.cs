using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    public class EditorTheme
    {
        public EditorTheme(string name, Color backgroundColor, Color buttonsColor, Color textColor)
        {
            Name = name;
            BackgroundColor = backgroundColor;
            ButtonsColor = buttonsColor;
            TextColor = textColor;
        }

        public Color BackgroundColor { get; private set; }
        public Color ButtonsColor { get; private set; }
        public Color TextColor { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{Name}|{BackgroundColor.ToArgb()}|{ButtonsColor.ToArgb()}|{TextColor.ToArgb()}";
        }

        static public EditorTheme FromString(string s)
        {
            string[] parts = s.Split('|');
            int backgroundColor = 0, buttonsColor = 0, textColor = 0;

            if (!(int.TryParse(parts[1], out backgroundColor) && int.TryParse(parts[2], out buttonsColor) && 
                int.TryParse(parts[2], out textColor)))
            {
                return null;
            }

            return new EditorTheme(parts[0], Color.FromArgb(backgroundColor), Color.FromArgb(buttonsColor), Color.FromArgb(textColor));
        }
    }
}
