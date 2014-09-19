using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class WorkspaceTypeEventArgs : EventArgs
    {
        public readonly WorkspaceType ActiveWorkspaceType;

        public WorkspaceTypeEventArgs(WorkspaceType workspaceType)
        {
            this.ActiveWorkspaceType = workspaceType;
        }
    }
}
