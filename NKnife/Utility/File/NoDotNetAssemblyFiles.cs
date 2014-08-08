using System.Collections.Generic;
using System.Linq;

namespace NKnife.Utility.File
{
    public class NoDotNetAssemblyFiles
    {
        public static List<string> _RemoveFiles = new List<string>();
        static NoDotNetAssemblyFiles()
        {
            _RemoveFiles.Add("sdtapi.dll");
            //_RemoveFiles.Add("WltRS.dll");
            _RemoveFiles.Add("Syn_IDCardRead.dll".ToLower());
            _RemoveFiles.Add("Microsoft.DirectX.DirectSound.dll".ToLower());
            _RemoveFiles.Add("microsoft.directx.audiovideoplayback.dll".ToLower());
            _RemoveFiles.Add("Microsoft.DirectX.dll".ToLower());
        }

        /// <summary>判断指定的文件是否是.Net程序集
        /// </summary>
        /// <param name="filename">指定的文件的文件名.</param>
        /// <returns>
        /// 	<c>true</c>是.Net程序集; 反之, <c>false</c>.
        /// </returns>
        public static bool IsDotNetAssembly(string filename)
        {
            return _RemoveFiles.All(removeFile => !filename.ToLower().Contains(removeFile));
        }
    }
}
