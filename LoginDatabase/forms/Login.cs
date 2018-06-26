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


namespace LGD
{
    public partial class Login : Form
    {
        private int errorTime = 10;

        public Login() {
            InitializeComponent();
  
        }

        private void loginBtn_Click(object sender, EventArgs e) {
            errorTime = errorTime - 1;

            string username = txtName.Text.Trim();  //取出账号
            string pw = txtPwd.Text.Trim();         //取出密码
            string constr = "Data Source=192.168.1.252;Initial Catalog=cs管理;Persist Security Info=True;User ID=sa;Password=WFGLServer2008"; //设置连接字符串
            SqlConnection mycon = new SqlConnection(constr);  //实例化连接对象
            mycon.Open();

            SqlCommand mycom = mycon.CreateCommand();         //创建SQL命令执行对象
            string s1 = "select 用户名,密码 from 登录表 where 用户名='" + username + "' and 密码='" + pw + "'";                                            //编写SQL命令
            mycom.CommandText = s1;                           //执行SQL命令
            SqlDataAdapter myDA = new SqlDataAdapter();       //实例化数据适配器
            myDA.SelectCommand = mycom;                       //让适配器执行SELECT命令
            DataSet myDS = new DataSet();                     //实例化结果数据集
            int n = myDA.Fill(myDS, "register");              //将结果放入数据适配器，返回元祖个数
            if (n != 0) {
                if (checkCode.Text == textCheck.Text) { 
                    MessageBox.Show("欢迎使用！");             //登录成功


                    //登陆成功，进入另一个窗体
                    跳转 跳转 = new 跳转();

                    UserInfo.UserName = txtName.Text.Trim();//记录用户名为全局变量

                    this.Hide();//隐藏当前窗口 
                    跳转.ShowDialog(); //弹出第二个窗口

                    //跳转.ShowDialog(this);//这里一定要用ShowDialog，否则画面程序依旧会结束。
                    this.Close();//关闭Form2后，程序退出


                } else {
                    MessageBox.Show("验证码填写错误");
                    textCheck.Text = "";
                }
            } else
                if (errorTime < 10) {
                    MessageBox.Show("用户名或密码有错。请重新输入！还有" + errorTime.ToString() + "次机会");
                    txtName.Text = "";   //清空账号
                    txtPwd.Text = "";    //清空密码?
                    txtName.Focus();     //光标设置在账号上
                } else {
                    MessageBox.Show("你输入的用户名或密码已达10次? 将退出程序");
                    this.Close();
                }

        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) {
            Register register = new Register();
            register.ShowDialog();
        }

        private void checkCode_Click(object sender, EventArgs e) {
            Random random = new Random();
            int minV = 1234, maxV = 9876;
            checkCode.Text = random.Next(minV, maxV).ToString();
        }


    }
}
