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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNewRails = new System.Windows.Forms.Button();
            this.btnNewIncurve = new System.Windows.Forms.Button();
            this.btnNewQP = new System.Windows.Forms.Button();
            this.btnNewCube = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBlockMove = new System.Windows.Forms.Button();
            this.btnBlockRotate = new System.Windows.Forms.Button();
            this.btnBlockDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.openglControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseWheel);
            this.openglControl.Paint += new System.Windows.Forms.PaintEventHandler(this.openglControl_Paint);
            this.openglControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseMove);
            this.openglControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseDown);
            this.openglControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openglControl_MouseUp);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 511);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnNewRails);
            this.groupBox3.Controls.Add(this.btnNewIncurve);
            this.groupBox3.Controls.Add(this.btnNewQP);
            this.groupBox3.Controls.Add(this.btnNewCube);
            this.groupBox3.Location = new System.Drawing.Point(4, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 133);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Block Creation";
            // 
            // btnNewRails
            // 
            this.btnNewRails.Location = new System.Drawing.Point(7, 70);
            this.btnNewRails.Name = "btnNewRails";
            this.btnNewRails.Size = new System.Drawing.Size(62, 45);
            this.btnNewRails.TabIndex = 6;
            this.btnNewRails.Text = "Rails";
            this.btnNewRails.UseVisualStyleBackColor = true;
            this.btnNewRails.Click += new System.EventHandler(this.btnNewRails_Click);
            // 
            // btnNewIncurve
            // 
            this.btnNewIncurve.Location = new System.Drawing.Point(143, 19);
            this.btnNewIncurve.Name = "btnNewIncurve";
            this.btnNewIncurve.Size = new System.Drawing.Size(62, 45);
            this.btnNewIncurve.TabIndex = 5;
            this.btnNewIncurve.Text = "Incurve";
            this.btnNewIncurve.UseVisualStyleBackColor = true;
            this.btnNewIncurve.Click += new System.EventHandler(this.btnNewQPIn_Click);
            // 
            // btnNewQP
            // 
            this.btnNewQP.Location = new System.Drawing.Point(75, 19);
            this.btnNewQP.Name = "btnNewQP";
            this.btnNewQP.Size = new System.Drawing.Size(62, 45);
            this.btnNewQP.TabIndex = 4;
            this.btnNewQP.Text = "Quarter Pipe";
            this.btnNewQP.UseVisualStyleBackColor = true;
            this.btnNewQP.Click += new System.EventHandler(this.btnNewQP_Click);
            // 
            // btnNewCube
            // 
            this.btnNewCube.Location = new System.Drawing.Point(7, 19);
            this.btnNewCube.Name = "btnNewCube";
            this.btnNewCube.Size = new System.Drawing.Size(62, 45);
            this.btnNewCube.TabIndex = 3;
            this.btnNewCube.Text = "Cube";
            this.btnNewCube.UseVisualStyleBackColor = true;
            this.btnNewCube.Click += new System.EventHandler(this.btnNewCube_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBlockMove);
            this.groupBox2.Controls.Add(this.btnBlockRotate);
            this.groupBox2.Controls.Add(this.btnBlockDelete);
            this.groupBox2.Location = new System.Drawing.Point(3, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 138);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Block Manipulation";
            // 
            // btnBlockMove
            // 
            this.btnBlockMove.Location = new System.Drawing.Point(8, 19);
            this.btnBlockMove.Name = "btnBlockMove";
            this.btnBlockMove.Size = new System.Drawing.Size(62, 45);
            this.btnBlockMove.TabIndex = 2;
            this.btnBlockMove.Text = "Move";
            this.btnBlockMove.UseVisualStyleBackColor = true;
            this.btnBlockMove.Click += new System.EventHandler(this.btnBlockMove_Click);
            // 
            // btnBlockRotate
            // 
            this.btnBlockRotate.Location = new System.Drawing.Point(76, 19);
            this.btnBlockRotate.Name = "btnBlockRotate";
            this.btnBlockRotate.Size = new System.Drawing.Size(62, 45);
            this.btnBlockRotate.TabIndex = 1;
            this.btnBlockRotate.Text = "Rotate";
            this.btnBlockRotate.UseVisualStyleBackColor = true;
            this.btnBlockRotate.Click += new System.EventHandler(this.btnBlockRotate_Click);
            // 
            // btnBlockDelete
            // 
            this.btnBlockDelete.Location = new System.Drawing.Point(144, 19);
            this.btnBlockDelete.Name = "btnBlockDelete";
            this.btnBlockDelete.Size = new System.Drawing.Size(62, 45);
            this.btnBlockDelete.TabIndex = 0;
            this.btnBlockDelete.Text = "Delete";
            this.btnBlockDelete.UseVisualStyleBackColor = true;
            this.btnBlockDelete.Click += new System.EventHandler(this.btnBlockDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(7, 19);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(62, 45);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(75, 19);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(62, 45);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnImport);
            this.groupBox4.Controls.Add(this.btnExport);
            this.groupBox4.Location = new System.Drawing.Point(4, 422);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(216, 76);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Scene";
            // 
            // MainOpenGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 511);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.openglControl);
            this.Name = "MainOpenGLForm";
            this.Text = "Skate Park Alpha";
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl openglControl;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnNewRails;
        private System.Windows.Forms.Button btnNewIncurve;
        private System.Windows.Forms.Button btnNewQP;
        private System.Windows.Forms.Button btnNewCube;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBlockMove;
        private System.Windows.Forms.Button btnBlockRotate;
        private System.Windows.Forms.Button btnBlockDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}


