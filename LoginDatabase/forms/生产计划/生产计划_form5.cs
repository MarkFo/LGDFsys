using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace LGD.forms.生产计划
{
    public partial class 生产计划_form5 : Form
    {
        public 生产计划_form5()
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

        //传值用
        private string varpdns;
        /// <summary>
        /// 接收传过来的值
        /// </summary>
        public string Varpdns//定义变量2
        {
            get { return varpdns; }
            set { varpdns = value; }
        }

        private void 生产计划_form5_Load(object sender, EventArgs e)
        {
            label2.Text = this.varpdn;//窗体加载时接收varpdn的值
            label5.Text = this.varpdns;//窗体加载时接收varpdn的值

            //开始加载逾期原因

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select production_plans_reason .pdn,actualstartdate,actualdate1,actualdate2,completionstate,plandate5,[datetime] " +
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
                if(label5.Text=="10")
                {
                    label3.Text = rdr[1].ToString();
                    label7.Text = rdr[6].ToString();//最近添加时间
                }
                else if (label5.Text == "12")
                {
                    label3.Text = rdr[2].ToString();
                    label7.Text = rdr[6].ToString();//最近添加时间
                }
                else if (label5.Text == "14")
                {
                    label3.Text = rdr[3].ToString();
                    label7.Text = rdr[6].ToString();//最近添加时间
                }
                else if (label5.Text == "18")
                {
                    label3.Text = rdr[4].ToString();
                    label7.Text = rdr[6].ToString();//最近添加时间
                }
                else
                {
                    label3.Text = "暂无数据";
                }
                conn.Close();

            }
            rdr.Close();



        }





    }



}
