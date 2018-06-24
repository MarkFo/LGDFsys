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
    public partial class 领料确认 : Form
    {
        public 领料确认()
        {
            InitializeComponent();
        }

        private void 领料确认_Load(object sender, EventArgs e)
        {
            //首先显示所有数据
            button1_Click(sender, e);
        }

        private void 领料确认_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dataGridView1.Visible = true;
            //button9.Visible = true;

            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [产品序列号],[前管板],[后管板],[回烟室前管板],[回烟室后管板],[锥形炉胆],[异形封头],[锅壳（Ⅰ）],[锅壳（Ⅱ）],[锅壳（Ⅲ）],[炉胆], " +
                         "[波型炉胆(Ⅰ)],[波型炉胆(Ⅱ)],[直炉胆],[回烟室直段],[后孔圈],[角撑板],[人孔圈],[人孔盖板],[人孔补强圈],[主汽阀补强圈],[安全阀补强圈],[进水补强圈], " +
                         "[手孔补强圈],[直拉杆],[手孔],[主汽管],[长烟管],[短烟管_螺纹],[弯烟管],[排污管],[拉撑管],[长拉杆]," +
                         "[电焊条牌号A],[电焊条批号A1],[电焊条批号A2],[电焊条批号A3],[电焊条批号A4], " +
                         "[电焊条牌号B],[电焊条批号B1],[电焊条批号B2],[电焊条批号B3],[电焊条批号B4], " +
                         "[焊丝牌号A],[焊丝批号A1],[焊丝批号A2],[焊丝批号A3],[焊丝批号A4], " +
                         "[焊丝牌号B],[焊丝批号B1],[焊丝批号B2],[焊丝批号B3],[焊丝批号B4], " +
                         "[焊丝牌号C],[焊丝批号C1],[焊丝批号C2],[焊丝批号C3],[焊丝批号C4], " +
                         "[焊丝牌号D],[焊丝批号D1],[焊丝批号D2],[焊丝批号D3],[焊丝批号D4], " +
                         "[焊剂牌号A],[焊剂批号A1],[焊剂批号A2],[焊剂批号A3],[焊剂批号A4], " +
                         "[焊剂牌号B],[焊剂批号B1],[焊剂批号B2],[焊剂批号B3],[焊剂批号B4] from BNconfirmation  " +
                         "WHERE rtrim(ltrim([产品序列号])) like '%" + textBox49.Text + "%'  ";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["产品序列号"].ColumnName = "产品序列号";
            ds.Tables[0].Columns["前管板"].ColumnName = "前管板";
            ds.Tables[0].Columns["后管板"].ColumnName = "后管板";
            ds.Tables[0].Columns["回烟室前管板"].ColumnName = "回烟室前管板";
            ds.Tables[0].Columns["回烟室后管板"].ColumnName = "回烟室后管板";
            ds.Tables[0].Columns["锥形炉胆"].ColumnName = "锥形炉胆";
            ds.Tables[0].Columns["异形封头"].ColumnName = "异形封头";
            ds.Tables[0].Columns["锅壳（Ⅰ）"].ColumnName = "锅壳（Ⅰ）";
            ds.Tables[0].Columns["锅壳（Ⅱ）"].ColumnName = "锅壳（Ⅱ）";
            ds.Tables[0].Columns["锅壳（Ⅲ）"].ColumnName = "锅壳（Ⅲ）";
            ds.Tables[0].Columns["炉胆"].ColumnName = "炉胆";
            ds.Tables[0].Columns["波型炉胆(Ⅰ)"].ColumnName = "波型炉胆(Ⅰ)";
            ds.Tables[0].Columns["波型炉胆(Ⅱ)"].ColumnName = "波型炉胆(Ⅱ)";
            ds.Tables[0].Columns["直炉胆"].ColumnName = "直炉胆";
            ds.Tables[0].Columns["回烟室直段"].ColumnName = "回烟室直段";
            ds.Tables[0].Columns["后孔圈"].ColumnName = "后孔圈";
            ds.Tables[0].Columns["角撑板"].ColumnName = "角撑板";
            ds.Tables[0].Columns["人孔圈"].ColumnName = "人孔圈";
            ds.Tables[0].Columns["人孔盖板"].ColumnName = "人孔盖板";
            ds.Tables[0].Columns["人孔补强圈"].ColumnName = "人孔补强圈";
            ds.Tables[0].Columns["主汽阀补强圈"].ColumnName = "主汽阀补强圈";
            ds.Tables[0].Columns["安全阀补强圈"].ColumnName = "安全阀补强圈";
            ds.Tables[0].Columns["进水补强圈"].ColumnName = "进水补强圈";
            ds.Tables[0].Columns["手孔补强圈"].ColumnName = "手孔补强圈";
            ds.Tables[0].Columns["直拉杆"].ColumnName = "直拉杆";
            ds.Tables[0].Columns["手孔"].ColumnName = "手孔";
            ds.Tables[0].Columns["主汽管"].ColumnName = "主汽管";
            ds.Tables[0].Columns["长烟管"].ColumnName = "长烟管";
            ds.Tables[0].Columns["短烟管_螺纹"].ColumnName = "短烟管_螺纹";
            ds.Tables[0].Columns["弯烟管"].ColumnName = "弯烟管";
            ds.Tables[0].Columns["排污管"].ColumnName = "排污管";
            ds.Tables[0].Columns["拉撑管"].ColumnName = "拉撑管";
            ds.Tables[0].Columns["长拉杆"].ColumnName = "长拉杆";
            ds.Tables[0].Columns["电焊条牌号A"].ColumnName = "电焊条牌号A";
            ds.Tables[0].Columns["电焊条批号A1"].ColumnName = "电焊条批号A1";
            ds.Tables[0].Columns["电焊条批号A2"].ColumnName = "电焊条批号A2";
            ds.Tables[0].Columns["电焊条批号A3"].ColumnName = "电焊条批号A3";
            ds.Tables[0].Columns["电焊条批号A4"].ColumnName = "电焊条批号A4";

            ds.Tables[0].Columns["电焊条牌号B"].ColumnName = "电焊条牌号B";
            ds.Tables[0].Columns["电焊条批号B1"].ColumnName = "电焊条批号B1";
            ds.Tables[0].Columns["电焊条批号B2"].ColumnName = "电焊条批号B2";
            ds.Tables[0].Columns["电焊条批号B3"].ColumnName = "电焊条批号B3";
            ds.Tables[0].Columns["电焊条批号B4"].ColumnName = "电焊条批号B4";

            ds.Tables[0].Columns["焊丝牌号A"].ColumnName = "焊丝牌号A";
            ds.Tables[0].Columns["焊丝批号A1"].ColumnName = "焊丝批号A1";
            ds.Tables[0].Columns["焊丝批号A2"].ColumnName = "焊丝批号A2";
            ds.Tables[0].Columns["焊丝批号A3"].ColumnName = "焊丝批号A3";
            ds.Tables[0].Columns["焊丝批号A4"].ColumnName = "焊丝批号A4";

            ds.Tables[0].Columns["焊丝牌号B"].ColumnName = "焊丝牌号B";
            ds.Tables[0].Columns["焊丝批号B1"].ColumnName = "焊丝批号B1";
            ds.Tables[0].Columns["焊丝批号B2"].ColumnName = "焊丝批号B2";
            ds.Tables[0].Columns["焊丝批号B3"].ColumnName = "焊丝批号B3";
            ds.Tables[0].Columns["焊丝批号B4"].ColumnName = "焊丝批号B4";

            ds.Tables[0].Columns["焊丝牌号C"].ColumnName = "焊丝牌号C";
            ds.Tables[0].Columns["焊丝批号C1"].ColumnName = "焊丝批号C1";
            ds.Tables[0].Columns["焊丝批号C2"].ColumnName = "焊丝批号C2";
            ds.Tables[0].Columns["焊丝批号C3"].ColumnName = "焊丝批号C3";
            ds.Tables[0].Columns["焊丝批号C4"].ColumnName = "焊丝批号C4";

            ds.Tables[0].Columns["焊丝牌号D"].ColumnName = "焊丝牌号D";
            ds.Tables[0].Columns["焊丝批号D1"].ColumnName = "焊丝批号D1";
            ds.Tables[0].Columns["焊丝批号D2"].ColumnName = "焊丝批号D2";
            ds.Tables[0].Columns["焊丝批号D3"].ColumnName = "焊丝批号D3";
            ds.Tables[0].Columns["焊丝批号D4"].ColumnName = "焊丝批号D4";

            ds.Tables[0].Columns["焊剂牌号A"].ColumnName = "焊剂牌号A";
            ds.Tables[0].Columns["焊剂批号A1"].ColumnName = "焊剂批号A1";
            ds.Tables[0].Columns["焊剂批号A2"].ColumnName = "焊剂批号A2";
            ds.Tables[0].Columns["焊剂批号A3"].ColumnName = "焊剂批号A3";
            ds.Tables[0].Columns["焊剂批号A4"].ColumnName = "焊剂批号A4";

            ds.Tables[0].Columns["焊剂牌号B"].ColumnName = "焊剂牌号B";
            ds.Tables[0].Columns["焊剂批号B1"].ColumnName = "焊剂批号B1";
            ds.Tables[0].Columns["焊剂批号B2"].ColumnName = "焊剂批号B2";
            ds.Tables[0].Columns["焊剂批号B3"].ColumnName = "焊剂批号B3";
            ds.Tables[0].Columns["焊剂批号B4"].ColumnName = "焊剂批号B4";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }



        //点击dataGridView1，传送到所有textbox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox49.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedCells[6].Value.ToString();
            textBox7.Text = dataGridView1.SelectedCells[7].Value.ToString();
            textBox8.Text = dataGridView1.SelectedCells[8].Value.ToString();
            textBox9.Text = dataGridView1.SelectedCells[9].Value.ToString();
            textBox10.Text = dataGridView1.SelectedCells[10].Value.ToString();
            textBox11.Text = dataGridView1.SelectedCells[11].Value.ToString();
            textBox12.Text = dataGridView1.SelectedCells[12].Value.ToString();
            textBox13.Text = dataGridView1.SelectedCells[13].Value.ToString();
            textBox14.Text = dataGridView1.SelectedCells[14].Value.ToString();
            textBox15.Text = dataGridView1.SelectedCells[15].Value.ToString();
            textBox16.Text = dataGridView1.SelectedCells[16].Value.ToString();
            textBox17.Text = dataGridView1.SelectedCells[17].Value.ToString();
            textBox18.Text = dataGridView1.SelectedCells[18].Value.ToString();
            textBox19.Text = dataGridView1.SelectedCells[19].Value.ToString();
            textBox20.Text = dataGridView1.SelectedCells[20].Value.ToString();
            textBox21.Text = dataGridView1.SelectedCells[21].Value.ToString();
            textBox22.Text = dataGridView1.SelectedCells[22].Value.ToString();
            textBox23.Text = dataGridView1.SelectedCells[23].Value.ToString();
            textBox24.Text = dataGridView1.SelectedCells[24].Value.ToString();
            textBox25.Text = dataGridView1.SelectedCells[25].Value.ToString();
            textBox26.Text = dataGridView1.SelectedCells[26].Value.ToString();
            textBox27.Text = dataGridView1.SelectedCells[27].Value.ToString();
            textBox28.Text = dataGridView1.SelectedCells[28].Value.ToString();
            textBox29.Text = dataGridView1.SelectedCells[29].Value.ToString();
            textBox30.Text = dataGridView1.SelectedCells[30].Value.ToString();
            textBox31.Text = dataGridView1.SelectedCells[31].Value.ToString();
            textBox32.Text = dataGridView1.SelectedCells[32].Value.ToString();
            //电焊条，这里要注意！【查询】代码是按顺序重排后的，所以这里的顺序【选中填值】也不按数据库表的顺序排列了！
            comboBox1.Text = dataGridView1.SelectedCells[33].Value.ToString();
            textBox33.Text = dataGridView1.SelectedCells[34].Value.ToString();
            textBox34.Text = dataGridView1.SelectedCells[35].Value.ToString();
            textBox50.Text = dataGridView1.SelectedCells[36].Value.ToString();
            textBox51.Text = dataGridView1.SelectedCells[37].Value.ToString();

            comboBox2.Text = dataGridView1.SelectedCells[38].Value.ToString();
            textBox35.Text = dataGridView1.SelectedCells[39].Value.ToString();
            textBox36.Text = dataGridView1.SelectedCells[40].Value.ToString();
            textBox52.Text = dataGridView1.SelectedCells[41].Value.ToString();
            textBox53.Text = dataGridView1.SelectedCells[42].Value.ToString();

            //焊丝
            comboBox3.Text = dataGridView1.SelectedCells[43].Value.ToString();
            textBox37.Text = dataGridView1.SelectedCells[44].Value.ToString();
            textBox38.Text = dataGridView1.SelectedCells[45].Value.ToString();
            textBox54.Text = dataGridView1.SelectedCells[46].Value.ToString();
            textBox55.Text = dataGridView1.SelectedCells[47].Value.ToString();

            comboBox4.Text = dataGridView1.SelectedCells[48].Value.ToString();
            textBox39.Text = dataGridView1.SelectedCells[49].Value.ToString();
            textBox40.Text = dataGridView1.SelectedCells[50].Value.ToString();
            textBox56.Text = dataGridView1.SelectedCells[51].Value.ToString();
            textBox57.Text = dataGridView1.SelectedCells[52].Value.ToString();

            comboBox5.Text = dataGridView1.SelectedCells[53].Value.ToString();
            textBox41.Text = dataGridView1.SelectedCells[54].Value.ToString();
            textBox42.Text = dataGridView1.SelectedCells[55].Value.ToString();
            textBox58.Text = dataGridView1.SelectedCells[56].Value.ToString();
            textBox59.Text = dataGridView1.SelectedCells[57].Value.ToString();

            comboBox6.Text = dataGridView1.SelectedCells[58].Value.ToString();
            textBox43.Text = dataGridView1.SelectedCells[59].Value.ToString();
            textBox44.Text = dataGridView1.SelectedCells[60].Value.ToString();
            textBox60.Text = dataGridView1.SelectedCells[61].Value.ToString();
            textBox61.Text = dataGridView1.SelectedCells[62].Value.ToString();
            //焊剂
            comboBox7.Text = dataGridView1.SelectedCells[63].Value.ToString();
            textBox45.Text = dataGridView1.SelectedCells[64].Value.ToString();
            textBox46.Text = dataGridView1.SelectedCells[65].Value.ToString();
            textBox62.Text = dataGridView1.SelectedCells[66].Value.ToString();
            textBox63.Text = dataGridView1.SelectedCells[67].Value.ToString();

            comboBox8.Text = dataGridView1.SelectedCells[68].Value.ToString();
            textBox47.Text = dataGridView1.SelectedCells[69].Value.ToString();
            textBox48.Text = dataGridView1.SelectedCells[70].Value.ToString();
            textBox64.Text = dataGridView1.SelectedCells[71].Value.ToString();
            textBox65.Text = dataGridView1.SelectedCells[72].Value.ToString();

            //comboBox1.Text = dataGridView1.SelectedCells[1].Value.ToString();
            //label5.Text = DateTime.Now.ToString("yyyyMMdd");
            //label8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //label3.Text = dataGridView1.SelectedCells[0].Value.ToString();

        }



        //新增，inert
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection("Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select Count(*) from BNconfirmation where [产品序列号] = '" + textBox49.Text + "'  "), con1);

            if (textBox49.Text.Length == 0)
            {
                MessageBox.Show("请补充必要信息！");
            }
            else if ((int)cmd1.ExecuteScalar() > 0)
            {
                MessageBox.Show("已存在相同产品编号，请搜索→选中后修改", "提示");
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
                string myadd = "insert into [BNconfirmation] (   [产品序列号],[前管板],[后管板],[回烟室前管板],[回烟室后管板],[锥形炉胆],[异形封头],[锅壳（Ⅰ）],[锅壳（Ⅱ）],[锅壳（Ⅲ）],[炉胆], " +
                         "[波型炉胆(Ⅰ)],[波型炉胆(Ⅱ)],[直炉胆],[回烟室直段],[后孔圈],[角撑板],[人孔圈],[人孔盖板],[人孔补强圈],[主汽阀补强圈],[安全阀补强圈],[进水补强圈], " +
                         "[手孔补强圈],[直拉杆],[手孔],[主汽管],[长烟管],[短烟管_螺纹],[弯烟管],[排污管],[拉撑管],[长拉杆], " +
                         "[电焊条牌号A],[电焊条批号A1],[电焊条批号A2],[电焊条批号A3],[电焊条批号A4], " +
                         "[电焊条牌号B],[电焊条批号B1],[电焊条批号B2],[电焊条批号B3],[电焊条批号B4], " +
                         "[焊丝牌号A],[焊丝批号A1],[焊丝批号A2],[焊丝批号A3],[焊丝批号A4], " +
                         "[焊丝牌号B],[焊丝批号B1],[焊丝批号B2],[焊丝批号B3],[焊丝批号B4], " +
                         "[焊丝牌号C],[焊丝批号C1],[焊丝批号C2],[焊丝批号C3],[焊丝批号C4], " +
                         "[焊丝牌号D],[焊丝批号D1],[焊丝批号D2],[焊丝批号D3],[焊丝批号D4], " +
                         "[焊剂牌号A],[焊剂批号A1],[焊剂批号A2],[焊剂批号A3],[焊剂批号A4], " +
                         "[焊剂牌号B],[焊剂批号B1],[焊剂批号B2],[焊剂批号B3],[焊剂批号B4]   ) values ('" + textBox49.Text + "','" + textBox1.Text + "', " +
                         "'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "', " +
                         "'" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox15.Text + "', " +
                         "'" + textBox16.Text + "','" + textBox17.Text + "','" + textBox18.Text + "','" + textBox19.Text + "','" + textBox20.Text + "','" + textBox21.Text + "','" + textBox22.Text + "', " +
                         "'" + textBox23.Text + "','" + textBox24.Text + "','" + textBox25.Text + "','" + textBox26.Text + "','" + textBox27.Text + "','" + textBox28.Text + "','" + textBox29.Text + "', " +
                         "'" + textBox30.Text + "','" + textBox31.Text + "','" + textBox32.Text + "', " +

                         "'" + comboBox1.Text + "','" + textBox33.Text + "','" + textBox34.Text + "','" + textBox50.Text + "','" + textBox51.Text + "', " +
                         "'" + comboBox2.Text + "','" + textBox35.Text + "','" + textBox36.Text + "','" + textBox52.Text + "','" + textBox53.Text + "', " +
                         "'" + comboBox3.Text + "','" + textBox37.Text + "','" + textBox38.Text + "','" + textBox54.Text + "','" + textBox55.Text + "', " +
                         "'" + comboBox4.Text + "','" + textBox39.Text + "','" + textBox40.Text + "','" + textBox56.Text + "','" + textBox57.Text + "', " +
                         "'" + comboBox5.Text + "','" + textBox41.Text + "','" + textBox42.Text + "','" + textBox58.Text + "','" + textBox59.Text + "', " +
                         "'" + comboBox6.Text + "','" + textBox43.Text + "','" + textBox44.Text + "','" + textBox60.Text + "','" + textBox61.Text + "', " +
                         "'" + comboBox7.Text + "','" + textBox45.Text + "','" + textBox46.Text + "','" + textBox62.Text + "','" + textBox63.Text + "', " +
                         "'" + comboBox8.Text + "','" + textBox47.Text + "','" + textBox48.Text + "','" + textBox64.Text + "','" + textBox65.Text + "'   )";

                SqlCommand cmd = new SqlCommand(myadd, con);


                cmd.ExecuteNonQuery();

                MessageBox.Show("新增完成！");

                //重新执行dgrid1查询
                button1_Click(sender, e);
            }

            //Showdata();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";

            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            textBox29.Text = "";
            textBox30.Text = "";

            textBox31.Text = "";
            textBox32.Text = "";
            textBox33.Text = "";
            textBox34.Text = "";
            textBox35.Text = "";
            textBox36.Text = "";
            textBox37.Text = "";
            textBox38.Text = "";
            textBox39.Text = "";
            textBox40.Text = "";

            textBox41.Text = "";
            textBox42.Text = "";
            textBox43.Text = "";
            textBox44.Text = "";
            textBox45.Text = "";
            textBox46.Text = "";
            textBox47.Text = "";
            textBox48.Text = "";

            textBox50.Text = "";
            textBox51.Text = "";
            textBox52.Text = "";
            textBox53.Text = "";
            textBox54.Text = "";
            textBox55.Text = "";
            textBox56.Text = "";
            textBox57.Text = "";
            textBox58.Text = "";
            textBox59.Text = "";
            textBox60.Text = "";
            textBox61.Text = "";
            textBox62.Text = "";
            textBox63.Text = "";
            textBox64.Text = "";
            textBox65.Text = "";

            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";

        }
        //update事件
        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox49.Text.Length == 0)
            {
                MessageBox.Show("请选择正确信息！");
            }
            //判断是否选择更改
            else if (MessageBox.Show("确认修改吗", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "update [BNconfirmation] set [前管板]='" + textBox1.Text + "',[后管板]='" + textBox2.Text + "',[回烟室前管板]='" + textBox3.Text + "'  " +
                                  ",[回烟室后管板] = '" + textBox4.Text + "',[锥形炉胆] = '" + textBox5.Text + "',[异形封头] = '" + textBox6.Text + "'  " +
                                  ",[锅壳（Ⅰ）] = '" + textBox7.Text + "',[锅壳（Ⅱ）] = '" + textBox8.Text + "',[锅壳（Ⅲ）] = '" + textBox9.Text + "'  " +
                                  ",[炉胆] = '" + textBox10.Text + "',[波型炉胆(Ⅰ)] = '" + textBox11.Text + "',[波型炉胆(Ⅱ)] = '" + textBox12.Text + "'  " +
                                  ",[直炉胆] = '" + textBox13.Text + "',[回烟室直段] = '" + textBox14.Text + "',[后孔圈] = '" + textBox15.Text + "'  " +
                                  ",[角撑板] = '" + textBox16.Text + "',[人孔圈] = '" + textBox17.Text + "',[人孔盖板] = '" + textBox18.Text + "'  " +
                                  ",[人孔补强圈] = '" + textBox19.Text + "',[主汽阀补强圈] = '" + textBox20.Text + "',[安全阀补强圈] = '" + textBox21.Text + "'  " +
                                  ",[进水补强圈] = '" + textBox22.Text + "',[手孔补强圈] = '" + textBox23.Text + "',[直拉杆] = '" + textBox24.Text + "'  " +
                                  ",[手孔] = '" + textBox25.Text + "',[主汽管] = '" + textBox26.Text + "',[长烟管] = '" + textBox27.Text + "'  " +
                                  ",[短烟管_螺纹] = '" + textBox28.Text + "',[弯烟管] = '" + textBox29.Text + "',[排污管] = '" + textBox30.Text + "'  " +
                                  ",[拉撑管] = '" + textBox31.Text + "',[长拉杆] = '" + textBox32.Text + "', " +

                                  "[电焊条牌号A] = '" + comboBox1.Text + "',[电焊条批号A1] = '" + textBox33.Text + "',[电焊条批号A2] = '" + textBox34.Text + "',[电焊条批号A3] = '" + textBox50.Text + "',[电焊条批号A4] = '" + textBox51.Text + "', " +
                                  "[电焊条牌号B] = '" + comboBox2.Text + "',[电焊条批号B1] = '" + textBox35.Text + "',[电焊条批号B2] = '" + textBox36.Text + "',[电焊条批号B3] = '" + textBox52.Text + "',[电焊条批号B4] = '" + textBox53.Text + "', " +
                                  "[焊丝牌号A] = '" + comboBox3.Text + "',[焊丝批号A1] = '" + textBox37.Text + "',[焊丝批号A2] = '" + textBox38.Text + "',[焊丝批号A3] = '" + textBox54.Text + "',[焊丝批号A4] = '" + textBox55.Text + "', " +
                                  "[焊丝牌号B] = '" + comboBox4.Text + "',[焊丝批号B1] = '" + textBox39.Text + "',[焊丝批号B2] = '" + textBox40.Text + "',[焊丝批号B3] = '" + textBox56.Text + "',[焊丝批号B4] = '" + textBox57.Text + "', " +
                                  "[焊丝牌号C] = '" + comboBox5.Text + "',[焊丝批号C1] = '" + textBox41.Text + "',[焊丝批号C2] = '" + textBox42.Text + "',[焊丝批号C3] = '" + textBox58.Text + "',[焊丝批号C4] = '" + textBox59.Text + "', " +
                                  "[焊丝牌号D] = '" + comboBox6.Text + "',[焊丝批号D1] = '" + textBox43.Text + "',[焊丝批号D2] = '" + textBox44.Text + "',[焊丝批号D3] = '" + textBox60.Text + "',[焊丝批号D4] = '" + textBox61.Text + "', " +
                                  "[焊剂牌号A] = '" + comboBox7.Text + "',[焊剂批号A1] = '" + textBox45.Text + "',[焊剂批号A2] = '" + textBox46.Text + "',[焊剂批号A3] = '" + textBox62.Text + "',[焊剂批号A4] = '" + textBox63.Text + "', " +
                                  "[焊剂牌号B] = '" + comboBox8.Text + "',[焊剂批号B1] = '" + textBox47.Text + "',[焊剂批号B2] = '" + textBox48.Text + "',[焊剂批号B3] = '" + textBox64.Text + "',[焊剂批号B4] = '" + textBox65.Text + "'  " +
                                  "where [产品序列号]='" + textBox49.Text + "' ";

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                SqlDataAdapter ada1 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada1.Fill(ds);
                MessageBox.Show("修改完成！");
            }

            
            else
            {
                //显示所有数据
                button1_Click(sender, e);
            }
            

            //Showdata();
            //textBox1.Text = "";

            //显示所有数据
            button1_Click(sender, e);

        }
        //删除事件
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
                SqlConnection con = new SqlConnection(coct);
                con.Open();
                string myupdate = "delete from [BNconfirmation] where [产品序列号]='" + textBox49.Text + "' ";

                SqlCommand cmd = new SqlCommand(myupdate, con);

                cmd.ExecuteNonQuery();

                MessageBox.Show("删除完成！");
                //Showdata();
                //显示所有数据
                button5_Click(sender, e);
            }
            else
            {
                //显示所有数据
                button1_Click(sender, e);
            }
        }
        //查询全部
        private void button5_Click(object sender, EventArgs e)
        {
            //dataGridView1.Visible = true;
            //button9.Visible = true;

            string coct = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008";
            SqlConnection con = new SqlConnection(coct);
            con.Open();

            string sql = "select [产品序列号],[前管板],[后管板],[回烟室前管板],[回烟室后管板],[锥形炉胆],[异形封头],[锅壳（Ⅰ）],[锅壳（Ⅱ）],[锅壳（Ⅲ）],[炉胆], " +
                         "[波型炉胆(Ⅰ)],[波型炉胆(Ⅱ)],[直炉胆],[回烟室直段],[后孔圈],[角撑板],[人孔圈],[人孔盖板],[人孔补强圈],[主汽阀补强圈],[安全阀补强圈],[进水补强圈], " +
                         "[手孔补强圈],[直拉杆],[手孔],[主汽管],[长烟管],[短烟管_螺纹],[弯烟管],[排污管],[拉撑管],[长拉杆], " +
                         "[电焊条牌号A],[电焊条批号A1],[电焊条批号A2],[电焊条批号A3],[电焊条批号A4], " +
                         "[电焊条牌号B],[电焊条批号B1],[电焊条批号B2],[电焊条批号B3],[电焊条批号B4], " +
                         "[焊丝牌号A],[焊丝批号A1],[焊丝批号A2],[焊丝批号A3],[焊丝批号A4], " +
                         "[焊丝牌号B],[焊丝批号B1],[焊丝批号B2],[焊丝批号B3],[焊丝批号B4], " +
                         "[焊丝牌号C],[焊丝批号C1],[焊丝批号C2],[焊丝批号C3],[焊丝批号C4], " +
                         "[焊丝牌号D],[焊丝批号D1],[焊丝批号D2],[焊丝批号D3],[焊丝批号D4], " +
                         "[焊剂牌号A],[焊剂批号A1],[焊剂批号A2],[焊剂批号A3],[焊剂批号A4], " +
                         "[焊剂牌号B],[焊剂批号B1],[焊剂批号B2],[焊剂批号B3],[焊剂批号B4] from BNconfirmation  ";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            ds.Tables[0].Columns["产品序列号"].ColumnName = "产品序列号";
            ds.Tables[0].Columns["前管板"].ColumnName = "前管板";
            ds.Tables[0].Columns["后管板"].ColumnName = "后管板";
            ds.Tables[0].Columns["回烟室前管板"].ColumnName = "回烟室前管板";
            ds.Tables[0].Columns["回烟室后管板"].ColumnName = "回烟室后管板";
            ds.Tables[0].Columns["锥形炉胆"].ColumnName = "锥形炉胆";
            ds.Tables[0].Columns["异形封头"].ColumnName = "异形封头";
            ds.Tables[0].Columns["锅壳（Ⅰ）"].ColumnName = "锅壳（Ⅰ）";
            ds.Tables[0].Columns["锅壳（Ⅱ）"].ColumnName = "锅壳（Ⅱ）";
            ds.Tables[0].Columns["锅壳（Ⅲ）"].ColumnName = "锅壳（Ⅲ）";
            ds.Tables[0].Columns["炉胆"].ColumnName = "炉胆";
            ds.Tables[0].Columns["波型炉胆(Ⅰ)"].ColumnName = "波型炉胆(Ⅰ)";
            ds.Tables[0].Columns["波型炉胆(Ⅱ)"].ColumnName = "波型炉胆(Ⅱ)";
            ds.Tables[0].Columns["直炉胆"].ColumnName = "直炉胆";
            ds.Tables[0].Columns["回烟室直段"].ColumnName = "回烟室直段";
            ds.Tables[0].Columns["后孔圈"].ColumnName = "后孔圈";
            ds.Tables[0].Columns["角撑板"].ColumnName = "角撑板";
            ds.Tables[0].Columns["人孔圈"].ColumnName = "人孔圈";
            ds.Tables[0].Columns["人孔盖板"].ColumnName = "人孔盖板";
            ds.Tables[0].Columns["人孔补强圈"].ColumnName = "人孔补强圈";
            ds.Tables[0].Columns["主汽阀补强圈"].ColumnName = "主汽阀补强圈";
            ds.Tables[0].Columns["安全阀补强圈"].ColumnName = "安全阀补强圈";
            ds.Tables[0].Columns["进水补强圈"].ColumnName = "进水补强圈";
            ds.Tables[0].Columns["手孔补强圈"].ColumnName = "手孔补强圈";
            ds.Tables[0].Columns["直拉杆"].ColumnName = "直拉杆";
            ds.Tables[0].Columns["手孔"].ColumnName = "手孔";
            ds.Tables[0].Columns["主汽管"].ColumnName = "主汽管";
            ds.Tables[0].Columns["长烟管"].ColumnName = "长烟管";
            ds.Tables[0].Columns["短烟管_螺纹"].ColumnName = "短烟管_螺纹";
            ds.Tables[0].Columns["弯烟管"].ColumnName = "弯烟管";
            ds.Tables[0].Columns["排污管"].ColumnName = "排污管";
            ds.Tables[0].Columns["拉撑管"].ColumnName = "拉撑管";
            ds.Tables[0].Columns["长拉杆"].ColumnName = "长拉杆";
            ds.Tables[0].Columns["电焊条牌号A"].ColumnName = "电焊条牌号A";
            ds.Tables[0].Columns["电焊条批号A1"].ColumnName = "电焊条批号A1";
            ds.Tables[0].Columns["电焊条批号A2"].ColumnName = "电焊条批号A2";
            ds.Tables[0].Columns["电焊条批号A3"].ColumnName = "电焊条批号A3";
            ds.Tables[0].Columns["电焊条批号A4"].ColumnName = "电焊条批号A4";

            ds.Tables[0].Columns["电焊条牌号B"].ColumnName = "电焊条牌号B";
            ds.Tables[0].Columns["电焊条批号B1"].ColumnName = "电焊条批号B1";
            ds.Tables[0].Columns["电焊条批号B2"].ColumnName = "电焊条批号B2";
            ds.Tables[0].Columns["电焊条批号B3"].ColumnName = "电焊条批号B3";
            ds.Tables[0].Columns["电焊条批号B4"].ColumnName = "电焊条批号B4";

            ds.Tables[0].Columns["焊丝牌号A"].ColumnName = "焊丝牌号A";
            ds.Tables[0].Columns["焊丝批号A1"].ColumnName = "焊丝批号A1";
            ds.Tables[0].Columns["焊丝批号A2"].ColumnName = "焊丝批号A2";
            ds.Tables[0].Columns["焊丝批号A3"].ColumnName = "焊丝批号A3";
            ds.Tables[0].Columns["焊丝批号A4"].ColumnName = "焊丝批号A4";

            ds.Tables[0].Columns["焊丝牌号B"].ColumnName = "焊丝牌号B";
            ds.Tables[0].Columns["焊丝批号B1"].ColumnName = "焊丝批号B1";
            ds.Tables[0].Columns["焊丝批号B2"].ColumnName = "焊丝批号B2";
            ds.Tables[0].Columns["焊丝批号B3"].ColumnName = "焊丝批号B3";
            ds.Tables[0].Columns["焊丝批号B4"].ColumnName = "焊丝批号B4";


            ds.Tables[0].Columns["焊丝牌号C"].ColumnName = "焊丝牌号C";
            ds.Tables[0].Columns["焊丝批号C1"].ColumnName = "焊丝批号C1";
            ds.Tables[0].Columns["焊丝批号C2"].ColumnName = "焊丝批号C2";
            ds.Tables[0].Columns["焊丝批号C3"].ColumnName = "焊丝批号C3";
            ds.Tables[0].Columns["焊丝批号C4"].ColumnName = "焊丝批号C4";

            ds.Tables[0].Columns["焊丝牌号D"].ColumnName = "焊丝牌号D";
            ds.Tables[0].Columns["焊丝批号D1"].ColumnName = "焊丝批号D1";
            ds.Tables[0].Columns["焊丝批号D2"].ColumnName = "焊丝批号D2";
            ds.Tables[0].Columns["焊丝批号D3"].ColumnName = "焊丝批号D3";
            ds.Tables[0].Columns["焊丝批号D4"].ColumnName = "焊丝批号D4";

            ds.Tables[0].Columns["焊剂牌号A"].ColumnName = "焊剂牌号A";
            ds.Tables[0].Columns["焊剂批号A1"].ColumnName = "焊剂批号A1";
            ds.Tables[0].Columns["焊剂批号A2"].ColumnName = "焊剂批号A2";
            ds.Tables[0].Columns["焊剂批号A3"].ColumnName = "焊剂批号A3";
            ds.Tables[0].Columns["焊剂批号A4"].ColumnName = "焊剂批号A4";

            ds.Tables[0].Columns["焊剂牌号B"].ColumnName = "焊剂牌号B";
            ds.Tables[0].Columns["焊剂批号B1"].ColumnName = "焊剂批号B1";
            ds.Tables[0].Columns["焊剂批号B2"].ColumnName = "焊剂批号B2";
            ds.Tables[0].Columns["焊剂批号B3"].ColumnName = "焊剂批号B3";
            ds.Tables[0].Columns["焊剂批号B4"].ColumnName = "焊剂批号B4";



            this.dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button6_Click(object sender, EventArgs e)
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
