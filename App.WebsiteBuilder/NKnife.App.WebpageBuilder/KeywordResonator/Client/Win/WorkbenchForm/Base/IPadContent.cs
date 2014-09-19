using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    public interface IPadContent : IDisposable
    {
        /// <summary>
        /// Returns the Windows.Control for this pad.
        /// </summary>
        Control Control
        {
            get;
        }

        /// <summary>
        /// Re-initializes all components of the pad. Don't call unless
        /// you know what you do.
        /// </summary>
        void RedrawContent();
    }
}