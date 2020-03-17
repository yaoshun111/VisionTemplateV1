using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastData
{
    public class CpkPro
    {
        /// <summary>
        /// 获得标准差
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private float GetSTD(float[] arrData)
        {
            float xSum = 0F;
            float xAvg = 0F;
            float sSum = 0F;
            float tmpStDev = 0F;
            int arrNum = arrData.Length;
            for (int i = 0; i < arrNum; i++)
            {
                xSum += arrData[i];
            }
            xAvg = xSum / arrNum;
            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((arrData[j] - xAvg) * (arrData[j] - xAvg));
            }
            tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
            return tmpStDev;
        }
        private float Cp(float UpperLimit, float LowerLimit, float StDev)//计算cp
        {
            float tmpV = 0F;
            tmpV = UpperLimit - LowerLimit;
            return Math.Abs(tmpV / (6 * StDev));
        }

        /// <summary>
        /// 获得平均值
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private float GetAverage(float[] arrData)
        {
            float tmpSum = 0F;
            for (int i = 0; i < arrData.Length; i++)
            {
                tmpSum += arrData[i];
            }
            return tmpSum / arrData.Length;
        }

        /// <summary>
        /// 获得最大值
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private float GetMax(float[] arrData)   //计算最大值
        {
            float tmpMax = 0;
            //for (int i = 0; i < arrData.Length; i++)
            //{
            //    if (tmpMax < arrData[i])
            //    {
            //        tmpMax = arrData[i];
            //    }
            //}
            tmpMax = arrData.Max();
            return tmpMax;
        }

        /// <summary>
        /// 获得最小值
        /// </summary>
        /// <param name="arrData"></param>
        /// <returns></returns>
        private float GetMin(float[] arrData)  //计算最小值
        {
            float tmpMin = arrData[0];
            for (int i = 1; i < arrData.Length; i++)
            {
                if (tmpMin > arrData[i])
                {
                    tmpMin = arrData[i];
                }
            }
            return tmpMin;
        }


        /// <summary>
        /// 获得极值
        /// </summary>
        /// <param name="k_valuesTOO"></param>
        /// <returns></returns>
        public float GetExtremum(float[] k_valuesTOO)
        {
            float min = k_valuesTOO[0];
            float max = k_valuesTOO[0];
            for (int i = 0; i < k_valuesTOO.Length; i++)
            {
                if (k_valuesTOO[i] < min)
                {
                    min = k_valuesTOO[i];
                }
                if (k_valuesTOO[i] > max)
                {
                    max = k_valuesTOO[i];
                }
            }
            return max - min;
        }



        private float CpkU(float UpperLimit, float Avage, float StDev)//计算CpkU
        {
            float tmpV = 0F;
            tmpV = UpperLimit - Avage;
            return tmpV / (3 * StDev);
        }
        private float CpkL(float LowerLimit, float Avage, float StDev) //计算CpkL
        {
            float tmpV = 0F;
            tmpV = Avage - LowerLimit;
            return tmpV / (3 * StDev);
        }
        private float Cpk(float CpkU, float CpkL)  //计算Cpk
        {
            return Math.Abs(Math.Min(CpkU, CpkL));
        }

        /// <summary>
        /// 获取CPK的值
        /// </summary>
        /// <param name="k"></param>
        /// <param name="UpperLimit"></param>
        /// <param name="LowerLimit"></param>
        /// <returns></returns>
        public float GetCPK(float[] k, float UpperLimit, float LowerLimit) //获取CPK值
        {
            //CpkPro cpkPro = new CpkPro();
            //float[] k = { 0.03F, 0.06F, 0.05F, 0.03F, 0.04F, 0.04F, 0.03F, 0.04F, 0.04F, 0.04F, 0.04F, 0.04F, 0.04F, 0.03F, 0.01F, 0.03F, 0.01F, 0.03F, 0.04F, 0.04F, 0.04F, 0.05F, 0.02F, 0.04F, 0.05F, 0.05F, 0.05F, 0.05F, 0.03F, 0.05F, 0.05F, 0.03F, 0.02F, 0.04F, 0.04F, 0.02F, 0.06F, 0.04F, 0.02F, 0.03F, 0.04F, 0.02F, 0.05F, 0.06F, 0.07F, 0.02F, 0.04F, 0.04F, 0.03F, 0.03F };
            //float UpperLimit = 0.12F;  //上限
            //float LowerLimit = 0F; //下限
            //float result = cpkPro.getCPK(k, UpperLimit, LowerLimit);
            //Console.WriteLine(result);
            if (k.Length <= 1 || UpperLimit <= LowerLimit)
            {
                return -1;
            }
            float cpk = Cpk(CpkU(UpperLimit, GetAverage(k), GetSTD(k)), CpkL(LowerLimit, GetAverage(k), GetSTD(k)));
            return cpk;
        }
    }
}