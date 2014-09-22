using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class TmpltHRuler : HRuler
    {
        #region 构造函数
        public TmpltHRuler(DesignPanel tD)
            : base(tD)
        {
            //InitContextMenu();
        }
        #endregion

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        protected void InitContextMenu()
        {
            this.ContextMenuStrip = new ContextMenuStrip();
            //ContextMenuStrip.
            ToolStripMenuItem item;

            ///添加像素菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.pixel}"));
            item.Name = "pixel";
            item.Checked = true;
            item.Click += delegate
            {
                if (RulerScaleStyle != EnumRulerScaleStyle.Pixel)
                {
                    RulerScaleStyle = EnumRulerScaleStyle.Pixel;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["pixel"]).Checked = true;


                    VRuler.RulerStyle = EnumRulerScaleStyle.Pixel;
                    ((TmpltVRuler)VRuler).ClearCheckItem();
                    ((ToolStripMenuItem)VRuler.ContextMenuStrip.Items["pixel"]).Checked = true;
                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加英寸菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.inch}"));
            item.Name = "Inch";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerScaleStyle != EnumRulerScaleStyle.Inch)
                {
                    RulerScaleStyle = EnumRulerScaleStyle.Inch;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["Inch"]).Checked = true;


                    VRuler.RulerStyle = EnumRulerScaleStyle.Inch;
                    ((TmpltVRuler)VRuler).ClearCheckItem();
                    ((ToolStripMenuItem)VRuler.ContextMenuStrip.Items["Inch"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加厘米菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.cm}"));
            item.Name = "CM";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerScaleStyle != EnumRulerScaleStyle.CM)
                {
                    RulerScaleStyle = EnumRulerScaleStyle.CM;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["CM"]).Checked = true;


                    VRuler.RulerStyle = EnumRulerScaleStyle.CM;
                    ((TmpltVRuler)VRuler).ClearCheckItem();
                    ((ToolStripMenuItem)VRuler.ContextMenuStrip.Items["CM"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加毫米菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.mm}"));
            item.Name = "MM";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerScaleStyle != EnumRulerScaleStyle.MM)
                {
                    RulerScaleStyle = EnumRulerScaleStyle.Inch;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["MM"]).Checked = true;


                    VRuler.RulerStyle = EnumRulerScaleStyle.MM;
                    ((TmpltVRuler)VRuler).ClearCheckItem();
                    ((ToolStripMenuItem)VRuler.ContextMenuStrip.Items["MM"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加百分比菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.percent}"));
            item.Name = "Percent";
            item.Click += delegate
            {
                if (RulerScaleStyle != EnumRulerScaleStyle.Percent)
                {
                    RulerScaleStyle = EnumRulerScaleStyle.Percent;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["Percent"]).Checked = true;


                    VRuler.RulerStyle = EnumRulerScaleStyle.Percent;
                    ((TmpltVRuler)VRuler).ClearCheckItem();
                    ((ToolStripMenuItem)VRuler.ContextMenuStrip.Items["Percent"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);
        }

        /// <summary>
        /// 清除其他菜单的被选状态
        /// </summary>
        public void ClearCheckItem()
        {
            foreach (ToolStripMenuItem menu in ContextMenuStrip.Items)
            {
                menu.Checked = false;
            }
        }
    }
}
