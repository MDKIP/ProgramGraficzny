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
            int backgroundColorAsInt = 0, buttonsColorAsInt = 0, textColorAsInt = 0;

            if (!(int.TryParse(parts[1], out backgroundColorAsInt) && int.TryParse(parts[2], out buttonsColorAsInt) && 
                int.TryParse(parts[3], out textColorAsInt)))
            {
                return null;
            }

            Color backgroundColor = Color.FromArgb(backgroundColorAsInt).GetWithAlpha(255);
            Color buttonsColor = Color.FromArgb(buttonsColorAsInt).GetWithAlpha(255);
            Color textColor = Color.FromArgb(textColorAsInt).GetWithAlpha(255);

            return new EditorTheme(parts[0], backgroundColor, buttonsColor, textColor);
        }
    }
}
