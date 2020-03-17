using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastData;
using System.IO;

namespace FastCtr
{
    public partial class CPKDisplayControl : UserControl
    {
        public CPKDisplayControl()
        {
            InitializeComponent();
        }

        //private double zhuye_min1;//注液量最小值
        //private double zhuye_max;//注液量最大值  //这里都是初始值，后面需要根据实际修改
        //private bool openCPKOffsetStatus;//是否启用CPK动态补偿行为
        ///// <summary>
        ///// 注液量最小值（范围限定值）
        ///// </summary>
        //public double Zhuye_min1 { get => zhuye_min1; set => zhuye_min1 = value; }
        ///// <summary>
        ///// 注液量最大值（范围限定值）
        ///// </summary>
        //public double Zhuye_max { get => zhuye_max; set => zhuye_max = value; }
        ///// <summary>
        ///// 是否启用CPK动态补偿行为
        ///// true：开启;  false:关闭
        ///// </summary>
        //public bool OpenCPKOffsetStatus { get => openCPKOffsetStatus; set => openCPKOffsetStatus = value; }

     


        //private bool CPKIsStart = false;
        //private float cpk1 = 0;
        //private float cpk2 = 0;
        //private float cpk3 = 0;
        //private float cpk4 = 0;
        //private float cpk5 = 0;
        //private float cpk6 = 0;
        //private float[] zhuye_1_Value;//注液量
        //private float[] zhuye_2_Value;
        //private float[] zhuye_3_Value;
        //private float[] zhuye_4_Value;
        //private float[] zhuye_5_Value;
        //private float[] zhuye_6_Value;
        ////记录过的标志位
        //private int nn1;
        //private int nn2;
        //private int nn3;
        //private int nn4;
        //private int nn5;
        //private int nn6;
        ////CPK文件路径
        //private string file_CPK_1 = "E:\\Configuration\\CPK数据\\CPK_1\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPK_2 = "E:\\Configuration\\CPK数据\\CPK_2\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPK_3 = "E:\\Configuration\\CPK数据\\CPK_3\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPK_4 = "E:\\Configuration\\CPK数据\\CPK_4\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPK_5 = "E:\\Configuration\\CPK数据\\CPK_5\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPK_6 = "E:\\Configuration\\CPK数据\\CPK_6\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

        ////CPK补偿值文件路径
        //private string file_CPKOffset_1 = "E:\\Configuration\\CPK数据\\CPK_Offset_1\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPKOffset_2 = "E:\\Configuration\\CPK数据\\CPK_Offset_2\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPKOffset_3 = "E:\\Configuration\\CPK数据\\CPK_Offset_3\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPKOffset_4 = "E:\\Configuration\\CPK数据\\CPK_Offset_4\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPKOffset_5 = "E:\\Configuration\\CPK数据\\CPK_Offset_5\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //private string file_CPKOffset_6 = "E:\\Configuration\\CPK数据\\CPK_Offset_6\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";




        ///// <summary>
        ///// 开启CPK 的扫描线程
        ///// </summary>
        //public void StartCPKCal()
        //{
        //    Task cpkTask = new Task(cpkMethod);
        //    CPKIsStart = true;
        //    cpkTask.Start();

        //    nn1 = SaveStatic.ReadTxt(file_CPK_1).Length;
        //    nn2 = SaveStatic.ReadTxt(file_CPK_2).Length;
        //    nn3 = SaveStatic.ReadTxt(file_CPK_3).Length;
        //    nn4 = SaveStatic.ReadTxt(file_CPK_4).Length;
        //    nn5 = SaveStatic.ReadTxt(file_CPK_5).Length;
        //    nn6 = SaveStatic.ReadTxt(file_CPK_6).Length;
        //}

        ///// <summary>
        ///// 停止CPK的扫描线程
        ///// </summary>
        //public void StopCPKCal()
        //{
        //    CPKIsStart = false;
        //}


        //private void cpkMethod()
        //{
        //    while (CPKIsStart)
        //    {
        //        if (!File.Exists(@"Data\CPK\CPK_A1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
        //        {
        //            lbFillingPort1.Text = "-";
        //        }
        //        else
        //        {
        //            float[] cpklist1 = new float[nn1];
        //            for (int i = 0; i < nn1; i++)
        //            {
        //                cpklist1[i] = Convert.ToSingle(SaveStatic.ReadTxt(file_CPK_1)[i]);
        //            }

        //            zhuye_1_Value = cpklist1;//这个数据用于注液量补偿的计算的

        //            //获得cpk值，并显示
        //            if (cpklist1.Count() > 2)
        //            {
        //                cpk1 = getCPK(cpklist1, (float)Zhuye_max, (float)Zhuye_min1);
        //            }
        //            this.Invoke(new EventHandler(delegate
        //            {
        //                lbFillingPort1.Text = cpk1.ToString("0.000");
        //            }));


        //            //注液量补偿-- 判断补偿开启状态
        //            if (OpenCPKOffsetStatus)
        //            {
        //                //注液量补偿
        //                int x1 = zhuye_1_Value.Length;

        //                if (x1 % 5 == 0 && x1 != 0 && x1 != nn1)
        //                {
        //                    nn1 = x1;
        //                    //求和--10个值
        //                    float sum = 0.0f;
        //                    for (int i = 1; i <= 5; i++)
        //                    {
        //                        sum += zhuye_1_Value[x1 - i];
        //                    }
        //                    //取平均值，并与标准值作差，求得补偿值
        //                    float zhuye_Value = GlobalVal.hls_omron.Read_FLOAT("D3610");
        //                    float zhuye_buchang = (zhuye_Value - (sum / 5));//8-注液量L
        //                    string zhuye_buchang2 = Convert.ToInt32((zhuye_Value - (sum / 5)) * 100 + 5000).ToString();//8-注液量L
        //                                                                                                               //将补偿值写入到PLC中
        //                    if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //                    {
        //                        int x = int.Parse(zhuye_buchang2, System.Globalization.NumberStyles.AllowHexSpecifier);
        //                        GlobalVal.hls_omron.Write("D4200", x);//补偿量L//这个先不写
        //                    }

        //                    this.Invoke(new EventHandler(delegate
        //                    {
        //                        lbCPKOffsetValue_1.Text = zhuye_buchang.ToString("0.00");
        //                    }));
        //                    string path = @"Data\注液量补偿\A1\" + DateTime.Now.ToString("yyyyMMdd");
        //                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    string[,] result = new string[1, 2];
        //                    result[0, 0] = time;
        //                    result[0, 1] = zhuye_buchang.ToString("0.00");
        //                    DataAction.Save.SaveData(result, path + ".txt");


        //                    //保存一份CSV文件，供查看(都记录)                         
        //                    GlobalVal.csv.SaveCSV(result, true, @"E:\Para\注液量补偿\注液量补偿\A1\" + DateTime.Now.ToString("yyyyMMdd") + ".csv");
        //                    result.Initialize();

        //                    //Task t1 = new Task(OffestInit1);
        //                    //t1.Start();

        //                }
        //            }
        //        }


        //        if (!File.Exists(@"Data\CPK\CPK_A2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
        //        {
        //            lbFillingPort2.Text = "-";
        //        }
        //        else
        //        {
        //            //float[] cpklist2 = txtServer.ReadData(@"E:\Para\CPK_A2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            float[] cpklist2 = DataAction.Save.ReadData(@"Data\CPK\CPK_A2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            zhuye_2_Value = cpklist2;//这个数据用于注液量补偿的计算的

        //            if (cpklist2.Count() > 2)
        //            {
        //                cpk2 = getCPK(cpklist2, (float)Zhuye_max, (float)Zhuye_min1);
        //            }
        //            this.Invoke(new EventHandler(delegate
        //            {
        //                lbFillingPort2.Text = cpk2.ToString("0.000");
        //            }));


        //            //注液量补偿-- 判断补偿开启状态
        //            if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //            {
        //                //注液量补偿
        //                int x2 = zhuye_2_Value.Length;

        //                if (x2 % 5 == 0 && x2 != 0 && x2 != nn2)
        //                {
        //                    nn2 = x2;
        //                    //求和--10个值
        //                    float sum = 0.0f;
        //                    for (int i = 1; i <= 5; i++)
        //                    {
        //                        sum += zhuye_2_Value[x2 - i];
        //                    }
        //                    //取平均值，并与标准值作差，求得补偿值
        //                    float zhuye_Value = GlobalVal.hls_omron.Read_FLOAT("D3510");
        //                    float zhuye_buchang = (zhuye_Value - (sum / 5));//8-注液量R
        //                    string zhuye_buchang2 = Convert.ToInt32((zhuye_Value - (sum / 5)) * 100 + 5000).ToString();//8-注液量R

        //                    //将补偿值写入到PLC中
        //                    if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //                    {
        //                        int x = int.Parse(zhuye_buchang2, System.Globalization.NumberStyles.AllowHexSpecifier);
        //                        GlobalVal.hls_omron.Write("D4100", x);//补偿量R//这个先不写
        //                    }
        //                    this.Invoke(new EventHandler(delegate
        //                    {
        //                        lbCPKOffsetValue_2.Text = zhuye_buchang.ToString("0.00");
        //                    }));
        //                    string path = @"Data\注液量补偿\A2\" + DateTime.Now.ToString("yyyyMMdd");
        //                    //DataAction.Save.SaveData(path, zhuye_buchang.ToString("0.00"));
        //                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    string[,] result = new string[1, 2];
        //                    result[0, 0] = time;
        //                    result[0, 1] = zhuye_buchang.ToString("0.00");
        //                    DataAction.Save.SaveData(result, path + ".txt");

        //                    //保存一份CSV文件，供查看(都记录)                         
        //                    GlobalVal.csv.SaveCSV(result, true, @"E:\Para\注液量补偿\注液量补偿\A2\" + DateTime.Now.ToString("yyyyMMdd") + ".csv");
        //                    result.Initialize();

        //                    //Task t2 = new Task(OffestInit2);
        //                    //t2.Start();
        //                }
        //            }
        //        }



        //        if (!File.Exists(@"Data\CPK\CPK_B1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
        //        {
        //            lbFillingPort3.Text = "-";
        //        }
        //        else
        //        {
        //            //float[] cpklist3 = txtServer.ReadData(@"E:\Para\CPK_B1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            float[] cpklist3 = DataAction.Save.ReadData(@"Data\CPK\CPK_B1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            zhuye_3_Value = cpklist3;//这个数据用于注液量补偿的计算的
        //            if (cpklist3.Count() > 2)
        //            {
        //                cpk3 = getCPK(cpklist3, (float)Zhuye_max, (float)Zhuye_min1);
        //            }
        //            this.Invoke(new EventHandler(delegate
        //            {
        //                lbFillingPort3.Text = cpk3.ToString("0.000");
        //            }));

        //            //注液量补偿-- 判断补偿开启状态
        //            if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //            {
        //                //注液量补偿
        //                int x1 = zhuye_3_Value.Length;

        //                if (x1 % 5 == 0 && x1 != 0 && x1 != nn3)
        //                {
        //                    nn3 = x1;
        //                    //求和--10个值
        //                    float sum = 0.0f;
        //                    for (int i = 1; i <= 5; i++)
        //                    {
        //                        sum += zhuye_3_Value[x1 - i];
        //                    }
        //                    //取平均值，并与标准值作差，求得补偿值
        //                    float zhuye_Value = GlobalVal.hls_omron.Read_FLOAT("D3610");
        //                    float zhuye_buchang = (zhuye_Value - (sum / 5));//8-注液量L
        //                    string zhuye_buchang2 = Convert.ToInt32((zhuye_Value - (sum / 5)) * 100 + 5000).ToString();//8-注液量L

        //                    //将补偿值写入到PLC中
        //                    if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //                    {
        //                        int x = int.Parse(zhuye_buchang2, System.Globalization.NumberStyles.AllowHexSpecifier);
        //                        GlobalVal.hls_omron.Write("D4200", x);//补偿量L//这个先不写
        //                    }
        //                    this.Invoke(new EventHandler(delegate
        //                    {
        //                        lbCPKOffsetValue_3.Text = zhuye_buchang.ToString("0.00");
        //                    }));
        //                    string path = @"Data\注液量补偿\B1\" + DateTime.Now.ToString("yyyyMMdd");
        //                    //DataAction.Save.SaveData(path, zhuye_buchang.ToString("0.00"));
        //                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    string[,] result = new string[1, 2];
        //                    result[0, 0] = time;
        //                    result[0, 1] = zhuye_buchang.ToString("0.00");
        //                    DataAction.Save.SaveData(result, path + ".txt");


        //                    //保存一份CSV文件，供查看(都记录)                         
        //                    GlobalVal.csv.SaveCSV(result, true, @"E:\Para\注液量补偿\注液量补偿\B1\" + DateTime.Now.ToString("yyyyMMdd") + ".csv");
        //                    result.Initialize();

        //                    //Task t3 = new Task(OffestInit3);
        //                    //t3.Start();
        //                }
        //                else
        //                { }
        //            }
        //        }


        //        if (!File.Exists(@"Data\CPK\CPK_B2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
        //        {
        //            lbFillingPort4.Text = "-";
        //        }
        //        else
        //        {
        //            //float[] cpklist4 = txtServer.ReadData(@"E:\Para\CPK_B2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            float[] cpklist4 = DataAction.Save.ReadData(@"Data\CPK\CPK_B2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        //            zhuye_4_Value = cpklist4;//这个数据用于注液量补偿的计算的

        //            if (cpklist4.Count() > 2)
        //            {
        //                cpk4 = getCPK(cpklist4, (float)Zhuye_max, (float)Zhuye_min1);
        //            }
        //            this.Invoke(new EventHandler(delegate
        //            {
        //                lbFillingPort4.Text = cpk4.ToString("0.000");
        //            }));


        //            //注液量补偿-- 判断补偿开启状态
        //            if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //            {
        //                int x2 = zhuye_4_Value.Length;

        //                if (x2 % 5 == 0 && x2 != 0 && x2 != nn4)
        //                {
        //                    nn4 = x2;
        //                    //求和--10个值
        //                    float sum = 0.0f;
        //                    for (int i = 1; i <= 5; i++)
        //                    {
        //                        sum += zhuye_4_Value[x2 - i];
        //                    }
        //                    //取平均值，并与标准值作差，求得补偿值
        //                    float zhuye_Value = GlobalVal.hls_omron.Read_FLOAT("D3510");
        //                    float zhuye_buchang = (zhuye_Value - (sum / 5));//8-注液量R
        //                    string zhuye_buchang2 = Convert.ToInt32((zhuye_Value - (sum / 5)) * 100 + 5000).ToString();//8-注液量R

        //                    //将补偿值写入到PLC中
        //                    if (GlobalVal.pageSetting._statusWeighOffest == "ON")
        //                    {
        //                        int x = int.Parse(zhuye_buchang2, System.Globalization.NumberStyles.AllowHexSpecifier);
        //                        GlobalVal.hls_omron.Write("D4100", x);//补偿量R//这个先不写
        //                    }
        //                    this.Invoke(new EventHandler(delegate
        //                    {
        //                        lbCPKOffsetValue_4.Text = zhuye_buchang.ToString("0.00");
        //                    }));
        //                    string path = @"Data\注液量补偿\B2\" + DateTime.Now.ToString("yyyyMMdd");
        //                    //DataAction.Save.SaveData(path, zhuye_buchang.ToString("0.00"));
        //                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    string[,] result = new string[1, 2];
        //                    result[0, 0] = time;
        //                    result[0, 1] = zhuye_buchang.ToString("0.00");
        //                    DataAction.Save.SaveData(result, path + ".txt");

        //                    //保存一份CSV文件，供查看(都记录)                         
        //                    GlobalVal.csv.SaveCSV(result, true, @"E:\Para\注液量补偿\注液量补偿\B2\" + DateTime.Now.ToString("yyyyMMdd") + ".csv");
        //                    result.Initialize();

        //                    //Task t4 = new Task(OffestInit4);
        //                    //t4.Start();
        //                }
        //            }
        //        }

        //        //string[] cpkstr1 = Save.ReadTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk1.txt");
        //        //string[] cpkstr2 = Save.ReadTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk2.txt");
        //        //string[] cpkstr3 = Save.ReadTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk3.txt");
        //        //string[] cpkstr4 = Save.ReadTxt("F:\\天CPK数据\\" + DateTime.Now.ToString("yyyyMMdd") + "_cpk4.txt");
        //        //string[] cpkstr1 = {"1","2","3","4","5"};
        //        //string[] cpkstr2 = { "1", "2", "3", "4", "5" };
        //        //string[] cpkstr3 = { "1", "2", "3", "4", "5" };
        //        //string[] cpkstr4 = { "1", "2", "3", "4", "5" };     

        //        Thread.Sleep(1500);
        //    }
        //}


        ///// <summary>
        ///// 补偿后，20s后，将补偿值置零，因为补偿是累加的
        ///// </summary>
        //public void OffestInit1()
        //{
        //    DateTime dt1 = DateTime.Now;
        //    bool a1 = true;
        //    while (a1)
        //    {
        //        if (dt1.AddSeconds(30) > DateTime.Now)
        //        {
        //            a1 = true;
        //        }
        //        else
        //        {
        //            a1 = false;
        //            int x = int.Parse("5010", System.Globalization.NumberStyles.AllowHexSpecifier);
        //            GlobalVal.hls_omron.Write("D4200", x);//补偿量L//这个先不写
        //        }
        //    }
        //}

        ///// <summary>
        ///// 补偿后，20s后，将补偿值置零，因为补偿是累加的
        ///// </summary>
        //public void OffestInit2()
        //{
        //    DateTime dt1 = DateTime.Now;
        //    bool a1 = true;
        //    while (a1)
        //    {
        //        if (dt1.AddSeconds(30) > DateTime.Now)
        //        {
        //            a1 = true;
        //        }
        //        else
        //        {
        //            a1 = false;
        //            int x = int.Parse("5010", System.Globalization.NumberStyles.AllowHexSpecifier);
        //            GlobalVal.hls_omron.Write("D4100", x);//补偿量R//这个先不写
        //        }
        //    }
        //}

        ///// <summary>
        ///// 补偿后，20s后，将补偿值置零，因为补偿是累加的
        ///// </summary>
        //public void OffestInit3()
        //{
        //    DateTime dt1 = DateTime.Now;
        //    bool a1 = true;
        //    while (a1)
        //    {
        //        if (dt1.AddSeconds(30) > DateTime.Now)
        //        {
        //            a1 = true;
        //        }
        //        else
        //        {
        //            a1 = false;
        //            int x = int.Parse("5010", System.Globalization.NumberStyles.AllowHexSpecifier);
        //            GlobalVal.hls_omron.Write("D4200", x);//补偿量L//这个先不写
        //        }
        //    }
        //}

        ///// <summary>
        ///// 补偿后，20s后，将补偿值置零，因为补偿是累加的
        ///// </summary>
        //public void OffestInit4()
        //{
        //    DateTime dt1 = DateTime.Now;
        //    bool a1 = true;
        //    while (a1)
        //    {
        //        if (dt1.AddSeconds(30) > DateTime.Now)
        //        {
        //            a1 = true;
        //        }
        //        else
        //        {
        //            a1 = false;
        //            int x = int.Parse("5010", System.Globalization.NumberStyles.AllowHexSpecifier);
        //            GlobalVal.hls_omron.Write("D4100", x);//补偿量R//这个先不写
        //        }
        //    }
        //}




        //private float StDev(float[] arrData) //计算标准偏差
        //{
        //    float xSum = 0F;
        //    float xAvg = 0F;
        //    float sSum = 0F;
        //    float tmpStDev = 0F;
        //    int arrNum = arrData.Length;
        //    for (int i = 0; i < arrNum; i++)
        //    {
        //        xSum += arrData[i];
        //    }
        //    xAvg = xSum / arrNum;
        //    for (int j = 0; j < arrNum; j++)
        //    {
        //        sSum += ((arrData[j] - xAvg) * (arrData[j] - xAvg));
        //    }
        //    tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
        //    return tmpStDev;
        //}
        //private float Cp(float UpperLimit, float LowerLimit, float StDev)//计算cp
        //{
        //    float tmpV = 0F;
        //    tmpV = UpperLimit - LowerLimit;
        //    return Math.Abs(tmpV / (6 * StDev));
        //}
        //private float Avage(float[] arrData)    //计算平均值
        //{
        //    float tmpSum = 0F;
        //    for (int i = 0; i < arrData.Length; i++)
        //    {
        //        tmpSum += arrData[i];
        //    }
        //    return tmpSum / arrData.Length;
        //}
        //private float Max(float[] arrData)   //计算最大值
        //{
        //    float tmpMax = 0;
        //    //for (int i = 0; i < arrData.Length; i++)
        //    //{
        //    //    if (tmpMax < arrData[i])
        //    //    {
        //    //        tmpMax = arrData[i];
        //    //    }
        //    //}
        //    tmpMax = arrData.Max();
        //    return tmpMax;
        //}
        //private float Min(float[] arrData)  //计算最小值
        //{
        //    float tmpMin = arrData[0];
        //    for (int i = 1; i < arrData.Length; i++)
        //    {
        //        if (tmpMin > arrData[i])
        //        {
        //            tmpMin = arrData[i];
        //        }
        //    }
        //    return tmpMin;
        //}
        //private float CpkU(float UpperLimit, float Avage, float StDev)//计算CpkU
        //{
        //    float tmpV = 0F;
        //    tmpV = UpperLimit - Avage;
        //    return tmpV / (3 * StDev);
        //}
        //private float CpkL(float LowerLimit, float Avage, float StDev) //计算CpkL
        //{
        //    float tmpV = 0F;
        //    tmpV = Avage - LowerLimit;
        //    return tmpV / (3 * StDev);
        //}
        //private float Cpk(float CpkU, float CpkL)  //计算Cpk
        //{
        //    return Math.Abs(Math.Min(CpkU, CpkL));
        //}
        //public float getR_value(float[] k_valuesTOO)
        //{
        //    float min = k_valuesTOO[0];
        //    float max = k_valuesTOO[0];
        //    for (int i = 0; i < k_valuesTOO.Length; i++)
        //    {
        //        if (k_valuesTOO[i] < min)
        //        {
        //            min = k_valuesTOO[i];
        //        }
        //        if (k_valuesTOO[i] > max)
        //        {
        //            max = k_valuesTOO[i];
        //        }
        //    }
        //    return max - min;
        //}
        //public float getCPK(float[] k, float UpperLimit, float LowerLimit) //获取CPK值
        //{
        //    //CpkPro cpkPro = new CpkPro();
        //    //float[] k = { 0.03F, 0.06F, 0.05F, 0.03F, 0.04F, 0.04F, 0.03F, 0.04F, 0.04F, 0.04F, 0.04F, 0.04F, 0.04F, 0.03F, 0.01F, 0.03F, 0.01F, 0.03F, 0.04F, 0.04F, 0.04F, 0.05F, 0.02F, 0.04F, 0.05F, 0.05F, 0.05F, 0.05F, 0.03F, 0.05F, 0.05F, 0.03F, 0.02F, 0.04F, 0.04F, 0.02F, 0.06F, 0.04F, 0.02F, 0.03F, 0.04F, 0.02F, 0.05F, 0.06F, 0.07F, 0.02F, 0.04F, 0.04F, 0.03F, 0.03F };
        //    //float UpperLimit = 0.12F;  //上限
        //    //float LowerLimit = 0F; //下限
        //    //float result = cpkPro.getCPK(k, UpperLimit, LowerLimit);
        //    //Console.WriteLine(result);
        //    if (k.Length <= 1 || UpperLimit <= LowerLimit)
        //    {
        //        return -1;
        //    }
        //    float cpk = Cpk(CpkU(UpperLimit, Avage(k), StDev(k)), CpkL(LowerLimit, Avage(k), StDev(k)));
        //    return cpk;
        //}



        ///// <summary>
        ///// 补偿值清空
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button11_Click(object sender, EventArgs e)
        //{
        //    DialogResult dr = MessageBox.Show("确认当前显示补偿值数据？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    if (dr == DialogResult.OK)
        //    {
        //        GlobalVal.admin.ShowDialog();
        //        if (GlobalVal.isAdminLogicSuccess)
        //        {
        //            lbCPKOffsetValue_1.Text = "0.00";
        //            lbCPKOffsetValue_2.Text = "0.00";
        //            lbCPKOffsetValue_3.Text = "0.00";
        //            lbCPKOffsetValue_4.Text = "0.00";
        //        }
        //        else
        //        {
        //            //管理员权限登录失败了
        //            MessageBox.Show("对不起，您没有操作权限！！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            GlobalVal.logMessage.MessageIn("管理员登录失败", "Warning");
        //        }
        //    }
        //}

        ///// <summary>
        ///// 注液量CPK清空
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button12_Click(object sender, EventArgs e)
        //{
        //    DialogResult dr = MessageBox.Show("确认清空CKP数据？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    if (dr == DialogResult.OK)
        //    {
        //        GlobalVal.admin.ShowDialog();
        //        if (GlobalVal.isAdminLogicSuccess)
        //        {
        //            string path1 = @"Data\CPK\CPK_A1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //            string path2 = @"Data\CPK\CPK_A2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //            string path3 = @"Data\CPK\CPK_B1\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //            string path4 = @"Data\CPK\CPK_B2\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //            string[] arr = new string[] { path1, path2, path3, path4 };

        //            for (int i = 0; i < 4; i++)
        //            {
        //                DataAction.Save.ClearTXT(arr[i]);
        //            }
        //            cpk1 = 0;
        //            cpk2 = 0;
        //            cpk3 = 0;
        //            cpk4 = 0;
        //        }
        //        else
        //        {
        //            //管理员权限登录失败了
        //            MessageBox.Show("对不起，您没有操作权限！！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            GlobalVal.logMessage.MessageIn("管理员登录失败", "Warning");
        //        }
        //    }
        //}


        ///// <summary>
        ///// 双击隐藏界面
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void label53_Click(object sender, EventArgs e)
        //{
        //    this.Visible = false;
        //}
    }
}
