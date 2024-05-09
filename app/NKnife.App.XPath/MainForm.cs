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
using NKnife.App.XPath.Performance;

namespace NKnife.App.XPath
{
    public class MainForm : Form
    {
        private readonly XmlDocument _xmlDoc = new XmlDocument();
        private readonly string _xmlString = "<?xml version=\"1.0\"?>";
        private Hashtable _attributes = new Hashtable();
        private ContextMenu _CtxAttrContextMenu;
        private bool _hasDefNS;
        private bool _hasNS;
        private bool _hasSiblings;
        private int _level;
        private MainMenu _MainMenu;
        private string _nsDefPrefix;
        private XmlNamespaceManager _nsMgr;
        private string _nsPrefix;
        private string _nsValue;
        private OpenFileDialog _OpenXmlFileDialog;
        private TreeNode _selectedNode;
        private string _strNamespaces;
        private TreeView _treeResult;

        internal string _XmlFileName;
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
        private bool isContextNode;
        private bool isVerbose;
        private Label label1;
        private Label label2;
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
        private StatusBar statusBar;
        private TextBox txtQuery;
        private TextBox txtXmlFileName;

        public MainForm()
        {
            InitializeComponent();
            _XmlFileName = txtXmlFileName.Text;
            mnuVerbose.RadioCheck = true;
            mnuAbbreviate.RadioCheck = true;
            mnuAbbreviate.Checked = true;
        }

        private void _treeResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (!isContextNode)
            {
                ClickAction();
            }
        }

        private void AddAllElementsMenu()
        {
            _CtxAttrContextMenu.MenuItems.Add(new MenuItem("-"));
            var item = new MenuItem("Select All <" + MatchRegex(_treeResult.SelectedNode.Text) + "> elements");
            if (_hasDefNS)
            {
                item.MenuItems.Add(
                    new MenuItem(".//" +
                                 DisplayForm.GetElementName
                                     (_nsDefPrefix + ":",
                                         MatchRegex(_treeResult.SelectedNode.Text)),
                        mnuSelectElements_Click));
            }
            else
            {
                item.MenuItems.Add(
                    new MenuItem(".//" + DisplayForm.GetElementName("", MatchRegex(_treeResult.SelectedNode.Text)),
                        mnuSelectElements_Click));
            }
            _CtxAttrContextMenu.MenuItems.Add(item);
        }

        private void AddAttributesMenu(Menu.MenuItemCollection mnuColl, EventHandler e)
        {
            var enumerator = _attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var str = "";
                var item = new MenuItem(enumerator.Key + str, e);
                mnuColl.Add(item);
            }
        }

        private void btnCSCode_Click(object sender, EventArgs e)
        {
            var str = _XmlFileName;
            var text = txtQuery.Text;
            var str3 = "\r\n";
            var code = "using System;" + str3 + "using System.Xml;" + str3 + "using System.Xml.XPath;" + str3 +
                       "public static void RunXPath()" + str3 + "{" + str3 + "XmlDocument xmlDoc = new XmlDocument();" +
                       str3 + "xmlDoc.Load(@\"" + str + "\");" + str3 + "";
            if (_hasNS)
            {
                code = code + "XmlNamespaceManager nsMgr= new XmlNamespaceManager(xmlDoc.NameTable);" + str3;
                foreach (string str5 in _nsMgr)
                {
                    if (_nsMgr.HasNamespace(str5))
                    {
                        var str6 = _nsMgr.LookupNamespace(str5);
                        var str7 = code;
                        code = str7 + "nsMgr.AddNamespace(\"" + str5 + "\",\"" + str6 + "\");" + str3 + "";
                    }
                }
                var str8 = code;
                code = str8 + "XmlNodeList nodes = xmlDoc.SelectNodes(\"" + text + "\",nsMgr);" + str3 +
                       "foreach(XmlNode node in nodes)" + str3 + "{" + str3 + "// Do anything with node" + str3 +
                       "Console.WriteLine(node.OuterXml);" + str3 + "}" + str3 + "}";
            }
            else
            {
                code = "using System;" + str3 + "using System.Xml;" + str3 + "using System.Xml.XPath;" + str3 +
                       "public static void RunXPath()" + str3 + "{" + str3 + "XmlDocument xmlDoc = new XmlDocument();" +
                       str3 + "xmlDoc.Load(@\"" + str + "\");" + str3 + "XmlNodeList nodes = xmlDoc.SelectNodes(\"" +
                       text + "\");" + str3 + "foreach(XmlNode node in nodes)" + str3 + "{" + str3 +
                       "// Do anything with node" + str3 + "Console.WriteLine(node.OuterXml);" + str3 + "}" + str3 + "}";
            }
            new CodeForm(code).Show(this);
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            btnSelXPath.Enabled = false;
            _treeResult.BeginUpdate();
            _treeResult.Nodes.Clear();
            XmlNode documentElement = _xmlDoc.DocumentElement;
            var node = new TreeNode(_xmlString);
            _treeResult.Nodes.Add(node);
            try
            {
                var timer = new PerformanceTimer();
                timer.Start();
                XmlNodeList list = null;
                if (_hasNS)
                {
                    list = documentElement.SelectNodes(txtQuery.Text, _nsMgr);
                }
                else
                {
                    list = documentElement.SelectNodes(txtQuery.Text);
                }
                timer.Stop();
                statusBar.Text = "XPath Query -- Elapsed time: " + timer.ElapsedTime + " ms";
                for (var i = 0; i < list.Count; i++)
                {
                    Xml2Tree(node, list[i]);
                }
                isContextNode = true;
            }
            catch (XPathException exception)
            {
                MessageBox.Show("Error in XPath Query:" + exception.Message);
                btnSelXPath.Enabled = true;
                isContextNode = false;
                return;
            }
            catch (XsltException exception2)
            {
                MessageBox.Show(exception2.Message);
                btnSelXPath.Enabled = true;
                isContextNode = false;
                return;
            }
            _treeResult.EndUpdate();
            node.Expand();
        }

        private void btnLoadXmlFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnNotepad_Click(object sender, EventArgs e)
        {
            OpenNotePad(_XmlFileName);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenXmlFile(true);
        }

        private void btnSelXPath_Click(object sender, EventArgs e)
        {
            txtQuery.Text = statusBar.Text;
        }

        private void btnShowHelp_Click(object sender, EventArgs e)
        {
            new HelpForm().Show();
        }

        private void btnShowNamespaces_Click(object sender, EventArgs e)
        {
            string str;
            if (_strNamespaces != "")
            {
                str = _strNamespaces + "\n'def' is the prefix of Default Namespace";
            }
            else
            {
                str = "No namespaces";
            }
            MessageBox.Show(str);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _treeResult.ExpandAll();
            }
            else
            {
                _treeResult.CollapseAll();
            }
        }

        private void ClickAction()
        {
            if (!isContextNode)
            {
                var selectedNode = _treeResult.SelectedNode;
                if (selectedNode != null)
                {
                    _xPathExp = "";
                    while (selectedNode.Parent != null)
                    {
                        if (selectedNode.Text.StartsWith("<") && selectedNode.Text.EndsWith(">"))
                        {
                            _level = GetNodeLevel(selectedNode);
                            var prefix = "";
                            var elementName = MatchRegex(selectedNode.Text);
                            if (_hasDefNS && (elementName.IndexOf(":", 0, elementName.Length) == -1))
                            {
                                prefix = _nsDefPrefix + ":";
                            }
                            if (_hasSiblings)
                            {
                                _xPathExp = DisplayForm.GetElementName(prefix, elementName) + "[" +
                                            DisplayForm.GetPositionName(_level) + "]/" + _xPathExp;
                            }
                            else
                            {
                                _xPathExp = DisplayForm.GetElementName(prefix, elementName) + "/" + _xPathExp;
                            }
                        }
                        else
                        {
                            _xPathExp = _xPathExp + DisplayForm.GetTextName();
                        }
                        selectedNode = selectedNode.Parent;
                    }
                    _xPathExp = "/" + _xPathExp;
                    if (_xPathExp.EndsWith("/"))
                    {
                        _xPathExp = _xPathExp.Remove(_xPathExp.Length - 1, 1);
                    }
                    _attributes = GetAttributes(_treeResult.SelectedNode.Text);
                    txtQuery.Text = _xPathExp;
                }
            }
        }

        private void ColorizeChildNodes(TreeNode tNode, Color c)
        {
            foreach (TreeNode node in tNode.Nodes)
            {
                ColorRecursive(node, c);
            }
        }

        private void ColorRecursive(TreeNode treeNode, Color c)
        {
            treeNode.BackColor = c;
            foreach (TreeNode node in treeNode.Nodes)
            {
                ColorRecursive(node, c);
            }
        }

        private void ctxAttr_Click(object sender, EventArgs e)
        {
            _xPathExp = _xPathExp + "/" + DisplayForm.GetAttrName(((MenuItem) sender).Text);
            txtQuery.Text = _xPathExp;
        }

        private void ctxAttr_Popup(object sender, EventArgs e)
        {
            _CtxAttrContextMenu.MenuItems.Clear();
            if (!isContextNode && (_treeResult.SelectedNode.Parent != null))
            {
                AddAttributesMenu(_CtxAttrContextMenu.MenuItems, ctxAttr_Click);
                AddAllElementsMenu();
                GetNodeLevel(_treeResult.SelectedNode);
                if (_hasSiblings && (_attributes.Count > 0))
                {
                    var item = new MenuItem("Select All " + MatchRegex(_treeResult.SelectedNode.Text) + "(s) for");
                    AddAttributesMenu(item.MenuItems, mnuSelectAttributeValue_Click);
                    _CtxAttrContextMenu.MenuItems.Add(item);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillXmlDocument(XmlDocument _xmlDoc)
        {
            _strNamespaces = "";
            _hasDefNS = false;
            _hasNS = false;
            isContextNode = false;
            _treeResult.BeginUpdate();
            _treeResult.Nodes.Clear();
            XmlNode documentElement = _xmlDoc.DocumentElement;
            _nsMgr = new XmlNamespaceManager(_xmlDoc.NameTable);
            var node = new TreeNode(_xmlString);
            _treeResult.Nodes.Add(node);
            Xml2Tree(node, documentElement);
            _treeResult.EndUpdate();
            node.Expand();
        }

        private Hashtable GetAttributes(string input)
        {
            var hashtable = new Hashtable();
            if (input.StartsWith("<") && input.EndsWith(">"))
            {
                hashtable.Clear();
                var none = RegexOptions.None;
                var str = @"\w:";
                var str2 = @"\w.\s-:?/=@#";
                var regex = new Regex("([" + str + "]*=\"[" + str2 + "]*\")", none);
                if (!regex.IsMatch(input))
                {
                    return hashtable;
                }
                var matchs = regex.Matches(input);
                for (var i = 0; i != matchs.Count; i++)
                {
                    var str4 = matchs[i].Groups[0].Value;
                    if (!str4.StartsWith("xmlns"))
                    {
                        var strArray = str4.Split('=');
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
            var num = 1;
            var num2 = 1;
            _hasSiblings = false;
            var str = MatchRegex(node.Text);
            var prevTreeNode = GetPrevTreeNode(node);
            var str2 = "";
            while (prevTreeNode != null)
            {
                str2 = MatchRegex(prevTreeNode.Text);
                if (str.Equals(str2))
                {
                    num++;
                }
                prevTreeNode = GetPrevTreeNode(prevTreeNode);
            }
            if (num > 1)
            {
                _hasSiblings = true;
            }
            var nextTreeNode = GetNextTreeNode(node);
            var str3 = "";
            while (nextTreeNode != null)
            {
                str3 = MatchRegex(nextTreeNode.Text);
                if (str.Equals(str3))
                {
                    num2++;
                }
                nextTreeNode = GetNextTreeNode(nextTreeNode);
            }
            if (num2 > 1)
            {
                _hasSiblings = true;
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
            components = new Container();
            var resources = new ComponentResourceManager(typeof (MainForm));
            label1 = new Label();
            txtXmlFileName = new TextBox();
            btnLoadXmlFile = new Button();
            label2 = new Label();
            txtQuery = new TextBox();
            btnExecuteQuery = new Button();
            _treeResult = new TreeView();
            _CtxAttrContextMenu = new ContextMenu();
            _OpenXmlFileDialog = new OpenFileDialog();
            statusBar = new StatusBar();
            btnSelXPath = new Button();
            checkBox1 = new CheckBox();
            btnShowNamespaces = new Button();
            btnShowHelp = new Button();
            btnCSCode = new Button();
            btnNotepad = new Button();
            btnOpenFile = new Button();
            _MainMenu = new MainMenu(components);
            menuItem1 = new MenuItem();
            mnuOpen = new MenuItem();
            mnuLoad = new MenuItem();
            menuItem4 = new MenuItem();
            mnuNotepad = new MenuItem();
            menuItem6 = new MenuItem();
            mnuGenCode = new MenuItem();
            menuItem8 = new MenuItem();
            mnuExit = new MenuItem();
            menuItem10 = new MenuItem();
            mnuVerbose = new MenuItem();
            mnuAbbreviate = new MenuItem();
            mnuHelp = new MenuItem();
            mnuShowHelp = new MenuItem();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 11);
            label1.Name = "label1";
            label1.Size = new Size(46, 13);
            label1.TabIndex = 0;
            label1.Text = "Xml File:";
            // 
            // txtXmlFileName
            // 
            txtXmlFileName.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                    | AnchorStyles.Right;
            txtXmlFileName.Location = new Point(57, 8);
            txtXmlFileName.Name = "txtXmlFileName";
            txtXmlFileName.Size = new Size(514, 21);
            txtXmlFileName.TabIndex = 0;
            // 
            // btnLoadXmlFile
            // 
            btnLoadXmlFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadXmlFile.Location = new Point(587, 8);
            btnLoadXmlFile.Name = "btnLoadXmlFile";
            btnLoadXmlFile.Size = new Size(72, 23);
            btnLoadXmlFile.TabIndex = 3;
            btnLoadXmlFile.Text = "&Load";
            btnLoadXmlFile.Click += btnLoadXmlFile_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 50);
            label2.Name = "label2";
            label2.Size = new Size(101, 13);
            label2.TabIndex = 3;
            label2.Text = "Enter XPath Query:";
            // 
            // txtQuery
            // 
            txtQuery.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                              | AnchorStyles.Right;
            txtQuery.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtQuery.Location = new Point(112, 40);
            txtQuery.Name = "txtQuery";
            txtQuery.Size = new Size(547, 32);
            txtQuery.TabIndex = 1;
            // 
            // btnExecuteQuery
            // 
            btnExecuteQuery.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExecuteQuery.Location = new Point(667, 40);
            btnExecuteQuery.Name = "btnExecuteQuery";
            btnExecuteQuery.Size = new Size(80, 32);
            btnExecuteQuery.TabIndex = 2;
            btnExecuteQuery.Text = "&Execute";
            btnExecuteQuery.Click += btnExecuteQuery_Click;
            // 
            // _treeResult
            // 
            _treeResult.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                                  | AnchorStyles.Left)
                                 | AnchorStyles.Right;
            _treeResult.ContextMenu = _CtxAttrContextMenu;
            _treeResult.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            _treeResult.ForeColor = SystemColors.WindowText;
            _treeResult.FullRowSelect = true;
            _treeResult.HideSelection = false;
            _treeResult.Location = new Point(8, 87);
            _treeResult.Name = "_treeResult";
            _treeResult.Size = new Size(651, 276);
            _treeResult.TabIndex = 6;
            _treeResult.KeyUp += _treeResult_KeyUp;
            _treeResult.MouseDown += treeResult_MouseDown;
            // 
            // _CtxAttrContextMenu
            // 
            _CtxAttrContextMenu.Popup += ctxAttr_Popup;
            // 
            // _OpenXmlFileDialog
            // 
            _OpenXmlFileDialog.Filter = "XML files|*.xml|All files|*.*";
            _OpenXmlFileDialog.InitialDirectory = "c:\\";
            _OpenXmlFileDialog.Title = "Open XML File";
            // 
            // statusBar
            // 
            statusBar.Location = new Point(0, 404);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(755, 22);
            statusBar.TabIndex = 8;
            statusBar.Text = "Ready";
            // 
            // btnSelXPath
            // 
            btnSelXPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSelXPath.Enabled = false;
            btnSelXPath.Location = new Point(595, 372);
            btnSelXPath.Name = "btnSelXPath";
            btnSelXPath.Size = new Size(152, 23);
            btnSelXPath.TabIndex = 7;
            btnSelXPath.Text = "&Use Selected XPath";
            btnSelXPath.Click += btnSelXPath_Click;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox1.Location = new Point(38, 372);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(184, 24);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Expand All/Collapse All";
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // btnShowNamespaces
            // 
            btnShowNamespaces.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnShowNamespaces.Location = new Point(435, 372);
            btnShowNamespaces.Name = "btnShowNamespaces";
            btnShowNamespaces.Size = new Size(152, 23);
            btnShowNamespaces.TabIndex = 6;
            btnShowNamespaces.Text = "&Show Namespaces";
            btnShowNamespaces.Click += btnShowNamespaces_Click;
            // 
            // btnShowHelp
            // 
            btnShowHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnShowHelp.Location = new Point(8, 372);
            btnShowHelp.Name = "btnShowHelp";
            btnShowHelp.Size = new Size(24, 23);
            btnShowHelp.TabIndex = 9;
            btnShowHelp.Text = "?";
            btnShowHelp.Click += btnShowHelp_Click;
            // 
            // btnCSCode
            // 
            btnCSCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCSCode.Location = new Point(667, 340);
            btnCSCode.Name = "btnCSCode";
            btnCSCode.Size = new Size(80, 23);
            btnCSCode.TabIndex = 10;
            btnCSCode.Text = "&C# Code";
            btnCSCode.Click += btnCSCode_Click;
            // 
            // btnNotepad
            // 
            btnNotepad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNotepad.Location = new Point(667, 87);
            btnNotepad.Name = "btnNotepad";
            btnNotepad.Size = new Size(80, 23);
            btnNotepad.TabIndex = 5;
            btnNotepad.Text = "&Notepad";
            btnNotepad.Click += btnNotepad_Click;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpenFile.Location = new Point(667, 8);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(80, 23);
            btnOpenFile.TabIndex = 4;
            btnOpenFile.Text = "&Open File...";
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // _MainMenu
            // 
            _MainMenu.MenuItems.AddRange(new[]
            {
                menuItem1,
                menuItem10,
                mnuHelp
            });
            // 
            // menuItem1
            // 
            menuItem1.Index = 0;
            menuItem1.MenuItems.AddRange(new[]
            {
                mnuOpen,
                mnuLoad,
                menuItem4,
                mnuNotepad,
                menuItem6,
                mnuGenCode,
                menuItem8,
                mnuExit
            });
            menuItem1.Text = "&File";
            // 
            // mnuOpen
            // 
            mnuOpen.Index = 0;
            mnuOpen.Text = "&Open";
            mnuOpen.Click += btnOpenFile_Click;
            // 
            // mnuLoad
            // 
            mnuLoad.Index = 1;
            mnuLoad.Text = "&Load";
            mnuLoad.Click += btnLoadXmlFile_Click;
            // 
            // menuItem4
            // 
            menuItem4.Index = 2;
            menuItem4.Text = "-";
            // 
            // mnuNotepad
            // 
            mnuNotepad.Index = 3;
            mnuNotepad.Text = "&Notepad";
            mnuNotepad.Click += btnNotepad_Click;
            // 
            // menuItem6
            // 
            menuItem6.Index = 4;
            menuItem6.Text = "-";
            // 
            // mnuGenCode
            // 
            mnuGenCode.Index = 5;
            mnuGenCode.Text = "&Generate Code";
            mnuGenCode.Click += btnCSCode_Click;
            // 
            // menuItem8
            // 
            menuItem8.Index = 6;
            menuItem8.Text = "-";
            // 
            // mnuExit
            // 
            mnuExit.Index = 7;
            mnuExit.Text = "E&xit";
            mnuExit.Click += mnuExit_Click;
            // 
            // menuItem10
            // 
            menuItem10.Index = 1;
            menuItem10.MenuItems.AddRange(new[]
            {
                mnuVerbose,
                mnuAbbreviate
            });
            menuItem10.Text = "&Options";
            // 
            // mnuVerbose
            // 
            mnuVerbose.Index = 0;
            mnuVerbose.Text = "Verbose";
            mnuVerbose.Click += mnuDisplayform_Click;
            // 
            // mnuAbbreviate
            // 
            mnuAbbreviate.Index = 1;
            mnuAbbreviate.Text = "Abbreviate";
            mnuAbbreviate.Click += mnuDisplayform_Click;
            // 
            // mnuHelp
            // 
            mnuHelp.Index = 2;
            mnuHelp.MenuItems.AddRange(new[]
            {
                mnuShowHelp
            });
            mnuHelp.Text = "&Help";
            // 
            // mnuShowHelp
            // 
            mnuShowHelp.Index = 0;
            mnuShowHelp.Text = "&Show";
            mnuShowHelp.Click += btnShowHelp_Click;
            // 
            // MainForm
            // 
            AcceptButton = btnLoadXmlFile;
            AllowDrop = true;
            AutoScaleBaseSize = new Size(5, 14);
            ClientSize = new Size(755, 426);
            Controls.Add(btnOpenFile);
            Controls.Add(btnNotepad);
            Controls.Add(btnCSCode);
            Controls.Add(btnShowHelp);
            Controls.Add(btnShowNamespaces);
            Controls.Add(checkBox1);
            Controls.Add(btnSelXPath);
            Controls.Add(statusBar);
            Controls.Add(_treeResult);
            Controls.Add(btnExecuteQuery);
            Controls.Add(txtQuery);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLoadXmlFile);
            Controls.Add(txtXmlFileName);
            Font = new Font("Tahoma", 8.25F);
            Icon = ((Icon) (resources.GetObject("$this.Icon")));
            Menu = _MainMenu;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Visual XPath";
            Load += MainForm_Load;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            ResumeLayout(false);
            PerformLayout();
        }

        private bool LoadXmlAndVerify(string strXml)
        {
            var document = new XmlDocument();
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
            var timer = new PerformanceTimer();
            timer.Start();
            _xmlDoc.Load(_XmlFileName);
            timer.Stop();
            statusBar.Text = "Xml Document Load Time: " + timer.ElapsedTime + " ms";
            btnExecuteQuery.Enabled = true;
            FillXmlDocument(_xmlDoc);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[]) (e.Data.GetData(DataFormats.FileDrop));
                _XmlFileName = txtXmlFileName.Text = data[0];
                LoadXmlFile();
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
            if (!_XmlFileName.Equals(""))
            {
                txtXmlFileName.Text = _XmlFileName;
                LoadXmlFile();
            }
        }

        private string MatchRegex(string matchStr)
        {
            var regex = new Regex(@"(<|</)?([\w:]+)(\s|>)?");
            var input = matchStr;
            var group = regex.Match(input).Groups[2];
            return group.Value;
        }

        private void mnuDisplayform_Click(object sender, EventArgs e)
        {
            mnuAbbreviate.Checked = !mnuAbbreviate.Checked;
            mnuVerbose.Checked = !mnuVerbose.Checked;
            DisplayForm.isVerbose = isVerbose = !isVerbose;
            ClickAction();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSelectAttributeValue_Click(object sender, EventArgs e)
        {
            var text = ((MenuItem) sender).Text;
            txtQuery.Text = DisplayForm.GetAllElementsName(_xPathExp) + "/@" + text;
        }

        private void mnuSelectElements_Click(object sender, EventArgs e)
        {
            txtQuery.Text = ((MenuItem) sender).Text;
        }

        private void OpenFile()
        {
            btnNotepad.Enabled = !txtXmlFileName.Text.StartsWith("http://");
            OpenXmlFile(!txtXmlFileName.Text.EndsWith(".xml"));
        }

        private void OpenNotePad(string filename)
        {
            Process.Start("notepad.exe", filename);
        }

        private void OpenXmlFile(bool openDialog)
        {
            var text = txtXmlFileName.Text;
            if (openDialog)
            {
                _OpenXmlFileDialog.InitialDirectory = txtXmlFileName.Text;
                if (_OpenXmlFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _XmlFileName = txtXmlFileName.Text = _OpenXmlFileDialog.FileName;
                    try
                    {
                        LoadXmlFile();
                    }
                    catch (XmlException exception)
                    {
                        _XmlFileName = txtXmlFileName.Text = text;
                        MessageBox.Show("Error loading Xml File\n" + exception.Message);
                    }
                }
            }
        }

        private void treeResult_MouseDown(object sender, MouseEventArgs e)
        {
            _selectedNode = _treeResult.GetNodeAt(e.X, e.Y);
            if ((_selectedNode != null) && (_selectedNode.Parent != null))
            {
                _treeResult.SelectedNode = _selectedNode;
                ClickAction();
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
                        for (var i = 0; i < xNode.Attributes.Count; i++)
                        {
                            if (xNode.Attributes[i].Name.StartsWith("xmlns"))
                            {
                                _hasNS = true;
                                var strArray = xNode.Attributes[i].Name.Split('=');
                                if (!strArray[0].Equals("xmlns"))
                                {
                                    var strArray2 = strArray[0].Split(':');
                                    _nsPrefix = strArray2[1];
                                    _nsValue = xNode.Attributes[i].Value;
                                }
                                else
                                {
                                    _nsDefPrefix = _nsPrefix = "def";
                                    _hasDefNS = true;
                                    _nsValue = xNode.Attributes[i].Value;
                                }
                                _nsMgr.AddNamespace(_nsPrefix, _nsValue);
                                var str2 = _strNamespaces;
                                _strNamespaces = str2 + "Prefix = " + _nsPrefix + " : Uri = " + _nsValue + "\n";
                            }
                            var text = node.Text;
                            node.Text = text + " " + xNode.Attributes[i].Name + "=\"" + xNode.Attributes[i].Value + "\"";
                        }
                    }
                    break;

                case XmlNodeType.Attribute:
                {
                    var node4 = new TreeNode(xNode.Value);
                    tNode.Nodes.Add(node4);
                    return;
                }
                case XmlNodeType.Text:
                {
                    var node3 = new TreeNode(xNode.Value);
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
                    LoadXmlFile();
                    return;

                default:
                    return;
            }
            node.Text = node.Text + ">";
            if (xNode.HasChildNodes)
            {
                for (var j = 0; j < xNode.ChildNodes.Count; j++)
                {
                    Xml2Tree(node, xNode.ChildNodes[j]);
                }
            }
            var node2 = new TreeNode("</" + xNode.Name + ">");
            tNode.Nodes.Add(node2);
        }
    }
}