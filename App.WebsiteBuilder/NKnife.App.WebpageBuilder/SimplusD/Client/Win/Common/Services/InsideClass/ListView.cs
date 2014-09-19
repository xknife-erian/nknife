using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    static public partial class Service
    {
        static public class ListView
        {
            static public DataGridViewColumn[] Columns;
            static public void InitColumn()
            {
                if (Columns == null)
                {
                    Columns = PageXmlDocument.ToDataGridViewColumns();
                }
            }
        }
    }
}
