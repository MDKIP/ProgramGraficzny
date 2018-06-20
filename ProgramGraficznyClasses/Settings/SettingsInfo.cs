using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ProgramGraficznyClasses
{
    public struct SettingsInfo
    {
        public Color VisualizerBackgroundColor { get; set; }
        public int StandardRPPEP { get; set; }

        static public SettingsInfo Default
        {
            get
            {
                return new SettingsInfo()
                {
                    VisualizerBackgroundColor = Color.White,
                    StandardRPPEP = 50,
                };
            }
        }
    }
}
