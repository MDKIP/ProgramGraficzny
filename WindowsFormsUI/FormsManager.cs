﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    static public class FormsManager
    {
        static public INotificator StandardNotificator { get; set; }
        static public ILog StandardLog { get; set; }

        static private List<Form> openForms = new List<Form>();
        static private PixelArtToolboxForm pixelArtToolboxForm;
        static private ToolboxForm toolboxForm;

        static public StartForm ShowStartForm()
        {
            return ShowStartForm(StandardLog, StandardNotificator);
        }
        static public StartForm ShowStartForm(ILog log, INotificator notificator)
        {
            StartForm form = new StartForm(log, notificator);
            form.Show();
            openForms.Add(form);
            return form;
        }
        static public NewGraphicForm ShowNewGraphicForm(IProject project)
        {
            return ShowNewGraphicsForm(project, StandardLog, StandardNotificator);
        }
        static public NewGraphicForm ShowNewGraphicsForm(IProject project, ILog log, INotificator notificator)
        {
            NewGraphicForm form = new NewGraphicForm(project, log, notificator);
            form.Show();
            openForms.Add(form);
            return form;
        }
        static public GraphicsEditor ShowGraphicsEditor(Size size, IProject project, GraphicTypes type, object[] plusParams)
        {
            return ShowGraphicsEditor(size, project, GetToolboxForm().MainToolbox, type, plusParams);
        }
        static public GraphicsEditor ShowGraphicsEditor(Size size, IProject project, IToolbox toolbox, GraphicTypes type, object[] plusParams)
        {
            return ShowGraphicsEditor(size, project, toolbox, type, StandardLog, StandardNotificator, plusParams);
        }
        static public GraphicsEditor ShowGraphicsEditor(Size size, IProject project, IToolbox toolbox, GraphicTypes type, ILog log, INotificator notificator, object[] plusParams)
        {
            GraphicsEditor form = new GraphicsEditor(project, toolbox, type, log, notificator, plusParams);
            form.Show();
            openForms.Add(form);
            form.SetWorkSpaceSize(size);
            return form;
        }
        static public PixelArtEditor ShowPixelArtEditor(int pixels, int realPixelsPerPixel, IProject project)
        {
            return ShowPixelArtEditor(pixels, realPixelsPerPixel, GetPixelArtToolboxForm(), project);
        }
        static public PixelArtEditor ShowPixelArtEditor(int pixels, int realPixelsPerPixel, IPixelArtToolbox toolbox, IProject project)
        {
            return ShowPixelArtEditor(pixels, realPixelsPerPixel, toolbox, project, StandardLog, StandardNotificator);
        }
        static public PixelArtEditor ShowPixelArtEditor(int pixels, int realPixelsPerPixel, IPixelArtToolbox toolbox, IProject project, ILog log, INotificator notificator)
        {
            PixelArtEditor form = new PixelArtEditor(pixels, toolbox, project, log, notificator);
            form.Show();
            openForms.Add(form);
            form.RealPixelsPerEditorPixels = realPixelsPerPixel;
            return form;
        }
        static public ToolboxForm GetToolboxForm()
        {
            return GetToolboxForm(StandardLog, StandardNotificator);
        }
        static public ToolboxForm GetToolboxForm(ILog log, INotificator notificator)
        {
            if (toolboxForm == null)
            {
                toolboxForm = new ToolboxForm(log, notificator);
                toolboxForm.Show();
                openForms.Add(toolboxForm);
            }
            return toolboxForm;
        }
        static public PixelArtToolboxForm GetPixelArtToolboxForm()
        {
            return GetPixelArtToolboxForm(StandardLog, StandardNotificator);
        }
        static public PixelArtToolboxForm GetPixelArtToolboxForm(ILog log, INotificator notificator)
        {
            if (pixelArtToolboxForm == null)
            {
                pixelArtToolboxForm = new PixelArtToolboxForm(log, notificator);
                pixelArtToolboxForm.Show();
                openForms.Add(pixelArtToolboxForm);
            }
            return pixelArtToolboxForm;
        }
    }
}
