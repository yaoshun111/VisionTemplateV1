using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TaskAction
{
    partial class TaskActionCom
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

    public partial class TaskActionCom :Component
    {
        Task funtask;
        FunctionHandle function;
        public event FunctionTaskEvent OnFunctionTaskOvered;
        public event FunctionTaskEvent OnFunctionTaskStarted;
        public event FunctionTaskEvent OnFunctionTaskFaulted;
        Stopwatch sw = new Stopwatch();
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

        public TaskActionCom()
        {
            InitializeComponent();
        }

        public TaskActionCom(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            
        }

        public FunctionHandle Function
        {
            set
            {
                function = value;
                if (State != FunctionTaskState.busy)
                {
                    funtask = new Task(new Action(function.Invoke));
                }
                else
                {
                    throw new Exception("任务运行中，无法重置任务");
                    
                }
            }
        }

        public void Start()
        {
            if (State == FunctionTaskState.idle)
            {
                funtask = new Task(new Action(function.Invoke));
                funtask.Start();
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
                        if (OnFunctionTaskOvered != null)
                        {
                            OnFunctionTaskOvered(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                        }
                    }
                    else if (State == FunctionTaskState.faulted)
                    {
                        if (OnFunctionTaskFaulted != null)
                        {
                            OnFunctionTaskFaulted(this, new FunctionTaskEventArgs(sw.ElapsedMilliseconds));
                        }
                    }
                }));
            }
            else
            {
                throw new Exception("任务当前状态无法启动");
            }
        }

        public void ReSet()
        {
            if (State != FunctionTaskState.busy)
                funtask = new Task(new Action(function.Invoke));
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
}
