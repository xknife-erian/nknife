using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using NKnife.Interface;
using NKnife.Utility;

namespace Didaku.Engine.Timeaxis.Implement.Environment
{
    /// <summary>工作流动作管理器的管理池
    /// </summary>
    public class ActivityPool : Dictionary<int, IActivity>, IFeaturePool<int, IActivity>, IInitializer
    {
        public ActivityPool()
        {
            Initialize();
        }
        
        public IEnumerable<ActivityImplAttribute> Attributes { get; set; }

        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            if(!IsInitialized)
            {
                Clear();
                var attrArray = UtilityType.FindAttributeMap<ActivityImplAttribute>(AppDomain.CurrentDomain.BaseDirectory);
                var attributes = new List<ActivityImplAttribute>(attrArray.Count);
                foreach (var pair in attrArray)
                {
                    attributes.Add(pair.First);
                    Add(pair.First.Id, (IActivity) Activator.CreateInstance(pair.Second));
                }
                attributes.Sort();
                Attributes = attributes;
                OnInitialized(EventArgs.Empty);
            }
            return true;
        }

        /// <summary>初始化完成时发生的事件
        /// </summary>
        public event EventHandler InitializedEvent;

        protected virtual void OnInitialized(EventArgs e)
        {
            if (InitializedEvent != null)
                InitializedEvent(this, e);
        }

        #endregion
    }
}
