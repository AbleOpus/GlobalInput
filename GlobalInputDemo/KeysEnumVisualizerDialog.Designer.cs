namespace GlobalInputDemo
{
    partial class KeysEnumVisualizerDialog
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
            this.hotkeyTextBox = new GlobalInput.Forms.HotkeyTextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.AllowSoloModifiers = true;
            this.hotkeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotkeyTextBox.HotkeyKeyCode = System.Windows.Forms.Keys.None;
            this.hotkeyTextBox.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyTextBox.Location = new System.Drawing.Point(12, 12);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.Size = new System.Drawing.Size(586, 20);
            this.hotkeyTextBox.TabIndex = 0;
            this.hotkeyTextBox.TextChanged += new System.EventHandler(this.hotkeyTextBox_TextChanged);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 38);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(586, 290);
            this.textBoxOutput.TabIndex = 1;
            this.textBoxOutput.WordWrap = false;
            // 
            // KeysEnumVisualizerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 340);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.hotkeyTextBox);
            this.Name = "KeysEnumVisualizerDialog";
            this.Text = "Keys Enum Visualizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GlobalInput.Forms.HotkeyTextBox hotkeyTextBox;
        private System.Windows.Forms.TextBox textBoxOutput;
    }
}