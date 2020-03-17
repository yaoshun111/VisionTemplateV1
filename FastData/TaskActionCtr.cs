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

namespace TaskAction
{
    public delegate void FunctionHandle();
    public delegate void FunctionTaskEvent(object sender, FunctionTaskEventArgs args);
    public delegate void DoubleEventhandle(object sender1, object sender2);
  
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
        


        AutoResetEvent autoreset = new AutoResetEvent(true);
        Task funtask;
        FunctionHandle function;
        public event FunctionTaskEvent OnFunctionTaskOvered;
        public event FunctionTaskEvent OnFunctionTaskStarted;
        public event FunctionTaskEvent OnFunctionTaskFaulted;
        Stopwatch sw = new Stopwatch();
        Exception Exp;
        bool _showinfo = false;
        bool _autosize = false;
        public CompilerResults CompilerRet;
        public CodeDomProvider objCodeDomProvider = CodeDomProvider.CreateProvider("CSharp");
        public CompilerParameters objCompilerParameters = new CompilerParameters();
        public MethodInfo methodInfo;
        private string _file;
        
        public TaskActionCtr(string file)
        {
            base.BackColor = Color.Silver;

           
        }
        public TaskActionCtr()
        {
            base.BackColor = Color.Silver;
            base.DoubleClick += new EventHandler(OnDoubleClick);
        }

        public void OnDoubleClick(object sender, EventArgs e)
        {
            this.Cursor= Cursors.WaitCursor;
            CodeEditor codeedit = new CodeEditor(sender);
            //CodeEditor codeedit = new CodeEditor("C:\\Users\\yaoshun\\Desktop\\9y.cs",sender);
            codeedit.ShowDialog();
            this.Cursor = Cursors.Arrow;

        }


         
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                try
                {
                    lock (this)
                    {
                      int chaoshi=0;
                        while(State==FunctionTaskState.busy&&chaoshi!=1000)
                        {
                            Thread.Sleep(10);
                            chaoshi++;
                        }
                        if (chaoshi ==1000)
                        {
                            MessageBox.Show("任务正在运行，写入数据失败！");
                        }
                        else
                        {
                            this.BackColor = Color.Silver;
                            _file = value;
                            objCompilerParameters.GenerateExecutable = false;
                            objCompilerParameters.GenerateInMemory = true;
                            string code = DataAction.Save.ReadAllTxt(_file);
                            string[] codeline = DataAction.Save.ReadTxt(_file);
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
                            object objInstance = objAssembly.CreateInstance("NameSpace.MainClass");
                            methodInfo = objInstance.GetType().GetMethod("Main");
                            Function = new FunctionHandle(() =>
                            {
                                methodInfo.Invoke(objInstance, null);
                            });
                            this.BackColor = Color.Red;
                        }
                    }
                    Dictionary<string, string> dic = DataAction.Save.ReadBin("dictionary") as Dictionary<string, string>;
                    if (!dic.ContainsKey(this.Name))
                    {
                        dic.Add(this.Name, _file);
                    }
                    else
                    {
                        dic[this.Name] = _file;
                    }
                    DataAction.Save.SaveBin("dictionary", dic);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

            }
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
                if (State == FunctionTaskState.idle)
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
                if (State != FunctionTaskState.busy)
                {
                    funtask = new Task(new Action(function.Invoke));
                    
                    Exp = new Exception("");
                    if (ShowInfo)
                        this.BackColor = Color.Red;
                    if (ShowInfo)
                        this.BeginInvoke(new Action(() =>
                        {
                            this.Text = this.Name;
                        }));
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
                if (State == FunctionTaskState.idle)
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
                    if (ShowInfo)
                        this.BackColor = Color.LimeGreen;
                    sw.Restart();

                    if (OnFunctionTaskStarted != null)
                    {
                        OnFunctionTaskStarted(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                    }


                    funtask.ContinueWith(new Action<Task>(t =>
                    {
                       
                        sw.Stop();
                        if (State == FunctionTaskState.idle)
                        {
                            
                            if (ShowInfo)
                                this.BackColor = Color.Red;
                            if (OnFunctionTaskOvered != null)
                            {
                                OnFunctionTaskOvered(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                            }
                        }
                        else if (State == FunctionTaskState.faulted)
                        {
                            
                            if (ShowInfo)
                                this.BackColor = Color.Yellow;
                            if (ShowInfo)
                                this.BeginInvoke(new Action(() =>
                                    {
                                        this.Text = this.Name + Exp.Message;
                                    }));

                            if (OnFunctionTaskFaulted != null)
                            {
                                OnFunctionTaskFaulted(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                            }
                        }
                        else
                        {
                            throw new Exception("未知任务状态");
                        }
                    }));
                }
                else
                {
                    throw new Exception("任务当前状态无法启动");
                }
            }
        }

        public void ReSet()
        {
            if (State != FunctionTaskState.busy)
            {
                funtask = new Task(new Action(function.Invoke));
                
                Exp = new Exception("");
                if (ShowInfo)
                    this.BackColor = Color.Red;
                if (ShowInfo)
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Text = this.Name;
                    }));
            }
            else
            {
                throw new Exception("任务运行中，无法重置任务");
            }
        }


        public FunctionTaskState State
        {
            get
            {
                if (funtask != null)
                {

                    if (funtask.Status == TaskStatus.RanToCompletion || funtask.Status == TaskStatus.Canceled || funtask.Status == TaskStatus.Created || funtask.Status == TaskStatus.WaitingForActivation || funtask.Status == TaskStatus.WaitingToRun)
                    {
                        return FunctionTaskState.idle;
                    }
                    else if (funtask.Status == TaskStatus.Running || funtask.Status == TaskStatus.WaitingForChildrenToComplete)
                    {
                        return FunctionTaskState.busy;
                    }
                    else if (funtask.Status == TaskStatus.Faulted)
                    {
                        return FunctionTaskState.faulted;
                    }
                    else
                    {
                        throw new Exception("意外的状态:" + funtask.Status.ToString());
                    }
                }
                else
                {
                    return FunctionTaskState.NULL;
                }
            }
        }

        
        



    }





    public enum FunctionTaskState
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
