using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VS2005ToolBox
{
    [ToolboxItem(true)]
    [ToolboxBitmap(@"ToolBoxBmp")]
    public partial class ToolBox : TreeView
    {
        #region Local classes

        public class VSTreeNode : TreeNode
        {
            #region Private fields

            private string mToolTipCaption;
            private bool mOnEdit;

            #endregion

            #region Public properties

            public string ToolTipCaption
            {
                get
                {
                    return this.mToolTipCaption;
                }
                set
                {
                    this.mToolTipCaption = value;
                }
            }

            public bool OnEdit
            {
                get
                {
                    return this.mOnEdit;
                }
                set
                {
                    this.mOnEdit = value;
                }
            }

            #endregion

            #region Constructor / Destructor

            public VSTreeNode()
                : base()
            {
                this.mToolTipCaption = string.Empty;
                this.mOnEdit = false;
            }

            #endregion
        }

        #endregion

        #region Private consts

        /// <summary>
        /// This is needed to disable the default tooltips
        /// of a treenode item.
        /// </summary>
        private const int TVS_NOTOOLTIPS = 0x80;

        #endregion

        #region Private fields

        /// <summary>
        /// Font to be used for the group headers in the toolbox.
        /// </summary>
        private Font mGroupHeaderFont;

        /// <summary>
        /// Custom tooltip for the treenodes.
        /// </summary>
        private ToolTip mToolTip;

        /// <summary>
        /// Stores the last mouse position for the custom tooltip.
        /// </summary>
        private TreeNode mPreviousNode;

        /// <summary>
        /// Textbox used for label edit.
        /// </summary>
        private TextBox mLabelEditBox;

        #endregion

        #region Protected properties

        /// <summary>
        /// Gets the create params property.
        /// 
        /// Disables the tooltip activity for the treenodes.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.Style = p.Style | TVS_NOTOOLTIPS;
                return p;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Draws a group header
        /// </summary>
        private void drawRootItem(DrawTreeNodeEventArgs e)
        {
            Rectangle nodeTextRect = e.Bounds;

            nodeTextRect.Y += 1;
            nodeTextRect.Width -= 1;
            nodeTextRect.Height -= 3;

            if ((e.State & TreeNodeStates.Marked) == TreeNodeStates.Marked
                || (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {

                using (Brush selBrush = new SolidBrush(Color.FromArgb(225, 230, 232)))
                {
                    e.Graphics.FillRectangle(selBrush, nodeTextRect);
                }

                using (Pen outerPen = new Pen(Color.FromArgb(49, 106, 197)))
                {
                    e.Graphics.DrawRectangle(outerPen, nodeTextRect);
                }
            }
            else
            {
                using (LinearGradientBrush lgBrush = new LinearGradientBrush(
                  e.Bounds, Color.FromArgb(221, 220, 203), Color.FromArgb(196, 193, 176), LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(lgBrush, nodeTextRect);
                }

                using (Pen linePen = new Pen(this.BackColor))
                {
                    e.Graphics.DrawLine(linePen, 0, nodeTextRect.Bottom - 2, nodeTextRect.Width, nodeTextRect.Bottom - 2);
                }
            }

            if (e.Node.IsExpanded == true)
            {
                e.Graphics.DrawImage(Properties.Resources.expanded, new Point(nodeTextRect.Left + 3, nodeTextRect.Top + 4));
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.collapsed, new Point(nodeTextRect.Left + 3, nodeTextRect.Top + 4));
            }

            nodeTextRect.Offset(16, 2);

            e.Graphics.DrawString(e.Node.Text, this.mGroupHeaderFont, SystemBrushes.ControlText, nodeTextRect.Location);
        }

        /// <summary>
        /// Draws a sub item of a group
        /// </summary>
        /// <param name="e"></param>
        private void drawItem(DrawTreeNodeEventArgs e)
        {
            Rectangle nodeTextRect = e.Bounds;

            nodeTextRect.Width -= 1;
            nodeTextRect.Height -= 1;

            if ((e.Node as VSTreeNode).OnEdit == true)
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, nodeTextRect);

                using (Pen clearBGPen = new Pen(Color.FromArgb(49, 106, 197)))
                {
                    e.Graphics.DrawRectangle(SystemPens.HotTrack, nodeTextRect);
                }

                if (this.ImageList != null && e.Node.ImageIndex < this.ImageList.Images.Count)
                {
                    e.Graphics.DrawImage(this.ImageList.Images[e.Node.ImageIndex], new Point(e.Bounds.Left + 3, e.Bounds.Top + 2));
                }
            }
            else
            {
                if ((e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
                {
                    using (Brush hoverBrush = new SolidBrush(Color.FromArgb(193, 210, 238)))
                    {
                        e.Graphics.FillRectangle(hoverBrush, nodeTextRect);
                    }

                    using (Pen clearBGPen = new Pen(Color.FromArgb(49, 106, 197)))
                    {
                        e.Graphics.DrawRectangle(SystemPens.HotTrack, nodeTextRect);
                    }
                }
                else
                {
                    if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
                    {
                        using (Brush hoverBrush = new SolidBrush(Color.FromArgb(225, 230, 232)))
                        {
                            e.Graphics.FillRectangle(hoverBrush, nodeTextRect);
                        }

                        e.Graphics.DrawRectangle(SystemPens.HotTrack, nodeTextRect);
                    }
                    else
                    {
                        using (Brush hoverBrush = new SolidBrush(this.BackColor))
                        {
                            e.Graphics.FillRectangle(hoverBrush, e.Bounds);
                        }
                    }
                }

                if (this.ImageList != null && e.Node.ImageIndex < this.ImageList.Images.Count)
                {
                    e.Graphics.DrawImage(this.ImageList.Images[e.Node.ImageIndex], new Point(e.Bounds.Left + 3, e.Bounds.Top + 2));
                }

                nodeTextRect.Offset(20, 3);
                e.Graphics.DrawString(e.Node.Text, this.Font, SystemBrushes.ControlText, nodeTextRect.Location);
            }
        }

        /// <summary>
        /// Handles the end edit event.
        /// 
        /// Hides the edit text box and stores the entered
        /// value if needed.
        /// </summary>
        /// <param name="setNewValues">Bool if the new text should be stored.</param>
        private void endLabelEdit(bool setNewValues)
        {
            if (setNewValues == true && this.mLabelEditBox.Tag != null)
            {
                (this.mLabelEditBox.Tag as VSTreeNode).Text = this.mLabelEditBox.Text;
            }

            this.mLabelEditBox.Visible = false;

            if (this.mLabelEditBox.Tag != null)
            {
                (this.mLabelEditBox.Tag as VSTreeNode).OnEdit = false;
                this.mLabelEditBox.Tag = null;
            }

            Invalidate();
        }

        /// <summary>
        /// Handles the lost forcus event of the edit text box.
        /// 
        /// Ends the label editing by calling the coresponding method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelEditBox_LostFocus(object sender, EventArgs e)
        {
            endLabelEdit(false);
        }

        /// <summary>
        /// Handles the key down event.
        /// 
        /// Calls the end label edit method and decides epending on the
        /// pressed key if the nw test should be stored.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mLabelEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        endLabelEdit(false);
                        break;
                    }
                case Keys.Enter:
                    {
                        endLabelEdit(true);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Decides wich node type should be drawn and calls the coresponding.
        /// methods.
        /// </summary>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                drawRootItem(e);
            }
            else
            {
                drawItem(e);
            }
        }

        /// <summary>
        /// Handles the mouse down event
        /// 
        /// Selects the node under the mouse cursor.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            TreeNode tn = this.GetNodeAt(e.X, e.Y);

            if (tn != null)
            {
                this.SelectedNode = tn;

                if (tn.Level == 0)
                {
                    if (tn.IsExpanded == true)
                    {
                        tn.Collapse();
                    }
                    else
                    {
                        tn.Expand();
                    }
                }
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handles the mouse move event
        /// 
        /// Checks if the node under the cursor contains a tooltip
        /// and display it.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            VSTreeNode currentNode = GetNodeAt(e.X, e.Y) as VSTreeNode;

            if (currentNode == null || this.mPreviousNode == currentNode)
            {
                // Nothing to show. Let's return.
                return;
            }

            if (currentNode.ToolTipText == String.Empty)
            {
                // This item doesn'n contain a tooltip text.
                // Hide the tooltip.
                if (this.mToolTip != null)
                {
                    this.mToolTip.Dispose();
                    this.mToolTip = null;
                }

                return;
            }

            // Store the current node.
            this.mPreviousNode = currentNode;

            string toolTipCaption = currentNode.ToolTipCaption;
            string toolTipText = currentNode.ToolTipText;

            // Turn off the tooltip so we can change the text.
            if (this.mToolTip != null && this.mToolTip.Active)
            {
                this.mToolTip.Dispose();
                this.mToolTip = null;
            }

            // Change the tooltip text.
            this.mToolTip = new ToolTip();
            this.mToolTip.ToolTipTitle = toolTipCaption;
            this.mToolTip.SetToolTip(this, toolTipText);

            // Turn on the tooltip .
            this.mToolTip.Active = true;

        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;

            (e.Node as VSTreeNode).OnEdit = true;

            mLabelEditBox.Bounds = new Rectangle(
              22, e.Node.Bounds.Top + 3, Width - 30, e.Node.Bounds.Height + 4);

            mLabelEditBox.Text = e.Node.Text;
            mLabelEditBox.Visible = true;
            mLabelEditBox.Tag = e.Node;

            mLabelEditBox.Focus();
            mLabelEditBox.SelectAll();
        }

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Creates a new VS 2005 ToolBox like control.
        /// </summary>
        public ToolBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            this.mGroupHeaderFont = new Font("Microsoft Sans Serif",
              8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            this.ShowLines = false;
            this.HotTracking = true;
            this.FullRowSelect = true;
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;

            this.mPreviousNode = null;

            this.mToolTip = new ToolTip();

            this.mLabelEditBox = new TextBox();
            this.mLabelEditBox.BorderStyle = BorderStyle.None;
            this.mLabelEditBox.Visible = false;
            this.mLabelEditBox.LostFocus += new EventHandler(mLabelEditBox_LostFocus);
            this.mLabelEditBox.KeyDown += new KeyEventHandler(mLabelEditBox_KeyDown);

            this.Controls.Add(this.mLabelEditBox);
        }

        #endregion
    }
}