using System;
using System.Windows.Forms;
using NKnife.Adapters;
using NKnife.IoC;

namespace NKnife.Domains
{
    public class AppDomainFirster
    {
        public Func<string[], ApplicationContext> Context { get; set; }

        /// <summary>
        /// ����������Starter��Ŀͨ��������ص��á�
        /// </summary>
        public virtual void RunMainMethod(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();

            var logger = LogFactory.GetCurrentClassLogger();
            logger.Info("IoC��ܵĳ�ʼ����ɡ�");

            //������ǰ�����������µ� ApplicationContext ʵ��
            Application.Run(Context.Invoke(args));
        }
    }
}