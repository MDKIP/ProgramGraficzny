using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgramGraficznyClasses
{
    static public class PlusMethods
    {
        static public bool IsGratherThen(this Point thisPoint, Point pointToCheck)
        {
            if (thisPoint.X > pointToCheck.X && thisPoint.Y > pointToCheck.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool IsSmallerThen(this Point thisPoint, Point pointToCheck)
        {
            if (thisPoint.X < pointToCheck.X && thisPoint.Y < pointToCheck.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
