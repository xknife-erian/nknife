using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace Jeelu.SimplusD.Client.Win
{
    class GeneralMethodsForDesign
    {
        public static void deleteColumn(IHTMLElement m_oHTMLCtxMenu)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
            if (cell == null)
                return;
            int index = cell.cellIndex;
            if (index < 0)
                index = 0;
            IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
            if (table == null)
                return;
            htmledit.DeleteCol(table, index);
        }

        public static void insertColumn(IHTMLElement m_oHTMLCtxMenu)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
            int index = 0;
            if (cell != null)
                index = cell.cellIndex;
            IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
            if (table == null)
                return;
            htmledit.InsertCol(table, index);
        }

        public static void deleteRow(IHTMLElement m_oHTMLCtxMenu)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            IHTMLTableRow row = htmledit.GetParentRow(m_oHTMLCtxMenu);
            int index = 0;
            if (row != null)
                index = row.rowIndex;
            IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
            if (table == null)
                return;
            htmledit.DeleteRow(table, index);
        }

        public static void insertRow(IHTMLElement m_oHTMLCtxMenu)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
            IHTMLTableRow row = htmledit.GetParentRow(m_oHTMLCtxMenu);
            int index = 0;
            if (row != null)
                index = row.rowIndex;
            IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
            if (table == null)
                return;
            htmledit.InsertRow(table, index, htmledit.Row_GetCellCount(row));
        }

        public static void changeRows(IHTMLTable table, int setrowcount)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            int rowcount = htmledit.GetRowCount(table);
            int colcount = htmledit.GetColCount(table, 0);
            int addrowcount = setrowcount - rowcount;
            if (addrowcount > 0)
            {
                for (int i = 0; i < addrowcount; i++)
                {
                    htmledit.InsertRow(table, rowcount++ - 1, colcount);
                }
            }
            else if (addrowcount < 0)
            {
                for (int i = 0; i > addrowcount; i--)
                {
                    htmledit.DeleteRow(table, rowcount-- - 1);
                }
            }
        }

        public static void changeColumns(IHTMLTable table, int setcolcount)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();
            int colcount = htmledit.GetColCount(table, 0);
            int rowcount = htmledit.GetRowCount(table);
            int addcolcount = setcolcount - colcount;
            if (addcolcount > 0)
            {
                for (int i = 0; i < addcolcount; i++)
                {
                    htmledit.InsertCol(table, 1);
                }
            }
            else if (addcolcount < 0)
            {
                for (int i = 0; i > addcolcount; i--)
                {
                    htmledit.DeleteCol(table, 1);
                }
            }
        }

        public static void MergerGrids(IHTMLTableCell tcell, int setcolcount)
        {
            HTMLEditHelper htmledit = new HTMLEditHelper();

            tcell.colSpan = 2;
        }

        public IHTMLElement Row_InsertCell(mshtml.IHTMLTableRow row, int index)
        {
            IHTMLElement elem = row.insertCell(index) as IHTMLElement;
            if (elem != null)
                elem.innerHTML = "&nbsp;";
            return elem;
        }
    }
}