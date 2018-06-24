using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Threading;

namespace LGD
{
    public partial class 修改密码 : Form
    {
        public 修改密码()
        {
            InitializeComponent();
            //this.ControlBox = false;//取消右上角最大最小化、关闭按钮的显示
            label5.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0 || textBox2.Text.Trim().Length == 0 || textBox3.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入完整！");
                return;
            }

            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致！");
                return;
            }

            String ConnString = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection conn = new SqlConnection(ConnString);
            DataTable dt = new DataTable();

            String sql = "SELECT * FROM 登录表 where 密码 = '" + textBox1.Text.Trim() + "'";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("旧密码输入错误！");

                dt.Dispose();
            }
            else
            {
                sql = "UPDATE 登录表 set 密码 ='" + textBox2.Text.Trim() + "' where 用户名= '" + label5.Text.Trim() + "'  ";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception Err)
                {
                    MessageBox.Show("更新出现异常！");
                }
                finally
                {
                    conn.Close();
                }

                MessageBox.Show("密码更新成功！");
                
            }
        }



        //窗口关闭，回到跳转窗体
        private void 修改密码_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }
    }
}
