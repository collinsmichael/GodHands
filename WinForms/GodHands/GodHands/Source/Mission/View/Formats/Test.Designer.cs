namespace GodHands {
    partial class Test {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.zndEditor1 = new GodHands.ZndEditor();
            this.SuspendLayout();
            // 
            // zndEditor1
            // 
            this.zndEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zndEditor1.Location = new System.Drawing.Point(0, 0);
            this.zndEditor1.Name = "zndEditor1";
            this.zndEditor1.Size = new System.Drawing.Size(642, 465);
            this.zndEditor1.TabIndex = 0;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 465);
            this.Controls.Add(this.zndEditor1);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);

        }

        #endregion

        private ZndEditor zndEditor1;
    }
}