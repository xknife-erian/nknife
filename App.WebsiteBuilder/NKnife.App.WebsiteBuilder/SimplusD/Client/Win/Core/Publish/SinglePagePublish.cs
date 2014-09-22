using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;


namespace Jeelu.SimplusD.Client.Win
{
    public class GetPreviewName
    {

        public static  string GetPageName(string pageId)
        {
            PageSimpleExXmlElement pageEle = Service.Sdsite.CurrentDocument.GetPageElementById(pageId);
            return pageEle.RelativeUrl;
        }

        static public string GetTmpltName(string tmpltId)
        {
            TmpltSimpleExXmlElement tmpltEle = Service.Sdsite.CurrentDocument.GetTmpltElementById(tmpltId);
            return tmpltEle.RelativeUrl;
        }
     
    }
}


   

       
       

   








      

       
     
      

      
      

   
