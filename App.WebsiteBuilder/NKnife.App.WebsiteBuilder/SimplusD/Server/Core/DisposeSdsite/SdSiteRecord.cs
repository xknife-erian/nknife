using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Server
{
    static class SdSiteRecord
    {
        static private Dictionary<string, string> _sdsiteMoveDictionary=new Dictionary<string,string>();

        static private List<string> _sdsiteDeleteList = new List<string>();

        static private List<string> _sdsiteSaveList = new List<string>();

        static private Dictionary<string, string> _sdsitePathMoveDictionary = new Dictionary<string, string>();

        static private List<string> _sdsitePathDeleteList = new List<string>();

        static private List<string> _sdsitePathSaveList = new List<string>();

        static public List<string> SdsiteSaveList
        {
            get
            {
                return _sdsiteSaveList;
            }
        }

        static public List<string> SdsiteDeleteList
        {
            get
            {
                return _sdsiteDeleteList;
            }
        }
        static public Dictionary<string, string> SdsiteMoveDictionary
        {
            get
            {
                return _sdsiteMoveDictionary;
            }
        }

        static public List<string> SdsitePathSaveList
        {
            get
            {
                return _sdsiteSaveList;
            }
        }

        static public List<string> SdsitePathDeleteList
        {
            get
            {
                return _sdsiteDeleteList;
            }
        }
        static public Dictionary<string, string> SdsitePathMoveDictionary
        {
            get
            {
                return _sdsiteMoveDictionary;
            }
        }

        static public void AddSdsiteMoveDictionary(string sourceFilePath, string targetFilePath)
        {
            _sdsiteMoveDictionary[sourceFilePath] = targetFilePath;
        }

        static public void AddSdsiteDeleteList(string deleteFilePath)
        {
            _sdsiteDeleteList.Add(deleteFilePath);
        }

        static public void AddSdsiteSaveList(string saveFilePath)
        {
            _sdsiteSaveList.Add(saveFilePath);
        }

        static public void AddSdsitePathMoveDictionary(string sourceFilePath, string targetFilePath)
        {
            _sdsitePathMoveDictionary[sourceFilePath] = targetFilePath;
        }

        static public void AddSdsitePathDeleteList(string deleteFilePath)
        {
            _sdsitePathDeleteList.Add(deleteFilePath);
        }

        static public void AddSdsitePathSaveList(string saveFilePath)
        {
            _sdsitePathSaveList.Add(saveFilePath);
        }

    }
}
