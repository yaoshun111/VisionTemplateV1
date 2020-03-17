using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace VisionFram3
{
    class SAVE
    {
        public static void ReadVar(string FName, ref saveVarible saveVarible1)
        {
            if (!File.Exists(@"" + FName + ".bin"))
            {
                IFormatter formatter0 = new BinaryFormatter();
                Stream stream0 = new FileStream(@"" + FName+ ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter0.Serialize(stream0, saveVarible1);
                stream0.Close();
                stream0.Dispose();
            }
            IFormatter formatter = new BinaryFormatter();
            Stream streamc = new FileStream(@"" + FName + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            saveVarible1 = formatter.Deserialize(streamc) as saveVarible;
            streamc.Close();
            streamc.Dispose();
        }

        public static void SaveVar(string FName, saveVarible saveVarible1)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"" + FName + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, saveVarible1);
            stream.Close();
            stream.Dispose();
        }

        public static void SaveArry(string FName, List<float> list)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"" + FName + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, list);
            stream.Close();
            stream.Dispose();
        }

        public static void ReadArry(string FName, ref List<float> list)
        {
            if (!File.Exists(@"" + FName + ".bin"))
            {
                IFormatter formatter0 = new BinaryFormatter();
                Stream stream0 = new FileStream(@"" + FName + ".bin", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter0.Serialize(stream0, list);
                stream0.Close();
                stream0.Dispose();
            }
            IFormatter formatter = new BinaryFormatter();
            Stream streamc = new FileStream(@"" + FName + ".bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            list = formatter.Deserialize(streamc) as List<float>;
            streamc.Close();
            streamc.Dispose();
        }





    }
}
