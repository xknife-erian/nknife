using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.SimplusD;

namespace Jeelu.SimplusPagePreviewer
{
    static public class ActiveTmpltAndSnip
    {
       static Dictionary<string, TmpltXmlDocument> _activeTmpltDictionary;
       static Dictionary<string, SnipXmlElement> _activeSnipDictionary;

        /// <summary>
        /// 获取已经运行过Tohtml的TmpltDoc实例，以便ToCSS的产生
        /// </summary>
       static public Dictionary<string, TmpltXmlDocument> ActiveTmpltDictionary
       {
           get
           {
               return _activeTmpltDictionary;
           }
       }

       /// <summary>
       ///  获取已经运行过Tohtml的SnipEle实例，以便ToCSS的产生
       /// </summary>
       static public Dictionary<string, SnipXmlElement> ActiveSnipDictionary
       {
           get
           {
               return _activeSnipDictionary;
           }
       }

      /// <summary>
       /// 将已经运行过Tohtml的TmpltDoc实例加入字典，以便ToCSS的产生
      /// </summary>
       static public void AddTmpltDocIntoDictionary(TmpltXmlDocument tmpltDoc)
       {
           if (_activeTmpltDictionary == null)
           {
               _activeTmpltDictionary = new Dictionary<string, TmpltXmlDocument>();
               _activeTmpltDictionary.Add(tmpltDoc.Id, tmpltDoc);
           }
           else
           {
               if (_activeTmpltDictionary.ContainsKey(tmpltDoc.Id)==true)
               {
                  _activeTmpltDictionary[tmpltDoc.Id] = tmpltDoc;
               }
               else
               {
                    _activeTmpltDictionary.Add(tmpltDoc.Id, tmpltDoc);
               }
           }
       }

       /// <summary>
       ///  将已经运行过Tohtml的SnipEle实例加入字典，以便ToCSS的产生
       /// </summary>
       static public void AddSnipElementIntoDictionary(SnipXmlElement snipEle)
       {
           if (_activeSnipDictionary == null)
           {
               _activeSnipDictionary = new Dictionary<string, SnipXmlElement>();
               _activeSnipDictionary.Add(snipEle.Id, snipEle);
           }
           else
           {
               if (!_activeSnipDictionary.ContainsKey(snipEle.Id))
               {
                   _activeSnipDictionary.Add(snipEle.Id, snipEle);
               }
           }
       }
       
        /// <summary>
        /// 将指定的SnipEle从字典中删除
        /// </summary>
       static public void DeleteSnipElementDictionary(string snipId)
       {
           _activeSnipDictionary.Remove(snipId);
       }

       /// <summary>
       /// 将指定的TmpltDoc从字典中删除
       /// </summary>
       static public void DeleteTmpltDocmentDictionary(string tmpltId)
       {
           _activeTmpltDictionary.Remove(tmpltId);
       }
    }
}
