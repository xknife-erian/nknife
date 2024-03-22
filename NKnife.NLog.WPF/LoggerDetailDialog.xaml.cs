using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using NLog;

namespace LEIAO.Mercury.Views.Dialogs;

/// <summary>
///     LoggerDetailDialog.xaml 的交互逻辑
/// </summary>
public partial class LoggerDetailDialog : Window
{
    public LoggerDetailDialog()
    {
        InitializeComponent();
    }
}

public class LpLevel2ColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is LogLevel level && parameter is string p)
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

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

}

public class LpMessageClearConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string log)
            return log.Replace("\r\n", " ");
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class LpLogger2ToolTipConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is LogEventInfo log)
            return $"{log.TimeStamp}\n{log.Level}\n{log.FormattedMessage}\n{log.LoggerName}";
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class LpTimeStamp2StringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
            return dt.ToString("HH:mm:ss fff");
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class LpSimplifyLoggerNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string caller)
        {
            if (caller.Contains("Leo.Tool.EngineerBox"))
                return caller.Replace("Leo.Tool.EngineerBox.", "");
            if (caller.Contains("Leo.Kernel"))
                return caller.Replace("Leo.Kernel.", "");
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}