using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFSC
{
    public partial class Info : System.Windows.Forms.Form
    {
        public Info()
        {
            InitializeComponent();
            ToolTip t = new ToolTip();
            t.SetToolTip(label1, "By Serghiy vishnevskiy, 2021");
        }

        private void CloseWinBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinWinBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BackToMainFrm_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Dispose();
            this.Close();
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("serhii.vishnevskyi@gmail.com");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(@"http://" + @"pek.nau.edu.ua/");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(@"http://" + @"github.com/Serhii-vishn/Automated_Control_Information_System_AFSC.git");
        }
    }
}
