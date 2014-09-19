using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class GetCHSTextforInsertTable
    {
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:table.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:table.unit.unitper}");
                return unit;
            }
        }
        public string[] CaptionAlign
        {
            get
            {
                string[] calign = new string[4];
                calign[0] = StringParserService.Parse("${res:table.captionalign.top}");
                calign[1] = StringParserService.Parse("${res:table.captionalign.bottom}");
                calign[2] = StringParserService.Parse("${res:table.captionalign.left}");
                calign[3] = StringParserService.Parse("${res:table.captionalign.right}");
                return calign;
            }
        }
    }
    public class GetCHSTextforInsertImage
    {
        public string[] Align
        {
            get
            {
                string[] calign = new string[10];
                calign[0] = StringParserService.Parse("${res:image.align.Default}");
                calign[1] = StringParserService.Parse("${res:image.align.BaseLine}");
                calign[2] = StringParserService.Parse("${res:image.align.Top}");
                calign[3] = StringParserService.Parse("${res:image.align.Middle}");
                calign[4] = StringParserService.Parse("${res:image.align.Bottom}");
                calign[5] = StringParserService.Parse("${res:image.align.TextTop}");
                calign[6] = StringParserService.Parse("${res:image.align.AbsoluteMiddle}");
                calign[7] = StringParserService.Parse("${res:image.align.AbsoluteBottom}");
                calign[8] = StringParserService.Parse("${res:image.align.Left}");
                calign[9] = StringParserService.Parse("${res:image.align.Right}");
                return calign;
            }
        }
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:image.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:image.unit.unitper}");
                return unit;
            }
        }
    }
    public class GetCHSTextforInsertMedia
    {
        public string[] Align
        {
            get
            {
                string[] calign = new string[10];
                calign[0] = StringParserService.Parse("${res:flash.FlashAlign.Default}");
                calign[1] = StringParserService.Parse("${res:flash.FlashAlign.BaseLine}");
                calign[2] = StringParserService.Parse("${res:flash.FlashAlign.Top}");
                calign[3] = StringParserService.Parse("${res:flash.FlashAlign.Middle}");
                calign[4] = StringParserService.Parse("${res:flash.FlashAlign.Bottom}");
                calign[5] = StringParserService.Parse("${res:flash.FlashAlign.TextTop}");
                calign[6] = StringParserService.Parse("${res:flash.FlashAlign.AbsoluteMiddle}");
                calign[7] = StringParserService.Parse("${res:flash.FlashAlign.AbsoluteBottom}");
                calign[8] = StringParserService.Parse("${res:flash.FlashAlign.Left}");
                calign[9] = StringParserService.Parse("${res:flash.FlashAlign.Right}");
                return calign;
            }
        }
        public string[] FlashQuality
        {
            get
            {
                string[] Quality = new string[4];
                Quality[0] = StringParserService.Parse("${res:flash.FlashQuality.Hight}");
                Quality[1] = StringParserService.Parse("${res:flash.FlashQuality.Low}");
                Quality[2] = StringParserService.Parse("${res:flash.FlashQuality.AutoHight}");
                Quality[3] = StringParserService.Parse("${res:flash.FlashQuality.AutoLow}");
                return Quality;
            }
        }
        public string[] Scale
        {
            get
            {
                string[] scale = new string[3];
                scale[0] = StringParserService.Parse("${res:flash.scale.default}");
                scale[1] = StringParserService.Parse("${res:flash.scale.noborder}");
                scale[2] = StringParserService.Parse("${res:flash.scale.match}");
                return scale;
            }
        }
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitper}");
                return unit;
            }
        }
    }
    public class GetCHSTextforInsertDateTime
    {
        public string[] DayFormat
        {
            get
            {
                string[] day = new string[5];
                day[0] = StringParserService.Parse("${res:date.dayformat.f0}");
                day[1] = StringParserService.Parse("${res:date.dayformat.f1}");
                day[2] = StringParserService.Parse("${res:date.dayformat.f2}");
                day[3] = StringParserService.Parse("${res:date.dayformat.f3}");
                day[4] = StringParserService.Parse("${res:date.dayformat.f4}");
                return day;
            }
        }
        public string[] TimeFormat
        {
            get
            {
                string[] time = new string[3];
                time[0] = StringParserService.Parse("${res:date.timeformat.f0}");
                time[1] = StringParserService.Parse("${res:date.timeformat.f1}");
                time[2] = StringParserService.Parse("${res:date.timeformat.f2}");
                return time;
            }
        }
        public string[] DateFormat
        {
            get
            {
                string[] date = new string[10];
                date[0] = StringParserService.Parse("${res:date.dateformat.f0}");
                date[1] = StringParserService.Parse("${res:date.dateformat.f1}");
                date[2] = StringParserService.Parse("${res:date.dateformat.f2}");
                date[3] = StringParserService.Parse("${res:date.dateformat.f3}");
                date[4] = StringParserService.Parse("${res:date.dateformat.f4}");
                date[5] = StringParserService.Parse("${res:date.dateformat.f5}");
                date[6] = StringParserService.Parse("${res:date.dateformat.f6}");
                date[7] = StringParserService.Parse("${res:date.dateformat.f7}");
                date[8] = StringParserService.Parse("${res:date.dateformat.f8}");
                date[9] = StringParserService.Parse("${res:date.dateformat.f9}");
                return date;
            }
        }
    }

    public class GetCHSTextforTablePropertyPanel
    {
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitper}");
                return unit;
            }
        }
        public string[] CaptionAlign
        {
            get
            {
                string[] calign = new string[4];
                calign[0] = StringParserService.Parse("${res:TablepropertyPanel.captionalign.default}");
                calign[1] = StringParserService.Parse("${res:TablepropertyPanel.captionalign.left}");
                calign[2] = StringParserService.Parse("${res:TablepropertyPanel.captionalign.center}");
                calign[3] = StringParserService.Parse("${res:TablepropertyPanel.captionalign.right}");
                return calign;
            }
        }
    }
    public class GetCHSTextforTextPropertyPanel
    {
        
        public string[] HAlign
        {
            get
            {
                string[] halign = new string[4];
                halign[0] = StringParserService.Parse("${res:TextpropertyPanel.Halign.default}");
                halign[1] = StringParserService.Parse("${res:TextpropertyPanel.Halign.left}");
                halign[2] = StringParserService.Parse("${res:TextpropertyPanel.Halign.center}");
                halign[3] = StringParserService.Parse("${res:TextpropertyPanel.Halign.right}");
                return halign;
            }
        }
        public string[] VAlign
        {
            get
            {
                string[] valign = new string[5];
                valign[0] = StringParserService.Parse("${res:TextpropertyPanel.valign.default}");
                valign[1] = StringParserService.Parse("${res:TextpropertyPanel.valign.top}");
                valign[2] = StringParserService.Parse("${res:TextpropertyPanel.valign.center}");
                valign[3] = StringParserService.Parse("${res:TextpropertyPanel.valign.button}");
                valign[4] = StringParserService.Parse("${res:TextpropertyPanel.valign.baseline}");
                return valign;
            }
        }
        public string[] Format
        {
            get
            {
                string[] format = new string[9];
                format[0] = StringParserService.Parse("${res:TextpropertyPanel.format.none}");
                format[1] = StringParserService.Parse("${res:TextpropertyPanel.format.paragraph}");
                format[2] = StringParserService.Parse("${res:TextpropertyPanel.format.h1}");
                format[3] = StringParserService.Parse("${res:TextpropertyPanel.format.h2}");
                format[4] = StringParserService.Parse("${res:TextpropertyPanel.format.h3}");
                format[5] = StringParserService.Parse("${res:TextpropertyPanel.format.h4}");
                format[6] = StringParserService.Parse("${res:TextpropertyPanel.format.h5}");
                format[7] = StringParserService.Parse("${res:TextpropertyPanel.format.h6}");
                format[8] = StringParserService.Parse("${res:TextpropertyPanel.format.pre}");
                return format;
            }
        }
        public string[] Size
        {
            get
            {
                string[] size = new string[18];
                size[0] = StringParserService.Parse("${res:TextpropertyPanel.size.none}");
                size[1] = StringParserService.Parse("${res:TextpropertyPanel.size.t9}");
                size[2] = StringParserService.Parse("${res:TextpropertyPanel.size.t10}");
                size[3] = StringParserService.Parse("${res:TextpropertyPanel.size.t12}");
                size[4] = StringParserService.Parse("${res:TextpropertyPanel.size.t14}");
                size[5] = StringParserService.Parse("${res:TextpropertyPanel.size.t16}");
                size[6] = StringParserService.Parse("${res:TextpropertyPanel.size.t18}");
                size[7] = StringParserService.Parse("${res:TextpropertyPanel.size.t24}");
                size[8] = StringParserService.Parse("${res:TextpropertyPanel.size.t36}");
                size[9] = StringParserService.Parse("${res:TextpropertyPanel.size.xxsmall}");
                size[10] = StringParserService.Parse("${res:TextpropertyPanel.size.xsmall}");
                size[11] = StringParserService.Parse("${res:TextpropertyPanel.size.small}");
                size[12] = StringParserService.Parse("${res:TextpropertyPanel.size.medium}");
                size[13] = StringParserService.Parse("${res:TextpropertyPanel.size.large}");
                size[14] = StringParserService.Parse("${res:TextpropertyPanel.size.xlarge}");
                size[15] = StringParserService.Parse("${res:TextpropertyPanel.size.xxlarge}");
                size[16] = StringParserService.Parse("${res:TextpropertyPanel.size.smaller}");
                size[17] = StringParserService.Parse("${res:TextpropertyPanel.size.larger}");
                return size;
            }
        }
        public string[] SizeUnit
        {
            get
            {
                string[] sizeUnit = new string[9];
                sizeUnit[0] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.px}");
                sizeUnit[1] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pt}");
                sizeUnit[2] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.in}");
                sizeUnit[3] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.cm}");
                sizeUnit[4] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.mm}");
                sizeUnit[5] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pc}");
                sizeUnit[6] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.em}");
                sizeUnit[7] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.ex}");
                sizeUnit[8] = StringParserService.Parse("${res:TextpropertyPanel.sizeunit.per}");
                return sizeUnit;
            }
        }
        public string[] Style
        {
            get
            {
                string[] style= new string[4];
                style[0] = StringParserService.Parse("${res:TextpropertyPanel.style.none}");
                style[1] = StringParserService.Parse("${res:TextpropertyPanel.style.g}");
                style[2] = StringParserService.Parse("${res:TextpropertyPanel.style.rename}");
                style[3] = StringParserService.Parse("${res:TextpropertyPanel.style.add}");
                return style;
            }
        }
    }
    public class GetCHSTextforImgPropertyPanel
    {
        public string[] Align
        {
            get
            {
                string[] calign = new string[10];
                calign[0] = StringParserService.Parse("${res:image.align.Default}");
                calign[1] = StringParserService.Parse("${res:image.align.BaseLine}");
                calign[2] = StringParserService.Parse("${res:image.align.Top}");
                calign[3] = StringParserService.Parse("${res:image.align.Middle}");
                calign[4] = StringParserService.Parse("${res:image.align.Bottom}");
                calign[5] = StringParserService.Parse("${res:image.align.TextTop}");
                calign[6] = StringParserService.Parse("${res:image.align.AbsoluteMiddle}");
                calign[7] = StringParserService.Parse("${res:image.align.AbsoluteBottom}");
                calign[8] = StringParserService.Parse("${res:image.align.Left}");
                calign[9] = StringParserService.Parse("${res:image.align.Right}");
                return calign;
            }
        }
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:image.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:image.unit.unitper}");
                return unit;
            }
        }
    }
    public class GetCHSTextforLinePropertyPanel
    {
        public string[] CaptionAlign
        {
            get
            {
                string[] calign = new string[4];
                calign[0] = StringParserService.Parse("${res:line.align.default}");
                calign[1] = StringParserService.Parse("${res:line.align.left}");
                calign[2] = StringParserService.Parse("${res:line.align.middle}");
                calign[3] = StringParserService.Parse("${res:line.align.right}");
                return calign;
            }
        }
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:line.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:line.unit.unitper}");
                return unit;
            }
        }
    }
    public class GetCHSTextforFlashPropertyPanel
    {
        public string[] Align
        {
            get
            {
                string[] calign = new string[10];
                calign[0] = StringParserService.Parse("${res:flash.FlashAlign.Default}");
                calign[1] = StringParserService.Parse("${res:flash.FlashAlign.BaseLine}");
                calign[2] = StringParserService.Parse("${res:flash.FlashAlign.Top}");
                calign[3] = StringParserService.Parse("${res:flash.FlashAlign.Middle}");
                calign[4] = StringParserService.Parse("${res:flash.FlashAlign.Bottom}");
                calign[5] = StringParserService.Parse("${res:flash.FlashAlign.TextTop}");
                calign[6] = StringParserService.Parse("${res:flash.FlashAlign.AbsoluteMiddle}");
                calign[7] = StringParserService.Parse("${res:flash.FlashAlign.AbsoluteBottom}");
                calign[8] = StringParserService.Parse("${res:flash.FlashAlign.Left}");
                calign[9] = StringParserService.Parse("${res:flash.FlashAlign.Right}");
                return calign;
            }
        }
        public string[] FlashQuality
        {
            get
            {
                string[] Quality = new string[4];
                Quality[0] = StringParserService.Parse("${res:flash.FlashQuality.Hight}");
                Quality[1] = StringParserService.Parse("${res:flash.FlashQuality.Low}");
                Quality[2] = StringParserService.Parse("${res:flash.FlashQuality.AutoHight}");
                Quality[3] = StringParserService.Parse("${res:flash.FlashQuality.AutoLow}");
                return Quality;
            }
        }
        public string[] Scale
        {
            get
            {
                string[] scale = new string[3];
                scale[0] = StringParserService.Parse("${res:flash.scale.default}");
                scale[1] = StringParserService.Parse("${res:flash.scale.noborder}");
                scale[2] = StringParserService.Parse("${res:flash.scale.match}");
                return scale;
            }
        }
        public string[] Unit
        {
            get
            {
                string[] unit = new string[2];
                unit[0] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitpx}");
                unit[1] = StringParserService.Parse("${res:TablepropertyPanel.unit.unitper}");
                return unit;
            }
        }

    }
}

    
