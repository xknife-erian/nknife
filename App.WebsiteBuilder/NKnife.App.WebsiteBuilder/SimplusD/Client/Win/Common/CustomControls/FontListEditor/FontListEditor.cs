using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class FontListEditor : BaseForm
    {
        string tip;
        bool isAdd = true;

        public string FontListString { get; set; }

        public FontListEditor()
        {
            InitializeComponent();
            addRowbtn.Image = ResourceService.GetResourceImage("fontlist.new");
            deleteRowBtn.Image = ResourceService.GetResourceImage("fontlist.delete");
            leftMoveBtn.Image = ResourceService.GetResourceImage("fontlist.leftArrow");
            rightMoveBtn.Image = ResourceService.GetResourceImage("fontlist.rightArrow");
            upMoveBtn.Image = ResourceService.GetResourceImage("fontlist.upArrow");
            downMoveBtn.Image = ResourceService.GetResourceImage("fontlist.downArrow");
            tip = StringParserService.Parse("${res:TextpropertyPanel.fontname.default}");
            LoadingForm();
        }

        private void leftMoveBtn_Click(object sender, EventArgs e)
        {
            LeftRemoveFont();
        }

        private void rightMoveBtn_Click(object sender, EventArgs e)
        {
            RightRemoveFont();
        }
        //字体列表
        private void fontListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isAdd)
            {
                this.checkedFontListbox.Items.Clear();
                string getFontList = this.fontListBox.SelectedItem.ToString();
                if (string.Compare(getFontList, tip) != 0)
                {
                    Regex r = new Regex("(,)");
                    string[] items = r.Split(getFontList);
                    foreach (string item in items)
                    {
                        if (item != ",")
                        {
                            this.checkedFontListbox.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void fontListBox_MouseClick(object sender, MouseEventArgs e)
        {
            isAdd = false;
        }

        private void addRowbtn_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void deleteRowBtn_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void upMoveBtn_Click(object sender, EventArgs e)
        {
            UpRemoveItem();
        }

        private void downMoveBtn_Click(object sender, EventArgs e)
        {
            DownRemoveItem();
        }

        private void fontListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteItem();

            }
        }
        //可用字体
        private void allFontListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO:设置列表失去焦距

            //this.rightMoveBtn.Enabled = false;
            string checkedFont = this.allFontListBox.SelectedItem.ToString();
            this.txtCheckedFont.Text = checkedFont;

            //this.leftMoveBtn.Enabled = CompareFont(checkedFont);

        }
        //选择字体
        private void checkedFontListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO:设置列表失去焦距
            if (checkedFontListbox.Items.Count < 1 || checkedFontListbox.SelectedItems.Count < 1)
            {
                this.rightMoveBtn.Enabled = false;
                return;
            }
            this.rightMoveBtn.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] items = new string[fontListBox.Items.Count];
            this.fontListBox.Items.CopyTo(items, 0);
            FontService.AddFontList(items, tip);
            string fontListString = fontListBox.SelectedItem.ToString();
            if (fontListString == tip)
            {
                fontListString = "";
                if (MessageService.Show(StringParserService.Parse("${res:TextpropertyPanel.fontname.err}"),
                    MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                FontListString = fontListBox.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            int index = this.fontListBox.Items.Count - 1;
            this.fontListBox.SetSelected(index, true);

            this.leftMoveBtn.Enabled = true;
            this.rightMoveBtn.Enabled = false;
        }
        /// <summary>
        /// 删除字体列表项
        /// </summary>
        private void DeleteItem()
        {
            isAdd = true;
            int index = this.fontListBox.SelectedIndex;
            int count = this.fontListBox.Items.Count;
            this.fontListBox.Items.RemoveAt(index);
            if (index + 1 < count)
            {
                this.fontListBox.SetSelected(index, true);
            }
            else if (index == 0 && count - 1 == 0)
            {
                this.fontListBox.Items.Add(tip);
                this.fontListBox.SetSelected(0, true);
                this.checkedFontListbox.Items.Clear();
                this.leftMoveBtn.Enabled = true;
            }
            else
            {
                this.fontListBox.SetSelected(index - 1, true);
            }
            isAdd = false;
        }
        /// <summary>
        /// 上移字体列表项
        /// </summary>
        private void UpRemoveItem()
        {
            isAdd = true;
            string currItem = this.fontListBox.SelectedItem.ToString();
            int index = this.fontListBox.SelectedIndex;

            if (index != 0)
            {
                this.fontListBox.Items.RemoveAt(index);
                this.fontListBox.Items.Insert(index - 1, currItem);
                this.fontListBox.SetSelected(index - 1, true);
            }
            isAdd = false;
        }
        /// <summary>
        /// 下移字体列表项
        /// </summary>
        private void DownRemoveItem()
        {
            isAdd = true;
            string currItem = this.fontListBox.SelectedItem.ToString();
            int index = this.fontListBox.SelectedIndex;

            if (index != fontListBox.Items.Count - 1)
            {
                this.fontListBox.Items.RemoveAt(index);
                this.fontListBox.Items.Insert(index + 1, currItem);
                this.fontListBox.SetSelected(index + 1, true);
            }
            isAdd = false;
        }
        /// <summary>
        /// 加载字体
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
        /// <summary>
        /// 比较可用字体是否已经被选中
        /// </summary>
        /// <returns></returns>
        private bool CompareFont(string CurrFont)
        {
            return !checkedFontListbox.Items.Contains(CurrFont);
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
            isAdd = true;
            if (!string.IsNullOrEmpty(txtCheckedFont.Text))
            {
                this.leftMoveBtn.Enabled = false;
                string getFont = txtCheckedFont.Text;// this.allFontListBox.SelectedItem.ToString();

                this.checkedFontListbox.Items.Add(getFont);

                string fontListItem = this.fontListBox.SelectedItem.ToString();
                int fontListIndex = this.fontListBox.SelectedIndex;
                if (string.Compare(fontListItem, tip) != 0)
                {
                    this.fontListBox.Items[fontListIndex] = fontListItem + "," + getFont;
                }
                else
                {
                    this.fontListBox.Items[fontListIndex] = getFont;
                }
            }
            txtCheckedFont.Text = "";
            leftMoveBtn.Enabled = false;
            isAdd = false;
        }
        string getCheckedFont = "";
        /// <summary>
        /// 右移字体
        /// </summary>
        private void RightRemoveFont()
        {
            isAdd = true;
            string fontListItem = this.fontListBox.SelectedItem.ToString();
            int getIndex = this.fontListBox.SelectedIndex;

            getCheckedFont = this.checkedFontListbox.SelectedItem.ToString();

            string result = RemoveItem(fontListItem);
            this.fontListBox.Items[getIndex] = result;

            this.checkedFontListbox.Items.Remove(getCheckedFont);
            if (checkedFontListbox.Items.Count< 1 || checkedFontListbox.SelectedItems.Count < 1)
            {
                rightMoveBtn.Enabled = false; 
            }
            
            isAdd = false;
        }
        private string RemoveItem(string fontListItem)
        {
            string fonts = "";
            Regex r = new Regex("(,)");
            string[] items = r.Split(fontListItem);

            List<string> lt = new List<string>();
            lt.AddRange(items);

            int index = lt.Count - 1;
            int pos = lt.FindIndex(0, FindFont);
            if (pos == 0 && pos == index)
            {
                lt.RemoveAt(0);
                lt.Add(tip);
            }
            else if (pos == 0 && pos != index)
            {
                lt.RemoveAt(1);
                lt.RemoveAt(0);

            }
            else
            {
                lt.RemoveAt(pos);
                lt.RemoveAt(pos - 1);
            }

            foreach (string tr in lt)
            {
                fonts += tr;
            }
            return fonts;
        }
        private bool FindFont(String s)
        {
            if (string.Compare(s, getCheckedFont) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string[] FontList
        {
            get
            {
                int fontlistcount=fontListBox.Items.Count;
                if (fontlistcount> 0)
                {
                    string[] fontlist=new string[fontlistcount];
                    for (int i = 0; i < fontlistcount; i++)
                    {
                        fontlist[i] = fontListBox.Items[i].ToString();
                    }
                    return fontlist;
                }
                else
                {
                    return null;
                }
            }
        }

        private void txtCheckedFont_TextChanged(object sender, EventArgs e)
        {
            this.leftMoveBtn.Enabled = CompareFont(txtCheckedFont.Text) && !string.IsNullOrEmpty(txtCheckedFont.Text);
        }
    }
}