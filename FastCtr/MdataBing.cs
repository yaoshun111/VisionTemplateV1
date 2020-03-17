using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace VisionFram3
{
    public delegate void SourceArgs(object sender);
    class MdataBing
    {
        // 把dataSource1里面的值赋给dataSource1_2里面的值
        object dataSource1;
        object dataSource1_2;
        public static Dictionary<object, object> dataDictionarySource = new Dictionary<object, object>();
        public static Dictionary<string, string> dataDictionaryMember = new Dictionary<string, string>();
        bool flag = true;

        public MdataBing(object _dataSource1, object _dataSource1_2)
        {
            dataSource1 = _dataSource1;
            dataSource1_2 = _dataSource1_2;
            dataDictionarySource.Add(dataSource1, dataSource1_2);
            dataDictionarySource.Add(dataSource1_2, dataSource1);

            dataSource1.GetType().GetEvent("PropertyChanged").AddEventHandler(dataSource1, new PropertyChangedEventHandler(fun));
            dataSource1_2.GetType().GetEvent("PropertyChanged").AddEventHandler(dataSource1_2, new PropertyChangedEventHandler(fun));
        }
        private void fun(object sender1, PropertyChangedEventArgs e1)
        {
            if (flag)
            {
                object aa = sender1.GetType().GetProperty(e1.PropertyName).GetValue(sender1);
                flag = false;
                try
                {
                    Type t = dataDictionarySource[sender1].GetType();
                    if (dataDictionaryMember.Keys.Contains(e1.PropertyName))
                    {
                        PropertyInfo info = t.GetProperty(dataDictionaryMember[e1.PropertyName]);
                        info.SetValue(dataDictionarySource[sender1], aa);
                    }
                }
                catch{ }
            }
            flag = true;
        }
        public void Bing(string dataMember1, string dataMember1_2, bool TwoWay)
        {
            if (TwoWay)
            {
                dataDictionaryMember.Add(dataMember1, dataMember1_2);
                dataDictionaryMember.Add(dataMember1_2, dataMember1);
               
            }
            else
            {
                dataDictionaryMember.Add(dataMember1, dataMember1_2);
                //dataSource1.GetType().GetEvent("PropertyChanged").AddEventHandler(dataSource1, new PropertyChangedEventHandler(fun));
            }
        }
    }
}
