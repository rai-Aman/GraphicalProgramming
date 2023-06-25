using System.Windows.Forms;

namespace GPL_Application
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.numberLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.HomePanel = new System.Windows.Forms.Panel();
            this.drawingArea = new System.Windows.Forms.PictureBox();
            this.errorPanel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ErrorTextBox = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TermsImage = new System.Windows.Forms.PictureBox();
            this.TerminalTextBox = new System.Windows.Forms.RichTextBox();
            this.midPanel = new System.Windows.Forms.Panel();
            this.Reset = new System.Windows.Forms.Button();
            this.Build = new System.Windows.Forms.Button();
            this.PageAddButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.HomePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.errorPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TermsImage)).BeginInit();
            this.midPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // numberLabel
            // 
            this.numberLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numberLabel.Location = new System.Drawing.Point(3, 0);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(37, 267);
            this.numberLabel.TabIndex = 1;
            this.numberLabel.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16";
            this.numberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1898, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.PageAddButton_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openTool);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitTool);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(193, 34);
            this.aboutToolStripMenuItem.Text = "View Help";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenToolStripMenuItem,
            this.themeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.CheckOnClick = true;
            this.fullScreenToolStripMenuItem.DoubleClickEnabled = true;
            this.fullScreenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fullScreenToolStripMenuItem.Image")));
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(198, 34);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkToolStripMenuItem,
            this.lightToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(198, 34);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(153, 34);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.FloralWhite;
            this.startButton.Enabled = false;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startButton.Location = new System.Drawing.Point(6, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(83, 31);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "▶ Start";
            this.startButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // HomePanel
            // 
            this.HomePanel.Controls.Add(this.drawingArea);
            this.HomePanel.Controls.Add(this.errorPanel5);
            this.HomePanel.Controls.Add(this.panel4);
            this.HomePanel.Controls.Add(this.panel2);
            this.HomePanel.Location = new System.Drawing.Point(0, 85);
            this.HomePanel.Name = "HomePanel";
            this.HomePanel.Size = new System.Drawing.Size(1886, 908);
            this.HomePanel.TabIndex = 6;
            // 
            // drawingArea
            // 
            this.drawingArea.BackColor = System.Drawing.Color.Transparent;
            this.drawingArea.Location = new System.Drawing.Point(1185, 52);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(690, 472);
            this.drawingArea.TabIndex = 1;
            this.drawingArea.TabStop = false;
            // 
            // errorPanel5
            // 
            this.errorPanel5.BackColor = System.Drawing.Color.LightGray;
            this.errorPanel5.Controls.Add(this.pictureBox1);
            this.errorPanel5.Controls.Add(this.label2);
            this.errorPanel5.Controls.Add(this.ErrorTextBox);
            this.errorPanel5.Location = new System.Drawing.Point(12, 626);
            this.errorPanel5.Name = "errorPanel5";
            this.errorPanel5.Size = new System.Drawing.Size(1159, 257);
            this.errorPanel5.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1107, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Error List";
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTextBox.ForeColor = System.Drawing.Color.IndianRed;
            this.ErrorTextBox.Location = new System.Drawing.Point(3, 37);
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ReadOnly = true;
            this.ErrorTextBox.Size = new System.Drawing.Size(1153, 217);
            this.ErrorTextBox.TabIndex = 0;
            this.ErrorTextBox.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Location = new System.Drawing.Point(6, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1173, 612);
            this.panel4.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1173, 609);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.TermsImage);
            this.panel2.Controls.Add(this.TerminalTextBox);
            this.panel2.Location = new System.Drawing.Point(1194, 549);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 356);
            this.panel2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Terminal";
            // 
            // TermsImage
            // 
            this.TermsImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TermsImage.BackgroundImage")));
            this.TermsImage.Location = new System.Drawing.Point(5, 11);
            this.TermsImage.Name = "TermsImage";
            this.TermsImage.Size = new System.Drawing.Size(34, 26);
            this.TermsImage.TabIndex = 5;
            this.TermsImage.TabStop = false;
            // 
            // TerminalTextBox
            // 
            this.TerminalTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TerminalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TerminalTextBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TerminalTextBox.Location = new System.Drawing.Point(3, 41);
            this.TerminalTextBox.Name = "TerminalTextBox";
            this.TerminalTextBox.Size = new System.Drawing.Size(686, 312);
            this.TerminalTextBox.TabIndex = 3;
            this.TerminalTextBox.Text = "";
            this.TerminalTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalTextBox_KeyDown);
            // 
            // midPanel
            // 
            this.midPanel.Controls.Add(this.Reset);
            this.midPanel.Controls.Add(this.Build);
            this.midPanel.Controls.Add(this.PageAddButton);
            this.midPanel.Controls.Add(this.startButton);
            this.midPanel.Location = new System.Drawing.Point(3, 44);
            this.midPanel.Name = "midPanel";
            this.midPanel.Size = new System.Drawing.Size(1883, 43);
            this.midPanel.TabIndex = 8;
            // 
            // Reset
            // 
            this.Reset.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reset.Location = new System.Drawing.Point(1775, -3);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(93, 40);
            this.Reset.TabIndex = 1;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = false;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Build
            // 
            this.Build.AccessibleName = "";
            this.Build.BackColor = System.Drawing.Color.FloralWhite;
            this.Build.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Build.Location = new System.Drawing.Point(201, 3);
            this.Build.Name = "Build";
            this.Build.Size = new System.Drawing.Size(100, 31);
            this.Build.TabIndex = 10;
            this.Build.Text = "Build";
            this.Build.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Build.UseVisualStyleBackColor = false;
            this.Build.Click += new System.EventHandler(this.Build_Click);
            // 
            // PageAddButton
            // 
            this.PageAddButton.BackColor = System.Drawing.Color.FloralWhite;
            this.PageAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PageAddButton.Location = new System.Drawing.Point(95, 3);
            this.PageAddButton.Name = "PageAddButton";
            this.PageAddButton.Size = new System.Drawing.Size(100, 31);
            this.PageAddButton.TabIndex = 10;
            this.PageAddButton.Text = "Add Page";
            this.PageAddButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PageAddButton.UseVisualStyleBackColor = false;
            this.PageAddButton.Click += new System.EventHandler(this.PageAddButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1024);
            this.Controls.Add(this.midPanel);
            this.Controls.Add(this.HomePanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.HomePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).EndInit();
            this.errorPanel5.ResumeLayout(false);
            this.errorPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TermsImage)).EndInit();
            this.midPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label numberLabel;


        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.Panel HomePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox TermsImage;
        private System.Windows.Forms.Panel midPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel errorPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private Button PageAddButton;
        private System.Windows.Forms.TabControl tabControl1;
        private ContextMenuStrip contextMenuStrip1;
        private Button Reset;
        protected internal RichTextBox TerminalTextBox;
        protected internal RichTextBox ErrorTextBox;
        private Timer timer1;
        private Button Build;
        public PictureBox drawingArea;
    }
}

