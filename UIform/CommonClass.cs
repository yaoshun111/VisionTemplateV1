using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HalconDotNet;
using System.Windows.Forms;

namespace UIform
{
    public static class CommonClass
    {
        public static bool m_bStartAutoRun = false;

        /// <summary>
        /// UI界面传递过来的相机采图句柄
        /// </summary>
        public static HTuple hv_AcqHandle;

        public static void Cam1Procedure(HDevProcedureCall m_CamProcedureCall, HTuple hv_hWindowHandle, HObject ho_ImageInput, out HObject ho_Contours)
        {
            try
            {
                m_CamProcedureCall.SetInputIconicParamObject("Image", ho_ImageInput);
                m_CamProcedureCall.SetInputCtrlParamTuple("threshold", 128);

                m_CamProcedureCall.Execute();

                ho_Contours = m_CamProcedureCall.GetOutputIconicParamObject("Contours");
            }
            catch (Exception df)
            {
                HOperatorSet.GenEmptyObj(out ho_Contours);

                // Fun_LogData("视觉处理异常！异常信息:" + df.ToString());
            }
        }

        public static void Set_Disp_Obj(HTuple ho_handwindow, HObject M_image)
        {
            //获取图像大小
            HTuple Width = 0;
            HTuple Height = 0;
            if (Count_Obj(M_image))
            {
                HOperatorSet.GetImageSize(M_image, out Width, out Height);
                HOperatorSet.SetPart(ho_handwindow, 0, 0, Height - 1, Width - 1);
                HOperatorSet.DispObj(M_image, ho_handwindow);
            }
        }

        public static bool Count_Obj(HObject obj)
        {
            //判断对象有没有被初始化
            if (!obj.IsInitialized())
            {
                return false;
            }
            if (obj.CountObj() < 1)
            {
                return false;
            }
            return true;

        }

        public static bool Write_image(ref HObject M_image, string path)
        {
            if (!M_image.IsInitialized())
            {
                MessageBox.Show("图片为空");
                return false;
            }
            int a = 0;
            string str;
            a = path.LastIndexOf(".");
            if (a < -1)
            {
                str = path.Substring(a + 1, path.Length - 1 - a);
            }
            else
            {
                str = "bmp";
            }
            HOperatorSet.WriteImage(M_image, str, 0, path);
            return true;
        }

    }
}
