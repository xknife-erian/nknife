using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Type
        {
            /// <summary>
            /// 通过object数组获取其中所有object的类型的最上层公共基类
            /// </summary>
            /// <param name="props">object的集合</param>
            /// <param name="interfaceType">输出。共有的接口类型</param>
            /// <returns>共有的最上层继承类</returns>
            static public System.Type GetCommonTypeService(object[] objs, out System.Type[] interfaceType)
            {
                Debug.Assert(objs != null && objs.Length > 0 && objs[0] != null);

                ///若只有一个对象，则返回这个对象的类型
                if (objs.Length == 1)
                {
                    interfaceType = objs[0].GetType().GetInterfaces();
                    return objs[0].GetType();
                }

                interfaceType = new System.Type[0];
                List<System.Type> listClass = new List<System.Type>();
                List<System.Type> listInterface = new List<System.Type>();

                ///获取类型的最小集合(即重复的类型则不添加进来)
                foreach (object obj in objs)
                {
                    if (obj == null)
                    {
                        continue;
                    }
                    System.Type objType = obj.GetType();

                    if (!listClass.Contains(objType))
                    {
                        listClass.Add(objType);
                    }
                }

                ///如果只有一种类型，则直接返回此类型
                if (listClass.Count == 1)
                {
                    return listClass[0];
                }

                ///遍历，找到其共有的Interface
                foreach (System.Type type in listClass)
                {
                    System.Type[] interfaces = type.GetInterfaces();
                    if (interfaces.Length == 0)
                    {
                        listInterface.Clear();
                        break;
                    }

                    ///第一次，添加
                    if (listInterface.Count == 0)
                    {
                        listInterface.AddRange(interfaces);
                    }
                    ///以后则进行比较，仅留相同项
                    else
                    {
                        List<System.Type> willRemoveList = new List<System.Type>();
                        foreach (System.Type typeInterface in listInterface)
                        {
                            int index = Array.IndexOf<System.Type>(interfaces, typeInterface);
                            if (index == -1)
                            {
                                willRemoveList.Add(typeInterface);
                            }
                        }
                        foreach (System.Type subtype in willRemoveList)
                        {
                            listInterface.Remove(subtype);
                        }
                    }
                }
                interfaceType = listInterface.ToArray();

                ///遍历，找其最上层的共有的继承类
                List<System.Type> beforeCommonTypes = new List<System.Type>();
                foreach (System.Type type in listClass)
                {
                    if (beforeCommonTypes.Count == 0)
                    {
                        ///将type的继承链的所有类都添加进去
                        System.Type baseType = type;
                        while (baseType != null)
                        {
                            beforeCommonTypes.Add(baseType);
                            baseType = baseType.BaseType;
                        }
                    }
                    else
                    {
                        ///通过继承链的遍历确保beforeCommanTypes里的元素是公共的
                        System.Type baseType = type;
                        while (baseType != null)
                        {
                            int index = beforeCommonTypes.IndexOf(baseType);
                            if (index >= 0)
                            {
                                ///index==0表示beforeCommanTypes的第一个类型便是当前类型，不用做处理。
                                ///(这种情况第一次遍历肯定不会出现，只有进行过RemoveRange后才有可能)
                                if (index == 0)
                                {
                                }
                                ///index!=0则删除index的前面几位，使index处的System.Type排到第一位
                                else
                                {
                                    beforeCommonTypes.RemoveRange(0, index);
                                }
                                break;
                            }
                            baseType = baseType.BaseType;
                        }
                    }
                }

                ///beforeCommandTypes的第一个元素则是这一些类型的继承链里最上层的共有继承类
                return beforeCommonTypes[0];
            }
        }
    }
}