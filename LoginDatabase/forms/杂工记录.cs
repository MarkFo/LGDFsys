using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace LGD
{
    public partial class 杂工记录 : Form
    {
        public 杂工记录()
        {
            InitializeComponent();
            label3.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写

            //开始就将dateTimePicker1加入textBox1，防止录入人员不选
            textBox1.Text = dateTimePicker1.Value.ToString();
        }

        //一.添加按钮
        private void 杂工记录button4_Click(object sender, EventArgs e)
        {


            /*
            SqlConnection con1 = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from additional where [guid] = '" + label4.Text + "' "), con1);
            if ((int)cmd1.ExecuteScalar() > 0)
            {
                MessageBox.Show("已存在，请勿重复记录", "提示");
                con1.Close();
            }


            else */if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("请补充必要信息！");
            }



            /*
            else if 
                (
            string coct1 = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con1 = new SqlConnection(coct1);
            con1.Open();
            string myadd = "insert into [codelist] (name,code,datetime,pdn,time) values ('" + label7.Text + "','" + label3.Text + "','" + label5.Text + "','" + label4.Text + "','" + label8.Text + "')";

            SqlCommand cmd1 = new SqlCommand(myadd, con1);
            

            cmd1.ExecuteNonQuery();

            )
            {
             MessageBox.Show("已报工！");

            }
            */

            else
            {
                //重新计算一遍，防止手工没点计算
                double num1 = double.Parse(textBox5.Text.Trim());
                double num2 = double.Parse(textBox4.Text.Trim());
                label14.Text = ((num1 / 8) * num2).ToString();


                //新增前刷新唯一标识符、实际添加时间
                label4.Text = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09唯一标识符
                label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [additional] ([owner],[actdispdate],[theodispdate],[project],[item],[userid],[name],[manhours],[stsala],[sala],[guid]) values ('" + label3.Text + "','" + textBox1.Text + "','" + label6.Text + "','杂工','" + textBox2.Text + "','" + label15.Text + "','" + label10.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + label14.Text + "','" + label4.Text + "')";

                SqlCommand cmd = new SqlCommand(myadd, con);


                cmd.ExecuteNonQuery();

                MessageBox.Show("新增完成！");

                //重新执行dgrid1查询
                button7_Click_1(sender, e);


                /*
                string coct1 = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
                SqlConnection con1 = new SqlConnection(coct1);
                con1.Open();
                string sql = string.Format("update workers set Wdone='{0}',Wpaid= '{0}' ,Wdays ='{0}'", a);
                SqlCommand cmd1 = new SqlCommand(sql, con);
                cmd1.ExecuteNonQuery();
                */
            }

            //Showdata();

            comboBox1.Text = "";
            label15.Text = "";
            textBox1.Text = "";
        }


        //二.删除按钮
        private void button3_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string myupdate = "delete from [additional] where guid='" + label4.Text + "' ";

            SqlCommand cmd = new SqlCommand(myupdate, con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("删除完成！");
            //Showdata();
        }


        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || comboBox1.Text.Length == 0 || label15.Text.Length == 0)
            {
                MessageBox.Show("请输入正确员工信息！");
            }
            else
            {
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update [additional] set Wname='" + textBox1.Text + "',Wpro='" + comboBox1.Text + "',Wtel='" + label15.Text + "'where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
            }

            //Showdata();
            textBox1.Text = "";
            comboBox1.Text = "";
            label15.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            //comboBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //label5.Text = DateTime.Now.ToString("yyyyMMdd");
            //label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //label3.Text = dataGridView1.SelectedCells[0].Value.ToString();
            label4.Text = dataGridView1.SelectedCells[0].Value.ToString();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        //窗口关闭，回到跳转窗体
        private void 杂工记录_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //private void Showdata()
 

                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();

                string sql = "select [guid],[owner],[actdispdate],[theodispdate],[project],[item],[userid],[name],[manhours],[stsala],[sala] from additional " +
                             "WHERE rtrim(ltrim([owner]))=rtrim(ltrim('" + label3.Text + "')) ";

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds);
                ds.Tables[0].Columns["guid"].ColumnName = "条码数据";
                ds.Tables[0].Columns["owner"].ColumnName = "派工者";
                ds.Tables[0].Columns["actdispdate"].ColumnName = "实际派工时间";
                ds.Tables[0].Columns["theodispdate"].ColumnName = "理论派工时间";
                ds.Tables[0].Columns["project"].ColumnName = "项目";
                ds.Tables[0].Columns["item"].ColumnName = "内容";
                ds.Tables[0].Columns["userid"].ColumnName = "工号";
                ds.Tables[0].Columns["name"].ColumnName = "姓名";
                ds.Tables[0].Columns["manhours"].ColumnName = "工时";
                ds.Tables[0].Columns["stsala"].ColumnName = "日工资标准";
                ds.Tables[0].Columns["sala"].ColumnName = "工资";

                this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

        }



        private void 杂工记录_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button9.Visible = false;
            timer1.Interval = 1000;
            timer1.Start();
            //首先显示所属数据
            button7_Click_1(sender, e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label15.Text = dataGridView2.SelectedCells[0].Value.ToString();
            label10.Text = dataGridView2.SelectedCells[1].Value.ToString();
            //textBox4.Text = dataGridView2.SelectedCells[0].Value.ToString();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            button9.Visible = true;

            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [用户名],[name] from 登录表 " +
                         "WHERE rtrim(ltrim([用户名])) like '%" + textBox3.Text + "%' or rtrim(ltrim([name])) like '%" + textBox3.Text + "%' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["用户名"].ColumnName = "工号";
            ds.Tables[0].Columns["name"].ColumnName = "姓名";

            this.dataGridView2.DataSource = ds.Tables[0].DefaultView;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button9.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = comboBox1.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            /*
            double num1 = double.Parse(textBox5.Text.Trim());
            double num2 = Convert.ToDouble(textBox4.Text.Trim());
            */
            double num1 = double.Parse(textBox5.Text.Trim());
            double num2 = double.Parse(textBox4.Text.Trim());
            label14.Text = ((num1 / 8) * num2).ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1.Text = textBox1.Text;
            textBox1.Text = dateTimePicker1.Value.ToString();
        }




        /*
        private void button5_Click(object sender, EventArgs e)//跳转考勤页面并传值ID
        {
           考勤情况 f2 = new 考勤情况(dataGridView1.SelectedCells[0].Value.ToString());
           f2.ShowDialog();
        }
        */

    }
}
