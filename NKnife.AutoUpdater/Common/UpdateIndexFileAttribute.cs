using System;

namespace NKnife.AutoUpdater.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class UpdateIndexFileAttribute : Attribute
    {
        public UpdateIndexFileAttribute()
        {
            Version = "1";
        }

        public string Version { get; set; }
    }
}
