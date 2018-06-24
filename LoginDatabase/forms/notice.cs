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
    public partial class notice : Form
    {
        public notice()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;//停止timer2计时器，  
            if (this.Opacity > 0 && this.Opacity <= 1)//开始执行弹出窗渐渐透明  
            {
                //this.Opacity = this.Opacity - 0.05;//透明频度0.05  
                this.Opacity = this.Opacity - 0.02;//透明频度0.05  
            }
            if (System.Windows.Forms.Control.MousePosition.X >= this.Location.X && System.Windows.Forms.Control.MousePosition.Y >= this.Location.Y)//每次都判断鼠标是否是在弹出窗上，使用鼠标在屏幕上的坐标跟弹出窗体的屏幕坐标做比较。  
            {
                timer2.Enabled = true;//如果鼠标在弹出窗上的时候，timer2开始工作  
                timer1.Enabled = false;//timer1停止工作。  
            }
            if (this.Opacity == 0)//当透明度==0的时候，关闭弹出窗以释放资源。  
            {
                this.Close();
            }
        }

        private void notice_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            this.ShowInTaskbar = false;///使窗体不显示在任务栏
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /// <summary>  
            ///   
            /// 判断鼠标是不是还在弹出框上，如果不是则timer1又可以开始工作了  
            /// </summary>  
            /// <param name="sender"></param>  
            /// <param name="e"></param>  

                timer1.Enabled = false;//timer1停止工作  
                this.Opacity = 1;//弹出窗透明度设置为1，完全不透明  
                if (System.Windows.Forms.Control.MousePosition.X < this.Location.X && System.Windows.Forms.Control.MousePosition.Y < this.Location.Y)//如下  
                {
                    timer1.Enabled = true;
                    timer2.Enabled = false;
                
                 }
        }
    }
}
