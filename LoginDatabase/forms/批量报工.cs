using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGD
{
    public partial class 批量报工 : Form
    {
        public 批量报工()
        {
            InitializeComponent();
        }

        private void 批量报工_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [用户名],[密码],[name] from 登录表 " +
                         "WHERE rtrim(ltrim([用户名]))=rtrim(ltrim('" + textBox1.Text + "')) ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["用户名"].ColumnName = "用户名";
            ds.Tables[0].Columns["密码"].ColumnName = "密码";
            ds.Tables[0].Columns["name"].ColumnName = "name";


            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }



    }
}
