using GlobalInput.Forms;

namespace GlobalInputDemo
{
    partial class MainForm
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
            if (disposing)
            {
                hotKeyHooker.Dispose();
                mouseHooker.Unhook();
            }
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
            System.Windows.Forms.GroupBox groupBox1;
            this.checkBoxInvokeEnabled = new System.Windows.Forms.CheckBox();
            this.buttonUnbindAll = new System.Windows.Forms.Button();
            this.buttonUnbindSelected = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxBindings = new System.Windows.Forms.ListBox();
            this.listBoxHKLog = new System.Windows.Forms.ListBox();
            this.contextMenuLogList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxMouse = new System.Windows.Forms.GroupBox();
            this.checkBoxMouseCallNext = new System.Windows.Forms.CheckBox();
            this.labelMousePos = new System.Windows.Forms.Label();
            this.checkBoxMouseHookerEnabled = new System.Windows.Forms.CheckBox();
            this.listBoxMousing = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxKeyCallNext = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardHookEnabled = new System.Windows.Forms.CheckBox();
            this.listBoxKeyboardHook = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonFindOccupied = new System.Windows.Forms.Button();
            this.listBoxOccupied = new System.Windows.Forms.ListBox();
            this.hotkeyTextBox = new GlobalInput.Forms.HotKeyTextBox();
            this.keyboardHooker = new GlobalInput.Keyboard.KeyboardHooker();
            this.mouseHooker = new GlobalInput.Mouse.MouseHooker();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuLogList.SuspendLayout();
            this.groupBoxMouse.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.checkBoxInvokeEnabled);
            groupBox1.Controls.Add(this.buttonUnbindAll);
            groupBox1.Controls.Add(this.buttonUnbindSelected);
            groupBox1.Controls.Add(this.tableLayoutPanel1);
            groupBox1.Controls.Add(this.buttonBind);
            groupBox1.Controls.Add(this.hotkeyTextBox);
            groupBox1.Controls.Add(this.label1);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(424, 161);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Hokeying";
            // 
            // checkBoxInvokeEnabled
            // 
            this.checkBoxInvokeEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxInvokeEnabled.AutoSize = true;
            this.checkBoxInvokeEnabled.Location = new System.Drawing.Point(317, 138);
            this.checkBoxInvokeEnabled.Name = "checkBoxInvokeEnabled";
            this.checkBoxInvokeEnabled.Size = new System.Drawing.Size(101, 17);
            this.checkBoxInvokeEnabled.TabIndex = 9;
            this.checkBoxInvokeEnabled.Text = "Invoke Enabled";
            this.checkBoxInvokeEnabled.UseVisualStyleBackColor = true;
            // 
            // buttonUnbindAll
            // 
            this.buttonUnbindAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUnbindAll.Location = new System.Drawing.Point(149, 131);
            this.buttonUnbindAll.Name = "buttonUnbindAll";
            this.buttonUnbindAll.Size = new System.Drawing.Size(137, 23);
            this.buttonUnbindAll.TabIndex = 8;
            this.buttonUnbindAll.Text = "Unbind All";
            this.buttonUnbindAll.UseVisualStyleBackColor = true;
            this.buttonUnbindAll.Click += new System.EventHandler(this.buttonUnbindAll_Click);
            // 
            // buttonUnbindSelected
            // 
            this.buttonUnbindSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUnbindSelected.Location = new System.Drawing.Point(6, 131);
            this.buttonUnbindSelected.Name = "buttonUnbindSelected";
            this.buttonUnbindSelected.Size = new System.Drawing.Size(137, 23);
            this.buttonUnbindSelected.TabIndex = 7;
            this.buttonUnbindSelected.Text = "Unbind Selected";
            this.buttonUnbindSelected.UseVisualStyleBackColor = true;
            this.buttonUnbindSelected.Click += new System.EventHandler(this.buttonUnbindSelected_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.listBoxBindings, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxHKLog, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(412, 80);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // listBoxBindings
            // 
            this.listBoxBindings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxBindings.FormatString = "{0}";
            this.listBoxBindings.FormattingEnabled = true;
            this.listBoxBindings.IntegralHeight = false;
            this.listBoxBindings.Location = new System.Drawing.Point(3, 16);
            this.listBoxBindings.Name = "listBoxBindings";
            this.listBoxBindings.Size = new System.Drawing.Size(200, 61);
            this.listBoxBindings.TabIndex = 7;
            // 
            // listBoxHKLog
            // 
            this.listBoxHKLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxHKLog.ContextMenuStrip = this.contextMenuLogList;
            this.listBoxHKLog.FormattingEnabled = true;
            this.listBoxHKLog.IntegralHeight = false;
            this.listBoxHKLog.Location = new System.Drawing.Point(209, 16);
            this.listBoxHKLog.Name = "listBoxHKLog";
            this.listBoxHKLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxHKLog.Size = new System.Drawing.Size(200, 61);
            this.listBoxHKLog.TabIndex = 8;
            this.listBoxHKLog.MouseEnter += new System.EventHandler(this.listBox_MouseEnter);
            // 
            // contextMenuLogList
            // 
            this.contextMenuLogList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemClear});
            this.contextMenuLogList.Name = "contextMenuLogList";
            this.contextMenuLogList.Size = new System.Drawing.Size(102, 26);
            // 
            // menuItemClear
            // 
            this.menuItemClear.Name = "menuItemClear";
            this.menuItemClear.Size = new System.Drawing.Size(101, 22);
            this.menuItemClear.Text = "Clear";
            this.menuItemClear.Click += new System.EventHandler(this.menuItemClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Bindings:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Log:";
            // 
            // buttonBind
            // 
            this.buttonBind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBind.Location = new System.Drawing.Point(343, 17);
            this.buttonBind.Name = "buttonBind";
            this.buttonBind.Size = new System.Drawing.Size(75, 23);
            this.buttonBind.TabIndex = 4;
            this.buttonBind.Text = "Bind";
            this.buttonBind.UseVisualStyleBackColor = true;
            this.buttonBind.Click += new System.EventHandler(this.buttonBind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hotkey";
            // 
            // groupBoxMouse
            // 
            this.groupBoxMouse.Controls.Add(this.checkBoxMouseCallNext);
            this.groupBoxMouse.Controls.Add(this.labelMousePos);
            this.groupBoxMouse.Controls.Add(this.checkBoxMouseHookerEnabled);
            this.groupBoxMouse.Controls.Add(this.listBoxMousing);
            this.groupBoxMouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMouse.Location = new System.Drawing.Point(3, 337);
            this.groupBoxMouse.Name = "groupBoxMouse";
            this.groupBoxMouse.Size = new System.Drawing.Size(424, 161);
            this.groupBoxMouse.TabIndex = 4;
            this.groupBoxMouse.TabStop = false;
            this.groupBoxMouse.Text = "Mouse";
            // 
            // checkBoxMouseCallNext
            // 
            this.checkBoxMouseCallNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMouseCallNext.AutoSize = true;
            this.checkBoxMouseCallNext.Location = new System.Drawing.Point(250, 138);
            this.checkBoxMouseCallNext.Name = "checkBoxMouseCallNext";
            this.checkBoxMouseCallNext.Size = new System.Drawing.Size(97, 17);
            this.checkBoxMouseCallNext.TabIndex = 7;
            this.checkBoxMouseCallNext.Text = "Call Next Hook";
            this.checkBoxMouseCallNext.UseVisualStyleBackColor = true;
            // 
            // labelMousePos
            // 
            this.labelMousePos.AutoSize = true;
            this.labelMousePos.Location = new System.Drawing.Point(6, 163);
            this.labelMousePos.Name = "labelMousePos";
            this.labelMousePos.Size = new System.Drawing.Size(105, 13);
            this.labelMousePos.TabIndex = 7;
            this.labelMousePos.Text = "Mouse Position: N/A";
            // 
            // checkBoxMouseHookerEnabled
            // 
            this.checkBoxMouseHookerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMouseHookerEnabled.AutoSize = true;
            this.checkBoxMouseHookerEnabled.Location = new System.Drawing.Point(353, 138);
            this.checkBoxMouseHookerEnabled.Name = "checkBoxMouseHookerEnabled";
            this.checkBoxMouseHookerEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxMouseHookerEnabled.TabIndex = 6;
            this.checkBoxMouseHookerEnabled.Text = "Enabled";
            this.checkBoxMouseHookerEnabled.UseVisualStyleBackColor = true;
            // 
            // listBoxMousing
            // 
            this.listBoxMousing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMousing.ContextMenuStrip = this.contextMenuLogList;
            this.listBoxMousing.FormattingEnabled = true;
            this.listBoxMousing.IntegralHeight = false;
            this.listBoxMousing.Location = new System.Drawing.Point(6, 19);
            this.listBoxMousing.Name = "listBoxMousing";
            this.listBoxMousing.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxMousing.Size = new System.Drawing.Size(412, 113);
            this.listBoxMousing.TabIndex = 3;
            this.listBoxMousing.MouseEnter += new System.EventHandler(this.listBox_MouseEnter);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxKeyCallNext);
            this.groupBox3.Controls.Add(this.checkBoxKeyboardHookEnabled);
            this.groupBox3.Controls.Add(this.listBoxKeyboardHook);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(424, 161);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Keyboard hook";
            // 
            // checkBoxKeyCallNext
            // 
            this.checkBoxKeyCallNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKeyCallNext.AutoSize = true;
            this.checkBoxKeyCallNext.Location = new System.Drawing.Point(250, 138);
            this.checkBoxKeyCallNext.Name = "checkBoxKeyCallNext";
            this.checkBoxKeyCallNext.Size = new System.Drawing.Size(97, 17);
            this.checkBoxKeyCallNext.TabIndex = 6;
            this.checkBoxKeyCallNext.Text = "Call Next Hook";
            this.checkBoxKeyCallNext.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardHookEnabled
            // 
            this.checkBoxKeyboardHookEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKeyboardHookEnabled.AutoSize = true;
            this.checkBoxKeyboardHookEnabled.Location = new System.Drawing.Point(353, 138);
            this.checkBoxKeyboardHookEnabled.Name = "checkBoxKeyboardHookEnabled";
            this.checkBoxKeyboardHookEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxKeyboardHookEnabled.TabIndex = 5;
            this.checkBoxKeyboardHookEnabled.Text = "Enabled";
            this.checkBoxKeyboardHookEnabled.UseVisualStyleBackColor = true;
            // 
            // listBoxKeyboardHook
            // 
            this.listBoxKeyboardHook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxKeyboardHook.ContextMenuStrip = this.contextMenuLogList;
            this.listBoxKeyboardHook.FormattingEnabled = true;
            this.listBoxKeyboardHook.IntegralHeight = false;
            this.listBoxKeyboardHook.Location = new System.Drawing.Point(6, 19);
            this.listBoxKeyboardHook.Name = "listBoxKeyboardHook";
            this.listBoxKeyboardHook.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxKeyboardHook.Size = new System.Drawing.Size(412, 113);
            this.listBoxKeyboardHook.TabIndex = 4;
            this.listBoxKeyboardHook.MouseEnter += new System.EventHandler(this.listBox_MouseEnter);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxMouse, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(430, 501);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonFindOccupied);
            this.groupBox2.Controls.Add(this.listBoxOccupied);
            this.groupBox2.Location = new System.Drawing.Point(448, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 501);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Occupied Hooks";
            // 
            // buttonFindOccupied
            // 
            this.buttonFindOccupied.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindOccupied.Location = new System.Drawing.Point(6, 472);
            this.buttonFindOccupied.Name = "buttonFindOccupied";
            this.buttonFindOccupied.Size = new System.Drawing.Size(221, 23);
            this.buttonFindOccupied.TabIndex = 2;
            this.buttonFindOccupied.Text = "Find All";
            this.buttonFindOccupied.UseVisualStyleBackColor = true;
            this.buttonFindOccupied.Click += new System.EventHandler(this.buttonFindOccupied_Click);
            // 
            // listBoxOccupied
            // 
            this.listBoxOccupied.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOccupied.FormattingEnabled = true;
            this.listBoxOccupied.IntegralHeight = false;
            this.listBoxOccupied.Location = new System.Drawing.Point(6, 19);
            this.listBoxOccupied.Name = "listBoxOccupied";
            this.listBoxOccupied.Size = new System.Drawing.Size(221, 447);
            this.listBoxOccupied.TabIndex = 0;
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.AllowSoloModifiers = true;
            this.hotkeyTextBox.AllowToggleKeys = true;
            this.hotkeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotkeyTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.hotkeyTextBox.HotkeyKeyCode = System.Windows.Forms.Keys.None;
            this.hotkeyTextBox.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyTextBox.Location = new System.Drawing.Point(53, 19);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.Size = new System.Drawing.Size(284, 20);
            this.hotkeyTextBox.TabIndex = 0;
            // 
            // keyboardHooker
            // 
            this.keyboardHooker.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.keyboardHooker_KeyDown);
            this.keyboardHooker.KeyUp += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.keyboardHooker_KeyUp);
            // 
            // mouseHooker
            // 
            this.mouseHooker.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mouseHooker_MouseWheel);
            this.mouseHooker.MouseMoved += new System.Windows.Forms.MouseEventHandler(this.mouseHooker_MouseMoved);
            this.mouseHooker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseHooker_MouseUp);
            this.mouseHooker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseHooker_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 525);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(481, 528);
            this.Name = "MainForm";
            this.Text = "Global Input Demo";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuLogList.ResumeLayout(false);
            this.groupBoxMouse.ResumeLayout(false);
            this.groupBoxMouse.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HotKeyTextBox hotkeyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxMousing;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxKeyboardHook;
        private System.Windows.Forms.Button buttonBind;
        private System.Windows.Forms.CheckBox checkBoxKeyboardHookEnabled;
        private System.Windows.Forms.CheckBox checkBoxMouseHookerEnabled;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBoxBindings;
        private System.Windows.Forms.ListBox listBoxHKLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonUnbindAll;
        private System.Windows.Forms.Button buttonUnbindSelected;
        private System.Windows.Forms.CheckBox checkBoxInvokeEnabled;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxMouse;
        private System.Windows.Forms.ContextMenuStrip contextMenuLogList;
        private System.Windows.Forms.ToolStripMenuItem menuItemClear;
        private System.Windows.Forms.Label labelMousePos;
        private System.Windows.Forms.CheckBox checkBoxKeyCallNext;
        private System.Windows.Forms.CheckBox checkBoxMouseCallNext;
        private GlobalInput.Keyboard.KeyboardHooker keyboardHooker;
        private GlobalInput.Mouse.MouseHooker mouseHooker;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFindOccupied;
        private System.Windows.Forms.ListBox listBoxOccupied;
    }
}

