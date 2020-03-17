using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAction
{
    //class Variables
    //{
    //    VarType type = VarType.String;
    //    object valueObj = new object();
    //    string name = "";
    //    private void saveVar()
    //    {
    //        Save.SaveIni("F:\\vardata.ini", "Variables", name, valueObj.ToString() + "|" + valueObj.GetType().Name);
    //    }
    //    private string readVar()
    //    {
    //        string ret = Save.ReadIni("F:\\vardata.ini", "Variables", name);
    //        return ret;
    //    }
    //    public Variables(string _name,VarType  _type)
    //    {
            
    //        type = _type;
    //        name = _name;
    //        try
    //        {
    //            var m_value = new object();
    //            string rets = readVar();
    //            string[] retarr = rets.Split('|');
    //            string ret = retarr[0];
    //            _type = (VarType)Enum.Parse(typeof(VarType), retarr[1]);
    //            switch (_type)
    //            {
    //                case VarType.String:
    //                    m_value = ret;
    //                    break;
    //                case VarType.Boolean:
    //                    m_value = bool.Parse(ret);
    //                    break;
    //                case VarType.Double:
    //                    m_value = double.Parse(ret);
    //                    break;
    //                case VarType.Single:
    //                    m_value = float.Parse(ret);
    //                    break;
    //                case VarType.Int32:
    //                    m_value = int.Parse(ret);
    //                    break;
    //                case VarType.Int64:
    //                    m_value = Int64.Parse(ret);
    //                    break;
    //            }
    //            Value = m_value;
    //        }
    //        catch { }
    //    }


    //    public dynamic Value
    //    {
    //        set
    //        {
    //            if (value.GetType() == valueObj.GetType())
    //            {
    //                valueObj = value;
    //                saveVar();
    //            }
    //            else
    //                throw new Exception("类型异常，当前类型为" + valueObj.GetType() + ",赋值类型为" + value.GetType());
    //        }
    //        get
    //        {
    //            try
    //            {
    //                string rets = readVar();
    //                string[] retarr = rets.Split('|');
    //                string ret = retarr[0];
    //                _type = (VarType)Enum.Parse(typeof(VarType), retarr[1]);
    //                switch (_type)
    //                {
    //                    case VarType.String:
    //                        valueObj = ret;
    //                        break;
    //                    case VarType.Boolean:
    //                        valueObj = bool.Parse(ret);
    //                        break;
    //                    case VarType.Double:
    //                        valueObj = double.Parse(ret);
    //                        break;
    //                    case VarType.Single:
    //                        valueObj = float.Parse(ret);
    //                        break;
    //                    case VarType.Int32:
    //                        valueObj = int.Parse(ret);
    //                        break;
    //                    case VarType.Int64:
    //                        valueObj = Int64.Parse(ret);
    //                        break;
    //                }
    //            }
    //            catch { }
    //            return valueObj;
    //        }
    //    }





    //}
}
