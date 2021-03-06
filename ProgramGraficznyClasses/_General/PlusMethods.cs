﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProgramGraficznyClasses
{
    static public class PlusMethods
    {
        // Point
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
        static public Point GetWithX(this Point thisPoint, int newX)
        {
            return new Point(newX, thisPoint.Y);
        }
        static public Point GetWithY(this Point thisPoint, int newY)
        {
            return new Point(thisPoint.X, newY);
        }
        // Color
        static public Color GetWithAlpha(this Color thisColor, byte alpha)
        {
            return Color.FromArgb(alpha, thisColor.R, thisColor.G, thisColor.B);
        }
        // ComboBox.ObjectCollection
        static public void RemoveAll(this ComboBox.ObjectCollection thisItems)
        {
            int itemsLenght = thisItems.Count;

            for (int l = 0; l < itemsLenght; l++)
            {
                thisItems.Remove(thisItems[0]);
            }
        }
    }
}
