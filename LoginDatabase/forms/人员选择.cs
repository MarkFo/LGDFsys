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
    public partial class 人员选择 : Form
    {
        public 人员选择()
        {
            InitializeComponent();

        }

        private void 人员选择_Load(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [用户名],[name] from 登录表 ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["用户名"].ColumnName = "工号";
            ds.Tables[0].Columns["name"].ColumnName = "姓名";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            
        }

        //公共变量，为给杂工记录添加引用
  


        private void 人员选择_FormClosing(object sender, FormClosingEventArgs e)
        {
            杂工记录 bs = new 杂工记录();
            this.Hide();//隐藏当前窗口 
            bs.ShowDialog(); //弹出第二个窗口 

            this.Close();//关闭第一个窗口
        }
    }
}
