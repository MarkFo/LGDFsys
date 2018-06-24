using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ZXing;
using System.Drawing.Printing;

namespace LGD
{
    public partial class QRcoderes : Form
    {
        public QRcoderes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入要生成的二维码！");
                return;
            }
            GenByZXingNet(textBox1.Text);
        }
        
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">二维码信息</param>
        /// <returns>图片</returns>
        private Bitmap GenByZXingNet(string msg)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//编码问题
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            const int codeSizeInPixels = 250;   //设置图片长宽
            writer.Options.Height = writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;//设置边框
            ZXing.Common.BitMatrix bm = writer.Encode(msg);
            Bitmap img = writer.Write(bm);



            //获取文本
            string texts = this.textBox2.Text;
            //得到Bitmap(传入Rectangle.Empty自动计算宽高)
            Bitmap bmp = TextToBitmap(texts, this.textBox2.Font, Rectangle.Empty, this.textBox2.ForeColor, this.textBox2.BackColor);

            /*//上下合并
            int iWidth = bmp.Width > img.Width ? bmp.Width : img.Width;
            int iHeight = bmp.Height + img.Height;
            Bitmap bitmap = new Bitmap(iWidth, iHeight);

            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(0, bmp.Height, img.Width, img.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            g.Dispose();

            pictureBox1.Image = bitmap;*/


            //上下合并
            int iWidth = img.Width > bmp.Width ? img.Width : bmp.Width;
            int iHeight = img.Height + bmp.Height;
            Bitmap bitmap = new Bitmap(iWidth, iHeight);

            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            g.DrawImage(bmp, new Rectangle(0, img.Height, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();

            pictureBox1.Image = bitmap;

            return img;
            /*
            //用PictureBox显示
            this.pictureBox2.Image = bmp;

            pictureBox1.Image = img;
            
            */

        }

        //保存picturebox图片
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.jpg)|*.jpg";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "导出文件保存路径";
            saveFileDialog.FileName = null;
            saveFileDialog.ShowDialog();
            string strPath = saveFileDialog.FileName;
            Image img = pictureBox1.Image;
            img.Save(strPath);
        }

        private void QRcoderes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//隐藏当前窗口 
            跳转 bs = new 跳转();
            bs.ShowDialog(); //弹出第二个窗口 
            this.Close();//关闭第一个窗口
        }



        /// <summary>
        /// 把文字转换才Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        private Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);
                //让文字条幅的留白跟随文字大小，但是留白太小会使文字外都是黑色，不美观
                //int width = (int)(sizef.Width + 1);
                //int height = (int)(sizef.Height + 1);

                int width = (int)(250);
                int height = (int)(60);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }


            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }


        //合并代码，暂时丢弃
        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(textBox1.Text);
            Bitmap img = new Bitmap(textBox2.Text);

            //上下合并
            int iWidth = bmp.Width > img.Width ? bmp.Width : img.Width;
            int iHeight = bmp.Height + img.Height;
            Bitmap bitmap = new Bitmap(iWidth, iHeight);

            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.DrawImage(img, new Rectangle(0, bmp.Height, img.Width, img.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            g.Dispose();

            pictureBox1.Image = bitmap;

        }
    }
}
