using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace LGD.forms
{
    public partial class 生产计划_form4 : Form
    {
        public 生产计划_form4()
        {
            InitializeComponent();
        }
        //传值用
        private string varpdn;
        /// <summary>
        /// 接收传过来的值
        /// </summary>
        public string Varpdn//定义变量
        {
            get { return varpdn; }
            set { varpdn = value; }
        }

        private void 生产计划_form4_Load(object sender, EventArgs e)
        {
            label2.Text = this.varpdn;//窗体加载时接收varpdn的值

            //计时器开始运行
            timer1.Interval = 1000;
            timer1.Start();

            //开始加载最新上次内容

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select production_plans_reason .pdn,actualstartdate,actualdate1,actualdate2,completionstate,plandate5,[datetime] "+
                         "from production_plans_reason " +
                         "left join (select pdn,max([datetime]) as maxdatetime  from production_plans_reason group by pdn) as ppr " +
                         "on production_plans_reason .pdn = ppr.pdn and production_plans_reason.[datetime]=ppr.maxdatetime " +
                         "where(rtrim(ltrim(production_plans_reason.pdn)) = rtrim(ltrim('" + label2.Text + "')) ) and ppr.pdn is not null ";

            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {

                textBox2.Text = rdr[1].ToString();
                textBox3.Text = rdr[2].ToString();
                textBox4.Text = rdr[3].ToString();
                textBox5.Text = rdr[4].ToString();
                textBox1.Text = rdr[5].ToString();

                label11.Text = rdr[6].ToString();
                conn.Close();

            }
            rdr.Close();



        }


        //提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0 && textBox3.Text.Length == 0 && textBox4.Text.Length == 0 && textBox5.Text.Length == 0)
            {
                MessageBox.Show("请补充必要信息！");
            }
            else
            {
                //label3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [production_plans_reason] ([pdn],[actualstartdate],[actualdate1],[actualdate2],[completionstate],[plandate5], " +
                    "[datetime]) values ('" + label2.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox1.Text + "','" + label3.Text + "')";

                SqlCommand cmd = new SqlCommand(myadd, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("完成提交！");

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox1.Text = "";


            }


        }

        //计时器跳动
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");//24小时
        }



    }
}
