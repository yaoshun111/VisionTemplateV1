using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAction
{
    public partial class VariableCtr : UserControl
    {
         VarType _type = VarType.String;
         object valueObj = "";
         bool _showinfo = false;
        public VariableCtr()
        {
            InitializeComponent();
            BackColor = Color.LightBlue;
            
        }

        private void saveVar()
        {
            Save.SaveIni("F:\\vardata.ini", "Variables", Name, valueObj.ToString() + "|" + valueObj.GetType().Name);
        }
        private string readVar()
        {
            string ret = Save.ReadIni("F:\\vardata.ini", "Variables", Name);
            return ret;
        }


        public bool ShowInfo
        {
            get
            {
                
                return _showinfo;

            }
            set
            {
                _showinfo = value;
                if (_showinfo)
                {
                    label1.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                }
                else
                {
                    label1.Text = Name;
                }
            }
        }

        public VarType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                switch (_type)
                {
                    case VarType.String:
                        valueObj = "";
                        break;
                    case VarType.Boolean:
                        valueObj = false;
                        break;
                    case VarType.Double:
                        valueObj = 0d; ;
                        break;
                    case VarType.Single:
                        valueObj = 0f;
                        break;
                    case VarType.Int32:
                        valueObj = 0;
                        break;
                    case VarType.Int64:
                        valueObj = (Int64)0;
                        break;
                }
                if (_showinfo)
                    label1.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    label1.Text = Name;
            }
        }

        public string ValueInit
        {
            get
            {
                return valueObj.ToString();
            }
            set
            {
                switch (_type)
                {
                    case VarType.String:
                        valueObj = value;
                        break;
                    case VarType.Boolean:
                        valueObj = bool.Parse(value);
                        break;
                    case VarType.Double:
                        valueObj = double.Parse(value);
                        break;
                    case VarType.Single:
                        valueObj = float.Parse(value);
                        break;
                    case VarType.Int32:
                        valueObj = int.Parse(value);
                        break;
                    case VarType.Int64:
                        valueObj = Int64.Parse(value);
                        break;
                }
                if (_showinfo)
                    label1.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    label1.Text = Name;
            }
        }
       [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public dynamic Value
        {
            set
            {
                if (value.GetType() == valueObj.GetType())
                {
                    valueObj = value;
                    saveVar();
                    if (_showinfo)
                        label1.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                    else
                        label1.Text = Name;
                }
                else
                    throw new Exception("类型异常，当前类型为" + valueObj.GetType() + ",赋值类型为" + value.GetType());
            }
            get
            {
                try
                {
                    string rets = readVar();
                    string[] retarr = rets.Split('|');
                    string ret = retarr[0];
                    _type = (VarType)Enum.Parse(typeof(VarType), retarr[1]);
                    switch (_type)
                    {
                        case VarType.String:
                            valueObj = ret;
                            break;
                        case VarType.Boolean:
                            valueObj = bool.Parse(ret);
                            break;
                        case VarType.Double:
                            valueObj = double.Parse(ret);
                            break;
                        case VarType.Single:
                            valueObj = float.Parse(ret);
                            break;
                        case VarType.Int32:
                            valueObj = int.Parse(ret);
                            break;
                        case VarType.Int64:
                            valueObj = Int64.Parse(ret);
                            break;
                    }
                }
                catch { }
                if (_showinfo)
                    label1.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    label1.Text = Name;
                return valueObj;
            }
        }

       private void VariableCtr_Load(object sender, EventArgs e)
       {
           
           valueObj = Value;
       }
    }
}
