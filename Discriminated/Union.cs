using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discriminated
{
    public class Union<T1, T2>
    {
        int tag;
        T1 case1;
        T2 case2;

        public Union(T1 value) { this.case1 = value; tag = 1; }
        public Union(T2 value) { this.case2 = value; tag = 2; }

        public virtual TResult Match<TResult>(Func<T1, TResult> case1Handler, Func<T2, TResult> case2Handler)
        {
            switch (tag)
            {
                case 1: return case1Handler(case1);
                case 2: return case2Handler(case2);
                default: throw new InvalidOperationException();
            }
        }

        public virtual void Match(Action<T1> case1Handler, Action<T2> case2Handler)
        {
            switch (tag)
            {
                case 1: case1Handler(case1); break;
                case 2: case2Handler(case2); break;
                default: throw new InvalidOperationException();
            }
        }

        public static bool operator ==(Union<T1,T2> a, Union<T1,T2> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Union<T1,T2> a, Union<T1,T2> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as Union<T1, T2>;

            if (other == null) return false;

            if (this.tag != other.tag) return false;

            switch (this.tag)
            {
                case 1: return this.case1.Equals(other.case1);
                case 2: return this.case2.Equals(other.case2);
                default: throw new InvalidOperationException();
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
