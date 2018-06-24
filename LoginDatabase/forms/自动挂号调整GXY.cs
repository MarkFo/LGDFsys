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
    public partial class 自动挂号调整GXY : Form
    {
        public 自动挂号调整GXY()
        {
            InitializeComponent();
        }


        //模糊搜索
        private void button1_Click(object sender, EventArgs e)
        {
            //private void Showdata()


            string coct = "Data Source=192.168.1.253;Initial Catalog=HONGYI;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select COPTC.TC001 as TC001,COPTC.TC002 as TC002,COPTD.TD003 as TD003,COPMA.MA002 as MA002,COPTD.TD006 as TD006,COPTD.UDF12 as UDF12,COPTD.UDF06 as UDF06 from COPTC " +
                         "left join COPTD on(COPTC.TC001+COPTC.TC002)= (COPTD.TD001 + COPTD.TD002) " +
                         "left join COPMA on COPTC.TC004 = COPMA.MA001 " +
                         "WHERE COPTD.TD016='N' and (rtrim(ltrim(COPTC.TC002)) like '%" + textBox1.Text + "%' " +
                         "or rtrim(ltrim(COPMA.MA002)) like '%" + textBox1.Text + "%' " +
                         "or rtrim(ltrim(COPTD.TD006)) like '%" + textBox1.Text + "%' " +
                         "or rtrim(ltrim(COPTD.UDF12)) like '%" + textBox1.Text + "%') " +
                         "order by COPTC.TC039 desc";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["TC001"].ColumnName = "单别";
            ds.Tables[0].Columns["TC002"].ColumnName = "单号";
            ds.Tables[0].Columns["TD003"].ColumnName = "序号";
            ds.Tables[0].Columns["MA002"].ColumnName = "客户简称";
            ds.Tables[0].Columns["TD006"].ColumnName = "型号";
            ds.Tables[0].Columns["UDF12"].ColumnName = "当前产品编号";
            ds.Tables[0].Columns["UDF06"].ColumnName = "锁定状态";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            this.dataGridView1.Columns["单别"].Visible = false;//单别隐藏
            this.dataGridView1.Columns["序号"].Visible = false;//序号隐藏
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = dataGridView1.SelectedCells[0].Value.ToString();
            label4.Text = dataGridView1.SelectedCells[1].Value.ToString();
            label5.Text = dataGridView1.SelectedCells[2].Value.ToString();

            textBox2.Text = dataGridView1.SelectedCells[5].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedCells[6].Value.ToString();
        }

        private void 自动挂号调整GXY_Load(object sender, EventArgs e)
        {
            //自动换行
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//所有单元格的内容自动调整列宽
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.dataGridView1.Columns[0].FillWeight = 43;//写法可行，暂不用
            //this.dataGridView1.Columns["备注时间"].FillWeight = 45;//备注时间列宽度

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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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

        //update修改编号锁定
        private void button2_Click(object sender, EventArgs e)
        {
            if (label4.Text.Length == 0)
            {
                MessageBox.Show("请选择正确的信息！");
            }
            //判断是否选择更改
            else if (MessageBox.Show("确认修改吗", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string coct = "Data Source=192.168.1.253;Initial Catalog=HONGYI;Persist Security Info=True;User ID=sa;Password=Server08";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update COPTD set UDF12='" + textBox2.Text + "',UDF06='" + comboBox1.Text + "',UDF07='FSYS'  " +
                                  "where TD001='" + label3.Text + "' and TD002='" + label4.Text + "' and TD003='" + label5.Text + "' ";

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
                MessageBox.Show("提交完成！");

            }
        }


    }

}

