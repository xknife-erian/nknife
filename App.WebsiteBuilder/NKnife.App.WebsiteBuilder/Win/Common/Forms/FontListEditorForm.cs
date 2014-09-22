using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using System.IO;
namespace Jeelu.Win
{
    public partial class FontListEditorForm : BaseForm
    {
        string tip;

        public string FontListString { get; set; }
        public List<string> listStr = new List<string>();

        public FontListEditorForm()
        {
            InitializeComponent();

            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            
            //没有判断文件是否存在， 
            //如果需要使用图片，则 放在
            //string strPath = Path.Combine(Application.StartupPath, "Image/FontListForm");
            //addRowbtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.new.ico"));
            //deleteRowBtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.delete.ico"));
            //leftMoveBtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.leftArrow.ico"));
            //rightMoveBtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.rightArrow.ico"));
            //upMoveBtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.upArrow.ico"));
            //downMoveBtn.Image = Image.FromFile(Path.Combine(strPath, "fontlist.downArrow.ico"));

            tip = StringParserService.Parse("${res:TextpropertyPanel.fontname.default}");

            
            LoadingForm();
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        private void LoadingForm()
        {
            List<string> lt = FontService.ReadFontList();
            if (lt.Count != 0)
            {
                this.fontListBox.Items.AddRange(lt.ToArray());
            }
            AddItem();
            string[] fonts = LoadFonts();
            this.allFontListBox.Items.AddRange(fonts);
            this.txtCheckedFont.Text = allFontListBox.Items[0].ToString();
            this.txtCheckedFont.Select();
            leftMoveBtn.Enabled = CompareFont(txtCheckedFont.Text) && !string.IsNullOrEmpty(txtCheckedFont.Text);
        }

        /// <summary>
        /// 添加空白项
        /// </summary>
        private void AddItem()
        {
            this.fontListBox.Items.Add(tip);

            downMoveBtn.Enabled = true;
            upMoveBtn.Enabled = true;

            int index = this.fontListBox.Items.Count - 1;
            this.fontListBox.SetSelected(index, true);

            this.leftMoveBtn.Enabled = true;
            this.rightMoveBtn.Enabled = false;
            this.deleteRowBtn.Enabled = true;
            
        }
        /// <summary>
        /// 删除字体列表项
        /// </summary>
        private void DeleteItem()
        {
            if (fontListBox.SelectedItem == null) return;

            int iSelindex = this.fontListBox.SelectedIndex;
            int iDelindex = GetDelIndex(iSelindex,fontListBox);
            this.fontListBox.Items.RemoveAt(iSelindex);
            
            if (iDelindex >= 0)
            {
                this.fontListBox.SetSelected(iDelindex, true);
            }
            else
            {
                deleteRowBtn.Enabled = false;
                this.fontListBox.Items.Add(tip);
                fontListBox.SetSelected(0, true);
            }
    
        }
        /// <summary>
        /// 加载当前客户机系统字体
        /// </summary>
        /// <returns></returns>
        private string[] LoadFonts()
        {
            // Gets the list of installed fonts.
            FontFamily[] ff = FontFamily.Families;

            List<string> fontList = new List<string>(ff.Length);
            // Loop and create a sample of each font.
            for (int x = 0; x < ff.Length; x++)
            {
                System.Drawing.Font font = null;

                // Create the font - based on the styles available.
                if (ff[x].IsStyleAvailable(FontStyle.Regular))
                    font = new System.Drawing.Font(
                        ff[x].Name,
                        this.Font.Size
                        );
                else if (ff[x].IsStyleAvailable(FontStyle.Bold))
                    font = new System.Drawing.Font(
                        ff[x].Name,
                        this.Font.Size,
                        FontStyle.Bold
                        );
                else if (ff[x].IsStyleAvailable(FontStyle.Italic))
                    font = new System.Drawing.Font(
                        ff[x].Name,
                        this.Font.Size,
                        FontStyle.Italic
                        );
                else if (ff[x].IsStyleAvailable(FontStyle.Strikeout))
                    font = new System.Drawing.Font(
                        ff[x].Name,
                        this.Font.Size,
                        FontStyle.Strikeout
                        );
                else if (ff[x].IsStyleAvailable(FontStyle.Underline))
                    font = new System.Drawing.Font(
                        ff[x].Name,
                        this.Font.Size,
                        FontStyle.Underline
                        );

                // Add the item
                if (font != null)
                    fontList.Add(font.Name);
            }
            return fontList.ToArray();
        }

        private void txtCheckedFont_TextChanged(object sender, EventArgs e)
        {
            this.leftMoveBtn.Enabled = CompareFont(txtCheckedFont.Text) && !string.IsNullOrEmpty(txtCheckedFont.Text);
        }

        #region 左移动字体处理
        
        private void leftMoveBtn_Click(object sender, EventArgs e)
        {
            LeftRemoveFont();
        }

        /// <summary>
        /// 左移字体
        /// </summary>
        private void LeftRemoveFont()
        {
            if (!CompareFont(txtCheckedFont.Text))
            {
                return;
            }
            if (!string.IsNullOrEmpty(txtCheckedFont.Text))
            {
                this.leftMoveBtn.Enabled = false;
                string getFont = txtCheckedFont.Text;// this.allFontListBox.SelectedItem.ToString();

                this.checkedFontListbox.Items.Add(getFont);

                string fontListItem = this.fontListBox.SelectedItem.ToString();
                int fontListIndex = this.fontListBox.SelectedIndex;
                if (string.Compare(fontListItem, tip) != 0)
                {
                    this.fontListBox.Items[fontListIndex] = fontListItem + ", " + getFont;
                }
                else
                {
                    this.fontListBox.Items[fontListIndex] = getFont;
                }
            }
            leftMoveBtn.Enabled = false;
        }

        /// <summary>
        /// 比较可用字体是否已经被选中
        /// </summary>
        /// <returns></returns>
        private bool CompareFont(string CurrFont)
        {
            return !checkedFontListbox.Items.Contains(CurrFont);
        }

        private void allFontListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string checkedFont = this.allFontListBox.SelectedItem.ToString();
            this.txtCheckedFont.Text = checkedFont;
        }
        #endregion

        #region 右移动字体处理
        private void rightMoveBtn_Click(object sender, EventArgs e)
        {
            RightRemoveFont();
        }

        /// <summary>
        /// 右移字体
        /// </summary>
        private void RightRemoveFont()
        {
            if (this.checkedFontListbox.SelectedItem == null)
            {
                rightMoveBtn.Enabled = false;
                return;
            }

            string strSelectContent = this.fontListBox.SelectedItem.ToString();
            int iSelectIndex = this.fontListBox.SelectedIndex;

            string strDelFont = this.checkedFontListbox.SelectedItem.ToString();
            int iDelIndex = GetDelIndex(this.checkedFontListbox.SelectedIndex,checkedFontListbox);

            string strReplce ="";
            if (strSelectContent.IndexOf(',') == -1)
            {//剩一个
                strReplce = strSelectContent;
            }
            else
            {//多于一个字体
                if (checkedFontListbox.SelectedIndex == 0)
                {//第一个
                    strReplce = strDelFont + ", ";
                }
                else if (checkedFontListbox.SelectedIndex == checkedFontListbox.Items.Count - 1)
                {//最后一个
                    strReplce = ", " + strDelFont;
                }
                else
                {//中间的   TIP 和第一个相同
                    strReplce = strDelFont + ", ";
                }            
            }
            this.checkedFontListbox.Items.Remove(strDelFont);
            string strReplaceNew = strReplce == strSelectContent ? tip:"";
            this.fontListBox.Items[iSelectIndex] = strSelectContent.Replace(strReplce, strReplaceNew);
            fontListBox.Refresh(); //防止fontListBox 闪烁

            //禁用
            if (checkedFontListbox.Items.Count < 1)
            {
                this.fontListBox.SetSelected(iSelectIndex,true);
                rightMoveBtn.Enabled = false;
            }
            else if(iDelIndex >= 0)
            {
                this.checkedFontListbox.SetSelected(iDelIndex,true);
            }
        }

        ///<summary>
        /// 得到删除后 欲选中的索引
        /// </summary>
        private int GetDelIndex(int item,ListBox lst_tem)
        {
            //算法决定向下移动索引 参考Dreamweaver 删除最后一个时候想上移动索引
            if (lst_tem.Items.Count == 1)
            {
                return -1; //最后一个了
            }
            else if (lst_tem.SelectedIndex == lst_tem.Items.Count - 1)
            {//
                return --item;  
            }
            else return item; //其他情况本身 因为删除一跳 已经少了一个 表现为上移
        }

        #endregion

        private void checkedFontListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO:设置列表失去焦距                   
            this.rightMoveBtn.Enabled = checkedFontListbox.Items.Count < 1? false : true;
        }

        private void fontListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //优化如果选本身则跳过 但如果是默认字体 ,则相应做处理
            this.checkedFontListbox.Items.Clear();
            if (this.fontListBox.SelectedItem == null) return;

            string getFontList = this.fontListBox.SelectedItem.ToString();
            if (string.Compare(getFontList, tip) != 0)
            {
                string[] items = getFontList.Split(new char[] {','});

                //Speed use AddRange;
                foreach (string item in items)
                {
                     this.checkedFontListbox.Items.Add(item.Trim());                    
                }
            }

            if (fontListBox.Items.Count == 1)
            {
                downMoveBtn.Enabled = false;
                upMoveBtn.Enabled = false;
            }
            else if (fontListBox.SelectedIndex == 0)
            {
                downMoveBtn.Enabled = true;
                upMoveBtn.Enabled = false;
            }
            else if (fontListBox.SelectedIndex == fontListBox.Items.Count - 1)
            {
                downMoveBtn.Enabled = false;
                upMoveBtn.Enabled = true;
            }
            else
            {
                downMoveBtn.Enabled = true;
                upMoveBtn.Enabled = true;
            }
            
        }

        private void addRowbtn_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void deleteRowBtn_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void downMoveBtn_Click(object sender, EventArgs e)
        {
            DownRemoveItem();
        }

        private void upMoveBtn_Click(object sender, EventArgs e)
        {
            UpRemoveItem();
        }
        /// <summary>
        /// 上移字体列表项
        /// </summary>
        private void UpRemoveItem()
        {
            if (fontListBox.SelectedItem == null || fontListBox.SelectedIndex == 0) return;

            string currItem = this.fontListBox.SelectedItem.ToString();
            int index = this.fontListBox.SelectedIndex;
            if (index != 0)
            {
                this.fontListBox.Items.RemoveAt(index);
                this.fontListBox.Items.Insert(index - 1, currItem);
                this.fontListBox.SetSelected(index - 1, true);
            }
        }
        /// <summary>
        /// 下移字体列表项
        /// </summary>
        private void DownRemoveItem()
        {
            if (fontListBox.SelectedItem == null || (fontListBox.SelectedIndex == fontListBox.Items.Count -1) ) return;

            string currItem = this.fontListBox.SelectedItem.ToString();
            int index = this.fontListBox.SelectedIndex;

            if (index != fontListBox.Items.Count - 1)
            {
                this.fontListBox.Items.RemoveAt(index);
                this.fontListBox.Items.Insert(index + 1, currItem);
                this.fontListBox.SetSelected(index + 1, true);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            listStr.Clear();
            for (int i = 0; i < fontListBox.Items.Count; i++)
            {
                if(fontListBox.Items[i].ToString() != tip)
                listStr.Add(fontListBox.Items[i].ToString());
            }
            FontService.AddFontList(listStr);
            DialogResult = DialogResult.OK;
        }

        private void checkedFontListbox_DoubleClick(object sender, EventArgs e)
        {
            rightMoveBtn.PerformClick();
        }

        private void allFontListBox_DoubleClick(object sender, EventArgs e)
        {
            leftMoveBtn.PerformClick();
        }
    }
}
