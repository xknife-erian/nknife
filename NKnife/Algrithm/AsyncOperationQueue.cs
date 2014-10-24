using System.Collections.Concurrent;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace NKnife.Algrithm
{
    public class AsyncOperationQueue
    {
        private static readonly object _Lock = new object();
        private readonly Timer _CheckOperationTmr = new Timer();
        private readonly Timer _CheckStatusTmr = new Timer();
        private readonly ConcurrentQueue<AsyncOperation> _OperationQueue = new ConcurrentQueue<AsyncOperation>();
        private readonly ManualResetEvent _Reset = new ManualResetEvent(false);

        private AsyncOperation _CurrentOperation;

        #region ����

        private static AsyncOperationQueue _Instance;

        private AsyncOperationQueue()
        {
            _CheckStatusTmr.Interval = 500;
            _CheckStatusTmr.Elapsed += CheckStatusTmrElapsed;
            _CheckStatusTmr.Start();

            _CheckOperationTmr.Interval = 200;
            _CheckOperationTmr.Elapsed += CheckOperationTmrElapsed;
            _CheckOperationTmr.Start();
        }

        public static AsyncOperationQueue Instance()
        {
            lock (_Lock)
            {
                return _Instance ?? (_Instance = new AsyncOperationQueue());
            }
        }

        #endregion

        #region �ڲ�ѭ��

        /// <summary>����Ƿ����µ��첽����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOperationTmrElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_OperationQueue.TryDequeue(out _CurrentOperation))
            {
                return;
            }

            _CheckOperationTmr.Stop();
            _Reset.Reset();
            if (!_Reset.WaitOne(_CurrentOperation.OperationTimeout)) //û�յ��źţ���ʱ
            {
                _CurrentOperation.OperationResultArrived(new OperationResult(1, "��ʱ"));
                _CurrentOperation = null;
            }
            else //�յ��ź�
            {
                _CurrentOperation.OperationResultArrived(new OperationResult(0, "�ɹ�"));
                _CurrentOperation = null;
            }
            _CheckOperationTmr.Start();
        }


        /// <summary>��鵱ǰ���õĽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStatusTmrElapsed(object sender, ElapsedEventArgs e)
        {
            if (_CurrentOperation == null) return;
            if (_CurrentOperation.CheckStatusFunc(_CurrentOperation.Id)) //�ﵽԤ���״̬��
            {
                _Reset.Set();
            }
        }

        #endregion

        /// <summary>�����첽���ã���һ������ִ�У��õȵ�ǰ�첽������ɺ�ſ�ʼ��
        /// </summary>
        /// <param name="asyncOperation"></param>
        public void Enter(AsyncOperation asyncOperation)
        {
            _OperationQueue.Enqueue(asyncOperation);
        }

        /// <summary>����Ƿ����첽�������ڽ���
        /// </summary>
        /// <returns></returns>
        public bool AnyOperation()
        {
            return _CurrentOperation != null;
        }
    }
}