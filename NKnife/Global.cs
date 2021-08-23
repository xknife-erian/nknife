using System;

namespace NKnife
{
    /// <summary>
    ///     软件全局属性设置静态类。类中的属性供软件整体调用。
    /// </summary>
    public static class Global
    {
        #region Culture：当前软件的文化语言标记

        private static string _culture = "zh-CN";

        /// <summary>
        ///     当前软件的文化语言标记
        /// </summary>
        public static string Culture
        {
            get => _culture;
            set
            {
                if (_culture != value)
                {
                    _culture = value;
                    OnCultureChanged();
                }
            }
        }

        /// <summary>
        /// 当当前软件的文化语言标记的设置值发生变化时
        /// </summary>
        public static event EventHandler<EventArgs> CultureChanged;

        private static void OnCultureChanged()
        {
            CultureChanged?.Invoke(null, EventArgs.Empty);
        }

        #endregion
    }
}