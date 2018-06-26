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
using System.Threading;

namespace LGD
{
    public partial class 生产计划 : Form
    {
        public 生产计划()
        {
            InitializeComponent();
        }
        /*private void dataGridView1_Paint(object sender, PaintEventArgs e)//添加颜色选框
        {
            //e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(0, 0, this.dataGridView1.Width - 1, this.dataGridView1.Height - 1));
            e.Graphics.DrawRectangle(Pens.Blue, new Rectangle(0, 0, dataGridView1.CurrentRow.Index - 1, this.dataGridView1.Height - 1));
        }*/
        private void 生产计划_Load(object sender, EventArgs e)
        {
            //计时器开始运行，标签闪烁用
            timer1.Interval = 1000;
            timer1.Start();

            //1.DataGridVeiw Style

            #region DataGridVeiw Style
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1.AllowUserToAddRows = true;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;//211, 223, 240
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.RowHeadersVisible = false;//row前面的小三角
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.ReadOnly = true;

            this.dataGridView1.AllowUserToResizeRows = false;
            //根据Header和所有单元格的内容自动调整列宽
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            #endregion

            //2.其他杂项

            button2.PerformClick();//调用刷新按钮button2
            groupBox1.Visible = false;//加载时groupbox为隐藏
            button10.Visible = false;//加载时收起按钮为隐藏
            comboBox3.Text = "高新园";
            comboBox2.Text = "不锁定";
            comboBox1.Text = "高新园";


            //dateTimePicker1设置为当前月的第一天
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //开始就刷新textBox14、15的时间，防止录入人员不选
            textBox14.Text = dateTimePicker1.Value.ToString("yyyyMMdd");
            textBox15.Text = dateTimePicker2.Value.ToString("yyyyMMdd");

            //3.显示账号姓名角色、分配权限


            //this.WindowState = FormWindowState.Maximized;//铺满全屏
            label26.Text = UserInfo.UserName;//引用全局变量username，默认加载时填写

            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select * from 登录表 where ltrim(rtrim(用户名)) = rtrim(ltrim('" + label26.Text + "')) ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {
                //显示姓名和角色
                label27.Text = rdr[2].ToString();
                label28.Text = rdr[3].ToString();
                conn.Close();

            }
            rdr.Close();


            button12_Click(null, null);//调用赋予权限按钮button12




        }

        private void 生产计划_FormClosing(object sender, FormClosingEventArgs e)
        {
        /*    this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口*/
        }

        //按产品编号查询
        private void button1_Click(object sender, EventArgs e)
        {

            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [lock],[fac],[orderstatus],[model],[drawingnumber],[Customername],[pdn],[deliverydate],[takedelivery],[Planstarttime],[actualstartdate],[plandate1], " +
                         "[actualdate1],[plandate2],[actualdate2],[plandate3],[plandate4],[plandate5],[completionstate],[remarks], " +

                         " ( case when [Planstarttime] is null or [Planstarttime] like'' then '3'  " +
                         " when([Planstarttime] is not null and[Planstarttime] not like'' and len(ltrim(rtrim(Planstarttime)) ) < 8) then '3' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " +      //正常
                         " when datediff(day, Planstarttime, actualstartdate) > 0 then '1' else '2' end) as starttimejudgment " +

                         ",( case when [plandate1] is null or [plandate1] like'' then '3'  " +
                         "when([plandate1] is not null and[plandate1] not like'' and len(ltrim(rtrim(plandate1)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate1, actualdate1) > 0 then '1' else '2' end) as date1judgment " +

                         ",( case when [plandate2] is null or [plandate2] like'' then '3'  " +
                         "when([plandate2] is not null and[plandate2] not like'' and len(ltrim(rtrim(plandate2)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate2, actualdate2) > 0 then '1' else '2' end) as date2judgment " +

                         ",( case when [plandate3] is null or [plandate3] like'' then '3'  " +
                         "when([plandate3] is not null and[plandate3] not like'' and len(ltrim(rtrim(plandate3)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate3, completionstate) > 0 then '1' else '2' end) as date3judgment " +



                         "FROM [cs管理].[dbo].[production_plans]  " +
                         "WHERE rtrim(ltrim([pdn]))=rtrim(ltrim('" + textBox1.Text + "')) and (completionstate is null or completionstate like '')  " +
                         "order by lock desc,orderstatus desc ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["lock"].ColumnName = "锁定状态";

            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["orderstatus"].ColumnName = "订单/备货";
            ds.Tables[0].Columns["model"].ColumnName = "锅炉型号";
            ds.Tables[0].Columns["drawingnumber"].ColumnName = "图号";
            ds.Tables[0].Columns["Customername"].ColumnName = "客户名称";
            ds.Tables[0].Columns["pdn"].ColumnName = "锅炉编号";
            ds.Tables[0].Columns["deliverydate"].ColumnName = "合同交期";
            ds.Tables[0].Columns["takedelivery"].ColumnName = "客户提货";

            ds.Tables[0].Columns["Planstarttime"].ColumnName = "计划开工";
            ds.Tables[0].Columns["actualstartdate"].ColumnName = "实际开工";
            ds.Tables[0].Columns["plandate1"].ColumnName = "计划穿管";
            ds.Tables[0].Columns["actualdate1"].ColumnName = "实际穿管";
            ds.Tables[0].Columns["plandate2"].ColumnName = "计划水压";
            ds.Tables[0].Columns["actualdate2"].ColumnName = "实际水压";
            ds.Tables[0].Columns["plandate3"].ColumnName = "计划完工";
            ds.Tables[0].Columns["plandate4"].ColumnName = "预留1";
            ds.Tables[0].Columns["plandate5"].ColumnName = "预留2";

            ds.Tables[0].Columns["completionstate"].ColumnName = "实际完工";

            ds.Tables[0].Columns["remarks"].ColumnName = "备注(人、设备、物料等）";

            ds.Tables[0].Columns["starttimejudgment"].ColumnName = "starttimejudgment";
            ds.Tables[0].Columns["date1judgment"].ColumnName = "date1judgment";
            ds.Tables[0].Columns["date2judgment"].ColumnName = "date2judgment";
            ds.Tables[0].Columns["date3judgment"].ColumnName = "date3judgment";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;



            dataGridView1.Columns["starttimejudgment"].Visible = false;//隐藏判断列1
            dataGridView1.Columns["date1judgment"].Visible = false;//隐藏判断列2
            dataGridView1.Columns["date2judgment"].Visible = false;//隐藏判断列3
            dataGridView1.Columns["date3judgment"].Visible = false;//隐藏判断列4

            dataGridView1.Columns["预留1"].Visible = false;//隐藏
            dataGridView1.Columns["预留2"].Visible = false;//隐藏
            dataGridView1.Columns["备注(人、设备、物料等）"].Visible = false;//隐藏

            /*
            //改变颜色
            if (dataGridView1.SelectedCells[17].Value.ToString() == "lag")
            {
                //dataGridView1.Rows[2].Cells[7].Style.BackColor = Color.Red;//改变指定格子的颜色

            }*/
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }


        }

        //刷新全部
        private void button2_Click(object sender, EventArgs e)
        {

            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [lock],[fac],[orderstatus],[model],[drawingnumber],[Customername],[pdn],[deliverydate],[takedelivery],[Planstarttime],[actualstartdate],[plandate1], " +
                         "[actualdate1],[plandate2],[actualdate2],[plandate3],[plandate4],[plandate5],[completionstate],[remarks], " +

                         " ( case when [Planstarttime] is null or [Planstarttime] like'' then '3'  " +
                         " when([Planstarttime] is not null and[Planstarttime] not like'' and len(ltrim(rtrim(Planstarttime)) ) < 8) then '3' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " +      //正常
                         " when datediff(day, Planstarttime, actualstartdate) > 0 then '1' else '2' end) as starttimejudgment " +

                         ",( case when [plandate1] is null or [plandate1] like'' then '3'  " +
                         "when([plandate1] is not null and[plandate1] not like'' and len(ltrim(rtrim(plandate1)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate1, actualdate1) > 0 then '1' else '2' end) as date1judgment " +

                         ",( case when [plandate2] is null or [plandate2] like'' then '3'  " +
                         "when([plandate2] is not null and[plandate2] not like'' and len(ltrim(rtrim(plandate2)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate2, actualdate2) > 0 then '1' else '2' end) as date2judgment " +

                         ",( case when [plandate3] is null or [plandate3] like'' then '3'  " +
                         "when([plandate3] is not null and[plandate3] not like'' and len(ltrim(rtrim(plandate3)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate3, completionstate) > 0 then '1' else '2' end) as date3judgment " +



                         "FROM [cs管理].[dbo].[production_plans]  " +
                         "where (completionstate is null or completionstate like '')  " +
                         "order by lock desc,orderstatus desc  ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["lock"].ColumnName = "锁定状态";

            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["orderstatus"].ColumnName = "订单/备货";
            ds.Tables[0].Columns["model"].ColumnName = "锅炉型号";
            ds.Tables[0].Columns["drawingnumber"].ColumnName = "图号";
            ds.Tables[0].Columns["Customername"].ColumnName = "客户名称";
            ds.Tables[0].Columns["pdn"].ColumnName = "锅炉编号";
            ds.Tables[0].Columns["deliverydate"].ColumnName = "合同交期";
            ds.Tables[0].Columns["takedelivery"].ColumnName = "客户提货";

            ds.Tables[0].Columns["Planstarttime"].ColumnName = "计划开工";
            ds.Tables[0].Columns["actualstartdate"].ColumnName = "实际开工";
            ds.Tables[0].Columns["plandate1"].ColumnName = "计划穿管";
            ds.Tables[0].Columns["actualdate1"].ColumnName = "实际穿管";
            ds.Tables[0].Columns["plandate2"].ColumnName = "计划水压";
            ds.Tables[0].Columns["actualdate2"].ColumnName = "实际水压";
            ds.Tables[0].Columns["plandate3"].ColumnName = "计划完工";
            ds.Tables[0].Columns["plandate4"].ColumnName = "预留1";
            ds.Tables[0].Columns["plandate5"].ColumnName = "预留2";

            ds.Tables[0].Columns["completionstate"].ColumnName = "实际完工";

            ds.Tables[0].Columns["remarks"].ColumnName = "备注(人、设备、物料等）";

            ds.Tables[0].Columns["starttimejudgment"].ColumnName = "starttimejudgment";
            ds.Tables[0].Columns["date1judgment"].ColumnName = "date1judgment";
            ds.Tables[0].Columns["date2judgment"].ColumnName = "date2judgment";
            ds.Tables[0].Columns["date3judgment"].ColumnName = "date3judgment";

            

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;



            dataGridView1.Columns["starttimejudgment"].Visible = false;//隐藏判断列1
            dataGridView1.Columns["date1judgment"].Visible = false;//隐藏判断列2
            dataGridView1.Columns["date2judgment"].Visible = false;//隐藏判断列3
            dataGridView1.Columns["date3judgment"].Visible = false;//隐藏判断列4

            dataGridView1.Columns["预留1"].Visible = false;//隐藏
            dataGridView1.Columns["预留2"].Visible = false;//隐藏
            dataGridView1.Columns["备注(人、设备、物料等）"].Visible = false;//隐藏
            /*
            //改变颜色
            if (dataGridView1.SelectedCells[17].Value.ToString() == "lag")
            {
                //dataGridView1.Rows[2].Cells[7].Style.BackColor = Color.Red;//改变指定格子的颜色

            }*/
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }




        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox12.Text = dataGridView1.SelectedCells[3].Value.ToString();

            textBox1.Text = dataGridView1.SelectedCells[6].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[9].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[11].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[13].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[15].Value.ToString();
            textBox6.Text = dataGridView1.SelectedCells[5].Value.ToString();
            textBox7.Text = dataGridView1.SelectedCells[16].Value.ToString();

            textBox13.Text = dataGridView1.SelectedCells[8].Value.ToString();
            //textBox8.Text = dataGridView1.SelectedCells[19].Value.ToString();//textBox8暂不显示备注,调整为逾期原因显示

            /*this.dataGridView1.SelectedCells[6].Style.BackColor = Color.DeepSkyBlue;//改变背景色
            this.dataGridView1.SelectedCells[6].Style.ForeColor = Color.White;//改变前景色*/

            //如果是锁定的，什么都不能改
            if (comboBox2.Text == "锁定")
            {
                textBox2.ReadOnly = true;

                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;

                textBox12.ReadOnly = true;

                textBox13.ReadOnly = true;

                button5.Visible = false;
                //button10.Visible = false;
            }
            else
            {
             button12_Click(null, null);//调用赋予权限按钮button12
            }


            //加载前先清空
            textBox8.Text = "";
            //开始加载二次完工时间
            SqlConnection conn = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            //string sql = "select column1,column2....columnn from database where column = '" + textBox1.Text.Trim() + "'";
            string sql = "select pdn,actualstartdate,actualdate1,actualdate2,completionstate,plandate5,max([datetime]) as maxdatetime " +
                         "from production_plans_reason " +
                         "where rtrim(ltrim(pdn)) = rtrim(ltrim('" + textBox1.Text + "')) " +
                         "group by pdn,actualstartdate,actualdate1,actualdate2,completionstate,plandate5 ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            //解决当前无数据，报错：在没有任何数据时进行无效的读取尝试
            if (rdr.Read())
            {
                textBox8.Text = rdr[5].ToString();
                conn.Close();
            }
            rdr.Close();


        }


        //修改、提交更改
        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请选择正确的信息！");
            }
            //判断是否选择更改
            else if (MessageBox.Show("确认修改吗", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update [production_plans] set [Planstarttime]='" + textBox2.Text + "',[plandate1]='" + textBox3.Text + "',[plandate2]='" + textBox4.Text + "'  " +
                                  ",[plandate3] = '" + textBox5.Text + "',[plandate4] = '" + textBox6.Text + "',[plandate5] = '" + textBox7.Text + "'  " +
                                  ",[model] = '" + textBox12.Text + "' ,[lock] = '" + comboBox2.Text + "' ,[takedelivery] = '" + textBox13.Text + "' ,[Customername] = '" + textBox6.Text + "'       " +
                                  "where [pdn]='" + textBox1.Text + "' ";

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
                MessageBox.Show("提交完成！");

            }

        }

        //新增追踪按钮，点击后显示groupbox
        private void button5_Click(object sender, EventArgs e)
        {

            button5.Visible = false;
            button10.Visible = true;
            groupBox1.Visible = true;//显示
        }

        //新增追踪确认，点击后写入数据表
        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from [production_plans] where [pdn] = '" + textBox9.Text + "'  "), con1);

            if (textBox9.Text.Length <= 5)
            {
                MessageBox.Show("请补充必要信息！");
            }
            else if ((int)cmd1.ExecuteScalar() > 0)
            {
                MessageBox.Show("已存在相同产品编号", "提示");
                con1.Close();
            }
            else
            {
                /*
                //新增前刷新唯一标识符、实际添加时间
                label4.Text = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09唯一标识符
                label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                */

                //int a = 0;
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myadd = "insert into [production_plans] (   [pdn],[fac],[model],[drawingnumber],[orderstatus],[lock]  ) values ('" + textBox9.Text + "','" + comboBox1.Text + "', " +
                         "'" + textBox10.Text + "','" + textBox11.Text + "','备货','不锁定' )";

                SqlCommand cmd = new SqlCommand(myadd, con);


                cmd.ExecuteNonQuery();

                MessageBox.Show("新增完成！");
            }


            //调用刷新按钮button2
            button2.PerformClick();
            groupBox1.Visible = false;//新增后重新设置groupbox为隐藏

        }

        //导出Excel
        private void button3_Click(object sender, EventArgs e)
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

        //收起
        private void button10_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            button5.Visible = true;
            groupBox1.Visible = false;//显示

        }


        //按日期选择查询
        private void button7_Click(object sender, EventArgs e)
        {

            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [lock],[fac],[orderstatus],[model],[drawingnumber],[Customername],[pdn],[deliverydate],[takedelivery],[Planstarttime],[actualstartdate],[plandate1], " +
                         "[actualdate1],[plandate2],[actualdate2],[plandate3],[plandate4],[plandate5],[completionstate],[remarks], " +

                         " ( case when [Planstarttime] is null or [Planstarttime] like'' then '3'  " +
                         " when([Planstarttime] is not null and[Planstarttime] not like'' and len(ltrim(rtrim(Planstarttime)) ) < 8) then '3' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " +      //正常
                         " when datediff(day, Planstarttime, actualstartdate) > 0 then '1' else '2' end) as starttimejudgment " +

                         ",( case when [plandate1] is null or [plandate1] like'' then '3'  " +
                         "when([plandate1] is not null and[plandate1] not like'' and len(ltrim(rtrim(plandate1)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate1, actualdate1) > 0 then '1' else '2' end) as date1judgment " +

                         ",( case when [plandate2] is null or [plandate2] like'' then '3'  " +
                         "when([plandate2] is not null and[plandate2] not like'' and len(ltrim(rtrim(plandate2)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate2, actualdate2) > 0 then '1' else '2' end) as date2judgment " +

                         ",( case when [plandate3] is null or [plandate3] like'' then '3'  " +
                         "when([plandate3] is not null and[plandate3] not like'' and len(ltrim(rtrim(plandate3)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate3, completionstate) > 0 then '1' else '2' end) as date3judgment " +



                         "FROM [cs管理].[dbo].[production_plans]  " +
                         "where ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "')) " +
                         "and (completionstate is null or completionstate like '')  " +
                         "order by lock desc,orderstatus desc  ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["lock"].ColumnName = "锁定状态";

            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["orderstatus"].ColumnName = "订单/备货";
            ds.Tables[0].Columns["model"].ColumnName = "锅炉型号";
            ds.Tables[0].Columns["drawingnumber"].ColumnName = "图号";
            ds.Tables[0].Columns["Customername"].ColumnName = "客户名称";
            ds.Tables[0].Columns["pdn"].ColumnName = "锅炉编号";
            ds.Tables[0].Columns["deliverydate"].ColumnName = "合同交期";
            ds.Tables[0].Columns["takedelivery"].ColumnName = "客户提货";

            ds.Tables[0].Columns["Planstarttime"].ColumnName = "计划开工";
            ds.Tables[0].Columns["actualstartdate"].ColumnName = "实际开工";
            ds.Tables[0].Columns["plandate1"].ColumnName = "计划穿管";
            ds.Tables[0].Columns["actualdate1"].ColumnName = "实际穿管";
            ds.Tables[0].Columns["plandate2"].ColumnName = "计划水压";
            ds.Tables[0].Columns["actualdate2"].ColumnName = "实际水压";
            ds.Tables[0].Columns["plandate3"].ColumnName = "计划完工";
            ds.Tables[0].Columns["plandate4"].ColumnName = "预留1";
            ds.Tables[0].Columns["plandate5"].ColumnName = "预留2";

            ds.Tables[0].Columns["completionstate"].ColumnName = "实际完工";

            ds.Tables[0].Columns["remarks"].ColumnName = "备注(人、设备、物料等）";

            ds.Tables[0].Columns["starttimejudgment"].ColumnName = "starttimejudgment";
            ds.Tables[0].Columns["date1judgment"].ColumnName = "date1judgment";
            ds.Tables[0].Columns["date2judgment"].ColumnName = "date2judgment";
            ds.Tables[0].Columns["date3judgment"].ColumnName = "date3judgment";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;



            dataGridView1.Columns["starttimejudgment"].Visible = false;//隐藏判断列1
            dataGridView1.Columns["date1judgment"].Visible = false;//隐藏判断列2
            dataGridView1.Columns["date2judgment"].Visible = false;//隐藏判断列3
            dataGridView1.Columns["date3judgment"].Visible = false;//隐藏判断列4

            dataGridView1.Columns["预留1"].Visible = false;//隐藏
            dataGridView1.Columns["预留2"].Visible = false;//隐藏
            dataGridView1.Columns["备注(人、设备、物料等）"].Visible = false;//隐藏
            /*
            //改变颜色
            if (dataGridView1.SelectedCells[17].Value.ToString() == "lag")
            {
                //dataGridView1.Rows[2].Cells[7].Style.BackColor = Color.Red;//改变指定格子的颜色

            }*/
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }


        }


        //选择日期后，刷新8位日期格式1
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox14.Text = dateTimePicker1.Value.ToString("yyyyMMdd");
            textBox15.Text = dateTimePicker2.Value.ToString("yyyyMMdd");
        }
        //选择日期后，刷新8位日期格式2
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox14.Text = dateTimePicker1.Value.ToString("yyyyMMdd");
            textBox15.Text = dateTimePicker2.Value.ToString("yyyyMMdd");
        }
        //按数据厂区选择查询
        private void button9_Click(object sender, EventArgs e)
        {


            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [lock],[fac],[orderstatus],[model],[drawingnumber],[Customername],[pdn],[deliverydate],[takedelivery],[Planstarttime],[actualstartdate],[plandate1], " +
                         "[actualdate1],[plandate2],[actualdate2],[plandate3],[plandate4],[plandate5],[completionstate],[remarks], " +

                         " ( case when [Planstarttime] is null or [Planstarttime] like'' then '3'  " +
                         " when([Planstarttime] is not null and[Planstarttime] not like'' and len(ltrim(rtrim(Planstarttime)) ) < 8) then '3' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " +      //正常
                         " when datediff(day, Planstarttime, actualstartdate) > 0 then '1' else '2' end) as starttimejudgment " +

                         ",( case when [plandate1] is null or [plandate1] like'' then '3'  " +
                         "when([plandate1] is not null and[plandate1] not like'' and len(ltrim(rtrim(plandate1)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate1, actualdate1) > 0 then '1' else '2' end) as date1judgment " +

                         ",( case when [plandate2] is null or [plandate2] like'' then '3'  " +
                         "when([plandate2] is not null and[plandate2] not like'' and len(ltrim(rtrim(plandate2)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate2, actualdate2) > 0 then '1' else '2' end) as date2judgment " +

                         ",( case when [plandate3] is null or [plandate3] like'' then '3'  " +
                         "when([plandate3] is not null and[plandate3] not like'' and len(ltrim(rtrim(plandate3)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate3, completionstate) > 0 then '1' else '2' end) as date3judgment " +



                         "FROM [cs管理].[dbo].[production_plans]  ";

           

            if (checkBox1.CheckState == CheckState.Checked && checkBox2.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)//只有第一个条件
            {
                sql += "WHERE rtrim(ltrim([pdn]))=rtrim(ltrim('" + textBox1.Text + "')) and (completionstate is null or completionstate like '')  " +
                       "order by lock desc,orderstatus desc ";

            }
            else if(checkBox2.CheckState == CheckState.Checked && checkBox1.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)//只有第二个条件
            {
                sql += "where ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "')) " +
                       "and (completionstate is null or completionstate like '')  " +
                       "order by lock desc,orderstatus desc  ";
            }
            else if (checkBox3.CheckState == CheckState.Checked && checkBox1.CheckState == CheckState.Unchecked && checkBox2.CheckState == CheckState.Unchecked)//只有第三个条件
            {
                sql += "where ltrim(rtrim(fac))=rtrim(ltrim('" + comboBox3.Text + "')) " +
                       "and (completionstate is null or completionstate like '') " +
                       "order by lock desc,orderstatus desc   ";
            }
            else if (checkBox1.CheckState == CheckState.Checked && checkBox2.CheckState == CheckState.Checked && checkBox3.CheckState == CheckState.Unchecked)//1&2
            {
                sql += "WHERE rtrim(ltrim([pdn]))=rtrim(ltrim('" + textBox1.Text + "')) and (completionstate is null or completionstate like '')  " +
                       "and (  ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "'))  ) " +
                       "order by lock desc,orderstatus desc ";
            }
            else if (checkBox2.CheckState == CheckState.Checked && checkBox3.CheckState == CheckState.Checked && checkBox1.CheckState == CheckState.Unchecked)//2&3
            {
                sql += "where ltrim(rtrim(fac))=rtrim(ltrim('" + comboBox3.Text + "')) " +
                       "and ( ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "')) ) " +
                       "and (completionstate is null or completionstate like '') " +
                       "order by lock desc,orderstatus desc   ";
            }
            else if (checkBox1.CheckState == CheckState.Checked && checkBox3.CheckState == CheckState.Checked && checkBox2.CheckState == CheckState.Unchecked)//1&3
            {
                sql += "where rtrim(ltrim([pdn]))=rtrim(ltrim('" + textBox1.Text + "')) " +
                       "and ( ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "')) ) " +
                       "and (completionstate is null or completionstate like '') " +
                       "order by lock desc,orderstatus desc   ";
            }
            else if (checkBox1.CheckState == CheckState.Unchecked && checkBox2.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)//三个条件都不满足
            {
                sql += "where (completionstate is null or completionstate like '')  " +
                       "order by lock desc,orderstatus desc   ";
            }
            else if (checkBox1.CheckState == CheckState.Checked && checkBox2.CheckState == CheckState.Checked && checkBox3.CheckState == CheckState.Checked)//三个条件都满足
            {
                sql += "where rtrim(ltrim([pdn]))=rtrim(ltrim('" + textBox1.Text + "')) " +
                       "and ltrim(rtrim(fac))=rtrim(ltrim('" + comboBox3.Text + "')) " +
                       "and ( ltrim(rtrim(Planstarttime))>=rtrim(ltrim('" + textBox14.Text + "')) and ltrim(rtrim(Planstarttime))<=rtrim(ltrim('" + textBox15.Text + "')) ) " +
                       "and (completionstate is null or completionstate like '') " +
                       "order by lock desc,orderstatus desc   ";
            }

            //sql拼接判断结束

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["lock"].ColumnName = "锁定状态";

            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["orderstatus"].ColumnName = "订单/备货";
            ds.Tables[0].Columns["model"].ColumnName = "锅炉型号";
            ds.Tables[0].Columns["drawingnumber"].ColumnName = "图号";
            ds.Tables[0].Columns["Customername"].ColumnName = "客户名称";
            ds.Tables[0].Columns["pdn"].ColumnName = "锅炉编号";
            ds.Tables[0].Columns["deliverydate"].ColumnName = "合同交期";
            ds.Tables[0].Columns["takedelivery"].ColumnName = "客户提货";

            ds.Tables[0].Columns["Planstarttime"].ColumnName = "计划开工";
            ds.Tables[0].Columns["actualstartdate"].ColumnName = "实际开工";
            ds.Tables[0].Columns["plandate1"].ColumnName = "计划穿管";
            ds.Tables[0].Columns["actualdate1"].ColumnName = "实际穿管";
            ds.Tables[0].Columns["plandate2"].ColumnName = "计划水压";
            ds.Tables[0].Columns["actualdate2"].ColumnName = "实际水压";
            ds.Tables[0].Columns["plandate3"].ColumnName = "计划完工";
            ds.Tables[0].Columns["plandate4"].ColumnName = "预留1";
            ds.Tables[0].Columns["plandate5"].ColumnName = "预留2";

            ds.Tables[0].Columns["completionstate"].ColumnName = "实际完工";

            ds.Tables[0].Columns["remarks"].ColumnName = "备注(人、设备、物料等）";

            ds.Tables[0].Columns["starttimejudgment"].ColumnName = "starttimejudgment";
            ds.Tables[0].Columns["date1judgment"].ColumnName = "date1judgment";
            ds.Tables[0].Columns["date2judgment"].ColumnName = "date2judgment";
            ds.Tables[0].Columns["date3judgment"].ColumnName = "date3judgment";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;



            dataGridView1.Columns["starttimejudgment"].Visible = false;//隐藏判断列1
            dataGridView1.Columns["date1judgment"].Visible = false;//隐藏判断列2
            dataGridView1.Columns["date2judgment"].Visible = false;//隐藏判断列3
            dataGridView1.Columns["date3judgment"].Visible = false;//隐藏判断列4

            dataGridView1.Columns["预留1"].Visible = false;//隐藏
            dataGridView1.Columns["预留2"].Visible = false;//隐藏
            dataGridView1.Columns["备注(人、设备、物料等）"].Visible = false;//隐藏
            /*
            //改变颜色
            if (dataGridView1.SelectedCells[17].Value.ToString() == "lag")
            {
                //dataGridView1.Rows[2].Cells[7].Style.BackColor = Color.Red;//改变指定格子的颜色

            }*/
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }



        }


        //赋予颜色
        private void button11_Click(object sender, EventArgs e)
        {
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
        }
        //赋予权限，原先权限写在load事件里，但是点击某些单元格，部分单元格变为只读，切换其他单元格恢复权限遇到问题，所以单独用赋予权限按钮，供load调用、单元格点击事件调用
        private void button12_Click(object sender, EventArgs e)
        {
            //要规定每个控件的权限，因为cellclick事件后，有些被锁定的还要返回不锁
            if (label28.Text == "销售部")
            {
                textBox2.ReadOnly = true;

                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox12.ReadOnly = true;
                button5.Visible = false;
                button10.Visible = false;

                comboBox2.Visible = false; //无锁定权限的，不显示锁定
                label20.Text = "";//锁定2个字标签不显示
                //有权限的
                textBox13.ReadOnly = false;


            }
            else if (label28.Text == "计划员")
            {
                textBox2.ReadOnly = true;

                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;

                textBox13.ReadOnly = true;

                comboBox2.Visible = false; //无锁定权限的，不显示锁定
                label20.Text = "";//锁定2个字标签不显示

                //有权限的

                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox12.ReadOnly = false;

                button4.Visible = true;
                //button10.Visible = true;
                button5.Visible = true;

            }
            else if (label28.Text == "生产部")
            {
                textBox13.ReadOnly = true;
                //有权限的
                textBox2.ReadOnly = false;

                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;

                textBox12.ReadOnly = false;

                button4.Visible = true;
                //button10.Visible = true;
                button5.Visible = true;
            }
            else if (label28.Text == "admin")//最高权限
            {
                textBox2.ReadOnly = false;

                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;

                textBox12.ReadOnly = false;

                textBox13.ReadOnly = false;


                button4.Visible = true;
                //button10.Visible = true;
                button5.Visible = true;
            }
            else
            {
                textBox2.ReadOnly = true;

                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;

                textBox12.ReadOnly = true;

                textBox13.ReadOnly = true;


                button4.Visible = false;
                button5.Visible = false;
                button10.Visible = false;
                comboBox2.Visible = false; //无锁定权限的，不显示锁定
                label20.Text = "";//锁定2个字标签不显示

            }
        }

        //帮助窗体
        private void button13_Click(object sender, EventArgs e)
        {
            生产计划_form1 f2 = new 生产计划_form1();
            f2.ShowDialog();
        }


        //查看历史备注
        private void button14_Click(object sender, EventArgs e)
        {
            /* 
               //登陆成功，进入另一个窗体
               //this.Hide();//隐藏当前窗口 
               生产计划_form2 ppform2 = new 生产计划_form2();
               ppform2.Show();
               //this.Close();
            */

            forms.生产计划_form4 ppform4 = new forms.生产计划_form4();
            ppform4.Varpdn = textBox1.Text;
            //关键地方 ↓
            if (ppform4.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ppform4.Varpdn;
            }

        }

        //查询已完工
        private void button8_Click(object sender, EventArgs e)
        {

            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [lock],[fac],[orderstatus],[model],[drawingnumber],[Customername],[pdn],[deliverydate],[takedelivery],[Planstarttime],[actualstartdate],[plandate1], " +
                         "[actualdate1],[plandate2],[actualdate2],[plandate3],[plandate4],[plandate5],[completionstate],[remarks], " +

                         " ( case when [Planstarttime] is null or [Planstarttime] like'' then '3'  " +
                         " when([Planstarttime] is not null and[Planstarttime] not like'' and len(ltrim(rtrim(Planstarttime)) ) < 8) then '3' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         " when((ltrim(rtrim(actualstartdate)) is null or ltrim(rtrim(actualstartdate)) like '') and datediff(day, Planstarttime, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " +      //正常
                         " when datediff(day, Planstarttime, actualstartdate) > 0 then '1' else '2' end) as starttimejudgment " +

                         ",( case when [plandate1] is null or [plandate1] like'' then '3'  " +
                         "when([plandate1] is not null and[plandate1] not like'' and len(ltrim(rtrim(plandate1)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate1)) is null or ltrim(rtrim(actualdate1)) like '') and datediff(day, plandate1, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate1, actualdate1) > 0 then '1' else '2' end) as date1judgment " +

                         ",( case when [plandate2] is null or [plandate2] like'' then '3'  " +
                         "when([plandate2] is not null and[plandate2] not like'' and len(ltrim(rtrim(plandate2)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(actualdate2)) is null or ltrim(rtrim(actualdate2)) like '') and datediff(day, plandate2, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate2, actualdate2) > 0 then '1' else '2' end) as date2judgment " +

                         ",( case when [plandate3] is null or [plandate3] like'' then '3'  " +
                         "when([plandate3] is not null and[plandate3] not like'' and len(ltrim(rtrim(plandate3)) ) < 8) then '3' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) > 0 ) then '1' " +
                         "when((ltrim(rtrim(completionstate)) is null or ltrim(rtrim(completionstate)) like '') and datediff(day, plandate3, CONVERT(varchar(100), GETDATE(), 112)) <= 0 ) then '2' " + //正常
                         "when datediff(day, plandate3, completionstate) > 0 then '1' else '2' end) as date3judgment " +



                         "FROM [cs管理].[dbo].[production_plans]  " +
                         "where (completionstate is not null and completionstate not like '') " +
                         "order by lock desc,orderstatus desc  ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["lock"].ColumnName = "锁定状态";

            ds.Tables[0].Columns["fac"].ColumnName = "工厂";
            ds.Tables[0].Columns["orderstatus"].ColumnName = "订单/备货";
            ds.Tables[0].Columns["model"].ColumnName = "锅炉型号";
            ds.Tables[0].Columns["drawingnumber"].ColumnName = "图号";
            ds.Tables[0].Columns["Customername"].ColumnName = "客户名称";
            ds.Tables[0].Columns["pdn"].ColumnName = "锅炉编号";
            ds.Tables[0].Columns["deliverydate"].ColumnName = "合同交期";
            ds.Tables[0].Columns["takedelivery"].ColumnName = "客户提货";

            ds.Tables[0].Columns["Planstarttime"].ColumnName = "计划开工";
            ds.Tables[0].Columns["actualstartdate"].ColumnName = "实际开工";
            ds.Tables[0].Columns["plandate1"].ColumnName = "计划穿管";
            ds.Tables[0].Columns["actualdate1"].ColumnName = "实际穿管";
            ds.Tables[0].Columns["plandate2"].ColumnName = "计划水压";
            ds.Tables[0].Columns["actualdate2"].ColumnName = "实际水压";
            ds.Tables[0].Columns["plandate3"].ColumnName = "计划完工";
            ds.Tables[0].Columns["plandate4"].ColumnName = "预留1";
            ds.Tables[0].Columns["plandate5"].ColumnName = "预留2";

            ds.Tables[0].Columns["completionstate"].ColumnName = "实际完工";

            ds.Tables[0].Columns["remarks"].ColumnName = "备注(人、设备、物料等）";

            ds.Tables[0].Columns["starttimejudgment"].ColumnName = "starttimejudgment";
            ds.Tables[0].Columns["date1judgment"].ColumnName = "date1judgment";
            ds.Tables[0].Columns["date2judgment"].ColumnName = "date2judgment";
            ds.Tables[0].Columns["date3judgment"].ColumnName = "date3judgment";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;



            dataGridView1.Columns["starttimejudgment"].Visible = false;//隐藏判断列1
            dataGridView1.Columns["date1judgment"].Visible = false;//隐藏判断列2
            dataGridView1.Columns["date2judgment"].Visible = false;//隐藏判断列3
            dataGridView1.Columns["date3judgment"].Visible = false;//隐藏判断列4

            dataGridView1.Columns["预留1"].Visible = false;//隐藏
            dataGridView1.Columns["预留2"].Visible = false;//隐藏
            dataGridView1.Columns["备注(人、设备、物料等）"].Visible = false;//隐藏
            /*
            //改变颜色
            if (dataGridView1.SelectedCells[17].Value.ToString() == "lag")
            {
                //dataGridView1.Rows[2].Cells[7].Style.BackColor = Color.Red;//改变指定格子的颜色

            }*/
            //判断计划开工、实际开工时间延迟
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[20, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[10, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[20, i].Value) >= 2 && Convert.ToDouble(dataGridView1[20, i].Value) < 3)
                {
                    dataGridView1[10, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[10, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan1
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[21, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[12, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[21, i].Value) >= 2 && Convert.ToDouble(dataGridView1[21, i].Value) < 3)
                {
                    dataGridView1[12, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[12, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

            //plan2
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[22, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[14, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[22, i].Value) >= 2 && Convert.ToDouble(dataGridView1[22, i].Value) < 3)
                {
                    dataGridView1[14, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[14, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }
            //plan3
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // if (  dataGridView1[18, i].Value.ToString() == "lag")//暂不可用
                if (Convert.ToDouble(dataGridView1[23, i].Value) <= 1)
                {
                    //dataGridView1[8, i].Style.ForeColor = Color.Red;//改变前景色
                    dataGridView1[18, i].Style.BackColor = Color.Red;//改变背景色
                }
                else if (Convert.ToDouble(dataGridView1[23, i].Value) >= 2 && Convert.ToDouble(dataGridView1[23, i].Value) < 3)
                {
                    dataGridView1[18, i].Style.BackColor = Color.LawnGreen;//改变背景色
                }
                else
                {
                    dataGridView1[18, i].Style.BackColor = Color.Yellow;//改变背景色
                }

            }

        }

        //双击单元行弹出备注窗口
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            生产计划_form2 ppform2 = new 生产计划_form2();
            ppform2.Varpdn = textBox1.Text;
            //关键地方 ↓
            if (ppform2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ppform2.Varpdn;
            }
        }

        //鼠标移动到单元格弹出form5
        //CellMouseMove事件：主要是鼠标指针移动到单元格时候的样式
        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            //设置第二行第一列的提示内容
            //this.dataGridView1[0, 1].ToolTipText = "该单元格的内容不能修改";

            //textBox16.Text = this.dataGridView1[3, 4].Value.ToString();
            //textBox16.Text = this.dataGridView1[6, int.Parse(textBox17.Text)].Value.ToString();//方式一的获取行号，尝试


            //textBox16.Text = this.dataGridView1[6, int.Parse(textBox17.Text)].Value.ToString();



            /*textBox16.Text = this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].Value.ToString();//当前单元格内容

            //this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].ToolTipText = "该单元格的内容不能修改";
            this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].ToolTipText = textBox8.Text;//textBox8为逾期原因显示*/

        }


        //返回光标处的行索引号（）

        public int GetRowIndexAt(int mouseLocation_Y)

        {

            if (dataGridView1.FirstDisplayedScrollingRowIndex < 0)

            {

                return 0;  // no rows.   

            }

            if (dataGridView1.ColumnHeadersVisible == true && mouseLocation_Y <= dataGridView1.ColumnHeadersHeight)

            {

                return 0;

            }

            int index = dataGridView1.FirstDisplayedScrollingRowIndex;

            int displayedCount = dataGridView1.DisplayedRowCount(true);

            for (int k = 1; k <= displayedCount;)  // 因为行不能ReOrder，故只需要搜索显示的行   

            {

                if (dataGridView1.Rows[index].Visible == true)

                {

                    Rectangle rect = dataGridView1.GetRowDisplayRectangle(index, true);  // 取该区域的显示部分区域   

                    if (rect.Top <= mouseLocation_Y && mouseLocation_Y < rect.Bottom)

                    {

                        return index;

                    }

                    k++;  // 只计数显示的行;   

                }

                index++;

            }

            return 0;

        }




        //鼠标移过控件时发生(等于行号)
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {

            //textBox17.Text = GetRowIndexAt(e.Y).ToString(); //textBox17为上面获取到的行号

            //方式二，获取鼠标所在datagridview的行列坐标
            int r = this.dataGridView1.HitTest(e.X, e.Y).RowIndex; //行
            int c = this.dataGridView1.HitTest(e.X, e.Y).ColumnIndex; //列

            textBox17.Text = r.ToString();
            textBox18.Text = c.ToString();
            label35.Text = this.dataGridView1.Rows.Count.ToString();//总行数
            // Convert.ToString(1 - Convert.ToDouble(textBox1.Text));
            label36.Text = Convert.ToString(Convert.ToDouble(label35.Text) - 1);//总行数-1
            label37.Text = Convert.ToString(Convert.ToDouble(label35.Text) - 2);//总行数-2

            //解决未选中行、列时的值为-1问题
            if (textBox17.Text == "-1")
            {
                label32.Text = "0";
            }
            else if(textBox17.Text == Convert.ToString(Convert.ToDouble(label35.Text) - 1))
            {
                label32.Text = Convert.ToString(Convert.ToDouble(label35.Text) - 2);
            }
            else
            {
                label32.Text = textBox17.Text;
            }

            if (textBox18.Text == "-1")
            {
                label33.Text = "0";
            }
            else
            {
                label33.Text = textBox18.Text;
            }






            //textBox16.Text = this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].Value.ToString();//当前单元格内容

            if(label35.Text == "1" )
            {
                textBox16.Text = "";
            }
            else
            {
                textBox16.Text = this.dataGridView1[6, int.Parse(label32.Text)].Value.ToString();//当前单元格内容//显示产品编号列的内容
            }
            //※//textBox16.Text = this.dataGridView1[6, int.Parse(label32.Text)].Value.ToString();//当前单元格内容//显示产品编号列的内容




            /*dataGridView的ToolTip事件，效率过低，暂不使用
            //this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].ToolTipText = "该单元格的内容不能修改";
            this.dataGridView1[int.Parse(label33.Text), int.Parse(label32.Text)].ToolTipText = textBox8.Text;//textBox8为逾期原因显示
            */

        }


        //右键菜单，查看逾期原因，弹出逾期查看窗口并传值
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            forms.生产计划.生产计划_form5 ppform5 = new forms.生产计划.生产计划_form5();
            ppform5.Varpdn = textBox16.Text;//传送产品编号
            ppform5.Varpdns = textBox18.Text;//传送列号
            //关键地方 ↓

            ppform5.StartPosition = FormStartPosition.Manual;//在鼠标位置边弹出
            ppform5.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            ppform5.Show();

            //textBox16.Text = ppform5.Varpdn;
            
        }



        //发送邮件提醒
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            forms.生产计划.生产计划_form6 ppform6 = new forms.生产计划.生产计划_form6();
            ppform6.Varpdn = textBox16.Text;//传送产品编号
            ppform6.Varpdns = textBox18.Text;//传送列号
            //关键地方 ↓

            //ppform6.StartPosition = FormStartPosition.Manual;
            //ppform6.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            ppform6.Show();
        }


        //二次完工日颜色闪烁
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(textBox8.Text.Length>=5)
            {

            if (this.timer1.Interval % 2 == 0)
            {
                this.label8.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.label8.ForeColor = System.Drawing.Color.DeepSkyBlue;
            }
            timer1.Interval++;
            }
            else if (textBox8.Text.Length < 5)
            {
                this.label8.ForeColor = System.Drawing.Color.Black;
            }

        }



        /*cellmousemove和mouseMove区别
         *测试都换成mouseMove事件
         *验证结束√
         */


    }
}
