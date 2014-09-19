using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using mshtml;
using System.Xml;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public class MdiHomePageDesignForm : BaseEditViewForm , IMarkPosition
    {

        string _homeId = "";
        //IHTMLDocument2 IDoc2 = null;
        //WebBrowser _designWebB = null;
        PageSimpleExXmlElement _elePageSimple;

        public MdiHomePageDesignForm(string homeId)
        {
            this._homeId = homeId;
            this.ShowIcon = true;
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("tree.img.article2").GetHicon());
            
            Label l1 = new Label();
            l1.Text = "此页面为主页面,设计请在模板中完成!";
            l1.Location = new Point(this.Width / 2,this.Height/2);
            l1.Size = new Size(400, 300);
            l1.Font = new Font("黑体", 16);

            Button btnOpenTmplt = new Button();
            btnOpenTmplt.Text = "打开关联模板";
            btnOpenTmplt.Size = new Size(120, 24);
            btnOpenTmplt.Location = new Point(this.Width / 2, this.Height / 2 + 40);
            btnOpenTmplt.Click += new EventHandler(btnOpenTmplt_Click);

            this.Controls.Add(btnOpenTmplt);
            this.Controls.Add(l1);
        }



        void btnOpenTmplt_Click(object sender, EventArgs e)
        {
            Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, _elePageSimple.TmpltId);
        }

        protected override void OnLoad(EventArgs e)
        {
            Debug.Assert(!string.IsNullOrEmpty(_homeId));
            _elePageSimple = Service.Sdsite.CurrentDocument.GetPageElementById(_homeId);
            if (_elePageSimple == null || !File.Exists(_elePageSimple.AbsoluteFilePath))
            {
                MessageService.Show("文件不存在，打开失败！",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this.Text = _elePageSimple.Title;
            base.OnLoad(e);
        }

        #region 重写的基类的实现

        public override WorkDocumentType WorkDocumentType
        {
            get { return WorkDocumentType.HomePage; }
        }

        public override string Id
        {
            get { return _homeId; }
        }


        #endregion

        public string TmpltID 
        {
            get { return _elePageSimple.TmpltId; }            
        }

        #region IMarkPosition 成员

        public void MarkPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public ISearch Search
        {
            get { throw new NotImplementedException(); }
        }

        public List<Position> SelectedPositions
        {
            get { throw new NotImplementedException(); }
        }

        public Position CurrentPosition
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMarkPosition 成员

        void IMarkPosition.MarkPosition(Position position)
        {
            throw new NotImplementedException();
        }

        ISearch IMarkPosition.Search
        {
            get { throw new NotImplementedException(); }
        }

        List<Position> IMarkPosition.SelectedPositions
        {
            get { throw new NotImplementedException(); }
        }

        Position IMarkPosition.CurrentPosition
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
