using System;

// ReSharper disable InconsistentNaming
namespace Abc.ClassLibrary2
{
    public class CreateObjectByStaticMethodClass1
    {
        private CreateObjectByStaticMethodClass1()
        {
        }

        public static CreateObjectByStaticMethodClass1 Build1()
        {
            return new CreateObjectByStaticMethodClass1();
        }

        public static CreateObjectByStaticMethodClass1 Build2()
        {
            return new CreateObjectByStaticMethodClass1();
        }

        public static CreateObjectByStaticMethodClass1 Build3()
        {
            return new CreateObjectByStaticMethodClass1();
        }

    }

    public class CreateObjectByStaticMethodClass2
    {
        private CreateObjectByStaticMethodClass2(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int A { get; private set; }
        public int B { get;private set; }
        public int C { get;private set; }

        public static CreateObjectByStaticMethodClass2 Build1(int a, int b, int c)
        {
            return new CreateObjectByStaticMethodClass2(a,b,c);
        }

        public static CreateObjectByStaticMethodClass2 Build2(int a, int b, int c)
        {
            return new CreateObjectByStaticMethodClass2(a,b,c);
        }

        public static CreateObjectByStaticMethodClass2 Build3(int a, int b, int c)
        {
            return new CreateObjectByStaticMethodClass2(a,b,c);
        }



    }

    public class CreateObjectByStaticPropertyClass
    {
        private CreateObjectByStaticPropertyClass(){}

        public static CreateObjectByStaticPropertyClass Me1 => new CreateObjectByStaticPropertyClass();
        public static CreateObjectByStaticPropertyClass Me2 => new CreateObjectByStaticPropertyClass();
        public static CreateObjectByStaticPropertyClass Me3 => new CreateObjectByStaticPropertyClass();
    }

    public class Abc2__OurType_00 { }
    public class Abc2__OurType_01 { }
    public class Abc2__OurType_02 { }
    public class Abc2__OurType_03 { }
    public class Abc2__OurType_04 { }
    public class Abc2__OurType_05 { }
    public class Abc2__OurType_06 { }
    public class Abc2__OurType_07 { }
    public class Abc2__OurType_08 { }
    public class Abc2__OurType_09 { }
    public class Abc2__OurType_10 { }
    public class Abc2__OurType_11 { }
    public class Abc2__OurType_12 { }
    public class Abc2__OurType_13 { }
    public class Abc2__OurType_14 { }
    public class Abc2__OurType_15 { }
    public class Abc2__OurType_16 { }
    public class Abc2__OurType_17 { }
    public class Abc2__OurType_18 { }
    public class Abc2__OurType_19 { }
    public class Abc2__OurType_20 { }
    public class Abc2__OurType_21 { }
    public class Abc2__OurType_22 { }
    public class Abc2__OurType_23 { }
    public class Abc2__OurType_24 { }
    public class Abc2__OurType_25 { }
    public class Abc2__OurType_26 { }
    public class Abc2__OurType_27 { }
    public class Abc2__OurType_28 { }
    public class Abc2__OurType_29 { }
}
