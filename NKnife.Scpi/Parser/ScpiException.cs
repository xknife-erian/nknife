using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    /**
     * Base class for SCPI-related exceptions
     */

    public class ScpiException : Exception
    {

        public ScpiException(string value)
            : base(value)
        {
        }

    }

    /**
     * An exception that may be raised while parsing a malformed-query, or query
     * that refers to an undefined command.
     */
}
