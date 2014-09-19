namespace Jeelu.SimplusD
{
    /// <summary>
    /// 文件的状态(未发布、已发布且未被修改、已发布但已被修改)
    /// </summary>
    public enum ServerFileState
    {
        None = 0,
        IsNotPublished = 1,
        IsPublishedAndEdited = 2,
        IsAlreadyPublished = 3
    }
}