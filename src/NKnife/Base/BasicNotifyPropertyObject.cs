using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
namespace NKnife.Base;

public class BasicNotifyPropertyObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) 
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}  

#pragma warning restore CS8632