using GPL_Application.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Button = System.Windows.Forms.Button;
using File = System.IO.File;

namespace GPL_Application
{
    /// <summary>
    /// This Class is to make normal GPL application.
    /// </summary>
    public partial class Form1 : Form
    {
        //Bitmap OutputBitmap = new Bitmap(680, 500);


        private string FileName = string.Empty;
        protected internal RichTextBox rt, focusedRtb;
        private TabPage tp;
        private Brush br;
        private bool DarkTheme = false;
        private bool pageStatus = false;
        ShapeFactory f;
        Color Color;
        Shape shape;
        string command;
        protected internal string parameters;
        protected internal bool Fills;
        public CommandParser  comParser1;
        string[] splittedCp;
        protected internal Color color;
        Bitmap bitmap;
        Graphics g1;
        protected internal int otherLines = 1;
        public int Lines = 1;
        protected internal int zeroLine = 0;
        protected internal Thread ThreadToBeUse;
        protected internal bool validationCheck;
        Method myMethod;

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private List<RichTextBox> TabRichTextBox = new List<RichTextBox>();
        private List<CommandParser> comParserList = new List<CommandParser>();
        internal protected List<string> ChooseColor = new List<string>();
        public bool Refresh = false;
        private List<Shape> addedShape = new List<Shape>();


        private Dictionary<string, int> varValue = new Dictionary<string, int>();




        //(No.Of Rows in Terminal, Starting Point of each Line)
        Dictionary<int, int> TSpaceSeq = new Dictionary<int, int>();

        //private string [] commands = {"Run", "Clear"};


        protected internal List<string> commandList = new List<string>();

        /// <summary>
        /// Class to make simple GPL Application
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            f = new ShapeFactory();

            ChooseColor.Add("BLUE");
            ChooseColor.Add("GREY");
            ChooseColor.Add("BLACK");
            ChooseColor.Add("BROWN");

            commandList.Add("CIRCLE");
            commandList.Add("TRIANGLE");
            commandList.Add("RECTANGLE");
            commandList.Add("SQUARE");
            commandList.Add("DRAWTO");
            commandList.Add("MOVETO");
            commandList.Add("PEN");
            commandList.Add("FILL");
            commandList.Add("RUN");
            commandList.Add("RESET");
            commandList.Add("CLEAR");
            commandList.Add("THEN");
            commandList.Add("ENDLOOP");
            commandList.Add("WHILE");
            commandList.Add("BROWNRED");
            commandList.Add("PINKBLACK");
            commandList.Add("METHOD");
            commandList.Add("ENDMETHOD");

            color = Color.Black;
            comParser1 = new CommandParser(this);
            myMethod = new Method(this,comParser1);
        }
        /// <summary>
        /// This method is made made to focus the running tab page to only execute that particular texxt box code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox_Enter(object sender, EventArgs e)
        {
            focusedRtb = (RichTextBox)sender;

        }


        /// <summary>
        /// This openTool function is designed to open a pre-coded file in a file browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openTool(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                //openFile.ShowDialog();
                openFile.Title = "Browse .cs extensions file only";
                openFile.Filter = ".cs files only (*.cs) | *.cs";
                openFile.DefaultExt = "cs";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1; 
                    createTabPage();
                    focusedRtb = TabRichTextBox.Last();
                    //rt = TabRichTextBox.ElementAt(tabControl1.SelectedIndex);
                    //this.rt.Clear();
                    focusedRtb.Text = File.ReadAllText(openFile.FileName);
                    startButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while uploading the file" + ex);
            }
        }
        /// <summary>
        /// This method is made to exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void exitTool(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Exit Application?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Application.Exit();
                    if(ThreadToBeUse != null) ThreadToBeUse.Abort();
                }
            }
            catch (ThreadAbortException ex)
            {
                // Do something with the exception, such as logging it.
            }

        }
        /// <summary>
        /// This method is made to open new form for new file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            MdiClient ctlMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;
                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;

                }
                catch
                {

                }
            }

            startButton.Enabled = false;


        }
        /// <summary>
        /// This method is made to maximize the screen of code box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel4.Dock = DockStyle.Fill;
                tabControl1.Dock = DockStyle.Fill;

                rt.Dock = DockStyle.Fill;

                panel2.Visible = false;
                //ShapePanel.Visible = false;
                errorPanel5.Visible = false;


            }
            else
            {
                panel4.Dock = DockStyle.None;
                tabControl1.Dock = DockStyle.Fill;

                rt.Dock = DockStyle.Fill;

                panel2.Visible = true;
                //ShapePanel.Visible = true;
                errorPanel5.Visible = true;

            }

        }
        private void saveFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.rt.Text))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = "Untitled";
                    saveFileDialog.Filter = ".cs files only (*.cs) | *.cs";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                        using (StreamWriter sw = new StreamWriter(s))
                        {
                            sw.Write(rt.Text);

                        }

                    }


                }
                else
                {
                    MessageBox.Show("Error", "Cannot Save empty file.", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                //Show Save filedialog
                saveAsToolStripMenuItem_Click(sender, e);
            }
            StreamWriter writer = new StreamWriter(FileName);
            writer.Write(rt.Text);
            writer.Close();

        }
        int TabNum = 1;


        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //taking color from hex code 
            HomePanel.BackColor = ColorTranslator.FromHtml("#3b444b");
            //tabControl1.TabPages[0].BackColor = Color.Green;
            tabControl1.BackColor = ColorTranslator.FromHtml("#3b444b");
            ErrorTextBox.BackColor = ColorTranslator.FromHtml("#3b444b");


            RichTextBox currRTB;
            for (int i = tabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                SetTabHeader(tabControl1.TabPages[i], ColorTranslator.FromHtml("#3b444b"));
                //tabControl1.Invalidate();
                currRTB = TabRichTextBox[i];
                currRTB.BackColor = ColorTranslator.FromHtml("#3b444b");
                currRTB.ForeColor = Color.White;

            }
            DarkTheme = true;
        }
        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DarkTheme == true)
            {
                DarkTheme = false;
            }
        }

        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            tabControl1.Invalidate();
        }



        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Text Drawn in tab pages.
            var currentPage = tabControl1.TabPages[e.Index];
            var currnetTabSize = tabControl1.GetTabRect(e.Index);
            TextRenderer.DrawText(e.Graphics, currentPage.Text, currentPage.Font, currnetTabSize, currentPage.ForeColor);


            br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]);

            e.Graphics.FillRectangle(br, e.Bounds);

            SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);

            string PageSize = tabControl1.TabPages[e.Index].Text;

            Color color = TabColors[tabControl1.TabPages[e.Index]];
            //Draw rectangle representing size of string.
            e.Bounds.Offset(0, -1);
            e.Bounds.Inflate(-5, -5);
            e.Graphics.DrawRectangle(new Pen(color, 1), e.Bounds);
            e.DrawFocusRectangle();


            // Draw string to screen.
            if (DarkTheme)
            {
                e.Graphics.DrawString(PageSize, e.Font, Brushes.White, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

            }
            else
            {
                e.Graphics.DrawString(PageSize, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);
            }

        }
        private void storeRtbObject(RichTextBox richtextbox)
        {
            TabRichTextBox.Add(richtextbox);
        }
        private void createTabPage()
        {
            tp = new TabPage();
            tabControl1.TabPages.Add(tp);
            tp.Text = "New_Tab" + TabNum;
            tp.BorderStyle = BorderStyle.None;

            string TextBoxName = "New_Tab" + TabNum;
            rt = new RichTextBox();
            rt.Dock = DockStyle.Fill;
            rt.Name = TextBoxName;
            rt.Enter += new System.EventHandler(richTextBox_Enter);
            //rt.KeyDown += new System.Windows.Forms.KeyEventHandler(richTextBox_KeyDown);
            //rt.TextChanged += new EventHandler(richTextBox_TextChanged);



            tabControl1.TabPages[TabNum - 1].Controls.Add(rt);
            TabNum++;

            storeRtbObject(rt);
            pageStatus = true;



            for (int i = tabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                //tabControl1.Invalidate();
                if (DarkTheme == false)
                {
                    SetTabHeader(tabControl1.TabPages[i], Color.White);
                    rt.BackColor = Color.White;
                    rt.ForeColor = Color.Black;
                }
                else
                {
                    SetTabHeader(tabControl1.TabPages[i], Color.Black);
                    rt.BackColor = Color.Black;
                    rt.ForeColor = Color.White;
                }
            }
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;



        }

        protected internal int lineIncrement = 0;

        /// <summary>
        /// This method is made to read every code line by line from coding area.
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        protected internal string getLineTextOfCode(RichTextBox rtb)
        {
            string lineText = "";
            if (focusedRtb.Lines.Length < Lines)
            {
                Lines--;
            }
            if (focusedRtb.Lines.ElementAt(Lines - 1) != "")
            {
                lineText = rtb.Lines.ElementAt(lineIncrement);
                lineIncrement++;
            }
            return lineText.Trim().ToUpper();
        }
        /// <summary>
        /// This method is to get the text which is written in terminal text box.
        /// </summary>
        /// <returns></returns>
        private string getLineTextOfTerminal()
        {
            int cursorPosition = TerminalTextBox.SelectionStart; //selectionStart tells you where your cursor is.
            int lineIndex = TerminalTextBox.GetLineFromCharIndex(cursorPosition);
            string lineText = TerminalTextBox.Lines.ElementAt(lineIndex);

            return lineText.Trim().ToUpper();

        }
        /// <summary>
        /// Methods to run commands.
        /// </summary>
        /// <param name="cp1"></param>
        public void RunCommand(String nowCmd, String pickColor)
        {
                if (nowCmd.Equals("FILL"))
                {
                    string fillStatus = pickColor;

                    if (fillStatus != "ON" && fillStatus != "OFF")
                    {
                       comParser1.showError("Please enter valid parameters");
                    }
                    else if (fillStatus == "ON")
                    {
                        Fills = true;
                    }
                    else if (fillStatus == "OFF")
                    {
                        Fills = false;
                    }
                }
                else if (nowCmd.Equals("PEN"))
                {
                    //string pickColor = nowCmd[1].Trim().ToUpper();
                    if (int.TryParse(pickColor, out _))
                    {
                        comParser1.showError("Invalid Color");
                    }
                    else
                    {
                        if (ChooseColor.Contains(pickColor))
                        {
                            color = chooseColor(pickColor);
                            //shape.draw(g1, false, chooseColor(pickColor));
                        }
                        else
                        {       
                            comParser1.showError("Color is not availabe");
                        }
                    }
                }
        }
        /// <summary>
        /// This method is made to read the each and every line which is written in text box.
        /// </summary>
        protected internal void escapeNewLine()
        {
            RichTextBox currentRtb = focusedRtb;
            int rtbLinesLength = 1;
            lineIncrement = 0;

            foreach (RichTextBox rtb in TabRichTextBox)
            {
                if (rtb.Equals(currentRtb) && currentRtb.Text != null)
                {
                    while (rtbLinesLength <= currentRtb.Lines.Count())
                    {
                        if (zeroLine >= 1)
                        {
                            Lines++;
                        }
                        string lineText = getLineTextOfCode(currentRtb);
                        comParser1.runCondStatement(lineText);
                        //ErrorTextBox.Text += lineText;
                        if (!lineText.Equals(""))
                        {
                            rtbLinesLength++;
                        }
                        zeroLine++;
                    }
                }
            }
        }

        /// <summary>
        /// This method is to clear all previous errors and output everytime when start button clicked or run command is executed
        /// </summary>
        protected internal void clearAll()
        {
            if (comParser1 != null)
            {
                comParser1.g1.Clear(Color.Transparent);
                Fills = false;
                Color = Color.BlanchedAlmond;
                comParser1.commandValidation("MOVETO", "0,0");
                ErrorTextBox.Text = "";
                Lines = 1;
                comParser1.lastWhile = false;
                otherLines = 1;
                zeroLine = 0;
                comParser1.splitterStatus = false;
                comParser1.while_status = false;
                comParser1.isMethod = false;
                comParser1.VariableAndValue.Clear();
                comParser1.whileCount = 0;
                comParser1.ifCount = 0;
                comParser1.multiLineIf = false;
                comParser1.multipleIfData.Clear();
                comParser1.VariableAndValue.Clear();
                comParser1.whileDatas.Clear();
                comParser1.NumberOfErrors.Clear();
                myMethod.methodLocalVar = new Dictionary<string, string>();
                myMethod.called = false;
                myMethod.methodList = new ArrayList();
                myMethod.methodCodeBlockLines = new ArrayList();
               
            }
        }
        private void TerminalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TerminalTextBox.ReadOnly = false;
                if (pageStatus == false)
                {
                    TerminalTextBox.Text = ("Please add page to proceed with command.\n");
                    TerminalTextBox.ReadOnly = true;
                    TerminalTextBox.Select(TerminalTextBox.Text.Length, 0);
                }
                if (e.KeyCode == Keys.Enter)
                {
                    string commandLine = getLineTextOfTerminal();
                    comParser1.getProgramText(commandLine);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// This chooseColor class is simply to choose colors for different shapes to draw and fill with it.
        /// </summary>
        /// <param name="penColor"></param>
        /// <returns></returns>
        public Color chooseColor(string penColor)
        {
            switch (penColor)
            {
                case "BLACK":
                    return Color.Black;
                   
                case "BLUE":
                    return Color.Blue;
               
                case "BROWN":
                    return Color.Brown;
                
                case "GREY":
                    return Color.Gray; 
           
                default:
                    comParser1.showError("INVALID COLOR. \n");
                    return Color.BlanchedAlmond; 
            }
        }
        /// <summary>
        /// This is methodis made to clear the drawing area.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (focusedRtb.Text == "")
                {
                    comParser1.showError("Error Occured \n\t Empty programming area.\r\n");
                }
                else
                {
                    validationCheck = false;
                    clearAll();
                    lineIncrement = 0;
                    escapeNewLine();

                }
            }
            catch (NullReferenceException)
            {
                comParser1.showError("Error Occured \n\t Empty programming area.\r\n");
            }
        }
        private void Build_Click(object sender, EventArgs e)
        {
            validationCheck = true;
            clearAll();
            String source = "Validation";
            Refresh = true;
            escapeNewLine();
            if (comParser1.isMethod)
            {
                comParser1.showError("Method was never terminated.");
            }
            if (comParser1.NumberOfErrors.Count > 0)
            {
                ErrorTextBox.Text = "";
                startButton.Enabled = false;
                foreach (var value in comParser1.NumberOfErrors.Values)
                {
                    ErrorTextBox.Text += (value + "\n\n");
                }
            }
            else
            {
                startButton.Enabled= true; 
            }
        }

        /// <summary>
        /// This method is made to add neew tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageAddButton_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count >= 0)
            {
                createTabPage();

                startButton.Enabled = true;
                TerminalTextBox.Clear();
            }
        }
    }
}




     



