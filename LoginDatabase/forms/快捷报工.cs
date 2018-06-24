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
    public partial class 快捷报工 : Form
    {
        public 快捷报工()
        {
            InitializeComponent();
        }
        //private void Showdata()
        private void button6_Click(object sender, EventArgs e)//高新园数据
        {

        
        string coct = "Data Source=192.168.1.253;Initial Catalog=HONGYI;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            
            string sql = "SELECT rtrim(ltrim(SFCTA.TA001))+rtrim(ltrim(SFCTA.TA002))+rtrim(ltrim(SFCTA.TA003)) as ts,rtrim(ltrim(MOCTA.TA057)) as TA057,rtrim(ltrim(CMSMW.MW002)) as MW002,SFCTA.UDF07 as UDF07,rtrim(ltrim(CMSMV.MV002)) as MV002,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 "+
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) "+
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 "+
                         "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' and   rtrim(ltrim(SFCTA.UDF09))='" + textBox2.Text + "' ";
                         
/*
            string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                         "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
*/
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["ts"].ColumnName = "条码数据";
            ds.Tables[0].Columns["TA057"].ColumnName = "产品序列号";
            ds.Tables[0].Columns["MW002"].ColumnName = "工序名称";
            ds.Tables[0].Columns["UDF07"].ColumnName = "焊缝编号";
            ds.Tables[0].Columns["MV002"].ColumnName = "姓名";
            ds.Tables[0].Columns["TA022"].ColumnName = "工时";
            ds.Tables[0].Columns["TA059"].ColumnName = "工资";
            
            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }


        
        private void 快捷报工_Load(object sender, EventArgs e)
        {
            //铺满全屏，隐藏任务栏
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            dataGridView1.RowsDefaultCellStyle.Font = new Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //Showdata();
            timer1.Interval = 1000;
            timer1.Start();

            textBox2.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写

        }


        //威孚报工数据
        private void button7_Click(object sender, EventArgs e)
        {

            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT rtrim(ltrim(SFCTA.TA001))+rtrim(ltrim(SFCTA.TA002))+rtrim(ltrim(SFCTA.TA003)) as ts,rtrim(ltrim(MOCTA.TA057)) as TA057,rtrim(ltrim(CMSMW.MW002)) as MW002,SFCTA.UDF07 as UDF07,rtrim(ltrim(CMSMV.MV002)) as MV002,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 " +
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) " +
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 " +
                         "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' and   rtrim(ltrim(SFCTA.UDF09))='" + textBox2.Text + "' ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["ts"].ColumnName = "条码数据";
            ds.Tables[0].Columns["TA057"].ColumnName = "产品序列号";
            ds.Tables[0].Columns["MW002"].ColumnName = "工序名称";
            ds.Tables[0].Columns["UDF07"].ColumnName = "焊缝编号";
            ds.Tables[0].Columns["MV002"].ColumnName = "姓名";
            ds.Tables[0].Columns["TA022"].ColumnName = "工时";
            ds.Tables[0].Columns["TA059"].ColumnName = "工资";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

        }


            private void 快捷报工button4_Click(object sender, EventArgs e)
        {



            SqlConnection con1 = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from codelist where code = '" + label3.Text + "' and pdn=  '" + label4.Text + "' "), con1);
            if ((int)cmd1.ExecuteScalar() > 0)
            {
                MessageBox.Show("已报工，请勿重复报工", "提示");
                con1.Close();
            }


            else if (label4.Text.Length == 0 )
            {
                MessageBox.Show("请选择正确信息！");
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
                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [codelist] (name,code,datetime,pdn,time,equipment) values ('" + label7.Text + "','" + label3.Text + "','" + label5.Text + "','" + label4.Text + "','" + label8.Text + "','PAD')";

                SqlCommand cmd = new SqlCommand(myadd, con);
                
                
                cmd.ExecuteNonQuery();

                MessageBox.Show("报工完成！");

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
            textBox3.Text = "";
            textBox1.Text = "";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string myupdate = "delete from [workers] where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

            SqlCommand cmd = new SqlCommand(myupdate, con);

            cmd.ExecuteNonQuery();

            //Showdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || comboBox1.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("请输入正确员工信息！");
            }
            else
            {
                string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update [workers] set Wname='" + textBox1.Text + "',Wpro='" + comboBox1.Text + "',Wtel='" + textBox3.Text + "'where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
            }

            //Showdata();
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox3.Text = DateTime.Now.ToString("yyyyMMdd");
            label3.Text = dataGridView1.SelectedCells[0].Value.ToString();
            label4.Text = dataGridView1.SelectedCells[1].Value.ToString();
            label5.Text = DateTime.Now.ToString("yyyyMMdd");
            label7.Text = textBox2.Text;
            label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
        private void 快捷报工_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
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
