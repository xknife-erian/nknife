using System;
using System.Collections.Generic;
using System.Dynamic;
using NKnife.App.PictureTextPicker.Common.Entities;

namespace NKnife.App.PictureTextPicker.Common.Base
{
    public interface IPictureList
    {
        PictureFrameDocument ActiveDocument { get;}
        EventHandler PictureListChanged { get; set; }
        EventHandler PictureSelected { get; set; }
        void Clear();

        void Add(PictureFrameDocument doc);

        void AddRange(List<PictureFrameDocument> docs);

        void RemoveAt(int index);
        /// <summary>
        /// 根据完整文件名获得对应的Document对象
        /// </summary>
        /// <param name="imageFullFileName"></param>
        /// <returns></returns>
        PictureFrameDocument GetPictureDocumentByFileName(string imageFullFileName);
        /// <summary>
        /// 根据完整文件名设置对应的Document对象为当前编辑对象
        /// </summary>
        /// <param name="imageFullFileName"></param>
        void SetActiveDocumentByFileName(string imageFullFileName);
    }
}
