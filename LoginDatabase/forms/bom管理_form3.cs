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
    public partial class bom管理_查看 : Form
    {
        public bom管理_查看()
        {
            InitializeComponent();
        }

        private void bom管理_查看_Load(object sender, EventArgs e)
        {
            //private void Showdata()


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select tpro.realend1idpro as realend1idpro,PLMBOMs2.end1basname as end1basname,PLMBOMs2.itemspecification as itemspecification, " +
                         "tpro.end2id as end2id,tpro.end2basname as end2basname,tpro.itemspecification2 as itemspecification2,sum(convert(decimal(18, 4), tpro.quantity)) as sumtproquantity,tpro.macdsscode as macdsscode from( " +

                         "select (case when t.realend1id = t.end2id then t.end1id else  " +
                         "t.realend1id end) as realend1idpro, " +
                         "t.end2id, t.end2basname, t.itemspecification2, t.macdsscode, t.quantity from( " +

                         "select " +
                         "(case when sort7property <> '4'  then sort7 " +
                         "when sort6property <> '4' then sort6 " +
                         "when sort5property <> '4' then sort5 " +
                         "when sort4property <> '4' then sort4 " +
                         "when sort3property <> '4' then sort3 " +
                         "when sort2property <> '4' then sort2 " +
                         "when sort1property <> '4' then sort1 " +
                         "else sort1 end) as realend1id " +
                         ", * from sbomtemp " +

                         ") as t " +

                         ") as tpro " +
                         "left join(select distinct PLMBOM.end1id, PLMBOM.end1basname, PLMBOM.itemspecification from PLMBOM) as PLMBOMs2 on tpro.realend1idpro = PLMBOMs2.end1id " +

                         "group by tpro.realend1idpro,PLMBOMs2.end1basname,PLMBOMs2.itemspecification,tpro.end2id,tpro.end2basname,tpro.itemspecification2,tpro.macdsscode ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["realend1idpro"].ColumnName = "主件品号";
            ds.Tables[0].Columns["end1basname"].ColumnName = "主件品名";
            ds.Tables[0].Columns["itemspecification"].ColumnName = "主件规格";

            ds.Tables[0].Columns["end2id"].ColumnName = "元件品号";
            ds.Tables[0].Columns["end2basname"].ColumnName = "元件品名";
            ds.Tables[0].Columns["itemspecification2"].ColumnName = "元件规格";
            ds.Tables[0].Columns["sumtproquantity"].ColumnName = "用量";
            ds.Tables[0].Columns["macdsscode"].ColumnName = "单位";

            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;

            //this.dataGridView1.Columns["产品编号"].Visible = false;//产品编号列隐藏
        }

        //导出Excel
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
    }
}
