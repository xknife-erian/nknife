using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class FileBinding
        {
            public const string SdProjectFileExtension = ".sdsite";
            public const string SdProjectFileType = "SimplusD.site";

            public const string SdPageFileExtension = ".sdpage";
            public const string SdPageFileType = "SimplusD.page";

            public const string SdTmpltFileExtension = ".sdtmplt";
            public const string SdTmpltFileType = "SimplusD.tmplt";

            public static void Initialize()
            {
                RegistryFileBind(SdProjectFileExtension, SdProjectFileType,
                    Path.Combine(PathService.SoftwarePath, @"Image\Shell\sdsite.ico"));

                RegistryFileBind(SdPageFileExtension, SdPageFileType,
                    Path.Combine(PathService.SoftwarePath , @"Image\Shell\sdpage.ico"));

                RegistryFileBind(SdTmpltFileExtension, SdTmpltFileType,
                    Path.Combine(PathService.SoftwarePath, @"Image\Shell\sdtmplt.ico"));
            }

            /// <summary>
            /// ��ע�����ע���ļ���
            /// </summary>
            /// <param name="extension">�ļ���չ��</param>
            /// <param name="fileType">�ļ�����</param>
            private static void RegistryFileBind(string extension, string fileType, string defaultIconPath)
            {
                /////����.sdsite��
                //RegistryKey sdsiteKey = EnsureRegistryItem(Registry.ClassesRoot, extension);
                //EnsureRegistryValue(sdsiteKey, string.Empty, fileType);
                //sdsiteKey.Close();

                /////����SimplusD.project��
                //RegistryKey simplusDProject = EnsureRegistryItem(Registry.ClassesRoot, fileType);
                //RegistryKey defaultIcon = EnsureRegistryItem(simplusDProject, "DefaultIcon");
                //EnsureRegistryValue(defaultIcon, null, defaultIconPath);
                //defaultIcon.Close();
                //RegistryKey shell = EnsureRegistryItem(simplusDProject, "shell");
                //EnsureRegistryValue(shell, null, "open");
                //RegistryKey open = EnsureRegistryItem(shell, "open");
                //RegistryKey command = EnsureRegistryItem(open, "command");
                //EnsureRegistryValue(command, null,
                //    string.Format("\"{0}\" -s \"%1\"", Application.ExecutablePath));

                /////�رմ򿪵�ע���
                //command.Close();
                //open.Close();
                //shell.Close();
                //simplusDProject.Close();
            }

            /// <summary>
            /// ȷ��ע����д���(���û�У������)
            /// </summary>
            private static RegistryKey EnsureRegistryItem(RegistryKey parentKey, string item)
            {
                RegistryKey newKey = parentKey.OpenSubKey(item, true);
                if (newKey == null)
                {
                    //newKey = parentKey.CreateSubKey(item, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                return newKey;
            }
            /// <summary>
            /// ȷ��ע����д�ֵ(���û�У�����ӣ������һ�������޸�)
            /// </summary>
            private static void EnsureRegistryValue(RegistryKey key, string name, object value)
            {
                //object objOld = key.GetValue(name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                //if (!value.Equals(objOld))
                //{
                //    key.SetValue(name, value, RegistryValueKind.String);
                //}
            }
        }
    }
}
