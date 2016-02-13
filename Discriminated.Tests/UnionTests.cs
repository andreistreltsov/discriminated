// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;

namespace Discriminated.Tests
{
    [TestFixture]
    public class UnionTests
    {
        public class MatchAction
        {
            [Test]
            public void HandlesCase1()
            {
                Union<int, string> u = new Union<int, string>(1);

                int? val = null;
                Action<int> handler = x => { val = x; };

                u.Match(handler, x => { });

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(1), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase2()
            {
                Union<int, string> u = new Union<int, string>("one");

                string val = null;
                Action<string> handler = x => { val = x; };

                u.Match(x => { }, handler);

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo("one"), "The handler got called with an invalid value.");
            }
        }

        public class MatchFunction
        {
            [Test]
            public void HandlesCase1()
            {
                Union<int, string> u = new Union<int, string>(1);

                int? val = null;

                val = u.Match(x => { return (int?)x; }, x => { return null; });

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(1), "The handler got called with an invalid value.");
            }
        }

        public class Equality
        {
            [Test]
            public void TwoInstancesEqualIfUnderlyingValuesEqual()
            {
                var u1 = new Union<int, string>(1);
                var u2 = new Union<int, string>(1);

                Assert.That(u1 == u2, Is.True, "Instances with the same value are not equal.");
            }

            [Test]
            public void TwoInstancesNotEqualIfUnderlyingTypesDifferent()
            {
                var u1 = new Union<int, object>(1);
                var u2 = new Union<int, object>((object)1);

                Assert.That(u1 == u2, Is.False, "Instances with different type cases are equal.");
            }

            [Test]
            public void InstanceNotEqualToNull()
            {
                var u1 = new Union<int, string>(1);

                Assert.That(u1 != null, Is.True, "Instance should not be equal to null.");
            }
        }

        public class GetHashCodeTests
        {
            [Test]
            public void EqualInstancesHaveEqualHashCode()
            {
                var u1 = new Union<int, string>(1);
                var u2 = new Union<int, string>(1);

                Assert.That(u1.GetHashCode(), Is.EqualTo(u2.GetHashCode()), "Equal instances did not have the same hash code.");
            }

            [Test]
            public void NonEqualInstancesHaveDifferentHashCodes()
            {
                var u1 = new Union<int, string>(1);
                var u2 = new Union<int, string>("one");

                Assert.That(u1.GetHashCode(), Is.Not.EqualTo(u2.GetHashCode()), "Non-equal instances had the same hash code.");
            }

            [Test]
            public void DifferentCasesHaveDifferentHashCodes()
            {
                var u1 = new Union<int, object>(1);
                var u2 = new Union<int, object>((object)1);

                Assert.That(u1.GetHashCode(), Is.Not.EqualTo(u2.GetHashCode()), "Non-equal instances had the same hash code.");
            }
        }
    }

    
}
