using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using NLog;

namespace NKnife.NLog.WPF
{
    /// <summary>
    ///     LoggerPane.xaml 的交互逻辑
    /// </summary>
    public partial class LoggerPane : UserControl
    {
        public LoggerPane()
        {
            InitializeComponent();
        }

        public bool AutoScrollToLast { get; set; }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void ScrollToFirst()
        {
            throw new NotImplementedException();
        }

        public void ScrollToLast()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<LogEventInfoEventArgs> ItemAdded;
    }

    public class LogEventInfoEventArgs : EventArgs
    {
        public LogEventInfoEventArgs(LogEventInfo logEventInfo)
        {
            LogEventInfo = logEventInfo;
        }

        public LogEventInfo LogEventInfo { get; }
    }

    public class LogLevel2VisibilityConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            return Visibility.Visible;
            /*if(value is LogLevel level
               && parameter is string p)
            {
                var ordinal = level.Ordinal;

                switch (p)
                {
                    case "Trace":
                        return ordinal >= 5 ? Visibility.Visible : Visibility.Collapsed;
                    case "Debug":
                        return ordinal >= 4 ? Visibility.Visible : Visibility.Collapsed;
                    case "Info":
                        return ordinal >= 3 ? Visibility.Visible : Visibility.Collapsed;
                    case "Warn":
                        return ordinal >= 2 ? Visibility.Visible : Visibility.Collapsed;
                    case "Error":
                        return ordinal >= 1 ? Visibility.Visible : Visibility.Collapsed;
                    case "Fatal":
                        return ordinal >= 0 ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;*/
        }

        public object ConvertBack(object? value,
                                  Type targetType,
                                  object? parameter,
                                  CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    public class LpLogSourceConverter: IValueConverter {
        public object? Convert(object? value,
                              Type targetType,
                              object? parameter,
                              CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object? value,
                                  Type targetType,
                                  object? parameter,
                                  CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LpLevel2ColorConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is LogLevel level
               && parameter is string p)
            {
                var ordinal = level.Ordinal;

                switch (p)
                {
                    case "Foreground":
                        switch (ordinal)
                        {
                            case 5:
                            case 4:
                                return Brushes.White;
                            case 1:
                                return Brushes.DimGray;
                            case 0:
                                return Brushes.DarkGray;
                            case 3:
                            case 2:
                            default:
                                return SystemColors.ControlTextBrush;
                        }
                    case "Background":
                        switch (ordinal)
                        {
                            case 5:
                                return Brushes.DarkRed;
                            case 4:
                                return Brushes.OrangeRed;
                            case 3:
                                return Brushes.Gold;
                            case 2:
                            case 1:
                            case 0:
                            default:
                                return SystemColors.ControlLightLightBrush;
                        }
                }
            }

            return Brushes.Gray;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LpMessageClearConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is string log)
                return log.Replace("\r\n", " ");

            return value;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LpLogger2ToolTipConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is LogEventInfo log)
                return $"{log.TimeStamp}\n{log.Level}\n{log.FormattedMessage}\n{log.LoggerName}";

            return value;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LpTimeStamp2StringConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is DateTime dt)
                return dt.ToString("HH:mm:ss fff");

            return string.Empty;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LpSimplifyLoggerNameConverter : IValueConverter
    {
        public object? Convert(object? value,
                               Type targetType,
                               object? parameter,
                               CultureInfo culture)
        {
            if(value is string caller)
            {
                if(caller.Contains("Leo.Tool.EngineerBox"))
                    return caller.Replace("Leo.Tool.EngineerBox.", "");
                if(caller.Contains("Leo.Kernel"))
                    return caller.Replace("Leo.Kernel.", "");
            }

            return value;
        }

        public object? ConvertBack(object? value,
                                   Type targetType,
                                   object? parameter,
                                   CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}