using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Base;

namespace Gean.Module.Chess
{
    public class PositionPair
    {
        public PositionPair(Position first, Position second)
        {
            this.First = first;
            this.Second = second;
        }
        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <value>The first.</value>
        public Position First { get; set; }

        /// <summary>
        /// Gets the second.
        /// </summary>
        /// <value>The second.</value>
        public Position Second { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PositionPair && Equals((PositionPair)obj);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<");
            builder.Append(First.ToString());
            builder.Append(", " + Second);
            builder.Append(">");
            return builder.ToString();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (First.GetHashCode() * 397) ^ Second.GetHashCode();
            }
        }

    }
}
