using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Abc.ClassLibrary1;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace NKnife.Reflection.UnitTests
{
    public class AssemblySourceUnitTest
    {
        private readonly ITestOutputHelper _output;

        public AssemblySourceUnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test0()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Abc*.dll");
            foreach (var file in files)
            {
                if (!file.Contains("ClassLibrary1"))
                    continue;

                var map1 = new Dictionary<string, Type>();
                var assembly1 = Assembly.LoadFile(file);
                var types1 = assembly1.GetTypes();

                foreach (var type in types1)
                {
                    map1.Add(type.Name, type);
                }

                var map2 = new Dictionary<string, Type>();
                var assembly2 = Assembly.LoadFrom(file);
                var types2 = assembly2.GetTypes();

                foreach (var type in types2)
                {
                    map2.Add(type.Name, type);
                }

                var map3 = new Dictionary<string, Type>();
                var assembly3 = Assembly.GetExecutingAssembly();
                var assList = assembly3?.GetReferencedAssemblies();
                foreach (var assemblyName in assList)
                {
                    if (assemblyName.FullName.Contains("Library1"))
                    {
                        var a = Assembly.Load(assemblyName);
                        var types3 = a?.GetTypes();

                        foreach (var type in types3)
                        {
                            map3.Add(type.Name, type);
                        }
                    }
                }

                _output.WriteLine($"Assembly.LoadFile(file) -> \tTypesCount: {map1.Count}");
                _output.WriteLine($"Assembly.LoadFrom(file) -> \tTypesCount: {map2.Count}");
                _output.WriteLine($"Assembly.Load(AssemblyName) -> \tTypesCount: {map3.Count}\r\n");

                foreach (var typeName in map1.Keys)
                {
                    switch (typeName)
                    {
                        case nameof(Abc1__OurType_00):
                        {
                            if (map1.ContainsKey(typeName))
                            {
                                _output.WriteLine("-->> Assembly.LoadFile(file)");
                                _output.WriteLine($"HashCode:{map1[typeName].GetHashCode()}");
                                _output.WriteLine($"{map1[typeName] == typeof(Abc1__OurType_00)}");
                                //注意：这种方式的两个类型是不一致的！！！！！！
                                map1[typeName].Should().NotBe(typeof(Abc1__OurType_00));
                            }

                            if (map2.ContainsKey(typeName))
                            {
                                _output.WriteLine("-->> Assembly.LoadFrom(file)");
                                _output.WriteLine($"HashCode:{map2[typeName].GetHashCode()}");
                                _output.WriteLine($"{map2[typeName] == typeof(Abc1__OurType_00)}");
                                //完全相同的两个类型
                                map2[typeName].Should().Be(typeof(Abc1__OurType_00));
                            }

                            if (map3.ContainsKey(typeName))
                            {
                                _output.WriteLine("-->> Assembly.Load(AssemblyName)");
                                _output.WriteLine($"HashCode:{map3[typeName].GetHashCode()}");
                                _output.WriteLine($"{map3[typeName] == typeof(Abc1__OurType_00)}");
                                //完全相同的两个类型
                                map3[typeName].Should().Be(typeof(Abc1__OurType_00));
                            }

                            _output.WriteLine("-->> typeof(Abc1__OurType_00)");
                            _output.WriteLine($"HashCode:{typeof(Abc1__OurType_00).GetHashCode()}");
                            break;
                        }
                    }
                }
            }
        }
    }
}
