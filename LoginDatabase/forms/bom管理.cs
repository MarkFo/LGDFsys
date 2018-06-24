using LGD.sqlHelper;
using Helpers;
//using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LGD
{
    public partial class bom管理 : Form
    {
        public bom管理()
        {
            InitializeComponent();
        }

        private void bom管理_Load(object sender, EventArgs e)
        {
            url.Text = "192.168.1.226"; //默认地址
            port.Text = "1521";
            radioButton1.Enabled = radioButton2.Enabled = false;
            comboBox1.Text = "ORCL";
            username.Text = "WFPLM";
            password.Text = "WFPLM";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //windows身份验证不需要用户名密码
            username.Enabled = password.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //sqlserver身份验证需要用户名密码
            username.Enabled = password.Enabled = true;
        }

        private void rb_sqlserver_CheckedChanged(object sender, EventArgs e)
        {
            url.Text = "localhost"; //默认地址
            port.Text = "1433";
            radioButton1.Enabled = radioButton2.Enabled = true;
        }

        private void rb_mysql_CheckedChanged(object sender, EventArgs e)
        {
            url.Text = "localhost"; //默认地址
            port.Text = "3306";
            radioButton1.Enabled = radioButton2.Enabled = false;
        }

        private void rb_oracle_CheckedChanged(object sender, EventArgs e)
        {
            url.Text = "192.168.1.226"; //默认地址
            port.Text = "1521";
            radioButton1.Enabled = radioButton2.Enabled = false;
            comboBox1.Text = "ORCL";
            username.Text = "WFPLM";
            password.Text = "WFPLM";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String uid = username.Text;
            String pwd = password.Text;

            //刷新数据库连接
            if (this.validDbUser(uid, pwd))
            {
                if (rb_sqlserver.Checked) //连接sqlServer
                {
                    this.RefreshSqlServerDb(uid, pwd);
                }
                else if (rb_mysql.Checked) //连接mysql
                {
                    this.RefreshMysql(uid, pwd);
                }
                else if (rb_oracle.Checked)
                {
                    this.RefreshOracle(uid, pwd);
                }
            }
        }

        private bool connectSqlServerDb(String db, String uid, String pwd)
        {
            int authType = 1;
            if (radioButton1.Checked) //windows身份验证
            {
                authType = 0;
            }
            else
            {
                authType = 1;
            }
            String connUrl = SqlServerHelper.
                buildConnectionUrl(authType, url.Text, db, uid, pwd);
            bool isConnected = SqlServerHelper.connect(connUrl);
            return isConnected;
        }

        private void RefreshSqlServerDb(String uid, String pwd)
        {
            bool isconnected = this.connectSqlServerDb("master", uid, pwd);
            if (isconnected)
            {
                comboBox1.DataSource = SqlServerHelper.getTable();
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "name";
            }
            else
            {
                conn_info.Text = "连接失败……";
            }
        }

        private void RefreshMysql(String uid, String pwd)
        {
            /* string mysqlConnStr = "server=" + url.Text + ";user id=" + uid + ";password=" + pwd + ";database=information_schema"; //根据自己的设置
             Helpers.MySqlHelper mysqlHelper = new Helpers.MySqlHelper(mysqlConnStr);

             String sql = "select schema_name name from SCHEMATA ";
             MySqlParameter[] parms = new MySqlParameter[] { };
             DataTable dt = mysqlHelper.ExecuteDataTable(sql, parms);
             comboBox1.DataSource = dt;
             comboBox1.DisplayMember = "name";
             comboBox1.ValueMember = "name";*/
        }

        private void RefreshOracle(String uid, String pwd)
        {
            String ConnectionString = //"Data Source="+ url.Text+";user Id="+uid+";Password="+pwd;

            //"Data Source = (DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =" + url.Text + ")(PORT = "+port.Text+")))(CONNECT_DATA =(SERVICE_NAME = "+comboBox1.Text+" )));User ID = "+uid+" ;Password=  "+pwd;  //这里改为copy下的服务名和用户名和密码

            "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = " + url.Text + ")(PORT = " + port.Text + "))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = " + comboBox1.Text + ")));User ID = " + uid + " ;Password=  " + pwd;

            OracleHelper.connstr = ConnectionString; //oh = new OracleHelper(ConnectionString);
            try
            {
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


                /*
                String sql = "SELECT TABLE_NAME FROM USER_TABLES";

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Xml.XmlTextWriter xw = new System.Xml.XmlTextWriter(sw);
                OracleDataAdapter ada = new OracleDataAdapter();
                //当前数据库所有的用户表
                DataSet dt1 = OracleHelper.GetDataSet(sql);
                DataTable dt = OracleHelper.GetDataTable(sql);
                dt.WriteXml(xw);

                ada.Fill(dt1);
                dt1.Tables[0].Columns["TABLE_NAME"].ColumnName = "TABLE";
                this.dataGridView1.DataSource = dt1.Tables[0].DefaultView;

                string s = sw.ToString();
                conn_info.Text = "连接成功……\n所有的用户表展示如下：\n" + s; 
                */



                /*//1,连接数据库
                OracleConnection con1 = new OracleConnection("Data Source=orcl;User id=scott; Password=system;");//oracleconnection类隶属于命名空间system.data.oracleclient
                                                                                                                 //2,连接数据库成功，生成执行sql脚本
                OracleCommand oc1 = new OracleCommand("select deptno,dname,loc from dept", con1);
                //OracleDataReader dr = oc1.ExecuteReader();
                //3,生成存放sql运行结果的容器
                DataSet ds1 = new DataSet();
                //4,用适配器把上述sql脚本填充到上述容器中
                OracleDataAdapter da1 = new OracleDataAdapter();
                //5,通过适配器的属性把上述命令sql脚本与适配器关联，即让适配器执行上述sql
                da1.SelectCommand = oc1;
                //6,通过适配器的fill方法向空器填充数据
                da1.Fill(ds1);
                //通过展示数据列表的datasource属性与上述已填充数据的空器进行关联．记得空器可能包含多个表，要用dataset.tables[0],仅提取一个表
                this.dataGridView1.DataSource = ds1.Tables[0];*/

            }
            catch (System.Exception ex)
            {
                conn_info.Text = "连接失败……";
                Console.WriteLine(ex.ToString());
            }

        }

        /*private bool connectMySql(String db, String uid, String pwd)
        {
            string mysqlConnStr = "server=" + url.Text + ";user id=" + uid + ";password=" + pwd + ";database=" + db; //根据自己的设置

            MySqlConnection connection = new MySqlConnection(mysqlConnStr);
            try
            {
                connection.Open();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
         }*/

        private void button2_Click(object sender, EventArgs e)
        {
            String uid = username.Text;
            String pwd = password.Text;
            //刷新数据库连接
            this.RefreshOracle(uid, pwd);
            //
            AddTree(0, (TreeNode)null);    //0就是处于最高级其f_front=0，数据库里1为顶层那就是1，这个随便

        }




        private bool validDbUser(String uid, String pwd)
        {
            if (uid == null || uid == "")
            {
                MessageBox.Show("用户名必须要输入!!!");//只显示一个“确定”按钮。
                return false;
            }
            if (pwd == null || pwd == "")
            {
                MessageBox.Show("密码必须要输入!!!");//只显示一个“确定”按钮。
                return false;
            }
            return true;
        }
        //导出excel
        private void button4_Click(object sender, EventArgs e)
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


        public void AddTree(int ParentID, TreeNode pNode)
        {

        }


         private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Right)
             {
                 DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);
                 
                 if (info.RowIndex >= 0)
                 {
                     DataGridViewRow dr = (DataGridViewRow)
                            dataGridView1.Rows[info.RowIndex];
                     if (dr != null)
                         dataGridView1.DoDragDrop(dr, DragDropEffects.Copy);
                 }
             }
         }
 
         private void treeView1_DragEnter(object sender, DragEventArgs e)
         {
             e.Effect = DragDropEffects.Copy;
         }
 
         private void treeView1_DragDrop(object sender, DragEventArgs e)
         {
             if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
             {                
                 Point p = treeView1.PointToClient(new Point(e.X, e.Y));
                 TreeViewHitTestInfo index = treeView1.HitTest(p);
 
                 if (index.Node != null)
                 {
 
                     DataGridViewRow drv = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                     index.Node.Text = "Drop: " + drv.Cells[0].ToString();
              
                 }
             }
         } 


    }


}

