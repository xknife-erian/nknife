using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class HalfOpacityForm : Form
    {
        public HalfOpacityForm()
        {
            this.BackColor = Color.FromArgb(0, 0, 255);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.AllowTransparency = true;
            this.Opacity = 0.2;
            this.AllowDrop = true;
        }

        static public void ShowForm(int x,int y,int width,int height)
        {
            HalfOpacityForm form = GetInstance();
            Utility.DllImport.SetWindowShow(form, null, x, y, width, height);
        }

        static private SnipPagePart[] _prevParts;
        static public void ShowForm(SnipPagePart[] parts)
        {
            ///若跟上次的一样，则不改变，不做操作
            if (_prevParts != null && IsSame(_prevParts, parts))
            {
                return;
            }

            ///隐藏不需要显示的Form
            int sameCount = -1;
            if (_prevParts != null && _prevParts.Length > 0)
            {
                int prevLength = _prevParts.Length;
                for (int i = 0; i < prevLength; i++)
                {
                    if (i < parts.Length && _prevParts[i] == parts[i])
                    {
                        sameCount = i;
                    }
                    else
                    {
                        _listUsingForm[sameCount + 1].HideForm();
                    }
                }
            }

            ///根据位置显示HalfOpacityForm
            for (int i = sameCount + 1; i < parts.Length; i++)
            {
                SnipPagePart part = parts[i];
                ///delete:Point screenLocation = part.Designer.PointToScreen(part.LocationForDesignerDisplay);
                Point screenLocation = part.Designer.PointToScreen(part.LocationForDesigner);
                ShowForm(screenLocation.X, screenLocation.Y, part.Bounds.Width, part.Bounds.Height);
            }
            _prevParts = parts;
        }

        /// <summary>
        /// 比较两组Part是否相同
        /// </summary>
        static public bool IsSame(SnipPagePart[] parts1, SnipPagePart[] parts2)
        {
            Debug.Assert(parts1 != null && parts2 != null);

            ///先比较长度是否一样
            if (parts1.Length != parts2.Length)
            {
                return false;
            }

            ///再比较每一个是否相同
            for (int i = 0; i < parts1.Length; i++)
            {
                if (!object.ReferenceEquals(parts1[i], parts2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        static public void HideAll()
        {
            while (_listUsingForm.Count > 0)
            {
                _listUsingForm[0].HideForm();
            }
            _prevParts = null;
        }

        public void HideForm()
        {
            this.Bounds = new Rectangle(0, 0, 0, 0);
            this.Hide();

            DeUsing();
        }

        /// <summary>
        /// 保存一个对象池
        /// </summary>
        static List<HalfOpacityForm> _listForm = new List<HalfOpacityForm>();

        /// <summary>
        /// 保存正在使用的列表
        /// </summary>
        static List<HalfOpacityForm> _listUsingForm = new List<HalfOpacityForm>();

        static private HalfOpacityForm GetInstance()
        {
            ///先尝试在对象池中取，若取不到则new一个
            HalfOpacityForm usingForm = null;
            if (_listForm.Count > 0)
            {
                usingForm = _listForm[0];

                _listForm.RemoveAt(0);
            }
            else
            {
                usingForm = new HalfOpacityForm();
            }

            ///将对象加入到正在使用的列表中
            _listUsingForm.Add(usingForm);

            return usingForm;
        }
        /// <summary>
        /// 处理已使用完毕的对象，将其从正在使用的列表删除，加入到对象池中
        /// </summary>
        private void DeUsing()
        {
            if (_listUsingForm.Contains(this))
            {
                _listUsingForm.Remove(this);
                _listForm.Add(this);
            }
        }
    }
}