using System;

namespace NKnife.Interface
{
    public interface IAbout
    {
        string AssemblyTitle { get; }
        Version AssemblyVersion { get; }
        string AssemblyDescription { get; }
        string AssemblyProduct { get; }
        string AssemblyCopyright { get; }
        string AssemblyCompany { get; }
    }
}