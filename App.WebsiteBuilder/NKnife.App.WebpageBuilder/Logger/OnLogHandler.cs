using System;

namespace Jeelu.Logger
{
    /// <summary>Defines the signature of logging events.</summary>
    public delegate void OnLogHandler(object sender, ILog log);
}