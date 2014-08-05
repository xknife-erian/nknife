namespace Gean.Client.XPathTool
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using Performance;

    public class MainForm : Form
    {
        private Hashtable _attributes = new Hashtable();
        private bool _hasDefNS;
        private bool _hasNS;
        private bool _hasSiblings;
        private int _level;
        private string _nsDefPrefix;
        private XmlNamespaceManager _nsMgr;
        private string _nsPrefix;
        private string _nsValue;
        private TreeNode _selectedNode;
        private string _strNamespaces;
        private TreeView _treeResult;
        private XmlDocument _xmlDoc = new XmlDocument();
        private string _xmlString = "<?xml version=\"1.0\"?>";
        private string _xPathExp;
        private BooleanOp boolOperation = new BooleanOp();
        private Button btnCSCode;
        private Button btnExecuteQuery;
        private Button btnLoadXmlFile;
        private Button btnNotepad;
        private Button btnOpenFile;
        private Button btnSelXPath;
        private Button btnShowHelp;
        private Button btnShowNamespaces;
        private CheckBox checkBox1;
        private IContainer components;
        private ContextMenu _CtxAttrContextMenu;
        private bool isContextNode = false;
        private bool isVerbose = false;
        private Label label1;
        private Label label2;
        private MainMenu _MainMenu;
        private MenuItem menuItem1;
        private MenuItem menuItem10;
        private MenuItem menuItem4;
        private MenuItem menuItem6;
        private MenuItem menuItem8;
        private MenuItem mnuAbbreviate;
        private MenuItem mnuExit;
        private MenuItem mnuGenCode;
        private MenuItem mnuHelp;
        private MenuItem mnuLoad;
        private MenuItem mnuNotepad;
        private MenuItem mnuOpen;
        private MenuItem mnuShowHelp;
        private MenuItem mnuVerbose;
        private OpenFileDialog _OpenXmlFileDialog;
        private StatusBar statusBar;
        private TextBox txtQuery;
        private TextBox txtXmlFileName;

        internal string _XmlFileName;

        public MainForm()
        {
            this.InitializeComponent();
            this._XmlFileName = this.txtXmlFileName.Text;
            this.mnuVerbose.RadioCheck = true;
            this.mnuAbbreviate.RadioCheck = true;
            this.mnuAbbreviate.Checked = true;
        }

        private void _treeResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (!this.isContextNode)
            {
                this.ClickAction();
            }
        }

        private void AddAllElementsMenu()
        {
            this._CtxAttrContextMenu.MenuItems.Add(new MenuItem("-"));
            MenuItem item = new MenuItem("Select All <" + this.MatchRegex(this._treeResult.SelectedNode.Text) + "> elements");
            if (this._hasDefNS)
            {
                item.MenuItems.Add(
                    new MenuItem(".//" +
                        DisplayForm.GetElementName
                        (this._nsDefPrefix + ":",
                        this.MatchRegex(this._treeResult.SelectedNode.Text)),
                        new EventHandler(this.mnuSelectElements_Click)));
            }
            else
            {
                item.MenuItems.Add(new MenuItem(".//" + DisplayForm.GetElementName("", this.MatchRegex(this._treeResult.SelectedNode.Text)), new EventHandler(this.mnuSelectElements_Click)));
            }
            this._CtxAttrContextMenu.MenuItems.Add(item);
        }

        private void AddAttributesMenu(Menu.MenuItemCollection mnuColl, EventHandler e)
        {
            IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string str = "";
                MenuItem item = new MenuItem(enumerator.Key.ToString() + str, e);
                mnuColl.Add(item);
            }
        }

        private void btnCSCode_Click(object sender, EventArgs e)
        {
            string str = this._XmlFileName;
            string text = this.txtQuery.Text;
            string str3 = "\r\n";
            string code = "using System;" + str3 + "using System.Xml;" + str3 + "using System.Xml.XPath;" + str3 + "public static void RunXPath()" + str3 + "{" + str3 + "XmlDocument xmlDoc = new XmlDocument();" + str3 + "xmlDoc.Load(@\"" + str + "\");" + str3 + "";
            if (this._hasNS)
            {
                code = code + "XmlNamespaceManager nsMgr= new XmlNamespaceManager(xmlDoc.NameTable);" + str3;
                foreach (string str5 in this._nsMgr)
                {
                    if (this._nsMgr.HasNamespace(str5))
                    {
                        string str6 = this._nsMgr.LookupNamespace(str5);
                        string str7 = code;
                        code = str7 + "nsMgr.AddNamespace(\"" + str5 + "\",\"" + str6 + "\");" + str3 + "";
                    }
                }
                string str8 = code;
                code = str8 + "XmlNodeList nodes = xmlDoc.SelectNodes(\"" + text + "\",nsMgr);" + str3 + "foreach(XmlNode node in nodes)" + str3 + "{" + str3 + "// Do anything with node" + str3 + "Console.WriteLine(node.OuterXml);" + str3 + "}" + str3 + "}";
            }
            else
            {
                code = "using System;" + str3 + "using System.Xml;" + str3 + "using System.Xml.XPath;" + str3 + "public static void RunXPath()" + str3 + "{" + str3 + "XmlDocument xmlDoc = new XmlDocument();" + str3 + "xmlDoc.Load(@\"" + str + "\");" + str3 + "XmlNodeList nodes = xmlDoc.SelectNodes(\"" + text + "\");" + str3 + "foreach(XmlNode node in nodes)" + str3 + "{" + str3 + "// Do anything with node" + str3 + "Console.WriteLine(node.OuterXml);" + str3 + "}" + str3 + "}";
            }
            new CodeForm(code).Show(this);
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            this.btnSelXPath.Enabled = false;
            this._treeResult.BeginUpdate();
            this._treeResult.Nodes.Clear();
            XmlNode documentElement = this._xmlDoc.DocumentElement;
            TreeNode node = new TreeNode(this._xmlString);
            this._treeResult.Nodes.Add(node);
            try
            {
                PerformanceTimer timer = new PerformanceTimer();
                timer.Start();
                XmlNodeList list = null;
                if (this._hasNS)
                {
                    list = documentElement.SelectNodes(this.txtQuery.Text, this._nsMgr);
                }
                else
                {
                    list = documentElement.SelectNodes(this.txtQuery.Text);
                }
                timer.Stop();
                this.statusBar.Text = "XPath Query -- Elapsed time: " + timer.ElapsedTime.ToString() + " ms";
                for (int i = 0; i < list.Count; i++)
                {
                    this.Xml2Tree(node, list[i]);
                }
                this.isContextNode = true;
            }
            catch (XPathException exception)
            {
                MessageBox.Show("Error in XPath Query:" + exception.Message);
                this.btnSelXPath.Enabled = true;
                this.isContextNode = false;
                return;
            }
            catch (XsltException exception2)
            {
                MessageBox.Show(exception2.Message);
                this.btnSelXPath.Enabled = true;
                this.isContextNode = false;
                return;
            }
            this._treeResult.EndUpdate();
            node.Expand();
        }

        private void btnLoadXmlFile_Click(object sender, EventArgs e)
        {
            this.OpenFile();
        }

        private void btnNotepad_Click(object sender, EventArgs e)
        {
            this.OpenNotePad(this._XmlFileName);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            this.OpenXmlFile(true);
        }

        private void btnSelXPath_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = this.statusBar.Text;
        }

        private void btnShowHelp_Click(object sender, EventArgs e)
        {
            new HelpForm().Show();
        }

        private void btnShowNamespaces_Click(object sender, EventArgs e)
        {
            string str;
            if (this._strNamespaces != "")
            {
                str = this._strNamespaces + "\n'def' is the prefix of Default Namespace";
            }
            else
            {
                str = "No namespaces";
            }
            MessageBox.Show(str);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this._treeResult.ExpandAll();
            }
            else
            {
                this._treeResult.CollapseAll();
            }
        }

        private void ClickAction()
        {
            if (!this.isContextNode)
            {
                TreeNode selectedNode = this._treeResult.SelectedNode;
                if (selectedNode != null)
                {
                    this._xPathExp = "";
                    while (selectedNode.Parent != null)
                    {
                        if (selectedNode.Text.StartsWith("<") && selectedNode.Text.EndsWith(">"))
                        {
                            this._level = this.GetNodeLevel(selectedNode);
                            string prefix = "";
                            string elementName = this.MatchRegex(selectedNode.Text);
                            if (this._hasDefNS && (elementName.IndexOf(":", 0, elementName.Length) == -1))
                            {
                                prefix = this._nsDefPrefix + ":";
                            }
                            if (this._hasSiblings)
                            {
                                this._xPathExp = DisplayForm.GetElementName(prefix, elementName) + "[" + DisplayForm.GetPositionName(this._level) + "]/" + this._xPathExp;
                            }
                            else
                            {
                                this._xPathExp = DisplayForm.GetElementName(prefix, elementName) + "/" + this._xPathExp;
                            }
                        }
                        else
                        {
                            this._xPathExp = this._xPathExp + DisplayForm.GetTextName();
                        }
                        selectedNode = selectedNode.Parent;
                    }
                    this._xPathExp = "/" + this._xPathExp;
                    if (this._xPathExp.EndsWith("/"))
                    {
                        this._xPathExp = this._xPathExp.Remove(this._xPathExp.Length - 1, 1);
                    }
                    this._attributes = this.GetAttributes(this._treeResult.SelectedNode.Text);
                    this.txtQuery.Text = this._xPathExp;
                }
            }
        }

        private void ColorizeChildNodes(TreeNode tNode, Color c)
        {
            foreach (TreeNode node in tNode.Nodes)
            {
                this.ColorRecursive(node, c);
            }
        }

        private void ColorRecursive(TreeNode treeNode, Color c)
        {
            treeNode.BackColor = c;
            foreach (TreeNode node in treeNode.Nodes)
            {
                this.ColorRecursive(node, c);
            }
        }

        private void ctxAttr_Click(object sender, EventArgs e)
        {
            this._xPathExp = this._xPathExp + "/" + DisplayForm.GetAttrName(((MenuItem)sender).Text);
            this.txtQuery.Text = this._xPathExp;
        }

        private void ctxAttr_Popup(object sender, EventArgs e)
        {
            this._CtxAttrContextMenu.MenuItems.Clear();
            if (!this.isContextNode && (this._treeResult.SelectedNode.Parent != null))
            {
                this.AddAttributesMenu(this._CtxAttrContextMenu.MenuItems, new EventHandler(this.ctxAttr_Click));
                this.AddAllElementsMenu();
                this.GetNodeLevel(this._treeResult.SelectedNode);
                if (this._hasSiblings && (this._attributes.Count > 0))
                {
                    MenuItem item = new MenuItem("Select All " + this.MatchRegex(this._treeResult.SelectedNode.Text) + "(s) for");
                    this.AddAttributesMenu(item.MenuItems, new EventHandler(this.mnuSelectAttributeValue_Click));
                    this._CtxAttrContextMenu.MenuItems.Add(item);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillXmlDocument(XmlDocument _xmlDoc)
        {
            this._strNamespaces = "";
            this._hasDefNS = false;
            this._hasNS = false;
            this.isContextNode = false;
            this._treeResult.BeginUpdate();
            this._treeResult.Nodes.Clear();
            XmlNode documentElement = _xmlDoc.DocumentElement;
            this._nsMgr = new XmlNamespaceManager(_xmlDoc.NameTable);
            TreeNode node = new TreeNode(this._xmlString);
            this._treeResult.Nodes.Add(node);
            this.Xml2Tree(node, documentElement);
            this._treeResult.EndUpdate();
            node.Expand();
        }

        private Hashtable GetAttributes(string input)
        {
            Hashtable hashtable = new Hashtable();
            if (input.StartsWith("<") && input.EndsWith(">"))
            {
                hashtable.Clear();
                RegexOptions none = RegexOptions.None;
                string str = @"\w:";
                string str2 = @"\w.\s-:?/=@#";
                Regex regex = new Regex("([" + str + "]*=\"[" + str2 + "]*\")", none);
                if (!regex.IsMatch(input))
                {
                    return hashtable;
                }
                MatchCollection matchs = regex.Matches(input);
                for (int i = 0; i != matchs.Count; i++)
                {
                    string str4 = matchs[i].Groups[0].Value;
                    if (!str4.StartsWith("xmlns"))
                    {
                        string[] strArray = str4.Split(new char[] { '=' });
                        hashtable.Add(strArray[0], strArray[1]);
                    }
                }
            }
            return hashtable;
        }

        private TreeNode GetNextTreeNode(TreeNode node)
        {
            if (node.NextNode != null)
            {
                return node.NextNode.NextNode;
            }
            return null;
        }

        private int GetNodeLevel(TreeNode node)
        {
            int num = 1;
            int num2 = 1;
            this._hasSiblings = false;
            string str = this.MatchRegex(node.Text);
            TreeNode prevTreeNode = this.GetPrevTreeNode(node);
            string str2 = "";
            while (prevTreeNode != null)
            {
                str2 = this.MatchRegex(prevTreeNode.Text);
                if (str.Equals(str2))
                {
                    num++;
                }
                prevTreeNode = this.GetPrevTreeNode(prevTreeNode);
            }
            if (num > 1)
            {
                this._hasSiblings = true;
            }
            TreeNode nextTreeNode = this.GetNextTreeNode(node);
            string str3 = "";
            while (nextTreeNode != null)
            {
                str3 = this.MatchRegex(nextTreeNode.Text);
                if (str.Equals(str3))
                {
                    num2++;
                }
                nextTreeNode = this.GetNextTreeNode(nextTreeNode);
            }
            if (num2 > 1)
            {
                this._hasSiblings = true;
            }
            return num;
        }

        private TreeNode GetPrevTreeNode(TreeNode node)
        {
            if (node.PrevNode != null)
            {
                return node.PrevNode.PrevNode;
            }
            return null;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtXmlFileName = new System.Windows.Forms.TextBox();
            this.btnLoadXmlFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this._treeResult = new System.Windows.Forms.TreeView();
            this._CtxAttrContextMenu = new System.Windows.Forms.ContextMenu();
            this._OpenXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.btnSelXPath = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnShowNamespaces = new System.Windows.Forms.Button();
            this.btnShowHelp = new System.Windows.Forms.Button();
            this.btnCSCode = new System.Windows.Forms.Button();
            this.btnNotepad = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this._MainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuOpen = new System.Windows.Forms.MenuItem();
            this.mnuLoad = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.mnuNotepad = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.mnuGenCode = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.mnuVerbose = new System.Windows.Forms.MenuItem();
            this.mnuAbbreviate = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuShowHelp = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xml File:";
            // 
            // txtXmlFileName
            // 
            this.txtXmlFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXmlFileName.Location = new System.Drawing.Point(57, 8);
            this.txtXmlFileName.Name = "txtXmlFileName";
            this.txtXmlFileName.Size = new System.Drawing.Size(514, 21);
            this.txtXmlFileName.TabIndex = 0;
            // 
            // btnLoadXmlFile
            // 
            this.btnLoadXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadXmlFile.Location = new System.Drawing.Point(587, 8);
            this.btnLoadXmlFile.Name = "btnLoadXmlFile";
            this.btnLoadXmlFile.Size = new System.Drawing.Size(72, 23);
            this.btnLoadXmlFile.TabIndex = 3;
            this.btnLoadXmlFile.Text = "&Load";
            this.btnLoadXmlFile.Click += new System.EventHandler(this.btnLoadXmlFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter XPath Query:";
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(112, 40);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(547, 32);
            this.txtQuery.TabIndex = 1;
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteQuery.Location = new System.Drawing.Point(667, 40);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(80, 32);
            this.btnExecuteQuery.TabIndex = 2;
            this.btnExecuteQuery.Text = "&Execute";
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // _treeResult
            // 
            this._treeResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._treeResult.ContextMenu = this._CtxAttrContextMenu;
            this._treeResult.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._treeResult.ForeColor = System.Drawing.SystemColors.WindowText;
            this._treeResult.FullRowSelect = true;
            this._treeResult.HideSelection = false;
            this._treeResult.Location = new System.Drawing.Point(8, 87);
            this._treeResult.Name = "_treeResult";
            this._treeResult.Size = new System.Drawing.Size(651, 276);
            this._treeResult.TabIndex = 6;
            this._treeResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this._treeResult_KeyUp);
            this._treeResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeResult_MouseDown);
            // 
            // _CtxAttrContextMenu
            // 
            this._CtxAttrContextMenu.Popup += new System.EventHandler(this.ctxAttr_Popup);
            // 
            // _OpenXmlFileDialog
            // 
            this._OpenXmlFileDialog.Filter = "XML files|*.xml|All files|*.*";
            this._OpenXmlFileDialog.InitialDirectory = "c:\\";
            this._OpenXmlFileDialog.Title = "Open XML File";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 404);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(755, 22);
            this.statusBar.TabIndex = 8;
            this.statusBar.Text = "Ready";
            // 
            // btnSelXPath
            // 
            this.btnSelXPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelXPath.Enabled = false;
            this.btnSelXPath.Location = new System.Drawing.Point(595, 372);
            this.btnSelXPath.Name = "btnSelXPath";
            this.btnSelXPath.Size = new System.Drawing.Size(152, 23);
            this.btnSelXPath.TabIndex = 7;
            this.btnSelXPath.Text = "&Use Selected XPath";
            this.btnSelXPath.Click += new System.EventHandler(this.btnSelXPath_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.Location = new System.Drawing.Point(38, 372);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(184, 24);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Expand All/Collapse All";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnShowNamespaces
            // 
            this.btnShowNamespaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowNamespaces.Location = new System.Drawing.Point(435, 372);
            this.btnShowNamespaces.Name = "btnShowNamespaces";
            this.btnShowNamespaces.Size = new System.Drawing.Size(152, 23);
            this.btnShowNamespaces.TabIndex = 6;
            this.btnShowNamespaces.Text = "&Show Namespaces";
            this.btnShowNamespaces.Click += new System.EventHandler(this.btnShowNamespaces_Click);
            // 
            // btnShowHelp
            // 
            this.btnShowHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowHelp.Location = new System.Drawing.Point(8, 372);
            this.btnShowHelp.Name = "btnShowHelp";
            this.btnShowHelp.Size = new System.Drawing.Size(24, 23);
            this.btnShowHelp.TabIndex = 9;
            this.btnShowHelp.Text = "?";
            this.btnShowHelp.Click += new System.EventHandler(this.btnShowHelp_Click);
            // 
            // btnCSCode
            // 
            this.btnCSCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCSCode.Location = new System.Drawing.Point(667, 340);
            this.btnCSCode.Name = "btnCSCode";
            this.btnCSCode.Size = new System.Drawing.Size(80, 23);
            this.btnCSCode.TabIndex = 10;
            this.btnCSCode.Text = "&C# Code";
            this.btnCSCode.Click += new System.EventHandler(this.btnCSCode_Click);
            // 
            // btnNotepad
            // 
            this.btnNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotepad.Location = new System.Drawing.Point(667, 87);
            this.btnNotepad.Name = "btnNotepad";
            this.btnNotepad.Size = new System.Drawing.Size(80, 23);
            this.btnNotepad.TabIndex = 5;
            this.btnNotepad.Text = "&Notepad";
            this.btnNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(667, 8);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(80, 23);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "&Open File...";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // _MainMenu
            // 
            this._MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem10,
            this.mnuHelp});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpen,
            this.mnuLoad,
            this.menuItem4,
            this.mnuNotepad,
            this.menuItem6,
            this.mnuGenCode,
            this.menuItem8,
            this.mnuExit});
            this.menuItem1.Text = "&File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Index = 0;
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // mnuLoad
            // 
            this.mnuLoad.Index = 1;
            this.mnuLoad.Text = "&Load";
            this.mnuLoad.Click += new System.EventHandler(this.btnLoadXmlFile_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // mnuNotepad
            // 
            this.mnuNotepad.Index = 3;
            this.mnuNotepad.Text = "&Notepad";
            this.mnuNotepad.Click += new System.EventHandler(this.btnNotepad_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "-";
            // 
            // mnuGenCode
            // 
            this.mnuGenCode.Index = 5;
            this.mnuGenCode.Text = "&Generate Code";
            this.mnuGenCode.Click += new System.EventHandler(this.btnCSCode_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 6;
            this.menuItem8.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 7;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuVerbose,
            this.mnuAbbreviate});
            this.menuItem10.Text = "&Options";
            // 
            // mnuVerbose
            // 
            this.mnuVerbose.Index = 0;
            this.mnuVerbose.Text = "Verbose";
            this.mnuVerbose.Click += new System.EventHandler(this.mnuDisplayform_Click);
            // 
            // mnuAbbreviate
            // 
            this.mnuAbbreviate.Index = 1;
            this.mnuAbbreviate.Text = "Abbreviate";
            this.mnuAbbreviate.Click += new System.EventHandler(this.mnuDisplayform_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 2;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuShowHelp});
            this.mnuHelp.Text = "&Help";
            // 
            // mnuShowHelp
            // 
            this.mnuShowHelp.Index = 0;
            this.mnuShowHelp.Text = "&Show";
            this.mnuShowHelp.Click += new System.EventHandler(this.btnShowHelp_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnLoadXmlFile;
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(755, 426);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnNotepad);
            this.Controls.Add(this.btnCSCode);
            this.Controls.Add(this.btnShowHelp);
            this.Controls.Add(this.btnShowNamespaces);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnSelXPath);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this._treeResult);
            this.Controls.Add(this.btnExecuteQuery);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadXmlFile);
            this.Controls.Add(this.txtXmlFileName);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this._MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visual XPath";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private bool LoadXmlAndVerify(string strXml)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.LoadXml(strXml);
            }
            catch (XmlException exception)
            {
                MessageBox.Show("Please double check the Xml Nodes you changed.\n" + exception.Message);
                return false;
            }
            return true;
        }

        private void LoadXmlFile()
        {
            PerformanceTimer timer = new PerformanceTimer();
            timer.Start();
            this._xmlDoc.Load(this._XmlFileName);
            timer.Stop();
            this.statusBar.Text = "Xml Document Load Time: " + timer.ElapsedTime.ToString() + " ms";
            this.btnExecuteQuery.Enabled = true;
            this.FillXmlDocument(this._xmlDoc);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] data = (string[])(e.Data.GetData(DataFormats.FileDrop));
                this._XmlFileName = this.txtXmlFileName.Text = data[0];
                this.LoadXmlFile();
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!this._XmlFileName.Equals(""))
            {
                this.txtXmlFileName.Text = this._XmlFileName;
                this.LoadXmlFile();
            }
        }

        private string MatchRegex(string matchStr)
        {
            Regex regex = new Regex(@"(<|</)?([\w:]+)(\s|>)?");
            string input = matchStr;
            Group group = regex.Match(input).Groups[2];
            return group.Value;
        }

        private void mnuDisplayform_Click(object sender, EventArgs e)
        {
            this.mnuAbbreviate.Checked = !this.mnuAbbreviate.Checked;
            this.mnuVerbose.Checked = !this.mnuVerbose.Checked;
            DisplayForm.isVerbose = this.isVerbose = !this.isVerbose;
            this.ClickAction();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSelectAttributeValue_Click(object sender, EventArgs e)
        {
            string text = ((MenuItem)sender).Text;
            this.txtQuery.Text = DisplayForm.GetAllElementsName(this._xPathExp) + "/@" + text;
        }

        private void mnuSelectElements_Click(object sender, EventArgs e)
        {
            this.txtQuery.Text = ((MenuItem)sender).Text;
        }

        private void OpenFile()
        {
            this.btnNotepad.Enabled = !this.txtXmlFileName.Text.StartsWith("http://");
            this.OpenXmlFile(!this.txtXmlFileName.Text.EndsWith(".xml"));
        }

        private void OpenNotePad(string filename)
        {
            Process.Start("notepad.exe", filename);
        }

        private void OpenXmlFile(bool openDialog)
        {
            string text = this.txtXmlFileName.Text;
            if (openDialog)
            {
                this._OpenXmlFileDialog.InitialDirectory = this.txtXmlFileName.Text;
                if (this._OpenXmlFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this._XmlFileName = this.txtXmlFileName.Text = this._OpenXmlFileDialog.FileName;
                    try
                    {
                        this.LoadXmlFile();
                    }
                    catch (XmlException exception)
                    {
                        this._XmlFileName = this.txtXmlFileName.Text = text;
                        MessageBox.Show("Error loading Xml File\n" + exception.Message);
                    }
                }
            }
        }

        private void treeResult_MouseDown(object sender, MouseEventArgs e)
        {
            this._selectedNode = this._treeResult.GetNodeAt(e.X, e.Y);
            if ((this._selectedNode != null) && (this._selectedNode.Parent != null))
            {
                this._treeResult.SelectedNode = this._selectedNode;
                this.ClickAction();
            }
        }

        private void Xml2Tree(TreeNode tNode, XmlNode xNode)
        {
            TreeNode node;
            Font font;
            switch (xNode.NodeType)
            {
                case XmlNodeType.Element:
                    node = new TreeNode("<" + xNode.Name);
                    node.ForeColor = Color.RoyalBlue;
                    font = new Font("Courier New", 9F);
                    node.NodeFont = font;
                    tNode.Nodes.Add(node);
                    if (xNode.Attributes.Count > 0)
                    {
                        for (int i = 0; i < xNode.Attributes.Count; i++)
                        {
                            if (xNode.Attributes[i].Name.StartsWith("xmlns"))
                            {
                                this._hasNS = true;
                                string[] strArray = xNode.Attributes[i].Name.Split(new char[] { '=' });
                                if (!strArray[0].Equals("xmlns"))
                                {
                                    string[] strArray2 = strArray[0].Split(new char[] { ':' });
                                    this._nsPrefix = strArray2[1];
                                    this._nsValue = xNode.Attributes[i].Value;
                                }
                                else
                                {
                                    this._nsDefPrefix = this._nsPrefix = "def";
                                    this._hasDefNS = true;
                                    this._nsValue = xNode.Attributes[i].Value;
                                }
                                this._nsMgr.AddNamespace(this._nsPrefix, this._nsValue);
                                string str2 = this._strNamespaces;
                                this._strNamespaces = str2 + "Prefix = " + this._nsPrefix + " : Uri = " + this._nsValue + "\n";
                            }
                            string text = node.Text;
                            node.Text = text + " " + xNode.Attributes[i].Name + "=\"" + xNode.Attributes[i].Value + "\"";
                        }
                    }
                    break;

                case XmlNodeType.Attribute:
                    {
                        TreeNode node4 = new TreeNode(xNode.Value);
                        tNode.Nodes.Add(node4);
                        return;
                    }
                case XmlNodeType.Text:
                    {
                        TreeNode node3 = new TreeNode(xNode.Value);
                        node3.ForeColor = Color.Red;
                        font = new Font("Courier New", 9F);
                        node3.NodeFont = font;
                        tNode.Nodes.Add(node3);
                        return;
                    }
                case XmlNodeType.CDATA:
                case XmlNodeType.EntityReference:
                case XmlNodeType.Entity:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.Comment:
                    return;

                case XmlNodeType.Document:
                    this.LoadXmlFile();
                    return;

                default:
                    return;
            }
            node.Text = node.Text + ">";
            if (xNode.HasChildNodes)
            {
                for (int j = 0; j < xNode.ChildNodes.Count; j++)
                {
                    this.Xml2Tree(node, xNode.ChildNodes[j]);
                }
            }
            TreeNode node2 = new TreeNode("</" + xNode.Name + ">");
            tNode.Nodes.Add(node2);
        }
    }
}

