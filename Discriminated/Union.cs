// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

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

            return Case(this).Equals(Case(other));
        }

        private static object Case(Union<T1,T2> u)
        {
            switch (u.tag)
            {
                case 1: return u.case1;
                case 2: return u.case2;
                default: throw new InvalidOperationException();
            }
        }

        public override int GetHashCode()
        {
            return 17 * this.tag + Case(this).GetHashCode();
        }
    }
}

