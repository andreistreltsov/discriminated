using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }

    
}
