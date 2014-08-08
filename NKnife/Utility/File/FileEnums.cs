namespace NKnife.Utility.File
{
    public enum FileErrorPolicy
    {
        Inform,
        ProvideAlternative
    }

    public enum FileOperationResult
    {
        OK,
        Failed,
        SavedAlternatively
    }
}
