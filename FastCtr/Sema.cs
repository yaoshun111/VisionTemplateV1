using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastCtr
{

    public partial class Sema : Component
    {
        Semaphore semaphore;
        int _initialCount = 0;
        int _maximumCount = 1;
        string _semaname = "sema";

        public Sema()
        {
            InitializeComponent();
            semaphore = new Semaphore(_initialCount, _maximumCount, _semaname);
        }


        public int initialCount
        {
            get { return _initialCount; }
            set { _initialCount = value; semaphore = new Semaphore(_initialCount, _maximumCount, _semaname); }
        }
        public int maximumCount
        {
            get { return _maximumCount; }
            set { _maximumCount = value; semaphore = new Semaphore(_initialCount, _maximumCount, _semaname); }
        }

        public string semaname
        {
            get { return _semaname; }
            set { _semaname = value; semaphore = new Semaphore(_initialCount, _maximumCount, _semaname); }
        }

        public static void Create(string _semaname,int _initialCount,int  _maximumCount)
        {
            Semaphore semaphore = new Semaphore(_initialCount, _maximumCount, _semaname);
        }


        public static void Release(string _semaname)
        {
            try
            {
                Semaphore sema = Semaphore.OpenExisting(_semaname);
                sema.Release(1);
            }
            catch
            {
                throw new Exception("不存在的信号量" + _semaname);
            }
        }

        public static void Waitone(string _semaname)
        {
            try
            {
                Semaphore sema = Semaphore.OpenExisting(_semaname);
                sema.WaitOne();
            }
            catch
            {
                throw new Exception("不存在的信号量" + _semaname);
            }
        }

        public static bool Waitone(string _semaname, int millisecondsTimeout)
        {
            try
            {
                Semaphore sema = Semaphore.OpenExisting(_semaname);
                return sema.WaitOne(millisecondsTimeout);
            }
            catch
            {
                throw new Exception("不存在的信号量" + _semaname);
            }
        }
    }




}
