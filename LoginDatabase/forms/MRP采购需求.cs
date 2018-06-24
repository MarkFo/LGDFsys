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
    public partial class MRP采购需求 : Form
    {
        public MRP采购需求()
        {
            InitializeComponent();
        }

        private void MRP采购需求_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }


        //全部
        private void button1_Click(object sender, EventArgs e)
        {
            //按下后，避免重复按，先让其变灰
            button1.Visible = false;
            MessageBox.Show("正在搜索，请稍等！");



            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            //dataGridView2
            string sql2 = "select MOCTA.TA057 as TA057,INVMB.UDF07 as UDF07,INVMB.UDF06 as UDF06,COPMA.MA002 as MA002 from MOCTA  " +
                          "left join INVMB on MOCTA.TA006 = INVMB.MB001 " +
                          "left join COPTD on ltrim(rtrim(MOCTA.TA057)) = ltrim(rtrim(COPTD.UDF12)) " +
                          "left join COPTC on(COPTD.TD001+COPTD.TD002)= (COPTC.TC001 + COPTC.TC002) " +
                          "left join COPMA on COPTC.TC004 = COPMA.MA001 " +
                          "where(MOCTA.TA006 like '1%' or(MOCTA.TA034 like '%节能器%') or(MOCTA.TA034 like '%冷凝器%') or(MOCTA.TA034 like '%缸%')) " +
                          "and(MOCTA.TA011 <> 'Y' and MOCTA.TA011 <> 'y') " +
                          "and MOCTA.TA057 not like '%***%' " +
                          "and MOCTA.TA003 > '20160101' ";
            SqlCommand cmd2 = new SqlCommand(sql2, con);
            SqlDataAdapter ada2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            ada2.Fill(ds2);
            ds2.Tables[0].Columns["TA057"].ColumnName = "产品编号";
            ds2.Tables[0].Columns["UDF07"].ColumnName = "品名";
            ds2.Tables[0].Columns["UDF06"].ColumnName = "规格";
            ds2.Tables[0].Columns["MA002"].ColumnName = "订单";

            this.dataGridView2.DataSource = ds2.Tables[0].DefaultView;


            //****************
            //查询完毕，恢复按钮
            button1.Visible = true;
        }


        //钢板
        private void button2_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' and [MOCTBTB012] like '%钢板%' ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;


        }

        //管板
        private void button4_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' and ([MOCTBTB012] like '%管板%' or [MOCTBTB012] like '%锥形炉胆%' or [MOCTBTB012] like '%封头%') ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        //阀门
        private void button7_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' and ([MOCTBTB012] like '%阀%') ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        //钢管
        private void button3_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' and ([MOCTBTB012] like '%钢管%') ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        //法兰
        private void button5_Click(object sender, EventArgs e)
        {
            string coct = "Data Source=192.168.1.253;Initial Catalog=WEIFU;Persist Security Info=True;User ID=sa;Password=Server08";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "SELECT [MOCTBTB003],[MOCTBTB012],[MOCTBTB013],[WL],[MOCTBTB007],[kc],[xq] FROM[dbo].[LGD_MRP] " +
                         "WHERE xq<>'0.00' and ([MOCTBTB012] like '%法兰%') ";

            /*
                        string sql = "SELECT SFCTA.TA001+SFCTA.TA002+SFCTA.TA003 as ts,SFCTA.UDF07 as UDF07,SFCTA.TA022 as TA022,SFCTA.TA059 as TA059 FROM SFCTA " +
                                     "WHERE SFCTA.UDF03 = 'Y' AND SFCTA.TA031 LIKE '        ' and SFCTA.UDF09 <> '' ";
            */
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["MOCTBTB003"].ColumnName = "品号";
            ds.Tables[0].Columns["MOCTBTB012"].ColumnName = "品名";
            ds.Tables[0].Columns["MOCTBTB013"].ColumnName = "规格";
            ds.Tables[0].Columns["WL"].ColumnName = "BOM用量";
            ds.Tables[0].Columns["MOCTBTB007"].ColumnName = "单位";
            ds.Tables[0].Columns["kc"].ColumnName = "库存量";
            ds.Tables[0].Columns["xq"].ColumnName = "需求量";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
    }
}
