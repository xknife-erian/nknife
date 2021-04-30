using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Win.Forms
{
    public class SimpleForm : System.Windows.Forms.Form
    {
        protected SimpleForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer _Components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._Components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // SimpleForm
            // 
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.MinimumSize = new Size(240, 180);
            this.Size = new Size(640, 480);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "SimpleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }
    }
}
