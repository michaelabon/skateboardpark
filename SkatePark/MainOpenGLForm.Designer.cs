namespace SkatePark
{
    partial class MainOpenGLForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openglControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // openglControl
            // 
            this.openglControl.AccumBits = ((byte)(0));
            this.openglControl.AutoCheckErrors = false;
            this.openglControl.AutoFinish = false;
            this.openglControl.AutoMakeCurrent = true;
            this.openglControl.AutoSwapBuffers = true;
            this.openglControl.BackColor = System.Drawing.Color.Black;
            this.openglControl.ColorBits = ((byte)(32));
            this.openglControl.DepthBits = ((byte)(16));
            this.openglControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openglControl.Location = new System.Drawing.Point(0, 0);
            this.openglControl.Name = "openglControl";
            this.openglControl.Size = new System.Drawing.Size(783, 511);
            this.openglControl.StencilBits = ((byte)(0));
            this.openglControl.TabIndex = 0;
            this.openglControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseMove);
            this.openglControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseDown);
            this.openglControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseUp);
            this.openglControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseWheel);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // MainOpenGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 511);
            this.Controls.Add(this.openglControl);
            this.Name = "MainOpenGLForm";
            this.Text = "Skate Park Alpha";
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl openglControl;
        private System.Windows.Forms.Timer refreshTimer;
    }
}

