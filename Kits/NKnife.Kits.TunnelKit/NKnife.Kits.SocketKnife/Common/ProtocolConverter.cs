using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.Common
{
    [ValueConversion(typeof (StringProtocol), typeof (string))]
    public class ProtocolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var protocol = value as StringProtocol;
            return protocol != null ? String.Format("[{0}]{1}", protocol.Command, protocol.CommandParam) : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
