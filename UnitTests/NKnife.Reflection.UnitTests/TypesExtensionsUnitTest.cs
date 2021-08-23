using System.Reflection;
using Abc.ClassLibrary2;
using Abc.ClassLibrary3.Xyz;
using FluentAssertions;
using Xunit;

namespace NKnife.Reflection.UnitTests
{
    public class TypesExtensionsUnitTest
    {
        [Fact]
        public void CreateObjectTest01()
        {
            var helloType = typeof(Hello);
            var hello = helloType.CreatingObject();
            hello.Should().NotBeNull();
        }

        [Fact]
        public void CreateObjectTest02()
        {
            var helloType = typeof(Hello);

            var hello = helloType.CreatingObject("111", "222", "333");
            hello.Should().BeOfType<Hello>();

            var myHello = (Hello) hello;
            myHello.Aaa.Should().Be("111");
            myHello.Bbb.Should().Be("222");
            myHello.Ccc.Should().Be("333");
        }

        [Fact]
        public void CreateObjectTest03()
        {
            var helloType = typeof(Hello);

            var hello = helloType.CreatingObject(1, 2, 3);
            hello.Should().BeNull();
        }

        [Fact]
        public void CreateObjectTest04()
        {
            var hwType = typeof(CreateObjectByStaticPropertyClass);
            var hwObj1 = hwType.CreatingObjectByStatic("Me1");
            hwObj1.Should().NotBeNull();
            hwObj1.Should().BeOfType<CreateObjectByStaticPropertyClass>();
            var hwObj2 = hwType.CreatingObjectByStatic("Me2");
            hwObj2.Should().NotBeNull();
            hwObj2.Should().BeOfType<CreateObjectByStaticPropertyClass>();
            var hwObj3 = hwType.CreatingObjectByStatic("Me3");
            hwObj3.Should().NotBeNull();
            hwObj3.Should().BeOfType<CreateObjectByStaticPropertyClass>();
        }

        [Fact]
        public void CreateObjectTest05()
        {
            var hwType = typeof(CreateObjectByStaticMethodClass1);
            var hwObj1 = hwType.CreatingObjectByStatic("Build1");
            hwObj1.Should().NotBeNull();
            hwObj1.Should().BeOfType<CreateObjectByStaticMethodClass1>();
            var hwObj2 = hwType.CreatingObjectByStatic("Build2");
            hwObj2.Should().NotBeNull();
            hwObj2.Should().BeOfType<CreateObjectByStaticMethodClass1>();
            var hwObj3 = hwType.CreatingObjectByStatic("Build3");
            hwObj3.Should().NotBeNull();
            hwObj3.Should().BeOfType<CreateObjectByStaticMethodClass1>();
        }


        [Fact]
        public void CreateObjectTest06()
        {
            var hwType = typeof(CreateObjectByStaticMethodClass2);
            var hwObj1 = hwType.CreatingObjectByStatic("Build1", new object[] {1, 2, 3});
            hwObj1.Should().NotBeNull();
            hwObj1.Should().BeOfType<CreateObjectByStaticMethodClass2>();
            ((CreateObjectByStaticMethodClass2) hwObj1).A.Should().Be(1);
            ((CreateObjectByStaticMethodClass2) hwObj1).B.Should().Be(2);
            ((CreateObjectByStaticMethodClass2) hwObj1).C.Should().Be(3);
            var hwObj2 = hwType.CreatingObjectByStatic("Build2", new object[] {1, 2, 3});
            hwObj2.Should().NotBeNull();
            hwObj2.Should().BeOfType<CreateObjectByStaticMethodClass2>();
            ((CreateObjectByStaticMethodClass2) hwObj2).A.Should().Be(1);
            ((CreateObjectByStaticMethodClass2) hwObj2).B.Should().Be(2);
            ((CreateObjectByStaticMethodClass2) hwObj2).C.Should().Be(3);
            var hwObj3 = hwType.CreatingObjectByStatic("Build3", new object[] {1, 2, 3});
            hwObj3.Should().NotBeNull();
            hwObj3.Should().BeOfType<CreateObjectByStaticMethodClass2>();
            ((CreateObjectByStaticMethodClass2) hwObj3).A.Should().Be(1);
            ((CreateObjectByStaticMethodClass2) hwObj3).B.Should().Be(2);
            ((CreateObjectByStaticMethodClass2) hwObj3).C.Should().Be(3);
        }

        [Fact]
        public void CreateObjectTest07()
        {
            var h1 = typeof(CreateObjectByStaticPropertyClass);
            var hwObj1 = h1.CreatingObjectByStatic("Your1");
            hwObj1.Should().BeNull();

            var h2 = typeof(CreateObjectByStaticMethodClass1);
            var hwObj2 = h2.CreatingObjectByStatic("Build999");
            hwObj2.Should().BeNull();

            var h3 = typeof(CreateObjectByStaticMethodClass2);
            var hwObj3 = h3.CreatingObjectByStatic("Build1");
            hwObj3.Should().BeNull();

            var h4 = typeof(CreateObjectByStaticMethodClass2);
            var hwObj4 = h4.CreatingObjectByStatic("Build1", new object[] {1, 2});
            hwObj4.Should().BeNull();

            var h5 = typeof(CreateObjectByStaticMethodClass2);
            var hwObj5 = h5.CreatingObjectByStatic("Build1", new object[] {1, 2, 3, 4, 5});
            hwObj5.Should().BeNull();
        }
    }

    public class AssemblyExtensionsUnitTest
    {
        [Fact]
        public void FindTypeTest()
        {
            var ass = UtilAssembly.FindAssembly(nameof(Abc.ClassLibrary3));
            var type = ass.FindType(typeof(HelloWorld).FullName);
            type.Should().Be<HelloWorld>();
        }
    }
}