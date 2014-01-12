using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace NKnife.Utility
{
    /// <summary>对一些压缩方法的封装
    /// </summary>
    public class UtilityCompression
    {
        /// <summary>判断指定的字节数组是否是被GZip压缩过的
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        ///   <c>true</c> if the specified bytes is compressed; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCompressed(byte[] bytes)
        {
            if (UtilityCollection.IsNullOrEmpty(bytes))
                return false;
            return (bytes[0] == 31) && (bytes[1] == 139);
        }

        /// <summary>压缩指定的字节数组
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                var compress = new GZipStream(ms, CompressionMode.Compress);
                compress.Write(bytes, 0, bytes.Length);
                compress.Close();
                return ms.ToArray();
            }
        }

        /// <summary>解压指定的字节数组
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static byte[] Decompress(Byte[] bytes)
        {
            using (var ms = new MemoryStream())
            {
                using (var compressMs = new MemoryStream(bytes))
                {
                    var decompress = new GZipStream(compressMs, CompressionMode.Decompress);
                    decompress.CopyTo(ms);
                    decompress.Close();
                    return ms.ToArray();
                }
            }
        }

        public static class RAR
        {
            public static string RARExecutableFile { get; private set; }

            public static void Initialization(string fileRARExecutable)
            {
                RARExecutableFile = fileRARExecutable;
            }

            /// <summary>
            /// 用RAR命令行执行程序解压指定的压缩文件
            /// </summary>
            /// <param name="compressionFile">指定的待解压缩文件</param>
            /// <returns>返回解压后的目录及文件数等相关信息</returns>
            public static string UnRar(string compressionFile)
            {
                //要解压的文件的路径，请自行设置 
                string rarFilePath = compressionFile;
                //确定要解压到的目录，是系统临时文件夹下，与原压缩文件同名的目录里 
                string unRarDestPath = Path.Combine(Path.GetDirectoryName(compressionFile), Path.GetFileNameWithoutExtension(rarFilePath));
                //组合出需要shell的完整格式 
                string shellArguments = String.Format("x -o+ \"{0}\" \"{1}\\\"", rarFilePath, unRarDestPath);

                //用Process调用 
                using (var unrar = new Process())
                {
                    unrar.StartInfo.FileName = RARExecutableFile;
                    unrar.StartInfo.Arguments = shellArguments;
                    unrar.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//隐藏rar本身的窗口 
                    unrar.Start();
                    unrar.WaitForExit();//等待解压完成  
                    unrar.Close();
                }

                //统计解压后的目录和文件数 
                var di = new DirectoryInfo(unRarDestPath);
                string info = String.Format("解压完成，共解压出：{0}个目录，{1}个文件",
                                            di.GetDirectories().Length,
                                            di.GetFiles().Length);
                return info;
            }
        }
    }
}

/*
更多WinRAR命令(引自WinRAR帮助文档)
a 添加文件到压缩文件
c 添加压缩文件注释
d 从压缩文件删除文件
e 从压缩文件解压压缩，忽略路径
f 刷新压缩文件中的文件
i 在压缩文件中查找字符串
k 锁定压缩文件
m 移动文件和文件夹到压缩文件
r 修复受损的压缩文件
rc 重建丢失的卷
rn 重命名压缩文件
rr[N] 添加数据恢复记录
rv[N] 创建恢复卷
s[name] 转换压缩文件成为自解压文件类型
s- 删除自解压模块
t 测试压缩文件
u 从压缩文件中更新文件
x 以完整路径名称从压缩文件解压压缩

更多WinRAR字母开头(引自WinRAR帮助文档)
-ac 在压缩或解压后清除存档属性
-ad 附加压缩文件名到目标路径中
-af<类型> 指定压缩文件格式
-ag[格式] 以当前日期生成压缩文件名
-ao 添加有存档属性设置的
-ap 设置内部压缩文件路径
-as 同步化压缩文件内容
-av 应用用户身份校验信息
-av- 禁用添加用户身份校验信息
-cfg- 忽略默认配置和环境变量
-cl 将文件名转换成为小写
-cu 将文件名转换成为大写
-df 压缩后删除压缩文件
-dh 打开共享的文件
-ds 不排序压缩的文件
-ed 不添加空文件夹
-en 不添加“压缩文件结束”块
-ep 从名称中排除路径
-ep1 从名称中排除主文件夹
-ep2 扩大成完整路径
-ep3 扩展包含盘符的完整路径
-e[+]<属性> 设置文件排除和包含属性
-f 刷新文件
-hp[密码] 加密文件数据和头
-ibck 在后台运行 WinRAR
-ieml 使用E-mail发送压缩文件
-iicon<名称> 指定自解压图标
-iimg<名称> 指定自解压图片
-ilog[名称] 记录错误到文件中
-inul 关闭错误信息
-ioff 关闭 PC 电源
-k 锁定压缩文件
-kb 保留坏掉的解压文件
-m<n> 设置压缩方式
-mc<参数> 设置高级压缩参数
-md<n> 选择字典大小
-ms 指定文件存储
-n<文件> 仅包含指定的文件
-n@<列表文件> 使用指定的列表文件包含文件
-os 保存 NTFS 数据流
-oc 设置 NTFS “压缩”属性
-ow 处理文件用户身份校验信息
-o+ 覆盖已存在的文件
-o- 不要覆盖已存在的文件
-p[密码] 设置密码
-r 返回子文件夹
-r0 只返回与通配符匹配的子文件夹
-ri 设置优先级和休眠时间
-rr[N] 添加数据恢复记录
-rv[N] 创建恢复卷
-s 创建固实压缩文件
-s<N> 以文件数量创建固实组
-se 以文件扩展名创建固实组
-sfx[名称] 创建自解压文件
-sv 创建互不依赖的固实压缩文件
-sv- 创建互相依赖的固实压缩文件
-s- 禁用固实算法
-t 压缩后测试文件
-ta<日期> 只处理指定日期之后修改的文件
-tb<日期> 只处理指定日期之前修改的文件
-tk 保持原有压缩文件时间
-tl 以最新的文件设置压缩文件时间
-tn<时间> 处理较新于指定时间的文件
-to<时间> 处理较旧于指定时间的文件
-ts<m,c,a> 保存或恢复文件时间(修改,创建,访问)
-u 更新文件
-v<n>[k|b|f|m|M|g|G] 创建分卷压缩
-vd 创建分卷压缩前清除磁盘内容
-ver 文件版本控制
-vn 使用旧风格的卷命名法则
-vp 每次分卷压缩前暂停
-x<文件> 排除指定的文件
-x@<文件列表> 使用指定的列表文件来排除指定的文件
-y 假设全部的询问回应皆为“是”
-z<文件> 从文件读取压缩文件注释
 */
