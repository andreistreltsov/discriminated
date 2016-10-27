using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discriminated
{
    public class Option<TValue> : Union<TValue, None>
    {
        private Option(TValue val) : base(val) { }

        private Option() : base(Discriminated.None.Instance) { }

        public static Option<TValue> Some(TValue val)
        {
            return new Discriminated.Option<TValue>(val);
        }

        public static Option<TValue> None { get { return new Option<TValue>(); } }
    }
}
