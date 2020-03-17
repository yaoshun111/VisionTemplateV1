using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Threading;
using Editor;
using DataAction;
using BaseEvent;
using System.IO;

namespace FastCtr
{
    public delegate void FunctionHandle();
    public delegate void FunctionTaskEvent(object sender, FunctionTaskEventArgs args);
    
  
    partial class TaskActionCtr
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
            this.SuspendLayout();
            // 
            // TaskActionCtr
            // 
            this.Name = "TaskActionCtr";
            this.Size = new System.Drawing.Size(94, 44);
            //this.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskActionCtr_Paint);
            this.ResumeLayout(false);
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BackColor = Color.Gray;
            this.Text = "null";
            base.AutoSize = false;
        }

        #endregion

    }
    [Serializable]
    [DefaultEvent("DoubleClick")]
    public partial class TaskActionCtr : Label
    {
        public event TaskEventHandler Ontask1;
        public event TaskEventHandler Ontask2;
        public event TaskEventHandler Ontask3;

        object _parent = new object();
        EventInfo Eventinfo1;
        EventInfo Eventinfo2;
        EventInfo Eventinfo3;
        public object TaskInstance;
        AutoResetEvent autoreset = new AutoResetEvent(true);
        Task funtask;
        FunctionHandle function;
        //public event FunctionTaskEvent OnFunctionTaskOvered;
        public event FunctionTaskEvent OnFunctionTaskStarted;
        public event FunctionTaskEvent OnFunctionTaskFaulted;
        public event FunctionTaskEvent OnFunctionTaskInitialed;
        Stopwatch sw = new Stopwatch();
        public Exception Exp;
        bool _showinfo = false;
        bool _autosize = false;
        public CompilerResults CompilerRet;
        public CodeDomProvider objCodeDomProvider = CodeDomProvider.CreateProvider("CSharp");
        public CompilerParameters objCompilerParameters = new CompilerParameters();
        public MethodInfo methodMain;
       // public PropertyInfo MemberInfo;
        private string _file;
        public string UItext;
        
        public TaskActionCtr(string file)
        {
            base.BackColor = Color.Silver;
            base.DoubleClick += new EventHandler(OnDoubleClick);
        }
        public TaskActionCtr()
        {
            base.BackColor = Color.Silver;
            base.DoubleClick += new EventHandler(OnDoubleClick);
        }

        public void OnDoubleClick(object sender, EventArgs e)
        {
            this.Cursor= Cursors.WaitCursor;
            CodeEditor codeedit = new CodeEditor(FileSet, ((TaskActionCtr)sender).Name);
            codeedit.OnPathChanged += new EventHandler(codeedit_OnPathChanged);
            codeedit.Show();
            this.Cursor = Cursors.Arrow;
        }

        public void Init()
        {
            if (FileSet != null)
                FileSet = FileSet;
            else
                throw new Exception("任务绑定文件不存在！");
        }




        private void codeedit_OnPathChanged(object pathsender, EventArgs e)
        {
            FileSet = (string)pathsender;
        }
          
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string FileSet
        {
            get
            {

                Dictionary<string, string> dic = DataAction.SaveStatic.ReadBin("dictionary") as Dictionary<string, string>;
                if (!dic.ContainsKey(this.Name))
                {
                    return null;
                }
                string path = dic[this.Name];
                return path;
            }
            set
            {
                //try
                //{
                    lock (this)
                    {
                        int chaoshi = 0;
                        while (State == StateType.busy && chaoshi != 1000)
                        {
                            Thread.Sleep(10);
                            chaoshi++;
                        }
                        if (chaoshi == 1000)
                        {
                            MessageBox.Show("任务正在运行，写入数据失败！");
                            return;
                        }
                        else
                        {
                            //this.BackColor = Color.Silver;
                            _file = value;
                            objCompilerParameters = new CompilerParameters();
                            objCompilerParameters.GenerateExecutable = false;
                            objCompilerParameters.GenerateInMemory = true;
                            if (!File.Exists(_file))
                            {
                                throw new Exception(this.Name + ":文件" + _file + "不存在！");
                            }
                            string code = DataAction.SaveStatic.ReadAllTxt(_file);
                            string[] codeline = DataAction.SaveStatic.ReadTxt(_file);
                            foreach (string dd in codeline)
                            {
                                if (dd.Contains("importDll"))
                                {
                                    string dllpath = dd.Split(new string[1] { "importDll " }, System.StringSplitOptions.RemoveEmptyEntries)[1];
                                    objCompilerParameters.ReferencedAssemblies.Add(dllpath);
                                }
                            }
                            
                            CompilerRet = objCodeDomProvider.CompileAssemblyFromSource(objCompilerParameters, code);
                            Assembly objAssembly = CompilerRet.CompiledAssembly;
                            TaskInstance = objAssembly.CreateInstance("NameSpace.MainClass");
                         
                            methodMain = TaskInstance.GetType().GetMethod("Main");
                            Eventinfo1 = TaskInstance.GetType().GetEvent("OnEvent1");
                            if (Eventinfo1 != null)
                                Eventinfo1.AddEventHandler(TaskInstance, new TaskEventHandler(TaskInstance_OnEvent1));
                            Eventinfo2 = TaskInstance.GetType().GetEvent("OnEvent2");
                            if (Eventinfo2 != null)
                                Eventinfo2.AddEventHandler(TaskInstance, new TaskEventHandler(TaskInstance_OnEvent2));
                            Eventinfo3 = TaskInstance.GetType().GetEvent("OnEvent3");
                            if (Eventinfo3 != null)
                                Eventinfo3.AddEventHandler(TaskInstance, new TaskEventHandler(TaskInstance_OnEvent3));
                            Function = new FunctionHandle(() =>
                            {
                                methodMain.Invoke(TaskInstance, null);
                            });
                            //Thread.Sleep(100);
                            //if (OnFunctionTaskInitialed != null)
                            //    OnFunctionTaskInitialed(this, new FunctionTaskEventArgs(0));
                            //this.BackColor = Color.Red;
                        }
                    }
                    Dictionary<string, string> dic = DataAction.SaveStatic.ReadBin("dictionary") as Dictionary<string, string>;
                    if (!dic.ContainsKey(this.Name))
                    {
                        dic.Add(this.Name, _file);
                    }
                    else
                    {
                        dic[this.Name] = _file;
                    }
                    DataAction.SaveStatic.SaveBin("dictionary", dic);
                   // MessageBox.Show("File:" + "生成并绑定成功！");
                //}
                //catch (Exception exp)
                //{
                //    MessageBox.Show("File:"+exp.Message);
                //}
            }
        }
        

        private void TaskInstance_OnEvent1(object sender)
        {
            if (Ontask1 != null)
                Ontask1(sender);
        }

        private void TaskInstance_OnEvent2(object sender)
        {
            if (Ontask2 != null)
                Ontask2(sender);
        }

        private void TaskInstance_OnEvent3(object sender)
        {
            if (Ontask3 != null)
                Ontask3(sender);
        }

        public new bool AutoSize
        {
            get
            {
                base.AutoSize = _autosize;
                return _autosize;
            }
            set
            {
                _autosize = value;
                base.AutoSize = _autosize;
            }
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
            }
        }
        public bool IsIdle //任务是否空闲
        {
            get
            {
                if (State == StateType.idle)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public long ContemporaryRunningTimeCount  //当前运行时间计时
        {
            
            get
            {
                return sw.ElapsedMilliseconds;
            }
        }

        public Exception Exception 
        {
            get
            {
                return Exp;
            }
        }





        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public FunctionHandle Function
        {
            
            set
            {
                function = value;
              
                    if (State != StateType.busy)
                    {
                        funtask = new Task(new Action(function.Invoke), TaskCreationOptions.LongRunning);

                        Exp = new Exception("");
                        //if (ShowInfo)
                        //    this.BackColor = Color.Red;
                        //if (ShowInfo)
                        //this.BeginInvoke(new Action(() =>
                        //{
                        //    this.Text = ;
                        //}));
                        UItext = this.Name;

                        Thread.Sleep(100);
                        if (OnFunctionTaskInitialed != null)
                            OnFunctionTaskInitialed(this, new FunctionTaskEventArgs(0));
                    }
                    else
                    {
                        throw new Exception("任务运行中，无法重置任务");
                    }
            
            }
            get
            {
                return function;
            }
        }






        public void Start()
        {
            lock (this)
            {
                if (State == StateType.idle)
                {
                    funtask = new Task(new Action(() =>
                        {
                            try
                            {
                                function.Invoke();
                            }
                            catch (Exception exp)
                            {
                                Exp = exp;
                                throw exp;
                            }
                        }
                    ), TaskCreationOptions.LongRunning);
                    funtask.Start();
                    //if (ShowInfo)
                    //    //this.BackColor = Color.LimeGreen;
                    sw.Restart();
                    Thread.Sleep(100);
                    if (OnFunctionTaskStarted != null)
                    {
                        OnFunctionTaskStarted(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                    }


                    funtask.ContinueWith(new Action<Task>(t =>
                    {
                       
                        sw.Stop();
                        if (State == StateType.idle)
                        {
                            
                            //if (ShowInfo)
                            //    this.BackColor = Color.Red;
                            Thread.Sleep(100);
                            if (OnFunctionTaskInitialed != null)
                                OnFunctionTaskInitialed(this, new FunctionTaskEventArgs(0));
                           
                        }
                        else if (State == StateType.faulted)
                        {
                            
                            //if (ShowInfo)
                            //   // this.BackColor = Color.Yellow;
                            //if (ShowInfo)
                                //this.BeginInvoke(new Action(() =>
                                //    {
                                //        this.Text = this.Name + Exp.Message;
                                //    }));
                            UItext = this.Name + Exp.Message;
                            Thread.Sleep(100);
                            if (OnFunctionTaskFaulted != null)
                            {
                                OnFunctionTaskFaulted(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                            }
                        }
                        else
                        {
                            throw new Exception(Name + ":未知任务状态");
                        }
                    }));
                }
                else
                {
                    throw new Exception(Name+":任务当前状态无法启动");
                }
            }
        }

        public void ReSet()
        {
            if (State != StateType.busy)
            {
                funtask = new Task(new Action(function.Invoke));
                
                Exp = new Exception("");
                //if (ShowInfo)
                //    this.BackColor = Color.Red;
                //if (ShowInfo)
                //    this.BeginInvoke(new Action(() =>
                //    {
                //        this.Text = this.Name;
                //    }));
                UItext = this.Name;
            }
            else
            {
                throw new Exception("任务运行中，无法重置任务");
            }
        }

        public object GetVarible(string varname)
        {
            if (TaskInstance == null)
                return null;
            FieldInfo property = TaskInstance.GetType().GetField(varname);
            try
            {
                var value = property.GetValue(TaskInstance);
                return value;
            }
            catch (Exception exp)
            {
                throw new Exception("获取变量" + varname + "失败！\r\n" + exp.ToString());
            }
        }

        public object GetProperty(string varname)
        {
            if (TaskInstance == null)
                return null;
            PropertyInfo property = TaskInstance.GetType().GetProperty(varname);
            var value = property.GetValue(TaskInstance);
            return value;
        }



        public List<string> GetAllVaribles()
        {
            if (TaskInstance == null)
                return null;
            List<string> varnames = new List<string>();
            FieldInfo[]  field = TaskInstance.GetType().GetFields();
            foreach(FieldInfo property in  field)
            {
                var name = property.Name;
                varnames.Add(name);
            }
            return varnames;
        }


        //public List<string> GetBoolVaribles()
        //{
        //    if (TaskInstance == null)
        //        return null;
        //    List<string> varnames = new List<string>();
        //    FieldInfo[] field = TaskInstance.GetType().GetFields();
        //    foreach (FieldInfo property in field)
        //    {
        //        if(property.FieldType==typeof(bool))
        //        {
        //            var name = property.Name;
        //            varnames.Add(name);
        //        }
        //    }
        //    return varnames;
        //}

        public List<string> GetBoolVaribles()
        {
            if (TaskInstance == null)
                return null;
            List<string> varnames = new List<string>();
            FieldInfo[] field = TaskInstance.GetType().GetFields();
            foreach (FieldInfo property in field)
            {
                if (property.FieldType == typeof(bool))
                {
                    var name = property.Name;
                    varnames.Add(name);
                }
            }
            return varnames;
        }



        public void SetVarible(string varname, object value)
        {
            try
            {
                if (TaskInstance == null)
                    throw new Exception("实例不能为空。");
                Type t = TaskInstance.GetType();
                FieldInfo[] fInfos = t.GetFields();
                FieldInfo fInfo = t.GetField(varname);
                fInfo.SetValue(TaskInstance, value);
            }
            catch (Exception exp)
            {
                throw new Exception("设置变量" + varname + "失败！" + exp.ToString());
            }
        }

        public void SetVarible(object instance,string varname, object value)
        {
            if (instance == null)
                return;
            Type t = instance.GetType();
            FieldInfo[] fInfos = t.GetFields();
            FieldInfo fInfo = t.GetField(varname);
            fInfo.SetValue(instance, value);
        }


        public void SetProperty(string varname, object value)
        {
            try
            {
                if (TaskInstance == null)
                    throw new Exception("实例不能为空。");
                Type t = TaskInstance.GetType();
                PropertyInfo pInfo = t.GetProperty(varname);
                pInfo.SetValue(TaskInstance, value);
            }
            catch (Exception exp)
            {
                throw new Exception("设置属性" + varname + "失败！" + exp.ToString());
            }
        }


        public MethodInfo[] GetMethods()
        {
            if (TaskInstance == null)
                return null;
            Type t = TaskInstance.GetType();
            MethodInfo[] mInfo = t.GetMethods();
            return mInfo;
        }




        public StateType State
        {
            get
            {
                if (funtask != null)
                {

                    if (funtask.Status == TaskStatus.RanToCompletion || funtask.Status == TaskStatus.Canceled || funtask.Status == TaskStatus.Created || funtask.Status == TaskStatus.WaitingForActivation || funtask.Status == TaskStatus.WaitingToRun)
                    {
                        return StateType.idle;
                    }
                    else if (funtask.Status == TaskStatus.Running || funtask.Status == TaskStatus.WaitingForChildrenToComplete)
                    {
                        return StateType.busy;
                    }
                    else if (funtask.Status == TaskStatus.Faulted)
                    {
                        return StateType.faulted;
                    }
                    else
                    {
                        throw new Exception("意外的状态:" + funtask.Status.ToString());
                    }
                }
                else
                {
                    return StateType.NULL;
                }
            }
        }


        public void UIshow(Control ctr)
        {
            if (ctr.IsHandleCreated)
            {
                
                ctr.BeginInvoke(new Action(() =>
                {
                    switch (State)
                    {
                        case StateType.busy:
                            ctr.BackColor = Color.LimeGreen;
                            ctr.Text = this.Name + ":任务进行中";
                            break;
                        case StateType.faulted:
                            ctr.BackColor = Color.Yellow;
                            ctr.Text = this.Name + ":任务出错,点击查看";
                            break;
                        case StateType.idle:
                            ctr.BackColor = Color.Red;
                            ctr.Text = this.Name + ":任务空闲";
                            break;
                        case StateType.NULL:
                            ctr.BackColor = Color.Silver;
                            ctr.Text = this.Name + ":NULL";
                            break;
                    }
                }));
            }
        }




        public void UIshowSelf()
        {
            switch (State)
            {
                case StateType.busy:
                    this.BackColor = Color.LimeGreen;
                    this.Text = this.Name + ":任务进行中";
                    break;
                case StateType.faulted:
                    this.BackColor = Color.Yellow;
                    this.Text = this.Name + ":任务出错" + Environment.NewLine + Exp.Message;
                    break;
                case StateType.idle:
                    this.BackColor = Color.Red;
                    this.Text = this.Name + ":任务空闲";
                    break;
                case StateType.NULL:
                    this.BackColor = Color.Silver;
                    this.Text = this.Name + ":NULL";
                    break;
            }
        }
    }





    public enum StateType
    {
        busy = 1,
        idle = 0,
        faulted = -1,
        NULL = -2
    }

    public class FunctionTaskEventArgs
    {
        public readonly long totalruntimes = 0;
        public FunctionTaskEventArgs(long _totalruntimes)
        {
            totalruntimes = _totalruntimes;
        }
    }

}
