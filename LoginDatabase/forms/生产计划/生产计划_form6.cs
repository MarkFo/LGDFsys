using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LGD.forms.生产计划
{
    public partial class 生产计划_form6 : Form
    {
        public 生产计划_form6()
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

        //发送邮件
        private void button1_Click(object sender, EventArgs e)
        {
            if(label6.Text.Length <= 2)
            {
                MessageBox.Show("无邮件地址！");
            }
            else
            { 
            MailMessage msg = new MailMessage();

            msg.To.Add(label6.Text);
            msg.CC.Add("1241448937@qq.com,405782283@qq.com");

            msg.From = new MailAddress("hongfeiyg@163.com", "快捷平台邮件系统");//发件人名称

            msg.Subject = "生产计划监控系统 | " + label2.Text+"   发送者: "+ label15.Text + "   接收者: " + label9.Text;//主题
            //标题格式为UTF8  
            msg.SubjectEncoding = Encoding.UTF8;

            msg.Body = textBox2.Text;//内容
            //内容格式为UTF8 
            msg.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient();
            //SMTP服务器地址 
            client.Host = "smtp.163.com";
            //SMTP端口，QQ邮箱填写587  ,163邮箱:25
            client.Port = 25;
            //启用SSL加密  
            client.EnableSsl = true;

            //client.Credentials = new NetworkCredential("15052923515@qq.com", "duyxwceajoyvbiee");
            client.Credentials = new NetworkCredential("hongfeiyg@163.com", "tianshi240304");

            //发送邮件  
            client.Send(msg);
            msg.Dispose();


            MessageBox.Show("发送成功！");
            }

        }

        //load事件
        private void 生产计划_form6_Load(object sender, EventArgs e)
        {
            label2.Text = this.varpdn;//窗体加载时接收varpdn的值

            //全局变量，加载姓名
            label16.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写
            SqlConnection connUserName = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sqlUserName = "select * from 登录表 where ltrim(rtrim(用户名)) = rtrim(ltrim('" + label16.Text + "')) ";
            connUserName.Open();
            SqlCommand cmdUserName = new SqlCommand(sqlUserName, connUserName);
            SqlDataReader rdrUserName = cmdUserName.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdrUserName.Read())
            {
                //显示姓名和角色
                label15.Text = rdrUserName[2].ToString();
                connUserName.Close();

            }
            rdrUserName.Close();



            //开始加载根据编号查询型号、客户
            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select production_plans.model as ppmodel,production_plans.Customername as ppCustomername from production_plans where[pdn] = '" + label2.Text + "' ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {
                label11.Text = rdr[0].ToString();
                label12.Text = rdr[1].ToString();
                conn.Close();
            }
            rdr.Close();

            textBox2.Text = "产品编号:" + label2.Text + " | 型号:" + label11.Text + " | 客户:" + label12.Text  ;
            //textBox2.Text += "内容：";//拼接

            textBox2.AppendText("\r\n");


        }


        //搜索人员
        private void button2_Click(object sender, EventArgs e)
        {
            //去除行列标题
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            // 禁止用户改变DataGridView1的所有列的列宽
            dataGridView1.AllowUserToResizeColumns = false;
            //禁止用户改变DataGridView1所有行的行高
            dataGridView1.AllowUserToResizeRows = false;

            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [用户名],[name],[mail] from 登录表 " +
                         "WHERE ( rtrim(ltrim([用户名])) like '%" + textBox1.Text + "%' or rtrim(ltrim([name])) like '%" + textBox1.Text + "%' ) " +
                         "and rtrim(ltrim([用户名])) NOT LIKE 'v%' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["用户名"].ColumnName = "工号";
            ds.Tables[0].Columns["name"].ColumnName = "姓名";
            ds.Tables[0].Columns["mail"].ColumnName = "邮件";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            dataGridView1.Columns["邮件"].Visible = false;//隐藏邮件列
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label7.Text = dataGridView1.SelectedCells[0].Value.ToString();
            label9.Text = dataGridView1.SelectedCells[1].Value.ToString();
            label6.Text = dataGridView1.SelectedCells[2].Value.ToString();
        }


        //发送邮件







    }
}
