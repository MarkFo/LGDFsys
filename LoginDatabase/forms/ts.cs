using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LGD
{
    public partial class ts : Form
    {
        public ts()
        {
            InitializeComponent();
        }

        //激活右下角弹出窗口
        private void button1_Click(object sender, EventArgs e)
        {
            notice frmShowWarning = new notice();//Form1为要弹出的窗体（提示框），  
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - frmShowWarning.Width, Screen.PrimaryScreen.WorkingArea.Height);
            frmShowWarning.PointToScreen(p);
            frmShowWarning.Location = p;
            frmShowWarning.Show();
            for (int i = 0; i <= frmShowWarning.Height; i++)
            {
                frmShowWarning.Location = new Point(p.X, p.Y - i);
                Thread.Sleep(10);//将线程沉睡时间调的越小升起的越快  
            }
        }

        //设想，通过spy++抓取其他软件的窗口句柄，实现按钮点击的效果//暂不考虑，改为测试传值
        private void button2_Click(object sender, EventArgs e)
        {
            flag = this.textBox1.Text;//回传到form1
            //关键地方 ↓
            this.DialogResult = DialogResult.OK;
        }

        private void ts_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口*/



        }

        //传值用
        private string flag;
        /// <summary>
        /// 接收传过来的值
        /// </summary>
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        private void ts_Load(object sender, EventArgs e)
        {
            textBox1.Text = this.flag;//窗体加载时接收flag的值
        }


        //测试生产计划右下弹窗
        private void button3_Click(object sender, EventArgs e)
        {
            forms.生产计划_form3 frmShowWarning = new forms.生产计划_form3();//Form1为要弹出的窗体（提示框），  
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - frmShowWarning.Width, Screen.PrimaryScreen.WorkingArea.Height);
            frmShowWarning.PointToScreen(p);
            frmShowWarning.Location = p;
            frmShowWarning.Show();
            for (int i = 0; i <= frmShowWarning.Height; i++)
            {
                frmShowWarning.Location = new Point(p.X, p.Y - i);
                Thread.Sleep(10);//将线程沉睡时间调的越小升起的越快  
            }
            
        }




    }
}
