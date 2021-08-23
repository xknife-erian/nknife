using System;
using System.Reflection;
using Abc.ClassLibrary1;
using Abc.ClassLibrary2;
using Abc.ClassLibrary3;
using Abc.Nf.ClassLibrary9;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace NKnife.Reflection.UnitTests
{
    public class UtilAssemblyUnitTest
    {
        public UtilAssemblyUnitTest(ITestOutputHelper output)
        {
            _output = output;
            var a1 = new Abc1__OurType_29();
            var a2 = new Abc2__OurType_29();
            var a3 = new Abc3__OurType_29();
            var a4 = new Abc_Nf_1__OurType_29();
            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);
            Console.WriteLine(a4);
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        public void SearchForAssembliesInDirectoryTest1()
        {
            var assemblies = UtilAssembly.SearchForAssembliesInDirectory(AppDomain.CurrentDomain.BaseDirectory, "Abc*.dll");
            assemblies.Length.Should().Be(4);
        }

        [Fact]
        public void SearchForAssembliesInDirectoryTest2()
        {
            var assemblies = UtilAssembly.SearchForAssembliesInDirectory(AppDomain.CurrentDomain.BaseDirectory, "Abc*.dll");

            var exe = Assembly.GetExecutingAssembly();
            var names = exe.GetReferencedAssemblies();
            foreach (var name in names)
            {
                if (name.FullName.Contains(nameof(Abc.ClassLibrary1)))
                {
                    foreach (var assembly in assemblies)
                    {
                        if (assembly.FullName != null && assembly.FullName.Contains(nameof(Abc.ClassLibrary1)))
                        {
                            var em = Assembly.Load(name);
                            em.Should().BeSameAs(assembly);
                            em.GetHashCode().Should().Be(assembly.GetHashCode());
                            _output.WriteLine($"===>> {nameof(Abc.ClassLibrary1)}");
                            _output.WriteLine($"{em.GetHashCode()}");
                            _output.WriteLine($"{assembly.GetHashCode()}");
                        }
                    }
                }
                else if (name.FullName.Contains(nameof(Abc.ClassLibrary2)))
                {
                    foreach (var assembly in assemblies)
                    {
                        if (assembly.FullName != null && assembly.FullName.Contains(nameof(Abc.ClassLibrary2)))
                        {
                            var em = Assembly.Load(name);
                            em.Should().BeSameAs(assembly);
                            em.GetHashCode().Should().Be(assembly.GetHashCode());
                            _output.WriteLine($"===>> {nameof(Abc.ClassLibrary2)}");
                            _output.WriteLine($"{em.GetHashCode()}");
                            _output.WriteLine($"{assembly.GetHashCode()}");
                        }
                    }
                }
                else if (name.FullName.Contains(nameof(Abc.ClassLibrary3)))
                {
                    foreach (var assembly in assemblies)
                    {
                        if (assembly.FullName != null && assembly.FullName.Contains(nameof(Abc.ClassLibrary3)))
                        {
                            var em = Assembly.Load(name);
                            em.Should().BeSameAs(assembly);
                            em.GetHashCode().Should().Be(assembly.GetHashCode());
                            _output.WriteLine($"===>> {nameof(Abc.ClassLibrary3)}");
                            _output.WriteLine($"{em.GetHashCode()}");
                            _output.WriteLine($"{assembly.GetHashCode()}");
                        }
                    }
                }
                else if (name.FullName.Contains(nameof(Abc.Nf.ClassLibrary9)))
                {
                    foreach (var assembly in assemblies)
                    {
                        if (assembly.FullName != null && assembly.FullName.Contains(nameof(Abc.Nf.ClassLibrary9)))
                        {
                            var em = Assembly.Load(name);
                            em.Should().BeSameAs(assembly);
                            em.GetHashCode().Should().Be(assembly.GetHashCode());
                            _output.WriteLine($"===>> {nameof(Abc.Nf.ClassLibrary9)}");
                            _output.WriteLine($"{em.GetHashCode()}");
                            _output.WriteLine($"{assembly.GetHashCode()}");
                        }
                    }
                }
            }
        }

        [Fact]
        public void SearchForAssembliesInDirectoryTest3()
        {
            var assemblies = UtilAssembly.SearchForAssembliesInDirectory(AppDomain.CurrentDomain.BaseDirectory, "Abc.Nf*.dll");
            assemblies.Length.Should().Be(1);
        }

        [Fact]
        public void FindAssemblyTest01()
        {
            var ass = UtilAssembly.FindAssembly("");
        }

        [Fact]
        public void EndsWithSuffixNamesTest01()
        {
            var suffixNames = new[] {"abc", "xyz"};

            var fileName = "hello.exe";
            var b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeTrue();

            fileName = "hello.dll";
            b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeTrue();

            fileName = "hello.a";
            b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeFalse();      
            
            fileName = "hello.b";
            b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeFalse();   
            
            fileName = "hello.c";
            b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeFalse();   

            fileName = "hello.abc";
            b = UtilAssembly.EndsWithSuffixNames(fileName);
            b.Should().BeFalse();

            fileName = "hello.abc";
            b = UtilAssembly.EndsWithSuffixNames(fileName, suffixNames);
            b.Should().BeTrue();

            fileName = "hello.abcd";
            b = UtilAssembly.EndsWithSuffixNames(fileName, suffixNames);
            b.Should().BeFalse();

            fileName = "hello.xyz";
            b = UtilAssembly.EndsWithSuffixNames(fileName, suffixNames);
            b.Should().BeTrue();

            fileName = "hello.xyzz";
            b = UtilAssembly.EndsWithSuffixNames(fileName, suffixNames);
            b.Should().BeFalse();
        }
    }
}