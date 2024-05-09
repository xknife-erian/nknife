using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NKnife.Reflection
{
    /// <summary>
    ///     面向程序集文件的工具库
    /// </summary>
    public static class AssemblyUtil
    {
        private static readonly IDictionary<string, Assembly> _AssMap = new Dictionary<string, Assembly>();

        /// <summary>
        ///     搜索指定目录下DotNet程序文件，并将该文件加载成程序集对象。（默认后缀名为"*.dll","*.exe"）
        /// </summary>
        /// <param name="directory">指定的程序集文件.</param>
        /// <param name="searchOption">文件过滤参数.</param>
        /// <param name="suffixNames">指定的后缀名.</param>
        public static Assembly[] SearchForAssembliesInDirectory(string directory, string searchOption = "", params string[] suffixNames)
        {
            var bag = new ConcurrentBag<Assembly>();
            var files = Directory.EnumerateFiles(directory, searchOption)
                .Where(file => EndsWithSuffixNames(file, suffixNames))
                .ToArray();
            Parallel.ForEach(files, file =>
            {
                try
                {
                    if (!_AssMap.ContainsKey(file))
                    {
                        var assembly = Assembly.LoadFrom(file);
                        _AssMap.Add(file, assembly);
                    }

                    bag.Add(_AssMap[file]);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine($"Exception occurred in Assembly.LoadFile，{file} cannot be found.\r\n{e.Message}");
                }
                catch (BadImageFormatException e)
                {
                    Console.WriteLine("{0} is not an Assembly.{1}", file, e.Message);
                }
                catch (FileLoadException e)
                {
                    Console.WriteLine("Exception occurred in Assembly.LoadFile，{0}has already been loaded\r\n{1}", file, e.Message);
                }
            });
            return bag.ToArray();
        }

        public static bool EndsWithSuffixNames(string fileName, params string[] suffixNames)
        {
            var fl = fileName.ToLower();
            if (suffixNames == null || suffixNames.Length <= 0)
                return fl.EndsWith("exe") || fl.EndsWith("dll");

            foreach (var name in suffixNames)
            {
                if (fl.EndsWith(name))
                    return true;
            }

            return false;
        }

        public static Assembly FindAssembly(string empty)
        {
            throw new NotImplementedException();
        }
    }
}