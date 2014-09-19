using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Server
{
    static class SdsiteCallBack
    {
        static List<string> _saveList;
        static Dictionary<string, string> _moveDictionary;
        static List<string> _savePathList;
        static Dictionary<string, string> _movePathDictionsry;
        static public void Initialize(List<string> saveList, Dictionary<string, string> moveDic, List<string> savePathList, Dictionary<string, string> movePathDictionsry)
        {
            _saveList = saveList;
            _moveDictionary = moveDic;
            _savePathList = savePathList;
            _movePathDictionsry = movePathDictionsry;
        }

        static public void CallBack()
        {
            foreach (string saveFilePath in _saveList)
            {
                Jeelu.SimplusD.Server.FileService.FileDelete(saveFilePath);
            }

            foreach (string savePath in _savePathList)
            {
                Jeelu.SimplusD.Server.FileService.FileDelete(savePath);
            }
            foreach (string soureFilePath in _moveDictionary.Keys)
            {
               string deFilePath= _moveDictionary[soureFilePath];
               Jeelu.SimplusD.Server.FolderService.FolderMove(deFilePath, soureFilePath);
            }
            foreach (string savePath in _savePathList)
            {
                Jeelu.SimplusD.Server.FolderService.FolderDelete(savePath);
            }

        }
    }
}
