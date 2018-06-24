using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGD
{
    public partial class 自动挂号调整_form1 : Form
    {
        public 自动挂号调整_form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            自动挂号调整WF 自动挂号调整WF = new 自动挂号调整WF();
            自动挂号调整WF.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            自动挂号调整GXY 自动挂号调整GXY = new 自动挂号调整GXY();
            自动挂号调整GXY.ShowDialog();
            this.Close();
        }
    }
}
