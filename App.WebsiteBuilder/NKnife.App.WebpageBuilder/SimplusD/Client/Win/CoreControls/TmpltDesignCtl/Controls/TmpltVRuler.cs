using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class TmpltVRuler : VRuler
    {
        public TmpltVRuler(DesignPanel tD)
            : base(tD)
        {
            //InitContextMenu();
        }

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        protected virtual void InitContextMenu()
        {
            this.ContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem item;

            ///添加像素菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.pixel}"));
            item.Name = "pixel";
            item.Checked = true;
            item.Click += delegate
            {
                if (RulerStyle != EnumRulerScaleStyle.Pixel)
                {
                    RulerStyle = EnumRulerScaleStyle.Pixel;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["pixel"]).Checked = true;

                    HRuler.RulerScaleStyle = EnumRulerScaleStyle.Pixel;
                    ((TmpltHRuler)HRuler).ClearCheckItem();
                    ((ToolStripMenuItem)HRuler.ContextMenuStrip.Items["pixel"]).Checked = true;
                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加英寸菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.inch}"));
            item.Name = "inch";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerStyle != EnumRulerScaleStyle.Inch)
                {
                    RulerStyle = EnumRulerScaleStyle.Inch;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["inch"]).Checked = true;


                    HRuler.RulerScaleStyle = EnumRulerScaleStyle.Inch;
                    ((TmpltHRuler)HRuler).ClearCheckItem();
                    ((ToolStripMenuItem)HRuler.ContextMenuStrip.Items["inch"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加厘米菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.cm}"));
            item.Name = "cm";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerStyle != EnumRulerScaleStyle.CM)
                {
                    RulerStyle = EnumRulerScaleStyle.CM;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["cm"]).Checked = true;


                    HRuler.RulerScaleStyle = EnumRulerScaleStyle.CM;
                    ((TmpltHRuler)HRuler).ClearCheckItem();
                    ((ToolStripMenuItem)HRuler.ContextMenuStrip.Items["cm"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加毫米菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.mm}"));
            item.Name = "mm";
            item.Enabled = true;
            item.Click += delegate
            {
                if (RulerStyle != EnumRulerScaleStyle.MM)
                {
                    RulerStyle = EnumRulerScaleStyle.Inch;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["mm"]).Checked = true;


                    HRuler.RulerScaleStyle = EnumRulerScaleStyle.MM;
                    ((TmpltHRuler)HRuler).ClearCheckItem();
                    ((ToolStripMenuItem)HRuler.ContextMenuStrip.Items["mm"]).Checked = true;

                }
            };
            this.ContextMenuStrip.Items.Add(item);

            ///添加百分比菜单
            item = new ToolStripMenuItem(StringParserService.Parse("${res:tmpltDesign.ruler.contextMenu.percent}"));
            item.Name = "percent";
            item.Click += delegate
            {
                if (RulerStyle != EnumRulerScaleStyle.Percent)
                {
                    RulerStyle = EnumRulerScaleStyle.Percent;
                    ClearCheckItem();
                    ((ToolStripMenuItem)ContextMenuStrip.Items["percent"]).Checked = true;


                    HRuler.RulerScaleStyle = EnumRulerScaleStyle.Percent;
                    ((TmpltHRuler)HRuler).ClearCheckItem();
                    ((ToolStripMenuItem)HRuler.ContextMenuStrip.Items["percent"]).Checked = true;

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
