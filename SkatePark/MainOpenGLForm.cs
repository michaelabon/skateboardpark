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
        public MainOpenGLForm()
        {
            InitializeComponent();
            this.simpleOpenGlControl1.InitializeContexts();
        }
    }
}
