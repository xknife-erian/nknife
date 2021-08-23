using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Record;

namespace Gean.Gui.ChessControl
{
    public class RecordListView : ListView
    {
        public RecordListView()
        {
            this.Number = 1;
            this.InitializeControl();
        }

        /*
        [Event "WCh"]
        [Site "Bonn GER"]
        [Date "2008.10.15"]
        [Round "2"]
        [White "Anand,V"]
        [Black "Kramnik,V"]
        [Result "1/2-1/2"]
        [WhiteElo "2783"]
        [BlackElo "2772"]
        [EventDate "2008.10.14"]
        [ECO "E25"]
        */

        private void InitializeControl()
        {
            ColumnHeader columnHeader01 = new ColumnHeader();
            columnHeader01.Text = "Number";
            ColumnHeader columnHeader02 = new ColumnHeader();
            columnHeader02.Text = "White";
            ColumnHeader columnHeader03 = new ColumnHeader();
            columnHeader03.Text = "WhiteElo";
            ColumnHeader columnHeader04 = new ColumnHeader();
            columnHeader04.Text = "Black";
            ColumnHeader columnHeader05 = new ColumnHeader();
            columnHeader05.Text = "BlackElo";
            ColumnHeader columnHeader06 = new ColumnHeader();
            columnHeader06.Text = "Event";
            ColumnHeader columnHeader07 = new ColumnHeader();
            columnHeader07.Text = "Site";
            ColumnHeader columnHeader08 = new ColumnHeader();
            columnHeader08.Text = "Result";
            ColumnHeader columnHeader09 = new ColumnHeader();
            columnHeader09.Text = "ECO";
            ColumnHeader columnHeader10 = new ColumnHeader();
            columnHeader10.Text = "Date";
            ColumnHeader columnHeader11 = new ColumnHeader();
            columnHeader11.Text = "PlyCount";
            ColumnHeader columnHeader12 = new ColumnHeader();
            columnHeader12.Text = "";
            ColumnHeader columnHeader13 = new ColumnHeader();
            columnHeader13.Text = "";
            ColumnHeader columnHeader14 = new ColumnHeader();
            columnHeader14.Text = "";
            ColumnHeader columnHeader15 = new ColumnHeader();
            columnHeader15.Text = "";
            this.Columns.AddRange(new ColumnHeader[] 
                {
                    columnHeader01,
                    columnHeader02,
                    columnHeader03,
                    columnHeader04,
                    columnHeader05,
                    columnHeader06,
                    columnHeader07,
                    columnHeader08,
                    columnHeader09,
                    columnHeader10,
                    columnHeader11,
                    columnHeader12,
                    columnHeader13,
                    columnHeader14,
                    columnHeader15
                });
            this.View = View.Details;
            this.FullRowSelect = true;
            this.GridLines = true;
        }

        public int Number { get; set; }

        public void Add(Record recode)
        {
            ListViewItem item = new ListViewItem(new string[]
                {
                    this.Number.ToString(),
                    recode.Tags.Update("White","").ToString(),
                    recode.Tags.Update("WhiteElo","").ToString(),
                    recode.Tags.Update("Black","").ToString(),
                    recode.Tags.Update("BlackElo","").ToString(),
                    recode.Tags.Update("Event","").ToString(),
                    recode.Tags.Update("Site","").ToString(),
                    recode.Tags.Update("Result","").ToString(),
                    recode.Tags.Update("ECO","").ToString(),
                    recode.Tags.Update("Date","").ToString(),
                    recode.Tags.Update("PlyCount", recode.Items.Count.ToString()),
                });
            item.Tag = recode;
            this.Items.Add(item);
            this.Number++;
        }

        public Record[] SelectedRecord
        {
            get
            {
                List<Record> records = new List<Record>();
                foreach (ListViewItem item in this.SelectedItems)
                {
                    records.Add(item.Tag as Record);
                }
                return records.ToArray();
            }
        }
    }
}
