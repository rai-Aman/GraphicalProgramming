using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPL_Application
{
    public partial class addFile : Form
    {
        public addFile()
        {
            InitializeComponent();
        }

        private void addFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)this.MdiParent).Controls["panel3"].Visible = true;
            ((Form)this.MdiParent).Controls["midPanel"].Visible = true;


        }
    }
}
