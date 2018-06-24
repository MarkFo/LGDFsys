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
using Oracle.ManagedDataAccess.Client;

namespace LGD.forms
{
    public partial class bom管理_form1 : Form
    {

        public bom管理_form1()
        {
            InitializeComponent();

        }

        //LOAD
        private void bom管理_form1_Load(object sender, EventArgs e)
        {


            url.Text = "192.168.1.226"; //默认地址
            port.Text = "1521";
            radioButton1.Enabled = radioButton2.Enabled = false;
            comboBox1.Text = "ORCL";
            username.Text = "WFPLM";
            password.Text = "WFPLM";



        }







        private void RefreshOracle(String uid, String pwd)
        {

            String ConnectionString = //"Data Source="+ url.Text+";user Id="+uid+";Password="+pwd;

            //"Data Source = (DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =" + url.Text + ")(PORT = "+port.Text+")))(CONNECT_DATA =(SERVICE_NAME = "+comboBox1.Text+" )));User ID = "+uid+" ;Password=  "+pwd;  //这里改为copy下的服务名和用户名和密码

            "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = " + url.Text + ")(PORT = " + port.Text + "))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = " + comboBox1.Text + ")));User ID = " + uid + " ;Password=  " + pwd;

            OracleHelper.connstr = ConnectionString; //oh = new OracleHelper(ConnectionString);

            OracleHelper.init();

            String sql = "select PLMBOMVIEW.end1id as end1id,PLMBOMVIEW.end1name as end1name,PLMBOMVIEW.end1basname as end1basname, " +
                         "PLMBOMVIEW.itemspecification as itemspecification,PLMBOMVIEW.macdcode as macdcode " +

                         ",PLMBOMVIEW.end2id as end2id,PLMBOMVIEW.end2name as end2name,PLMBOMVIEW.end2basname as end2basname, " +
                         "PLMBOMVIEW.itemspecification2 as itemspecification2,PLMBOMVIEW.macdsscode as macdsscode " +
                         ",PLMBOMVIEW.quantity as quantity,PLMBOMVIEW.sequence as sequence " +
                         "from PLMBOMVIEW " +
                         "start with end1id like '" + textBox1.Text + "' " +
                         "connect by  prior END2ID = END1ID " +
                         "order by end1id,sequence ";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(sw);
            //当前数据库所有的用户表
            DataTable dt = OracleHelper.GetDataTable(sql);
            dt.WriteXml(xw);
            string s = sw.ToString();
            conn_info.Text = "连接成功……\n所有的用户表展示如下：\n" + s;

            DataSet dt1 = OracleHelper.GetDataSet(sql);
            dt1.Tables[0].Columns["end1id"].ColumnName = "主件品号";
            dt1.Tables[0].Columns["end1name"].ColumnName = "主件属性";
            dt1.Tables[0].Columns["end1basname"].ColumnName = "主件品名";
            dt1.Tables[0].Columns["itemspecification"].ColumnName = "主件规格";
            dt1.Tables[0].Columns["macdcode"].ColumnName = "主件单位";
            dt1.Tables[0].Columns["end2id"].ColumnName = "元件品号";
            dt1.Tables[0].Columns["end2name"].ColumnName = "元件属性";
            dt1.Tables[0].Columns["end2basname"].ColumnName = "元件品名";
            dt1.Tables[0].Columns["itemspecification2"].ColumnName = "元件规格";
            dt1.Tables[0].Columns["macdsscode"].ColumnName = "元件单位";
            dt1.Tables[0].Columns["quantity"].ColumnName = "用量";
            dt1.Tables[0].Columns["sequence"].ColumnName = "序列";

            this.dataGridView1.DataSource = dt1.Tables[0].DefaultView;


            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


        }
        //查询
        private void button2_Click(object sender, EventArgs e)
        {

            this.button2.Visible = false;
            conn_info.Text = "请稍后，正在搜索...";   //设置Text属性   
            String uid = username.Text;
            String pwd = password.Text;
            //刷新数据库连接
            this.RefreshOracle(uid, pwd);

            //conn_info.Text = "100%";   //设置Text属性   
            this.button2.Visible = true;
        }
        //刷新
        private void button1_Click(object sender, EventArgs e)
        {
            String uid = username.Text;
            String pwd = password.Text;
            this.RefreshOracle(uid, pwd);
            /// <param name="node"> 根节点</param>
            /// <param name="end2id">主键</param>


        }

        //循环判断，一次将整个datagridview数据加载到数据库
        private void button3_Click(object sender, EventArgs e)
        {
            //添加之前先清除所有该主件的数据
            if (MessageBox.Show("确认提交吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cocta = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection cona = new SqlConnection(cocta);
                cona.Open();
                //string myupdate = "delete from [PLMBOM] where [mainpart]='" + textBox1.Text + "' ";
                string myupdate = "delete from [PLMBOM] ";

                SqlCommand cmd = new SqlCommand(myupdate, cona);

                cmd.ExecuteNonQuery();

                conn_info.Text = "清除过期任务...";   //设置Text属性  
                //MessageBox.Show("提交完成！");
                //Showdata();
                //显示所有数据
                button2_Click(sender, e);
            }
            else
            {
                //显示所有数据
                button2_Click(sender, e);
            }


            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            try
            {
                string sTrSql = string.Empty;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sTrSql += "insert into PLMBOM (end1id,end1name,end1basname,itemspecification,macdcode,end2id,end2name,end2basname,itemspecification2,macdsscode,quantity,sequence,mainpart)  " +
                              "values('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "' , " +
                              "'" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "','" + dataGridView1.Rows[i].Cells[6].Value + "','" + dataGridView1.Rows[i].Cells[7].Value + "', " +
                              "'" + dataGridView1.Rows[i].Cells[8].Value + "','" + dataGridView1.Rows[i].Cells[9].Value + "','" + dataGridView1.Rows[i].Cells[10].Value + "','" + dataGridView1.Rows[i].Cells[11].Value + "','" + textBox1.Text + "' ) ";
                }
                //SqlCommand cmd = new SqlCommand(myadd, con);
                using (SqlCommand cmd = new SqlCommand(sTrSql, con))
                {
                    if (cmd.ExecuteNonQuery() != -1)
                    {
                        MessageBox.Show("成功提交到服务端，请稍后");
                        conn_info.Text = "已提交...";   //设置Text属性  
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败,详细信息：" + ex.ToString());
            }
            con.Close();



        }

        /**-------/////////////////////////////////////////////-------------------//////////////////////////////////////************************///////////////////////////////*/
        private void button4_Click(object sender, System.EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请输入主件品号！");
            }
            else
            { 
            //跳转到BOM管理form2

            bom管理_form2 bom管理_form2 = new bom管理_form2();
            bom管理_form2.Flag = textBox1.Text;
            //关键地方 ↓
            if (bom管理_form2.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = bom管理_form2.Flag;
            }


            }

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

        //叶子节点
        private void button6_Click(object sender, EventArgs e)
        {

            this.button6.Visible = false;
            conn_info.Text = "请稍后，正在搜索...";   //设置Text属性   
            String uid = username.Text;
            String pwd = password.Text;


            String ConnectionString = //"Data Source="+ url.Text+";user Id="+uid+";Password="+pwd;

            "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = " + url.Text + ")(PORT = " + port.Text + "))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = " + comboBox1.Text + ")));User ID = " + uid + " ;Password=  " + pwd;

            OracleHelper.connstr = ConnectionString; //oh = new OracleHelper(ConnectionString);

            OracleHelper.init();

            String sql = "select PLMBOMVIEW.end1id as end1id,PLMBOMVIEW.end1name as end1name,PLMBOMVIEW.end1basname as end1basname, " +
                         "PLMBOMVIEW.itemspecification as itemspecification,PLMBOMVIEW.macdcode as macdcode " +

                         ",PLMBOMVIEW.end2id as end2id,PLMBOMVIEW.end2name as end2name,PLMBOMVIEW.end2basname as end2basname, " +
                         "PLMBOMVIEW.itemspecification2 as itemspecification2,PLMBOMVIEW.macdsscode as macdsscode " +
                         ",PLMBOMVIEW.quantity as quantity,PLMBOMVIEW.sequence as sequence,CONNECT_BY_ISLEAF as isleaf " +
                         "from PLMBOMVIEW " +
                         "where CONNECT_BY_ISLEAF='1' " +
                         "start with end1id like '" + textBox1.Text + "' " +
                         "connect by  prior END2ID = END1ID " +
                         "order by end1id,sequence ";

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(sw);
            //当前数据库所有的用户表
            DataTable dt = OracleHelper.GetDataTable(sql);
            dt.WriteXml(xw);
            string s = sw.ToString();
            conn_info.Text = "连接成功……\n所有的用户表展示如下：\n" + s;

            DataSet dt1 = OracleHelper.GetDataSet(sql);
            dt1.Tables[0].Columns["end1id"].ColumnName = "主件品号";
            dt1.Tables[0].Columns["end1name"].ColumnName = "主件属性";
            dt1.Tables[0].Columns["end1basname"].ColumnName = "主件品名";
            dt1.Tables[0].Columns["itemspecification"].ColumnName = "主件规格";
            dt1.Tables[0].Columns["macdcode"].ColumnName = "主件单位";
            dt1.Tables[0].Columns["end2id"].ColumnName = "元件品号";
            dt1.Tables[0].Columns["end2name"].ColumnName = "元件属性";
            dt1.Tables[0].Columns["end2basname"].ColumnName = "元件品名";
            dt1.Tables[0].Columns["itemspecification2"].ColumnName = "元件规格";
            dt1.Tables[0].Columns["macdsscode"].ColumnName = "元件单位";
            dt1.Tables[0].Columns["quantity"].ColumnName = "用量";
            dt1.Tables[0].Columns["sequence"].ColumnName = "序列";
            dt1.Tables[0].Columns["isleaf"].ColumnName = "叶子节点";

            this.dataGridView1.DataSource = dt1.Tables[0].DefaultView;

            dataGridView1.Columns["叶子节点"].Visible = false;//隐藏判断列1

            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.button6.Visible = true;
        }



    }
}
