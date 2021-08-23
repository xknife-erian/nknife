using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace NKnife.SerialBox
{
    public class MultiSendInfoFrame : Form
    {
        private IContainer components;
        private DataGridView dgvMultiSend;
        private Button btnSure;
        private Button btnCancel;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button btnExportMultiSend;
        private Button btnImportMultiSend;
        private DataGridViewCheckBoxColumn Column3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private Button btnClearMultiSendInfo;

        public MultiSendInfoFrame()
        {
            this.InitializeComponent();
            this.dgvInit();
            this.dgvLoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnClearMultiSendInfo_Click(object sender, EventArgs e)
        {
            int num = 0;
            while (num < this.dgvMultiSend.Rows.Count)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= this.dgvMultiSend.Columns.Count)
                    {
                        num++;
                        break;
                    }
                    this.dgvMultiSend.Rows[num].Cells[num2].Value = (num2 != 0) ? ((object) "") : ((object) false);
                    num2++;
                }
            }
        }

        private void btnExportMultiSend_Click(object sender, EventArgs e)
        {
            if (this.exportExcel())
            {
                MessageBox.Show("成功导出条目", "提示");
            }
        }

        private void btnImportMultiSend_Click(object sender, EventArgs e)
        {
            if (this.importExcel(this.dgvMultiSend))
            {
                MessageBox.Show("成功导入条目", "提示");
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            this.dgvSaveData();
            MainFrame.isMultiSendInfoFrameSureClosed = true;
            base.Close();
        }

        public void dgvInit()
        {
            this.dgvMultiSend.Rows.Add(40);
            for (int i = 0; i < this.dgvMultiSend.Rows.Count; i++)
            {
                this.dgvMultiSend.Rows[i].HeaderCell.Value = i.ToString();
            }
        }

        public void dgvLoadData()
        {
            for (int i = 0; i < this.dgvMultiSend.Rows.Count; i++)
            {
                this.dgvMultiSend.Rows[i].Cells[0].Value = MainFrame.strCkbMultiSendList[i] == "1";
                this.dgvMultiSend.Rows[i].Cells[1].Value = MainFrame.strTbxMultiSendList[i];
                this.dgvMultiSend.Rows[i].Cells[2].Value = MainFrame.strReMarkTbxMultiSendList[i];
            }
        }

        public void dgvSaveData()
        {
            for (int i = 0; i < this.dgvMultiSend.Rows.Count; i++)
            {
                MainFrame.strCkbMultiSendList[i] = !((bool) this.dgvMultiSend.Rows[i].Cells[0].Value) ? "0" : "1";
                MainFrame.strTbxMultiSendList[i] = (string) this.dgvMultiSend.Rows[i].Cells[1].Value;
                MainFrame.strReMarkTbxMultiSendList[i] = (string) this.dgvMultiSend.Rows[i].Cells[2].Value;
            }
            Settings.Default.StrCkbMultiSendList = MainFrame.strCkbMultiSendList;
            Settings.Default.StrTbxMultiSendList = MainFrame.strTbxMultiSendList;
            Settings.Default.StrReMarkTbxMultiSendList = MainFrame.strReMarkTbxMultiSendList;
            Settings.Default.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool exportExcel()
        {
            this.dgvMultiSend.Columns.GetColumnCount(DataGridViewElementStates.Visible);
            int rowCount = this.dgvMultiSend.Rows.GetRowCount(DataGridViewElementStates.Visible);
            if ((this.dgvMultiSend.Rows.Count == 0) || (rowCount == 0))
            {
                MessageBox.Show("表中没有数据", "提示");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    Filter = "Excel文件(*.xls)|*.xls",
                    Title = "请选择要导出文件的路径",
                    FileName = DateTime.Now.ToLongDateString()
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // string fileName = dialog.FileName;
                    // Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application) Activator.CreateInstance(System.Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                    // if (application == null)
                    // {
                    //     MessageBox.Show("Excel无法启动", "提示");
                    //     return false;
                    // }
                    // Workbook workbook = application.Workbooks.Add(true);
                    // (Worksheet) workbook.Worksheets[1];
                    // int num2 = 0;
                    // int num3 = 0;
                    // while (true)
                    // {
                    //     if (num3 < this.dgvMultiSend.RowCount)
                    //     {
                    //         num2 = 0;
                    //         int num4 = 1;
                    //         while (true)
                    //         {
                    //             if (num4 >= this.dgvMultiSend.ColumnCount)
                    //             {
                    //                 num3++;
                    //                 break;
                    //             }
                    //             if (this.dgvMultiSend.Columns[num4].Visible)
                    //             {
                    //                 application.Cells[num3 + 1, num2 + 1] = this.dgvMultiSend[num4, num3].Value.ToString();
                    //             }
                    //             num2++;
                    //             num4++;
                    //         }
                    //         continue;
                    //     }
                    //     try
                    //     {
                    //         workbook.Saved = true;
                    //         workbook.SaveCopyAs(fileName);
                    //     }
                    //     catch
                    //     {
                    //         MessageBox.Show("导出失败！该文件被锁定，请先关闭此文件！", "提示");
                    //         return false;
                    //     }
                    //     break;
                    // }
                }
            }
            return true;
        }

        public bool importExcel(DataGridView dgv)
        {
            bool flag = true;
            OleDbConnection connection = null;
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "请选择要导入的Excel文件",
                Filter = "Excel文件(*.xls)|*.xls",
                CheckFileExists = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bool flag2;
                string fileName = dialog.FileName;
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + fileName + ";Extended Properties ='Excel 8.0;HDR=NO;IMEX=1'";
                try
                {
                    connection = new OleDbConnection(connectionString);
                    try
                    {
                        connection.Open();
                        goto TR_0034;
                    }
                    catch
                    {
                        MessageBox.Show("导入失败！该正在使用中或其他异常！", "提示");
                        flag2 = false;
                    }
                    return flag2;
                TR_0034:;
                    DataSet dataSet = null;
                    dataSet = new DataSet();
                    new OleDbDataAdapter("select  * from   [sheet1$]", connectionString).Fill(dataSet, "table1");
                    DataTable table = new DataTable();
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        if (column.Visible && (column.CellType != typeof(DataGridViewCheckBoxCell)))
                        {
                            DataColumn column2 = new DataColumn {
                                ColumnName = column.DataPropertyName
                            };
                            table.Columns.Add(column2);
                        }
                    }
                    int num = 0;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        int num2 = 0;
                        DataRow row2 = table.NewRow();
                        foreach (DataColumn column3 in table.Columns)
                        {
                            if (num2 >= row.ItemArray.Length)
                            {
                                break;
                            }
                            row2[column3] = row[num2];
                            num2++;
                        }
                        table.Rows.Add(row2);
                        if ((num + 1) >= 40)
                        {
                            break;
                        }
                    }
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= dgv.Rows.Count)
                        {
                            int num5 = 0;
                            while (num5 < table.Rows.Count)
                            {
                                int index = 0;
                                while (true)
                                {
                                    if (index >= table.Columns.Count)
                                    {
                                        num5++;
                                        break;
                                    }
                                    dgv.Rows[num5].Cells[index + 1].Value = (table.Rows[num5].ItemArray[index] != DBNull.Value) ? ((string) table.Rows[num5].ItemArray[index]) : "";
                                    index++;
                                }
                            }
                            break;
                        }
                        int num4 = 1;
                        while (true)
                        {
                            if (num4 >= dgv.Columns.Count)
                            {
                                num3++;
                                break;
                            }
                            dgv.Rows[num3].Cells[num4].Value = "";
                            num4++;
                        }
                    }
                    return flag;
                }
                catch (Exception exception)
                {
                    MessageBox.Show("表单名必须为sheet1！", "提示");
                    MessageBox.Show(exception.ToString());
                    connection.Close();
                    flag2 = false;
                }
                finally
                {
                    connection.Close();
                }
                return flag2;
            }
            return false;
        }

        private void InitializeComponent()
        {
            this.dgvMultiSend = new System.Windows.Forms.DataGridView();
            this.btnSure = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportMultiSend = new System.Windows.Forms.Button();
            this.btnImportMultiSend = new System.Windows.Forms.Button();
            this.btnClearMultiSendInfo = new System.Windows.Forms.Button();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSend)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMultiSend
            // 
            this.dgvMultiSend.AllowUserToAddRows = false;
            this.dgvMultiSend.AllowUserToDeleteRows = false;
            this.dgvMultiSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMultiSend.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMultiSend.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvMultiSend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMultiSend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMultiSend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dgvMultiSend.Location = new System.Drawing.Point(-1, -2);
            this.dgvMultiSend.Name = "dgvMultiSend";
            this.dgvMultiSend.RowHeadersWidth = 61;
            this.dgvMultiSend.RowTemplate.Height = 23;
            this.dgvMultiSend.Size = new System.Drawing.Size(625, 399);
            this.dgvMultiSend.TabIndex = 2;
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(531, 3);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(90, 38);
            this.btnSure.TabIndex = 1;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(435, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 38);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnSure);
            this.flowLayoutPanel2.Controls.Add(this.btnCancel);
            this.flowLayoutPanel2.Controls.Add(this.btnExportMultiSend);
            this.flowLayoutPanel2.Controls.Add(this.btnImportMultiSend);
            this.flowLayoutPanel2.Controls.Add(this.btnClearMultiSendInfo);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 403);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(624, 49);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // btnExportMultiSend
            // 
            this.btnExportMultiSend.Location = new System.Drawing.Point(339, 3);
            this.btnExportMultiSend.Name = "btnExportMultiSend";
            this.btnExportMultiSend.Size = new System.Drawing.Size(90, 38);
            this.btnExportMultiSend.TabIndex = 2;
            this.btnExportMultiSend.Text = "导出条目";
            this.btnExportMultiSend.UseVisualStyleBackColor = true;
            this.btnExportMultiSend.Click += new System.EventHandler(this.btnExportMultiSend_Click);
            // 
            // btnImportMultiSend
            // 
            this.btnImportMultiSend.Location = new System.Drawing.Point(243, 3);
            this.btnImportMultiSend.Name = "btnImportMultiSend";
            this.btnImportMultiSend.Size = new System.Drawing.Size(90, 38);
            this.btnImportMultiSend.TabIndex = 4;
            this.btnImportMultiSend.Text = "导入条目";
            this.btnImportMultiSend.UseVisualStyleBackColor = true;
            this.btnImportMultiSend.Click += new System.EventHandler(this.btnImportMultiSend_Click);
            // 
            // btnClearMultiSendInfo
            // 
            this.btnClearMultiSendInfo.Location = new System.Drawing.Point(147, 3);
            this.btnClearMultiSendInfo.Name = "btnClearMultiSendInfo";
            this.btnClearMultiSendInfo.Size = new System.Drawing.Size(90, 38);
            this.btnClearMultiSendInfo.TabIndex = 5;
            this.btnClearMultiSendInfo.Text = "清空";
            this.btnClearMultiSendInfo.UseVisualStyleBackColor = true;
            this.btnClearMultiSendInfo.Click += new System.EventHandler(this.btnClearMultiSendInfo_Click);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "选择";
            this.Column3.Name = "Column3";
            this.Column3.Width = 45;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "指令";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "描述";
            this.Column2.Name = "Column2";
            this.Column2.Width = 300;
            // 
            // MultiSendInfoFrame
            // 
            this.AcceptButton = this.btnSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(624, 452);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.dgvMultiSend);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MultiSendInfoFrame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑条目";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMultiSend)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

