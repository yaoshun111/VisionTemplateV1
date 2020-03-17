using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionFram3
{
    [Serializable]
    public class saveVarible
    {
        public int comboBox1select = 0;
        public int comboBox2select = 5;
        public int comboBox3select = 0;
        public int comboBox4select = 0;
        public int comboBox5select = 1;
        public string ip = "192.168.1.1";
        public int AutoCheckTime = 5;
        public object[] wuliaobianma = new object[3]{1,2,3};
        public string wuliaobianma_dianjieyehao = "0,0";
        public string jitaihao = "00000";
        public float dianjianwucha = 0.015f;
        public float famazhiliang = 50.000f;
        public double zhuyemin = 1;
        public double zhuyemax = 1.2;
        public double dianchimin = 5.6d;
        public double dianchimax = 6.4d;
        public string tiaomaqianjiwei = "000";
        public bool dianxinsaoma = false;
        public bool danjiasaoma = false;
        public int qinglingzhouqi = 4;
    }
}
