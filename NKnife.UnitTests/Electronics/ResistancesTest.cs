using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Electronics;
using NKnife.Electronics.Collections;

namespace NKnife.UnitTests.Electronics
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ResistancesTest
    {
        [TestMethod]
        public void SortTestMethod1()
        {
            var r = new Resistances();
            r.Add(new Resistance() { Value = 1000 });
            r.Add(new Resistance() { Value = 1 });
            r.Add(new Resistance() { Value = 100 });
            r.Add(new Resistance() { Value = 10000 });
            r.Add(new Resistance() { Value = 10 });
            r.Add(new Resistance() { Value = 100000 });

            var expected = 100000;
            for (int i = 0; i < r.Count; i++)
            {
                if (i != 0)
                {
                    var e = Math.Pow(10, i);
                    expected = (int)(100000 / e);
                }
                Assert.AreEqual(expected, r[i].Value);
            }
        }

        [TestMethod]
        public void ParallelingValueGetterTestMethod1()
        {
            var r = new Resistances();
            r.Add(new Resistance() {Value = 3});
            r.Add(new Resistance() {Value = 3});
            r.Add(new Resistance() {Value = 3});
            r.CircuitType = CircuitType.Paralleling;
            Assert.AreEqual(1, r.EquivalentValue);
        }

        [TestMethod]
        public void ParallelingValueGetterTestMethod2()
        {
            var r = new Resistances();
            r.Add(new Resistance() { Value = 7 });
            r.Add(new Resistance() { Value = 8 });
            r.Add(new Resistance() { Value = 9 });
            r.CircuitType = CircuitType.Paralleling;
            Assert.AreEqual(2.6387, Math.Round(r.EquivalentValue,4));
        }

        [TestMethod]
        public void ParallelingValueGetterTestMethod3()
        {
            var r = new Resistances();
            r.Add(new Resistance() { Value = 123 });
            r.Add(new Resistance() { Value = 234 });
            r.Add(new Resistance() { Value = 345 });
            r.Add(new Resistance() { Value = 456 });
            r.Add(new Resistance() { Value = 567 });
            r.Add(new Resistance() { Value = 678 });
            r.Add(new Resistance() { Value = 789 });
            r.CircuitType = CircuitType.Paralleling;

            var actual = System.Math.Round(r.EquivalentValue, 2);
            Assert.AreEqual(45.45, actual);
        }

    }
}
