using System;
using System.Collections.Generic;
using System.Dynamic;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public interface IPictureList
    {
        List<string> FilePathList { get;}
        EventHandler PictureListChanged { get; set; }
        EventHandler<PicutureEventArgs> PictureSelected { get; set; }

        void Clear();

        void Add(string path);

        void AddRange(List<string> paths);

        void RemoveAt(int index);
        void SetSelectedPicuture(string picName);
    }
}
