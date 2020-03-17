using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  OMRON.Compolet.CIP;
using System.Collections;
using BaseEvent;
using System.Threading;
using System.ComponentModel;

namespace OmroPlcVar
{
    public delegate void OmroPLCvarChangedEventHandle(VarChangeEventArgs e);
    [DefaultEvent("OnChanged")]
    partial class OmroPLCvar
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }


    public   partial class OmroPLCvar:Component
    {
        public static event LogEventHandler OnLogEvent;
        private static object object_lock = new object();
        public event OmroPLCvarChangedEventHandle OnChanged;
        public  NJCompolet njCompolet;
        public string m_TickSign = "";
        private int retVals = 0;
        private bool task_flag = true;
        private  Task task;
        int chaoshi = 0;

        public OmroPLCvar(NJCompolet _NJCompolet, string _TickSign)
        {
            m_TickSign = _TickSign;
            njCompolet = _NJCompolet;
        }

        public OmroPLCvar()
        {
            InitializeComponent();
        }

        public  bool WriteSingleVarible(string varibleName, object value)
        {
            lock (object_lock)     //线程锁，防止多个线程同时读或者写，使PLC造成拥堵
            {
                try
                {
                    njCompolet.WriteVariable(varibleName, value);
                    return true;
                }
                catch
                {
                    if (OnLogEvent != null)
                        OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + varibleName + "=" + value.ToString() + "失败1次！"));
                    try
                    {
                        njCompolet.WriteVariable(varibleName, value);
                        return true;
                    }
                    catch
                    {
                        if (OnLogEvent != null)
                            OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + varibleName + "=" + value.ToString() + "失败2次！"));
                        try
                        {
                            njCompolet.WriteVariable(varibleName, value);
                            return true;
                        }
                        catch
                        {
                            if (OnLogEvent != null)
                                OnLogEvent(new LogEventArgs("ERR_B:写入PLC变量" + varibleName + "=" + value.ToString() + "失败3次！"));
                            return false;
                        }
                    }
                }
            }
        }

        public string TickSign     //设置需要监测触发信号
        {
            get { return m_TickSign; }
            set { m_TickSign = value; }
        }

        private int RetVals        //PLC自动更新变量
        {
            get
            {
                return retVals;
            }
            set
            {
                if (retVals == value)
                    return;
                retVals = value;
                if (OnChanged != null)     //更新值，同时触发值改变事件
                {
                    OnChanged(new VarChangeEventArgs(retVals));    //事件指针传出参数值的字符串形式
                }
                //onLogEvent(new OnLogEventArgs("SIGNAL:" + this.m_TickSign + "=" + retVals));
            }
        }


        private object ReadVariable(string _var)
        {
            object mRetVals = null;
            lock (object_lock)
            {
                try
                {
                    mRetVals = njCompolet.ReadVariable(_var);
                }
                catch
                {
                    try
                    {
                        mRetVals = njCompolet.ReadVariable(_var);
                    }
                    catch
                    {
                        try
                        {
                            mRetVals = njCompolet.ReadVariable(_var);
                        }
                        catch
                        {
                            OnLogEvent(new LogEventArgs("ERR:PLC读取失败！"));
                        }
                    }
                }
                return mRetVals;
            }
        }
        public void Start()
        {
            task_flag = true;
            if (ReadVariable(m_TickSign) == null)
                return;
            task = new Task(new Action(() =>
               {
                   while (task_flag)
                   {
                       Thread.Sleep(100);
                       lock (object_lock)     //线程锁，防止多个线程同时读或者写，使PLC造成拥堵
                       {
                           //try
                           //{
                           object aa = ReadVariable(m_TickSign);
                           if (aa == null)
                           {
                               chaoshi++;
                               Thread.Sleep(500);
                               if (OnLogEvent != null)
                                   OnLogEvent(new LogEventArgs("WARNING:读取PLC变量" + m_TickSign + "失败！尝试重新读取！_" + chaoshi.ToString()));
                               if (chaoshi > 10)
                               {
                                   if (OnLogEvent != null)
                                       OnLogEvent(new LogEventArgs("ERR_B:读取PLC变量" + m_TickSign + "失败！尝试重新读取！_" + chaoshi.ToString()));
                               }
                           }
                           else
                           {
                               RetVals = (int)aa;
                               if(chaoshi>0)
                               {
                                   chaoshi = 0;
                                   if (OnLogEvent != null)
                                       OnLogEvent(new LogEventArgs("LOG:读取PLC变量" + m_TickSign + "=" + RetVals.ToString()));
                               }
                           }
                       }
                   }
               }),TaskCreationOptions.LongRunning);
            if (task.Status != TaskStatus.Running)
            {
                task.Start();
                if (OnLogEvent != null)
                    OnLogEvent(new LogEventArgs("LOG:PLC监测值 " + m_TickSign + " 已经开启！"));
            }
            //Thread.Sleep(2000);
        }

        public void Stop()
        {
            if (task.Status == TaskStatus.Running)
            {
                task_flag = false;
                if (OnLogEvent != null)
                    OnLogEvent(new LogEventArgs("LOG:PLC监测值 " + m_TickSign + " 已经关闭！"));
            }
        }


       

        public  bool ReadSingleVarible(string _varibleName,ref object _value)
        {
            lock (object_lock)     //线程锁，防止多个线程同时读或者写，使PLC造成拥堵
            {
                try
                {
                    _value = njCompolet.ReadVariable(_varibleName);
                    return true;
                }
                catch
                {

                    if (OnLogEvent != null)
                        OnLogEvent(new LogEventArgs("WARNING:读取PLC变量" + _varibleName + "失败1次"));
                    Thread.Sleep(500);
                    try
                    {
                        _value = njCompolet.ReadVariable(_varibleName);
                        return true;
                    }
                    catch
                    {

                        if (OnLogEvent != null)
                            OnLogEvent(new LogEventArgs("WARNING:读取PLC变量" + _varibleName + "失败2次！"));
                        Thread.Sleep(500);
                        try
                        {
                            _value = njCompolet.ReadVariable(_varibleName);
                            return true;
                        }
                        catch
                        {

                            if (OnLogEvent != null)
                                OnLogEvent(new LogEventArgs("ERR_B:读取PLC变量" + _varibleName + "失败3次！"));
                            return false;
                        }
                    }
                }
            }
        }


        public  void Finished()
        {
            lock (object_lock)     //线程锁，防止多个线程同时读或者写，使PLC造成拥堵
            {
                try
                {
                    njCompolet.WriteVariable(m_TickSign, 3);
                }
                catch
                {
                    if (OnLogEvent != null)
                        OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + m_TickSign + "=3" + "失败1次！"));
                    try
                    {
                        njCompolet.WriteVariable(m_TickSign, 3);
                    }
                    catch
                    {
                        if (OnLogEvent != null)
                            OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + m_TickSign + "=3" + "失败2次！"));
                        try
                        {
                            njCompolet.WriteVariable(m_TickSign, 3);
                        }
                        catch
                        {
                            if (OnLogEvent != null)
                                OnLogEvent(new LogEventArgs("ERR_B:写入PLC变量" + m_TickSign + "=3" + "失败3次！"));
                        }
                    }
                }
            }
        }


        public void ErrOccured()
        {
            lock (object_lock)     //线程锁，防止多个线程同时读或者写，使PLC造成拥堵
            {
                try
                {
                    njCompolet.WriteVariable(m_TickSign, -1);
                }
                catch
                {
                    if (OnLogEvent != null)
                        OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + m_TickSign + "=-1" + "失败1次！"));
                    try
                    {
                        njCompolet.WriteVariable(m_TickSign, -1);
                    }
                    catch
                    {
                        if (OnLogEvent != null)
                            OnLogEvent(new LogEventArgs("WARNING:写入PLC变量" + m_TickSign + "=-1" + "失败2次！"));
                        try
                        {
                            njCompolet.WriteVariable(m_TickSign, -1);
                        }
                        catch
                        {
                            if (OnLogEvent != null)
                                OnLogEvent(new LogEventArgs("ERR_B:写入PLC变量" + m_TickSign + "=-1" + "失败3次！"));
                        }

                    }
                }
            }
        }

    }
}
