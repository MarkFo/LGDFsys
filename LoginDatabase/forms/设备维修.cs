using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace LGD
{
    public partial class 设备维修 : Form
    {
        public 设备维修()
        {
            InitializeComponent();
            label3.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写

            //开始就将dateTimePicker1加入textBox1，防止录入人员不选
            textBox1.Text = dateTimePicker1.Value.ToString();
            textBox6.Text = dateTimePicker2.Value.ToString();
        }

        //一.添加按钮
        private void button4_Click(object sender, EventArgs e)
        {


            /*
            SqlConnection con1 = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from additional where [guid] = '" + label4.Text + "' "), con1);
            if ((int)cmd1.ExecuteScalar() > 0)
            {
                MessageBox.Show("已存在，请勿重复记录", "提示");
                con1.Close();
            }


            else */
            if (label15.Text.Length == 0)
            {
                MessageBox.Show("请补充必要信息！");
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

                //新增前刷新唯一标识符、实际添加时间
                label4.Text = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09唯一标识符
                label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [EQmaintenance] ([owner],[dispdate],[fac],[eqname],[userid],[username],[manhours],[reason],[duty],[maintenancedate],[guid],[remarks]) values ('" + label3.Text + "','" + textBox1.Text + "','" + comboBox2.Text + "','" + comboBox1.Text + "','" + label15.Text + "','" + label10.Text + "','" + textBox4.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + textBox6.Text + "','" + label4.Text + "','" + textBox2.Text + "')";

                SqlCommand cmd = new SqlCommand(myadd, con);


                cmd.ExecuteNonQuery();

                MessageBox.Show("新增完成！");

                //重新执行dgrid1查询
                button7_Click_1(sender, e);


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
            label15.Text = "";
            label10.Text = "";

        }


        //二.删除按钮
        private void button3_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();
            string myupdate = "delete from [EQmaintenance] where guid='" + label4.Text + "' ";

            SqlCommand cmd = new SqlCommand(myupdate, con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("删除完成！");
            //Showdata();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || comboBox1.Text.Length == 0 || label15.Text.Length == 0)
            {
                MessageBox.Show("请输入正确员工信息！");
            }
            else
            {
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update [additional] set Wname='" + textBox1.Text + "',Wpro='" + comboBox1.Text + "',Wtel='" + label15.Text + "'where id=" + Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
            }

            //Showdata();
            textBox1.Text = "";
            comboBox1.Text = "";
            label15.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            //comboBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //label5.Text = DateTime.Now.ToString("yyyyMMdd");
            //label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //label3.Text = dataGridView1.SelectedCells[0].Value.ToString();
            label4.Text = dataGridView1.SelectedCells[0].Value.ToString();



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
        private void 杂工记录_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [guid],[owner],[dispdate],[fac],[eqname],[userid],[username],[manhours],[reason],[duty],[maintenancedate],[remarks] from EQmaintenance " +
                         "WHERE rtrim(ltrim([owner]))=rtrim(ltrim('" + label3.Text + "')) ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["guid"].ColumnName = "识别码";
            ds.Tables[0].Columns["owner"].ColumnName = "维修者";
            ds.Tables[0].Columns["dispdate"].ColumnName = "维修日期";
            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["eqname"].ColumnName = "设备名称";
            ds.Tables[0].Columns["userid"].ColumnName = "设备使用者";
            ds.Tables[0].Columns["username"].ColumnName = "使用者姓名";
            ds.Tables[0].Columns["manhours"].ColumnName = "维修工时";
            ds.Tables[0].Columns["reason"].ColumnName = "故障原因";
            ds.Tables[0].Columns["duty"].ColumnName = "责任区分";
            ds.Tables[0].Columns["maintenancedate"].ColumnName = "保养日期";
            ds.Tables[0].Columns["remarks"].ColumnName = "备注";


            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

        }



        private void 设备维修_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button9.Visible = false;
            timer1.Interval = 1000;
            timer1.Start();
            //首先显示所属数据
            button7_Click_1(sender, e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label15.Text = dataGridView2.SelectedCells[0].Value.ToString();
            label10.Text = dataGridView2.SelectedCells[1].Value.ToString();
            //textBox4.Text = dataGridView2.SelectedCells[0].Value.ToString();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            button9.Visible = true;

            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [用户名],[name] from 登录表 " +
                         "WHERE rtrim(ltrim([用户名])) like '%" + textBox3.Text + "%' or rtrim(ltrim([name])) like '%" + textBox3.Text + "%' ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["用户名"].ColumnName = "工号";
            ds.Tables[0].Columns["name"].ColumnName = "姓名";

            this.dataGridView2.DataSource = ds.Tables[0].DefaultView;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button9.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox5.Text = comboBox1.Text;
        }


        //2个时间选择器，变化传递
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1.Text = textBox1.Text;
            textBox1.Text = dateTimePicker1.Value.ToString();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker1.Text = textBox1.Text;
            textBox6.Text = dateTimePicker2.Value.ToString();
        }



        //导出excel
        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，您的电脑可能未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook =
                        workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet =
                        (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 
                                                                                         //写入标题             
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            { worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText; }
            //写入数值
            for (int r = 0; r < dataGridView1.Rows.Count; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            MessageBox.Show(fileName + "资料保存成功", "提示", MessageBoxButtons.OK);
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);  //fileSaved = true;                 
                }
                catch (Exception ex)
                {//fileSaved = false;                      
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁       


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
