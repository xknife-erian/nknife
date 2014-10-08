using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NKnife.App.PictureTextPicker.Common.Base;

namespace NKnife.App.PictureTextPicker.Common.Entities
{
    public class PictureList : IPictureList
    {
        private readonly List<PictureFrameDocument> _FilePathList;

        public PictureFrameDocument ActiveDocument { get; private set; }

        public EventHandler PictureListChanged { get; set; }

        public EventHandler PictureSelected { get; set; }

        public PictureList()
        {
            _FilePathList = new List<PictureFrameDocument>();
        }

        public void Clear()
        {
            _FilePathList.Clear();
            InvokePictureListChanged();
        }

        public void Add(PictureFrameDocument doc)
        {
            _FilePathList.Add(doc);
            InvokePictureListChanged();
        }

        public void AddRange(List<PictureFrameDocument> docs)
        {
            _FilePathList.AddRange(docs);
            InvokePictureListChanged();
        }

        public void RemoveAt(int index)
        {
            if (index > -1 && index < _FilePathList.Count)
            {
                _FilePathList.RemoveAt(index);
                InvokePictureListChanged();
            }
        }

        public PictureFrameDocument GetPictureDocumentByFileName(string imageFullFileName)
        {
            return _FilePathList.First(doc => doc
                .ImageFullFileName
                .Equals(
                    imageFullFileName));
        }

        public void SetActiveDocumentByFileName(string imageFullFileName)
        {
            ActiveDocument = _FilePathList.First(doc => doc
                .ImageFullFileName
                .Equals(
                    imageFullFileName));
            if (ActiveDocument != null)
                InvokePictureSelected();

        }

        private void InvokePictureListChanged()
        {
            var handler = PictureListChanged;
            if(handler !=null)
                handler.Invoke(this,EventArgs.Empty);
        }

        private void InvokePictureSelected()
        {
            var handler = PictureSelected;
            if(handler !=null)
                handler.Invoke(this,EventArgs.Empty);
        }
    }
}
