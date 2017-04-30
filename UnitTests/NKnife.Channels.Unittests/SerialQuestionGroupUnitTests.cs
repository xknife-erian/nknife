using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NKnife.Channels.Channels.Serials;
using NUnit.Framework;

namespace NKnife.Channels.Unittests
{
    [TestFixture]
    public class SerialQuestionGroupUnitTests
    {
        [Test(Description = "全部是需要循环的")]
        public void PeekOrDequeueTest1()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().Be(q1);
            group.Count.Should().Be(5);
        }

        [Test(Description = "全部是不需要循环的")]
        public void PeekOrDequeueTest2()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().BeNull();
            group.Count.Should().Be(0);
        }

        [Test(Description = "需要循环的和不需要循环的均存在1")]
        public void PeekOrDequeueTest3()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(3);
        }

        [Test(Description = "需要循环的和不需要循环的均存在2")]
        public void PeekOrDequeueTest4()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true,  new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true,  new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(2);
            group.CurrentIndex.Should().Be(0);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q4);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q4);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q4);
            group.Count.Should().Be(2);
        }

        [Test(Description = "需要循环的和不需要循环的均存在3")]
        public void PeekOrDequeueTest5()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.Count.Should().Be(3);
        }

        [Test(Description = "需要循环的和不需要循环的均存在4")]
        public void PeekOrDequeueTest6()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(2);
        }

        [Test(Description = "需要循环的和不需要循环的均存在5")]
        public void PeekOrDequeueTest7()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(2);
        }

        [Test(Description = "需要循环的和不需要循环的均存在6")]
        public void PeekOrDequeueTest8()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.Count.Should().Be(3);
        }

        [Test(Description = "需要循环的和不需要循环的均存在7")]
        public void PeekOrDequeueTest9()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q1);
            group.Count.Should().Be(1);
        }

        [Test(Description = "需要循环的和不需要循环的均存在8")]
        public void PeekOrDequeueTest10()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q2);
            group.Count.Should().Be(1);
        }

        [Test(Description = "需要循环的和不需要循环的均存在9")]
        public void PeekOrDequeueTest11()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, false, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, false, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(1);
        }

        [Test(Description = "需要循环的和不需要循环的均存在10")]
        public void PeekOrDequeueTest12()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, false, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(4);
        }

        [Test(Description = "需要循环的和不需要循环的均存在11")]
        public void PeekOrDequeueTest13()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, false, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, true, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q5);
            group.Count.Should().Be(4);
        }

        [Test(Description = "需要循环的和不需要循环的均存在12")]
        public void PeekOrDequeueTest14()
        {
            var group = new SerialQuestionGroup();

            var q1 = new SerialQuestion(null, null, null, true, new byte[] { 0x01 });
            var q2 = new SerialQuestion(null, null, null, true, new byte[] { 0x02 });
            var q3 = new SerialQuestion(null, null, null, true, new byte[] { 0x03 });
            var q4 = new SerialQuestion(null, null, null, true, new byte[] { 0x04 });
            var q5 = new SerialQuestion(null, null, null, false, new byte[] { 0x05 });

            group.Add(q1, q2, q3, q4, q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q5);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);

            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.PeekOrDequeue().Should().Be(q1);
            group.PeekOrDequeue().Should().Be(q2);
            group.PeekOrDequeue().Should().Be(q3);
            group.PeekOrDequeue().Should().Be(q4);
            group.Count.Should().Be(4);
        }

    }
}
