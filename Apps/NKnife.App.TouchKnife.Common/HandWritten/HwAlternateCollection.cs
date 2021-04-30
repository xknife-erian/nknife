using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Ink;
using Common.Logging;
using NKnife.IoC;

namespace NKnife.App.TouchInputKnife.Commons.HandWritten
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class HwAlternateCollection : ObservableCollection<string>
    {
        private static readonly ILog _logger = LogManager.GetLogger<HwAlternateCollection>();

        private Visibility _HasAlternates = Visibility.Hidden;

        public HwAlternateCollection()
        {
            _logger.Info(string.Format("HwAlternateCollection实例正常"));
        }

        /// <summary>
        /// 针对指的墨迹集合进行识别
        /// </summary>
        /// <param name="sc">指定的墨迹集合</param>
        public void Recognize(StrokeCollection sc)
        {
            int i = 0;
            string[] alts = DI.Get<ICharactorRecognizer>().Recognize(sc);
            ClearAlternates();
            foreach (string alt in alts)//将识别出来的字添加到集合中
            {
                Add(alt);
                if (i == 0)
                {
                    HasAlternates = Visibility.Visible;
                }
                i++;
            }
        }

        /// <summary>
        ///     清空候选词
        /// </summary>
        public void ClearAlternates()
        {
            Clear();
            HasAlternates = Visibility.Hidden;
        }

        public Visibility HasAlternates
        {
            get { return _HasAlternates; }
            set
            {
                _HasAlternates = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HasAlternates"));
            }
        }
    }
}