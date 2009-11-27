using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkatePark
{
    public partial class MainOpenGLForm : Form
    {
        Scene scene;
        public MainOpenGLForm()
        {
            scene = new Scene();

            InitializeComponent();
            this.simpleOpenGlControl1.InitializeContexts();

            scene.InitGL();
            scene.SetView(this.Height, this.Width);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            scene.RenderScene();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            scene.SetView(this.Height, this.Width);
        }
    }
}
