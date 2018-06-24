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
    public partial class 生产计划_form2 : Form
    {
        public 生产计划_form2()
        {
            InitializeComponent();
        }

        /*
         * 1.当前时间label √
         * 2.提交事件，pdn、时间、备注内容 √
         * 3.生产计划中备注栏，是不是需要继续显示？如果要显示，就显示备注表中最新的记录 √
         * 4.解决多线程问题
         * 5.已完工的默认不显示、查询后显示 √
         * 6.按锁定、订单默认排序 √
         * 7.无计划时间的筛选条件 ×
         * 8.备注表添加guid ×
         * 9.FSYS手工锁定编号 √
         * 10.颜色选中列样式
         */


        //提交（插入）数据
        private void button1_Click(object sender, EventArgs e)
        {

            if (label3.Text.Length <= 4)
            {
                MessageBox.Show("请选择产品编号后添加！");
            }
            else if (textBox1.Text.Length <= 2)
            {
                MessageBox.Show("请填写备注内容！");
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
                string myadd = "insert into [production_plans_remark] (   [pdn],[remarktime],[remarks]  ) values ('" + label3.Text + "','" + label4.Text + "', " +
                               " '" + textBox1.Text + "' )";

                SqlCommand cmd = new SqlCommand(myadd, con);


                cmd.ExecuteNonQuery();

                MessageBox.Show("添加完成！");
                button2_Click(null, null);//调用button12  添加后重新查询一遍

            }
        }
        //刷新选定编号的备注
        private void button2_Click(object sender, EventArgs e)
        {
            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = " SELECT[pdn],[remarktime],[remarks] " +
                         "FROM [dbo].[production_plans_remark] " +
                         "where [pdn]= '" + label3.Text + "' " +
                         "order by [remarktime] desc  ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["pdn"].ColumnName = "产品编号";
            ds.Tables[0].Columns["remarktime"].ColumnName = "备注时间";
            ds.Tables[0].Columns["remarks"].ColumnName = "内容";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            this.dataGridView1.Columns["产品编号"].Visible = false;//产品编号列隐藏
            /*
            this.dataGridView1.Columns[0].FillWeight = 140;
            this.dataGridView1.Columns[1].FillWeight = 140;
            this.dataGridView1.Columns[2].FillWeight = 400;
            */

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


        //窗体加载时接收varpdn的值
        private void 生产计划_form2_Load(object sender, EventArgs e)
        {
            label3.Text = this.varpdn;//窗体加载时接收varpdn的值
            button2_Click(null, null);//调用button12  加载时自动查询

            //计时器开始运行
            timer1.Interval = 1000;
            timer1.Start();

            //自动换行
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//所有单元格的内容自动调整列宽
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.dataGridView1.Columns[0].FillWeight = 43;//写法可行，暂不用
            this.dataGridView1.Columns["备注时间"].FillWeight = 45;//备注时间列宽度

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView1.RowsDefaultCellStyle.WrapMode = (DataGridViewTriState.True);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.RowTemplate.ReadOnly = true;

            this.dataGridView1.AllowUserToResizeRows = false;

            #endregion



        }

        //计时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            //label4.Text = DateTime.Now.ToString();
            label4.Text =  Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");//24小时
        }



    }
}
