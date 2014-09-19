using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class AutoLayoutPanelEx : AutoLayoutPanel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected AutoLayoutPanelEx(Type attributeType, TypeAndInterfaceArr typeAndInterface, bool isStatic, bool singleObject)
            : base(attributeType, typeAndInterface, isStatic, singleObject)
        {
        }

        protected override ValueControl CreateValueControl(AutoAttributeData data, GroupBoxEx group)
        {
            return new ValueControlEx(data, group);
        }

        #region 静态工厂方法与静态变量

        /// <summary>
        /// 类型为Key,AutoLayoutPanel控件为value的一个Dictionary
        /// </summary>
        static Dictionary<TypeAndInterfaceArr, AutoLayoutPanelEx> autoPanelDictionary = new Dictionary<TypeAndInterfaceArr, AutoLayoutPanelEx>();

        /// <summary>
        /// 工厂模式创建控件：针对 [对象] 的属性中是否设定定制特性创建
        /// </summary>
        static public AutoLayoutPanel CreatePanel(Type attributeType, object obj, bool needCache, bool singleObject)
        {
            return CreatePanel(attributeType, new object[] { obj }, needCache, singleObject);
        }
        /// <summary>
        /// 工厂模式创建控件：针对 [对象] 的属性中是否设定定制特性创建
        /// </summary>
        static public AutoLayoutPanel CreatePanel(Type attributeType, object[] objs, bool needCache, bool singleObject)
        {
            Type[] listInterface;
            Type objsType = Utility.Type.GetCommonTypeService(objs, out listInterface);
            TypeAndInterfaceArr typeAndInterface = new TypeAndInterfaceArr(objsType, listInterface, attributeType.FullName);
            AutoLayoutPanel tempPanel = CreatePanelCore(attributeType, typeAndInterface, false, needCache, singleObject);
            return tempPanel;
        }

        /// <summary>
        /// 工厂模式创建控件：针对 [静态类] 的属性中是否设定定制特性创建
        /// </summary>
        static public AutoLayoutPanel CreatePanel(Type attributeType, Type staticClassType, bool needCache, bool singleObject)
        {
            TypeAndInterfaceArr typeAndInterface = new TypeAndInterfaceArr(staticClassType, attributeType.FullName);
            AutoLayoutPanel tempPanel = CreatePanelCore(attributeType, typeAndInterface, true, needCache, singleObject);
            return tempPanel;
        }
        /// <summary>
        /// 上两个重载方法的内部子方法：创建Panel
        /// </summary>
        static private AutoLayoutPanelEx CreatePanelCore(Type attributeType, TypeAndInterfaceArr typeAndInterface, bool isStatic, bool needCache, bool singleObject)
        {
            ///不需要缓存，直接chuanjiang创建
            if (!needCache)
            {
                return new AutoLayoutPanelEx(attributeType, typeAndInterface, isStatic, singleObject);
            }

            AutoLayoutPanelEx tempPanel = null;
            ///单建模式AutoLayoutPanel, 
            if (!autoPanelDictionary.TryGetValue(typeAndInterface, out tempPanel) || tempPanel.IsDisposed)
            {
                tempPanel = new AutoLayoutPanelEx(attributeType, typeAndInterface, isStatic, singleObject);
                autoPanelDictionary[typeAndInterface] = tempPanel;
            }
            return tempPanel;
        }

        #endregion
    }
}
