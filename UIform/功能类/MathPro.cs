using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathPro
{
    class CpkPro
    {
        private float StDev(float[] arrData) //计算标准偏差
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
        private float Avage(float[] arrData)    //计算平均值
        {
            float tmpSum = 0F;
            for (int i = 0; i < arrData.Length; i++)
            {
                tmpSum += arrData[i];
            }
            return tmpSum / arrData.Length;
        }
        private float Max(float[] arrData)   //计算最大值
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
        private float Min(float[] arrData)  //计算最小值
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
        public float getR_value(float[] k_valuesTOO)
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
        public  float getCPK(float[] k, float UpperLimit, float LowerLimit) //获取CPK值
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
            float cpk = Cpk(CpkU(UpperLimit, Avage(k), StDev(k)), CpkL(LowerLimit, Avage(k), StDev(k)));
            return cpk;
        }
    }
}
