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
    public partial class bom管理_form2 : Form
    {
        public bom管理_form2()
        {
            InitializeComponent();
        }
 
        //传值用
        private string flag;
        /// <summary>
        /// 接收传过来的值
        /// </summary>
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        //第二件事，load中显示flag到form2的textBox1.Text
        private void bom管理_form2_Load(object sender, EventArgs e)
        {
            label2.Text = this.flag;
        }


        //跳入查看简化结果
        private void button1_Click(object sender, EventArgs e)
        {
            //登陆成功，进入另一个窗体
            //this.Hide();//隐藏当前窗口 
            bom管理_查看 bomform3 = new bom管理_查看();
            bomform3.Show();
            //this.Close();
        }



        //创建视图simplifybom
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter sda;
            DataSet myDataSet;

            conn = new SqlConnection("server=192.168.1.252;uid=sa;pwd=WFGLServer2008;database=cs管理;Connection TimeOut=2");
            conn.Open();
            //调用存储过程
            comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "creatsimplifybomview";//存储过程的名字
            comm.CommandType = CommandType.StoredProcedure;
            IDataParameter[] parameters =
                {
                    new SqlParameter("@end1idimp", SqlDbType.VarChar,255) , //存储过程中参数的名字
                    //new SqlParameter("@intSeason", SqlDbType.VarChar,1)   //存储过程中参数的名字
                };
            parameters[0].Value = label2.Text ;
            //parameters[1].Value = "2";
            comm.Parameters.Add(parameters[0]);
            //comm.Parameters.Add(parameters[1]);
            sda = new SqlDataAdapter();
            sda.SelectCommand = comm;
            myDataSet = new DataSet();
            sda.Fill(myDataSet);
            conn.Close();
            //dataGridView1.DataSource = myDataSet.Tables[0];

            MessageBox.Show("创建成功！");
        }

        //标记提交项.PLMBOM表
        private void button6_Click(object sender, EventArgs e)
        {

            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter sda;
            DataSet myDataSet;

            conn = new SqlConnection("server=192.168.1.252;uid=sa;pwd=WFGLServer2008;database=cs管理;Connection TimeOut=2");
            conn.Open();
            //调用存储过程
            comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "signPLMBOM";//存储过程的名字
            comm.CommandType = CommandType.StoredProcedure;
            //IDataParameter[] parameters =
            //    {
            //        new SqlParameter("@end1idimp", SqlDbType.VarChar,255) , //存储过程中参数的名字
                    //new SqlParameter("@intSeason", SqlDbType.VarChar,1)   //存储过程中参数的名字
            //    };
            //parameters[0].Value = label2.Text;
            //parameters[1].Value = "2";
            //comm.Parameters.Add(parameters[0]);
            //comm.Parameters.Add(parameters[1]);
            sda = new SqlDataAdapter();
            sda.SelectCommand = comm;
            myDataSet = new DataSet();
            sda.Fill(myDataSet);
            conn.Close();
            //dataGridView1.DataSource = myDataSet.Tables[0];

            MessageBox.Show("标记成功！");

        }


        //创建临时表
        //从simplifybom视图insert到新创建的表sbomtemp，调用存储过程
        private void button3_Click(object sender, EventArgs e)
        {
            
                SqlConnection conn;
                SqlCommand comm;
                SqlDataAdapter sda;
                DataSet myDataSet;

                conn = new SqlConnection("server=192.168.1.252;uid=sa;pwd=WFGLServer2008;database=cs管理;Connection TimeOut=2");
                conn.Open();
                //调用存储过程
                comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "creatsimplifybomtable";//存储过程的名字
                comm.CommandType = CommandType.StoredProcedure;
                //IDataParameter[] parameters =
                //    {
                //        new SqlParameter("@end1idimp", SqlDbType.VarChar,255) , //存储过程中参数的名字
                //new SqlParameter("@intSeason", SqlDbType.VarChar,1)   //存储过程中参数的名字
                //    };
                //parameters[0].Value = label2.Text;
                //parameters[1].Value = "2";
                //comm.Parameters.Add(parameters[0]);
                //comm.Parameters.Add(parameters[1]);
                sda = new SqlDataAdapter();
                sda.SelectCommand = comm;
                myDataSet = new DataSet();
                sda.Fill(myDataSet);
                conn.Close();
                //dataGridView1.DataSource = myDataSet.Tables[0];

                MessageBox.Show("已创建临时表！");


        }

        //标记虚拟件sbomtemp表
        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter sda;
            DataSet myDataSet;

            conn = new SqlConnection("server=192.168.1.252;uid=sa;pwd=WFGLServer2008;database=cs管理;Connection TimeOut=2");
            conn.Open();
            //调用存储过程
            comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "signsbomtemptable";//存储过程的名字
            comm.CommandType = CommandType.StoredProcedure;
            //IDataParameter[] parameters =
            //    {
            //        new SqlParameter("@end1idimp", SqlDbType.VarChar,255) , //存储过程中参数的名字
            //new SqlParameter("@intSeason", SqlDbType.VarChar,1)   //存储过程中参数的名字
            //    };
            //parameters[0].Value = label2.Text;
            //parameters[1].Value = "2";
            //comm.Parameters.Add(parameters[0]);
            //comm.Parameters.Add(parameters[1]);
            sda = new SqlDataAdapter();
            sda.SelectCommand = comm;
            myDataSet = new DataSet();
            sda.Fill(myDataSet);
            conn.Close();
            //dataGridView1.DataSource = myDataSet.Tables[0];

            MessageBox.Show("标记完成！");

        }





    }
}
