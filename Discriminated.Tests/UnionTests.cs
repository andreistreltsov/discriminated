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
                var u = new Union<int, string, byte, decimal, bool, char, long>(1);

                int? val = null;
                Action<int> handler = x => { val = x; };

                u.Match(handler, x => { }, x => { }, x => { }, x => { }, x => { }, x => { });

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(1), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase2()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>("one");

                string val = null;
                Action<string> handler = x => { val = x; };

                u.Match(x => { }, handler, x => { }, x => { }, x => { }, x => { }, x => { });

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo("one"), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase3()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>((byte)2);

                byte? val = null;
                Action<byte> handler = x => { val = x; };

                u.Match(x => { }, x => { }, handler, x => { }, x => { }, x => { }, x => { });

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo((byte)2), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase4()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(1.1m);

                decimal? val = null;
                Action<decimal> handler = x => { val = x; };

                u.Match(x => { }, x => { }, x => { }, handler, x => { }, x => { }, x => { });

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo(1.1m), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase5()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(true);

                bool? val = null;
                Action<bool> handler = x => { val = x; };

                u.Match(x => { }, x => { }, x => { }, x => { }, handler, x => { }, x => { });

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo(true), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase6()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>('a');

                char? val = null;
                Action<char> handler = x => { val = x; };

                u.Match(x => { }, x => { }, x => { }, x => { }, x => { }, handler, x => { });

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo('a'), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase7()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(3L);

                long? val = null;
                Action<long> handler = x => { val = x; };

                u.Match(x => { }, x => { }, x => { }, x => { }, x => { }, x => { }, handler);

                Assert.That(val, Is.Not.Null, "The handler was not called.");
                Assert.That(val, Is.EqualTo(3L), "The handler got called with an invalid value.");
            }
        }

        public class MatchFunction
        {
            [Test]
            public void HandlesCase1()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(1);

                int? val = null;

                val = u.Match<int?>(x => x, x => null, x => null, x => null, x => null, x => null, x => null);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(1), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase2()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>("one");

                string val = null;

                val = u.Match<string>(x => null, x => x, x => null, x => null, x => null, x => null, x => null);

                Assert.True(val != null, "The handler was not called.");
                Assert.That(val, Is.EqualTo("one"), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase3()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>((byte)2);

                byte? val = null;

                val = u.Match<byte?>(x => null, x => null, x => x, x => null, x => null, x => null, x => null);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo((byte)2), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase4()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(1.1M);

                decimal? val = null;

                val = u.Match<decimal?>(x => null, x => null, x => null, x => x, x => null, x => null, x => null);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(1.1M), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase5()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(true);

                bool? val = null;

                val = u.Match<bool?>(x => null, x => null, x => null, x => null, x => x, x => null, x => null);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(true), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase6()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>('a');

                char? val = null;

                val = u.Match<char?>(x => null, x => null, x => null, x => null, x => null, x => x, x => null);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo('a'), "The handler got called with an invalid value.");
            }

            [Test]
            public void HandlesCase7()
            {
                var u = new Union<int, string, byte, decimal, bool, char, long>(3L);

                long? val = null;

                val = u.Match<long?>(x => null, x => null, x => null, x => null, x => null, x => null, x => x);

                Assert.True(val.HasValue, "The handler was not called.");
                Assert.That(val.Value, Is.EqualTo(3L), "The handler got called with an invalid value.");
            }
        }

        public class Equality
        {
            [Test]
            public void TwoInstancesEqualIfUnderlyingValuesEqual()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);
                var u2 = new Union<int, string, byte, decimal, bool, char, long>(1);

                Assert.That(u1 == u2, Is.True, "Instances with the same value are not equal.");
            }

            [Test]
            public void TwoInstancesNotEqualIfUnderlyingTypesDifferent()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);
                var u2 = new Union<int, string, byte, decimal, bool, char, long>((byte)1);

                Assert.That(u1 == u2, Is.False, "Instances with different type cases are equal.");
            }

            [Test]
            public void InstanceNotEqualToNull()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);

                Assert.That(u1 != null, Is.True, "Instance should not be equal to null.");
            }
        }

        public class GetHashCodeTests
        {
            [Test]
            public void EqualInstancesHaveEqualHashCode()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);
                var u2 = new Union<int, string, byte, decimal, bool, char, long>(1);

                Assert.That(u1.GetHashCode(), Is.EqualTo(u2.GetHashCode()), "Equal instances did not have the same hash code.");
            }

            [Test]
            public void NonEqualInstancesHaveDifferentHashCodes()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);
                var u2 = new Union<int, string, byte, decimal, bool, char, long>("one");

                Assert.That(u1.GetHashCode(), Is.Not.EqualTo(u2.GetHashCode()), "Non-equal instances had the same hash code.");
            }

            [Test]
            public void DifferentCasesHaveDifferentHashCodes()
            {
                var u1 = new Union<int, string, byte, decimal, bool, char, long>(1);
                var u2 = new Union<int, string, byte, decimal, bool, char, long>((byte)1);

                Assert.That(u1.GetHashCode(), Is.Not.EqualTo(u2.GetHashCode()), "Non-equal instances had the same hash code.");
            }
        }
    }
}

  