using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;

namespace NKnife.App.PictureTextPicker.Common
{
    public class PictureList : IPictureList
    {
        public List<string> FilePathList { get; private set; }
        public EventHandler PictureListChanged { get; set; }

        public PictureList()
        {
            FilePathList = new List<string>();
        }

        public void Clear()
        {
            FilePathList.Clear();
            InvokePictureListChanged();
        }

        public void Add(string path)
        {
            FilePathList.Add(path);
            InvokePictureListChanged();
        }

        public void AddRange(List<string> paths)
        {
            FilePathList.AddRange(paths);
            InvokePictureListChanged();
        }

        public void RemoveAt(int index)
        {
            if (index > -1 && index < FilePathList.Count)
            {
                FilePathList.RemoveAt(index);
                InvokePictureListChanged();
            }
        }

        private void InvokePictureListChanged()
        {
            var handler = PictureListChanged;
            if(handler !=null)
                handler.Invoke(this,EventArgs.Empty);
        }
    }
}
