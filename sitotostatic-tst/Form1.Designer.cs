namespace sitotostatic_tst
{
    partial class Form1
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
            this.ie1 = new Gecko.GeckoWebBrowser();
            this.tmgotonext = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ie1
            // 
            this.ie1.FrameEventsPropagateToMainWindow = false;
            this.ie1.Location = new System.Drawing.Point(12, 12);
            this.ie1.Name = "ie1";
            this.ie1.Size = new System.Drawing.Size(776, 633);
            this.ie1.TabIndex = 0;
            this.ie1.UseHttpActivityObserver = false;
            // 
            // tmgotonext
            // 
            this.tmgotonext.Enabled = true;
            this.tmgotonext.Interval = 2000;
            this.tmgotonext.Tick += new System.EventHandler(this.tmgotonext_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(843, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 657);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ie1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser ie1;
        private System.Windows.Forms.Timer tmgotonext;
        private System.Windows.Forms.Label label1;
    }
}

