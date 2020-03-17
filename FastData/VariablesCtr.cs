using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAction
{
    public partial class VariablesCtr : Label
    {
        VarType _type = VarType.String;
        object valueObj = "";
        bool _showinfo = false;
        public VariablesCtr()
        {
            InitializeComponent();
            BackColor = Color.LightBlue;
        }

        public VariablesCtr(string _name,object _value)
        {
            InitializeComponent();
            BackColor = Color.LightBlue;
            Name = _name;
            valueObj = _value;
            saveVar();
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
                    base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);

                }
                else
                {
                    base.Text = Name;
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
                valueObj = Value;
                if (_showinfo)
                    base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    base.Text = Name;
            }
        }
         [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string ValueInit
        {
            get
            {
                if (_showinfo)
                    base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    base.Text = Name;
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
                saveVar();
                valueObj = Value;
                if (_showinfo)
                    base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    base.Text = Name;

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
                        base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                    else
                        base.Text = Name;
                }
                else
                    throw new Exception("类型异常，当前类型为" + valueObj.GetType() + ",赋值类型为" + value.GetType());
            }
            get
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
              
                if (_showinfo)
                    base.Text = Name + " | " + valueObj.ToString() + " | " + Enum.GetName(typeof(VarType), _type);
                else
                    base.Text = Name;
                return valueObj;
            }
        }
    }
}
