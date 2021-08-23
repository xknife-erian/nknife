using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NKnife.Util
{
    /// <summary>
    /// 面向程序集文件的工具库
    /// </summary>
    public static class UtilAssembly
    {
        private static IList<string> _assemblyFiles;
        private static readonly Dictionary<string, Assembly[]> _AssembliesMap 
            = new Dictionary<string, Assembly[]>(1); 

        /// <summary>
        ///     搜索指定目录下所有.Net的程序集文件(Dll,Exe)
        /// </summary>
        /// <param name="directory">指定目录.</param>
        /// <returns></returns>
        public static IList<string> SearchAssemblyFileByDirectory(string directory)
        {
            if (_assemblyFiles == null)
            {
                IList<string> dllList = UtilFile.SearchDirectory(directory, "*.dll", true, true);
                IList<string> exeList = UtilFile.SearchDirectory(directory, "*.exe", true, true);
                _assemblyFiles = new List<string>();
                Parallel.ForEach(dllList, dll =>
                {
                    try
                    {
                        AssemblyName.GetAssemblyName(dll);
                        _assemblyFiles.Add(dll);
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{dll} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", dll, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", dll, e.Message);
                    }
                });
                Parallel.ForEach(exeList, exe =>
                {
                    try
                    {
                        AssemblyName.GetAssemblyName(exe);
                        _assemblyFiles.Add(exe);
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{exe} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", exe, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", exe, e.Message);
                    }
                });
            }
            return _assemblyFiles;
        }

        /// <summary>
        ///     搜索指定目录下所有.Net的程序集("*.dll","*.exe")
        /// </summary>
        /// <param name="directory">指定目录.</param>
        public static Assembly[] SearchAssemblyByDirectory(string directory)
        {
            if (!_AssembliesMap.TryGetValue(directory, out var assemblies))
            {
                var result = new ConcurrentBag<Assembly>();
                IList<string> dllList = UtilFile.SearchDirectory(directory, "*.dll", true, true);
                IList<string> exeList = UtilFile.SearchDirectory(directory, "*.exe", true, true);
                Parallel.ForEach(dllList, dll =>
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(dll);
                        result.Add(assembly);
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{dll} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", dll, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", dll, e.Message);
                    }
                });
                Parallel.ForEach(exeList, exe =>
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFile(exe);
                        result.Add(assembly);
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{exe} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", exe, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", exe, e.Message);
                    }
                });
                if (result.Count > 0)
                {
                    assemblies = result.ToArray();
                    _AssembliesMap.Add(directory, assemblies);
                }
            }
            return assemblies;
        }

        /// <summary>
        ///     搜索指定目录下所有.Net的程序集("*.dll","*.exe")
        /// </summary>
        /// <param name="directory">指定目录.</param>
        /// <param name="nameFilters">正则表达式：对程序集文件名进行过滤</param>
        public static Assembly[] SearchAssemblyByDirectory(string directory, params string[] nameFilters)
        {
            if (nameFilters == null)
                return SearchAssemblyByDirectory(directory);

            if (!_AssembliesMap.TryGetValue(directory, out var assemblies))
            {
                var result = new ConcurrentBag<Assembly>();
                IList<string> dllList = UtilFile.SearchDirectory(directory, "*.dll", true, true);
                IList<string> exeList = UtilFile.SearchDirectory(directory, "*.exe", true, true);
                Parallel.ForEach(dllList, dll =>
                {
                    try
                    {
                        if (!dll.MatchFilters(nameFilters))
                        {
                            Assembly assembly = Assembly.LoadFile(dll);
                            result.Add(assembly);
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{dll} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", dll, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", dll, e.Message);
                    }
                });
                Parallel.ForEach(exeList, exe =>
                {
                    try
                    {
                        if (!exe.MatchFilters(nameFilters))
                        {
                            Assembly assembly = Assembly.LoadFile(exe);
                            result.Add(assembly);
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Debug.Fail($"Assembly.LoadFile导常，{exe} cannot be found.\r\n{e.Message}");
                    }
                    catch (BadImageFormatException e)
                    {
                        Console.WriteLine("{0} is not an Assembly.{1}", exe, e.Message);
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("Assembly.LoadFile导常，{0}has already been loaded\r\n{1}", exe, e.Message);
                    }
                });
                if (result.Count > 0)
                {
                    assemblies = result.ToArray();
                    _AssembliesMap.Add(directory, assemblies);
                }
            }
            return assemblies;
        }
    }
}