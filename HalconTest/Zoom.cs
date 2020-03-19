using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace zoom
{
    class Zoom
    {

        public static bool isSelected;//鼠标是否点下，是否可以移动图像
        public static HTuple Width, Height;
        public static double zoom = 1;
        public static HTuple mousex = 0, mousey = 0;//鼠标点下时的xy像素坐标
        public static void mousewheel(HTuple ID, HObject img, HMouseEventArgs e)
        {
            try
            {
                HTuple row1, col1, row2, col2;
                HTuple row3, col3, button3;
                HOperatorSet.GetImageSize(img, out Width, out Height);
                HOperatorSet.GetPart(ID, out row1, out col1, out row2, out col2);
                HOperatorSet.ClearWindow(ID);
                int xx = Width;
                int yy = Height;
                double div = Convert.ToDouble(yy) / Convert.ToDouble(xx);
                HOperatorSet.GetMposition(ID, out row3, out col3, out button3);
                if (e.Delta > 0)
                {
                    zoom = 1 + 0.001 * e.Delta;
                }
                if (e.Delta < 0)
                {
                    zoom = Math.Max(0.01F, (1 + 0.001 * e.Delta));
                }
                HTuple aaa = (col2 - col1) / zoom;
                HTuple bbb = aaa * div;
                double biliX = (col3 - col1) / (col2 - col1);//左右显示比例
                double biliY = (row3 - row1) / (row2 - row1);//上下显示比例
                //double biliY = Height / Width * biliX;

                HOperatorSet.SetPart(ID, (row3 - bbb * biliY), (col3 - aaa * biliX), (row3 + bbb * (1 - biliY)), (col3 + aaa * (1 - biliX)));
                HOperatorSet.DispObj(img, ID);
            }
            catch(Exception EX)
            {}
        }
        public static void mousewheel(HTuple ID, HObject img,HObject hb_region,HObject hb_XLD, HMouseEventArgs e)
        {
            try
            {
                HTuple row1, col1, row2, col2;
                HTuple row3, col3, button3;
                HTuple objcounttmp;
                HOperatorSet.GetImageSize(img, out Width, out Height);
                HOperatorSet.GetPart(ID, out row1, out col1, out row2, out col2);
                HOperatorSet.ClearWindow(ID);
                int xx = Width;
                int yy = Height;
                double div = Convert.ToDouble(yy) / Convert.ToDouble(xx);
                HOperatorSet.GetMposition(ID, out row3, out col3, out button3);
                if (e.Delta > 0)
                {
                    zoom = 1 + 0.001 * e.Delta;
                }
                if (e.Delta < 0)
                {
                    zoom = Math.Max(0.01F, (1 + 0.001 * e.Delta));
                }
                HTuple aaa = (col2 - col1) / zoom;
                HTuple bbb = aaa * div;
                double biliX = (col3 - col1) / (col2 - col1);//左右显示比例
                double biliY = (row3 - row1) / (row2 - row1);//上下显示比例
                HOperatorSet.SetPart(ID, (row3 - bbb * biliY), (col3 - aaa * biliX), (row3 + bbb * (1 - biliY)), (col3 + aaa * (1 - biliX)));
                HOperatorSet.DispObj(img, ID);
                try
                {
                    HOperatorSet.DispObj(hb_region, ID);
                }
                catch
                {
                }
                try
                {
                    HOperatorSet.DispObj(hb_XLD, ID);
                }
                catch
                {
                }

            }
            catch (Exception EX)
            { }


        }
        public static void mousemove(HTuple ID, HObject img,HObject hb_region,HObject hb_XLD)
        {
            
            if (isSelected)
            {
                HTuple row1, col1, row2, col2;
                HTuple row3, col3, button1;
                HOperatorSet.GetPart(ID, out row1, out col1, out row2, out col2);
                HOperatorSet.GetImageSize(img, out Width, out Height);
                double zoom = (col2 - col1) / Width;
                HOperatorSet.GetMposition(ID, out row3, out col3, out button1);
                HOperatorSet.ClearWindow(ID);
                HOperatorSet.SetPart(ID, row1 - (row3 - mousey), col1 - (col3 - mousex), row2 - (row3 - mousey), col2 - (col3 - mousex));
                HOperatorSet.DispObj(img, ID);
                try
                {
                    HOperatorSet.DispObj(hb_region, ID);
                }
                catch
                {
                }
                try
                {
                    HOperatorSet.DispObj(hb_XLD, ID);
                }
                catch
                {
                }
            }

        }
        public static void mousedown(HTuple ID, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HTuple row4, col4, button2;
                HOperatorSet.GetMposition(ID, out row4, out col4, out button2);
                mousex = col4;
                mousey = row4;
                isSelected = true;
            }
        }
        public static void mouseup()
        {
            isSelected = false;
        }
        public static void mouseenter(HWindowControl HW)
        {
            
                HW.Focus();
                //OperateClass.resetPic(HW);
          
        }

    }
}
