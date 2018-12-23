﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ninject;
using Common.Logging;
using NKnife.Interface;
using NKnife.IoC;

namespace NKnife.Protocol.Generic
{
    /// <summary>协议的抽象实现
    /// </summary>
    public class StringProtocol : IProtocol<string>
    {
        public StringProtocol() 
            : this(string.Empty, string.Empty)
        {
        }

        protected StringProtocol(string family, string command)
        {
            Information = new Dictionary<string, string>(0);
            Tags = new List<object>(0);
            Family = family;
            Command = command;
        }

        #region IProtocol Members

        /// <summary>协议族名称
        /// </summary>
        /// <value>The family.</value>
        public string Family { get; set; }

        public string Command { get; set; }

        public string CommandParam { get; set; }
        public Dictionary<string, string> Information { get; private set; }
        public List<object> Tags { get; set; }

        public StringProtocol NewInstance()
        {
            var protocol = Di.Get<StringProtocol>();
            protocol.Family = Family;
            protocol.Command = Command;
            return protocol;
        }

        IProtocol<string> IProtocol<string>.NewInstance()
        {
            return NewInstance();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StringProtocol) obj);
        }

        protected bool Equals(StringProtocol other)
        {
            return string.Equals(Family, other.Family) &&
                   Equals(Command, other.Command) &&
                   Equals(CommandParam, other.CommandParam) &&
                   Equals(Tags, other.Tags) &&
                   Equals(Information, other.Information);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Command != null ? Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Family != null ? Family.GetHashCode() : 0);
                return hashCode;
            }
        }
        #endregion
    }
}