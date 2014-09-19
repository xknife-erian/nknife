namespace VS2005ToolBox
{
  partial class ToolBox
  {
		/// <summary>
		/// Required designer variable.
		/// </summary>
    private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // ToolBox
      // 
      this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
      this.HideSelection = false;
      this.HotTracking = true;
      this.Name = "ToolBox1";
      this.ShowLines = false;
      this.ShowNodeToolTips = true;
      this.ShowPlusMinus = false;
      this.ShowRootLines = false;
      this.Size = new System.Drawing.Size(171, 256);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
