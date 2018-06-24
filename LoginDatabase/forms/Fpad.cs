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
    public partial class Fpad : Form
    {
        public Fpad()
        {
            InitializeComponent();
        }
        private void Showdata()
        {




            string coct = "Data Source=192.168.1.252;Initial Catalog=vbnetuser;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string sql = "select id,Wname,Wpro,Wtel from [workers]";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["id"].ColumnName = "员工编号";
            ds.Tables[0].Columns["Wname"].ColumnName = "员工姓名";
            ds.Tables[0].Columns["Wpro"].ColumnName = "所属工序";
            ds.Tables[0].Columns["Wtel"].ColumnName = "员工电话";
            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void 员工名单_Load(object sender, EventArgs e)
        {
            Showdata();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 0 || comboBox1.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入正确员工信息！");
            }
            else
            {
                int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=vbnetuser;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [workers] (Wname,Wpro,Wtel) values ('" + textBox1.Text + "','" + comboBox1.Text + "'," + textBox3.Text + ")";

                SqlCommand cmd = new SqlCommand(myadd, con);

                cmd.ExecuteNonQuery();
                string coct1 = "Data Source=192.168.1.252;Initial Catalog=vbnetuser;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con1 = new SqlConnection(coct1);
                con1.Open();
                string sql = string.Format("update workers set Wdone='{0}',Wpaid= '{0}' ,Wdays ='{0}'", a);
                SqlCommand cmd1 = new SqlCommand(sql, con);
                cmd1.ExecuteNonQuery();
            }
           
            Showdata();
            
            comboBox1.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.252;Initial Catalog=vbnetuser;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string myupdate = "delete from [workers] where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
           
            SqlCommand cmd = new SqlCommand(myupdate, con);
           
            cmd.ExecuteNonQuery();
            
            Showdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             if (textBox1.Text.Length == 0 || comboBox1.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入正确员工信息！");
            }
            else
            {
            string coct = "Data Source=192.168.1.252;Initial Catalog=vbnetuser;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string myupdate = "update [workers] set Wname='" + textBox1.Text + "',Wpro='" + comboBox1.Text + "',Wtel='" + textBox3.Text + "'where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
           
            SqlCommand cmd = new SqlCommand(myupdate, con);
            
            cmd.ExecuteNonQuery();
           
            SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada1.Fill(ds);}
          
            Showdata();
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

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
