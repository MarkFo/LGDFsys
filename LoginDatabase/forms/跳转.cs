using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;   //命名空间

namespace LGD
{
    public partial class 跳转 : Form
    {

        public 跳转()
        {
            InitializeComponent();


        }


        /*
        private void 跳转_Load(object sender, EventArgs e)//原关闭登录，显示跳转窗体，同时关闭登录
        {
        this.Owner.Hide();//隐藏Form1 
        }
        */


        /*
        //private void EventForm_Load(object sender, EventArgs e)
        private void 跳转_Activated(object sender, System.EventArgs e)
        {
        this.Owner.Hide();//隐藏Form1  
        }
        */



        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            //登陆成功，进入另一个窗体
            快捷报工 快捷报工 = new 快捷报工();
            快捷报工.ShowDialog();

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            //登陆成功，进入另一个窗体
            修改密码 修改密码 = new 修改密码();
            修改密码.ShowDialog();

            this.Close();
        }

        //跳转窗体右上关闭按钮，彻底退出
        private void 跳转_FormClosing(object sender, FormClosingEventArgs e)
        {
         Environment.Exit(Environment.ExitCode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            杂工记录 杂工记录 = new 杂工记录();
            杂工记录.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            工资查询 工资查询 = new 工资查询();
            工资查询.ShowDialog();
            this.Close();
        }





        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("请补充必要信息！");
            }
            else
            {
                label3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [notice] ([item],[datetime]) values ('" + textBox2.Text + "','" + label3.Text + "')";

                SqlCommand cmd = new SqlCommand(myadd, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("发布完成,重新登录生效！");

                label3.Text = "";
                textBox2.Text = "";

                //发布完成后，重新隐藏2个控件
                button6.Visible = false;
                textBox2.Visible = false;

            }
        }

        private void 跳转_Load(object sender, EventArgs e)
        {

            //铺满全屏，隐藏任务栏
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //引用全局变量username，默认加载时填写
            label5.Text = UserInfo.UserName;

            //全局变量，加载姓名
            SqlConnection connUserName = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sqlUserName = "select * from 登录表 where ltrim(rtrim(用户名)) = rtrim(ltrim('" + label5.Text + "')) ";
            connUserName.Open();
            SqlCommand cmdUserName = new SqlCommand(sqlUserName, connUserName);
            SqlDataReader rdrUserName = cmdUserName.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdrUserName.Read())
            {
                //显示姓名和角色
                label9.Text = rdrUserName[2].ToString();
                connUserName.Close();

            }
            rdrUserName.Close();



            button19_Click(null, null);//调用button19  加载时检测权限,这条必须放在全集变量（UserName）下面，因为检测权限是按登录名来的

            //开始加载公告

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select * from notice where[datetime] = (select max([datetime]) from notice)";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {

                textBox1.Text = rdr[0].ToString();
                label2.Text = rdr[1].ToString();
                conn.Close();

            }
            rdr.Close();





        }
        //点击修改按钮，显示load时隐藏的2个控件
        private void button7_Click(object sender, EventArgs e)
        {
            button6.Visible = true;
            textBox2.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            QRcoderes QRcoderes = new QRcoderes();
            QRcoderes.ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            设备维修 设备维修 = new 设备维修();
            设备维修.ShowDialog();
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            领料确认 领料确认 = new 领料确认();
            领料确认.ShowDialog();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
           /* //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            ts ts = new ts();
            ts.ShowDialog();
            this.Close();*/

           // ts ts = new ts();
            //ts.ShowDialog();

            ts f2 = new ts();
            f2.Flag = textBox1.Text;
            //关键地方 ↓
            if (f2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = f2.Flag;
            }




        }

        private void button14_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            批量报工 批量报工 = new 批量报工();
            批量报工.ShowDialog();
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            this.Hide();//隐藏当前窗口 
            MRP采购需求 MRP采购需求 = new MRP采购需求();
            MRP采购需求.ShowDialog();
            this.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\FSYS\快捷平台自动生成项目.exe");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            //this.Hide();//隐藏当前窗口 
            生产计划 生产计划 = new 生产计划();
            生产计划.Show();
            //this.Close();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            自动挂号调整_form1 自动挂号调整_form1 = new 自动挂号调整_form1();
            自动挂号调整_form1.Show();
        }

        //权限检测
        private void button19_Click(object sender, EventArgs e)
        {
            //判断有权限修改公告的几个账户，显示出修改公告按钮button7
            if (label5.Text == "ss" || label5.Text == "00025" || label5.Text == "00001" || label5.Text == "00005" || label5.Text == "00031" || label5.Text == "10018" || label5.Text == "00332")
            {
                button7.Visible = true;
            }
            else
            {
                button7.Visible = false;
            }

            //判断有权限记录领料确认单，按钮button12
            if (label5.Text == "ss" || label5.Text == "00025" || label5.Text == "00005" || label5.Text == "00029" || label5.Text == "00298" || label5.Text == "10018" || label5.Text == "00332" || label5.Text == "00062" || label5.Text == "00067" || label5.Text == "00330")
            {
                button12.Visible = true;
            }
            else
            {
                button12.Visible = false;
            }
            //判断有权限记录修改订单产品编号，按钮button18
            if (label5.Text == "ss" || label5.Text == "00025" || label5.Text == "00012" || label5.Text == "00376" || label5.Text == "00330" || label5.Text == "00332" || label5.Text == "10018" || label5.Text == "00001" || label5.Text == "00005")
            {
                button18.Visible = true;
            }
            else
            {
                button18.Visible = false;
            }

            //判断有权限杂工派工，按钮button18
            if (label5.Text == "ss" || label5.Text == "00025" || label5.Text == "00031" || label5.Text == "00376" || label5.Text == "00330" || label5.Text == "00332" || label5.Text == "00062" || label5.Text == "00067" || label5.Text == "10018" || label5.Text == "00005")
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }



        }

        /*//bom管理
        private void button20_Click(object sender, EventArgs e)
        {
            bom管理 bomM = new bom管理();
            bomM.Show();
        }*/

        //bom管理_正式
        private void button21_Click(object sender, EventArgs e)
        {
            forms.bom管理_form1 bomMf1 = new forms.bom管理_form1();
            bomMf1.Show();
        }
    }
}