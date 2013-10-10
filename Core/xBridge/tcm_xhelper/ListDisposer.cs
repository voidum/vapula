using System;
using System.Collections.Generic;
using System.Threading;

namespace TCM
{
    public class ListDisposer
    {
        private int _ScanSpan = 50;
        private bool _Stop = false;
        private object _Lock = new object();
        private List<IDisposable> _QueueObjects = new List<IDisposable>();
        private List<Func<IDisposable, bool>> _WaitActions = new List<Func<IDisposable, bool>>();
        private Thread _Thread = null;

        /// <summary>
        /// 构造自动释放器
        /// </summary>
        public ListDisposer(int scan = 50) 
        {
            _ScanSpan = scan;
        }

        /// <summary>
        /// 添加待释放目标
        /// </summary>
        public void Add(IDisposable target, Func<IDisposable, bool> wait)
        {
            _QueueObjects.Add(target);
            _WaitActions.Add(wait);
        }

        /// <summary>
        /// 启动自动释放器
        /// </summary>
        public void Run()
        {
            _Thread = new Thread(new ThreadStart(ThreadProc_Dispose));
            _Thread.Start();
        }

        /// <summary>
        /// 停止自动释放器
        /// </summary>
        public void Stop()
        {
            _Stop = true;
        }

        private void ThreadProc_Dispose()
        {
            while (true)
            {
                Thread.Sleep(50);
                lock (_Lock)
                {
                    if (_QueueObjects.Count == 0) continue;
                    List<int> rmv_ids = new List<int>();
                    for (int i = 0; i < _QueueObjects.Count; i++)
                    {
                        bool ret = _WaitActions[i].Invoke(_QueueObjects[i]);
                        if (ret)
                        {
                            _QueueObjects[i].Dispose();
                            rmv_ids.Add(i);
                        }
                    }
                    foreach (int i in rmv_ids) 
                    {
                        _QueueObjects.RemoveAt(i);
                        _WaitActions.RemoveAt(i);
                    }
                    if (_Stop) break;
                }
            }
        }
    }
}
