using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SkatePark.Drawables;

namespace SkatePark
{
    public partial class MainOpenGLForm : Form
    {
        Scene scene;
        public MainOpenGLForm()
        {
            CubletWarehouse.LoadAllData();

            scene = new Scene();

            InitializeComponent();
            this.openglControl.InitializeContexts();

            scene.InitGL();
            scene.SetView(this.Height, this.Width);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(!scene.StopRender) scene.RenderScene();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            scene.SetView(this.Height, this.Width);
        }

        private void openglControl_MouseUp(object sender, MouseEventArgs e)
        {
            scene.onMouseRelease(e);
        }

        private void openglControl_MouseDown(object sender, MouseEventArgs e)
        {
            scene.onMouseDown(e);
        }

        private void openglControl_MouseMove(object sender, MouseEventArgs e)
        {
            scene.onMouseMove(e);
        }

        private void openglControl_MouseWheel(object sender, MouseEventArgs e)
        {
            scene.onMouseWheel(e);
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            Refresh();
        }

    }
}
