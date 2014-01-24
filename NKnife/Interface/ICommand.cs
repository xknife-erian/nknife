using System;

namespace NKnife.Interface
{
    /// <summary>
    /// A basic command interface. A command has simply an owner which "runs" the command
    /// and a Run method which invokes the command.
    /// </summary>
    public interface ICommand
    {

        /// <summary>
        /// Returns the owner of the command.
        /// </summary>
        object Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Invokes the command.
        /// </summary>
        void Run();

        /// <summary>
        /// Is called when the Owner property is changed.
        /// </summary>
        event EventHandler OwnerChanged;
    }
}
