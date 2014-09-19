using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class Tag
        {
            public static string[] PaserTags(string values)
            {
                char[] sign = new char[] {'\r', '\n',',','，','.','．','/','／','<','＜','>','＞','?','？',
                '~','!','@','#','$','%','^','&','*','(',')','+','～','！','＠','＃','＄','％','＾','＆','＊','（','）','＿','＋',
                ';','；',':','：','"','＂','\'','＇','\\','＼','|','｜','、','’','。','‘','“','”',']','[','［','］','{','}',
                '｛','｝'};
                string[] tags = values.Split(sign, StringSplitOptions.RemoveEmptyEntries);

                //消重
                List<string> lt = new List<string>();
                foreach (string tag in tags)
                {
                    if (!lt.Contains(tag))
                    {
                        lt.Add(tag);
                    }
                }
                return lt.ToArray();
            }

            public static string PaserTags(string[] tags)
            {
                string allTag = "";
                for (int i = 0; i <= tags.Length - 1; i++)
                {
                    if (i != tags.Length - 1)
                    {
                        allTag += tags[i] + ",";
                    }
                    else
                    {
                        allTag += tags[i];
                    }
                }
                return allTag;
            }
        }
    }
}