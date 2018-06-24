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
    public partial class 工资查询 : Form
    {
        public 工资查询()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //按下后，避免重复按，先让其变灰
            button1.Visible = false;
            MessageBox.Show("正在搜索，请稍等！");

            string coct = "Data Source=192.168.1.253;Initial Catalog=HONGYI;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT rtrim(ltrim(SFCTA.TA001))+rtrim(ltrim(SFCTA.TA002))+rtrim(ltrim(SFCTA.TA003)) as ts,MOCTA.TA057 as TA057,CMSMW.MW002 as MW002,SFCTA.UDF07 as UDF07,CMSMV.MV002 as MV002,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 " +
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) " +
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 " +
                         "WHERE SFCTA.UDF03 = 'Y' AND (SFCTA.TA031>='" + textBox8.Text + "' and SFCTA.TA031<='" + textBox9.Text + "')  and rtrim(ltrim(SFCTA.UDF09))='" + textBox1.Text + "' ";

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

            label6.Text = "目前显示：高新园";

            //****************总计，2个按钮

            button3_Click(sender, e);
            button4_Click(sender, e);


            double num1 = double.Parse(textBox2.Text.Trim());
            double num2 = double.Parse(textBox4.Text.Trim());
            double num3 = double.Parse(textBox3.Text.Trim());
            double num4 = double.Parse(textBox5.Text.Trim());
            textBox6.Text = (num1 + num2).ToString();
            textBox7.Text = (num3 + num4).ToString();
            //****************

            //查询完毕，恢复按钮
            button1.Visible = true;
        }

        private void 工资查询_Load(object sender, EventArgs e)
        {
            textBox1.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写
            //dateTimePicker1设置为当前月的第一天
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //开始就刷新textBox8、9的时间，防止录入人员不选
            textBox8.Text = dateTimePicker1.Value.ToString("yyyyMMdd");
            textBox9.Text = dateTimePicker2.Value.ToString("yyyyMMdd");


            textBox2.Text = "0";
            textBox3.Text = "0";
            label7.Text = "--";
            textBox4.Text = "0";
            textBox5.Text = "0";

        }

        private void 工资查询_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //按下后，避免重复按，先让其变灰
            button2.Visible = false;
            MessageBox.Show("正在搜索，请稍等！");



            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT rtrim(ltrim(SFCTA.TA001))+rtrim(ltrim(SFCTA.TA002))+rtrim(ltrim(SFCTA.TA003)) as ts,MOCTA.TA057 as TA057,CMSMW.MW002 as MW002,SFCTA.UDF07 as UDF07,CMSMV.MV002 as MV002,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 " +
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) " +
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 " +
                         "WHERE SFCTA.UDF03 = 'Y' AND (SFCTA.TA031>='" + textBox8.Text + "' and SFCTA.TA031<='" + textBox9.Text + "')  and rtrim(ltrim(SFCTA.UDF09))='" + textBox1.Text + "' ";

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

            label6.Text = "目前显示：威孚";

            //****************总计，2个按钮

            button3_Click(sender, e);
            button4_Click(sender, e);

            double num1 = double.Parse(textBox2.Text.Trim());
            double num2 = double.Parse(textBox4.Text.Trim());
            double num3 = double.Parse(textBox3.Text.Trim());
            double num4 = double.Parse(textBox5.Text.Trim());
            textBox6.Text = (num1 + num2).ToString();
            textBox7.Text = (num3 + num4).ToString();
            /*
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("无数据！");
            }
            else
            {
                double num1 = double.Parse(textBox2.Text.Trim());
                double num2 = double.Parse(textBox4.Text.Trim());
                double num3 = double.Parse(textBox3.Text.Trim());
                double num4 = double.Parse(textBox5.Text.Trim());

                textBox6.Text = (num1 + num2).ToString();
                textBox7.Text = (num3 + num4).ToString();
             }
             */



            //****************
            //查询完毕，恢复按钮
            button2.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)//威孚总计
        {
            //清空当前textbox，默认0是为了合计的时候按数值相加，避免以string相加报错
            textBox2.Text = "0";
            textBox3.Text = "0";
            label7.Text = "--";

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "SELECT CMSMV.MV002,sum(SFCTA.TA022),Convert(decimal(18,2),sum(SFCTA.TA059) ) FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 " +
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) " +
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 " +
                         "WHERE SFCTA.UDF03 = 'Y' AND (SFCTA.TA031>='" + textBox8.Text + "' and SFCTA.TA031<='" + textBox9.Text + "')  and rtrim(ltrim(SFCTA.UDF09))='" + textBox1.Text + "' group by CMSMV.MV002 ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //SqlDataAdapter rdr = new SqlDataAdapter(cmd);
            //rdr.Read();
            //if (rdr["bookBarCode"] != DBNull.Value) //可以直接指定列名，以后参考


            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {
                label7.Text = rdr[0].ToString();
                textBox3.Text = rdr[1].ToString();
                textBox2.Text = rdr[2].ToString();
                conn.Close();
                
            }
            rdr.Close();

        }
        //开始时间，同步到textbox8中
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1.Value.ToString("yyyy-MM-dd")
            textBox8.Text = dateTimePicker1.Value.ToString("yyyyMMdd");
        }
        //截止时间，同步到textbox9中
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox9.Text = dateTimePicker2.Value.ToString("yyyyMMdd");
        }

        private void button4_Click(object sender, EventArgs e)//高新园总计
        {

            //清空当前textbox，默认0是为了合计的时候按数值相加，避免以string相加报错
            textBox4.Text = "0";
            textBox5.Text = "0";

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.253;Initial Catalog=HONGYI;Persist Security Info=True;User ID=sa;Password=Server08");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "SELECT CMSMV.MV002,sum(SFCTA.TA022),Convert(decimal(18,2),sum(SFCTA.TA059) ) FROM SFCTA " +
                         "LEFT JOIN CMSMW ON SFCTA.TA004 = CMSMW.MW001 " +
                         "LEFT JOIN MOCTA ON(SFCTA.TA001+SFCTA.TA002)= (MOCTA.TA001 + MOCTA.TA002) " +
                         "LEFT JOIN CMSMV ON SFCTA.UDF09 = CMSMV.MV001 " +
                         "WHERE SFCTA.UDF03 = 'Y' AND (SFCTA.TA031>='" + textBox8.Text + "' and SFCTA.TA031<='" + textBox9.Text + "')  and rtrim(ltrim(SFCTA.UDF09))='" + textBox1.Text + "' group by CMSMV.MV002 ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {

                textBox5.Text = rdr[1].ToString();
                textBox4.Text = rdr[2].ToString();
                conn.Close();

            }
            rdr.Close();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
