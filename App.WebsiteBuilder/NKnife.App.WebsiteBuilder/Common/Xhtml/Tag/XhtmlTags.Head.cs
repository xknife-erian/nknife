using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public partial class XhtmlTags
    {
        public class Head : XhtmlTagElement
        {
            internal Head() { }
            /// <summary>
            /// Xhtml文件Meta节点集合
            /// </summary>
            private XhtmlTCollection<XhtmlTags.Meta> _MetaCollection;
            /// <summary>
            /// Xhtml文件Meta节点集合
            /// </summary>
            public XhtmlTCollection<XhtmlTags.Meta> MetaCollection
            {
                get
                {
                    if (this._MetaCollection == null)
                    {
                        this._MetaCollection = new XhtmlTCollection<XhtmlTags.Meta>();
                    }
                    return this._MetaCollection;
                }
            }
            /// <summary>
            /// Xhtml文件Link节点集合
            /// </summary>
            private XhtmlTCollection<XhtmlTags.Link> _LinkCollection;
            /// <summary>
            /// Xhtml文件Link节点集合
            /// </summary>
            public XhtmlTCollection<XhtmlTags.Link> LinkCollection
            {
                get
                {
                    if (this._LinkCollection == null)
                    {
                        this._LinkCollection = new XhtmlTCollection<XhtmlTags.Link>();
                    }
                    return this._LinkCollection;
                }
            }
            /// <summary>
            /// Xhtml文件Script节点集合
            /// </summary>
            private XhtmlTCollection<XhtmlTags.Script> _ScriptCollection;
            /// <summary>
            /// Xhtml文件Script节点集合
            /// </summary>
            public XhtmlTCollection<XhtmlTags.Script> ScriptCollection
            {
                get
                {
                    if (this._ScriptCollection == null)
                    {
                        this._ScriptCollection = new XhtmlTCollection<XhtmlTags.Script>();
                    }
                    return this._ScriptCollection;
                }
            }
            /// <summary>
            /// Xhtml文件Style节点集合
            /// </summary>
            private XhtmlTCollection<XhtmlTags.Style> _StyleCollection;
            /// <summary>
            /// Xhtml文件Style节点集合
            /// </summary>
            public XhtmlTCollection<XhtmlTags.Style> StyleCollection
            {
                get
                {
                    if (this._StyleCollection == null)
                    {
                        this._StyleCollection = new XhtmlTCollection<XhtmlTags.Style>();
                    }
                    return this._StyleCollection;
                }
            }

            internal override void InitializeComponent(XhtmlSection section, string localName)
            {
                base.InitializeComponent(section, localName);

                this.MetaCollection.Inserted += new EventHandler<EventArgs<XhtmlTags.Meta>>(MetaCollection_Inserted);
                this.MetaCollection.Removed += new EventHandler<EventArgs<XhtmlTags.Meta>>(MetaCollection_Removed);
                this.MetaCollection.ItemChanged += new EventHandler<ChangedEventArgs<XhtmlTags.Meta>>(MetaCollection_ItemChanged);

                this.LinkCollection.Inserted += new EventHandler<EventArgs<XhtmlTags.Link>>(LinkCollection_Inserted);
                this.LinkCollection.Removed += new EventHandler<EventArgs<XhtmlTags.Link>>(LinkCollection_Removed);
                this.LinkCollection.ItemChanged += new EventHandler<ChangedEventArgs<XhtmlTags.Link>>(LinkCollection_ItemChanged);

                this.ScriptCollection.Inserted += new EventHandler<EventArgs<XhtmlTags.Script>>(ScriptCollection_Inserted);
                this.ScriptCollection.Removed += new EventHandler<EventArgs<XhtmlTags.Script>>(ScriptCollection_Removed);
                this.ScriptCollection.ItemChanged += new EventHandler<ChangedEventArgs<XhtmlTags.Script>>(ScriptCollection_ItemChanged);

                this.StyleCollection.Inserted += new EventHandler<EventArgs<XhtmlTags.Style>>(StyleCollection_Inserted);
                this.StyleCollection.Removed += new EventHandler<EventArgs<XhtmlTags.Style>>(StyleCollection_Removed);
                this.StyleCollection.ItemChanged += new EventHandler<ChangedEventArgs<XhtmlTags.Style>>(StyleCollection_ItemChanged);
            }

            void StyleCollection_ItemChanged(object sender, ChangedEventArgs<XhtmlTags.Style> e)
            {
                this.ReplaceChild(e.NewItem, e.OldItem);
            }
            void StyleCollection_Removed(object sender, EventArgs<XhtmlTags.Style> e)
            {
                this.RemoveChild(e.Item);
            }
            void StyleCollection_Inserted(object sender, EventArgs<XhtmlTags.Style> e)
            {
                this.AppendChild(e.Item);
            }
            void ScriptCollection_ItemChanged(object sender, ChangedEventArgs<XhtmlTags.Script> e)
            {
                this.ReplaceChild(e.NewItem, e.OldItem);
            }
            void ScriptCollection_Removed(object sender, EventArgs<XhtmlTags.Script> e)
            {
                this.RemoveChild(e.Item);
            }
            void ScriptCollection_Inserted(object sender, EventArgs<XhtmlTags.Script> e)
            {
                this.AppendChild(e.Item);
            }
            void LinkCollection_Removed(object sender, EventArgs<XhtmlTags.Link> e)
            {
                this.RemoveChild(e.Item);
            }
            void LinkCollection_ItemChanged(object sender, ChangedEventArgs<XhtmlTags.Link> e)
            {
                this.ReplaceChild(e.NewItem, e.OldItem);
            }
            void LinkCollection_Inserted(object sender, EventArgs<XhtmlTags.Link> e)
            {
                this.AppendChild(e.Item);
            }
            void MetaCollection_ItemChanged(object sender, ChangedEventArgs<XhtmlTags.Meta> e)
            {
                this.ReplaceChild(e.NewItem, e.OldItem);
            }
            void MetaCollection_Removed(object sender, EventArgs<XhtmlTags.Meta> e)
            {
                this.RemoveChild(e.Item);
            }
            void MetaCollection_Inserted(object sender, EventArgs<XhtmlTags.Meta> e)
            {
                this.AppendChild(e.Item);
            }

        }
    }
}
