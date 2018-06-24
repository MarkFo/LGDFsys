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
    public partial class 生产计划_form3 : Form
    {
        public 生产计划_form3()
        {
            InitializeComponent();
        }

        private void bom管理_form3_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select pdn+'|' + remarks as item,remarktime " +
                         "from production_plans_remark " +
                         "where CONVERT(varchar(100), cast(remarktime as datetime), 112) = CONVERT(varchar(100), GETDATE(), 112) " +
                         "order by remarktime desc ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {

                textBox1.Text = rdr[0].ToString();
                label1.Text = rdr[1].ToString();
                conn.Close();

            }
            rdr.Close();
        }





    }
}
