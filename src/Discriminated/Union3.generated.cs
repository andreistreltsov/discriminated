// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.



using System;



namespace Discriminated

{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Multiple type parameters are necessary.")]

    public class Union<T1, 

        T2,

        T3









        >

    {

        int tag;

        T1 case1;

        T2 case2;

        T3 case3;











        public Union(T1 value) { this.case1 = value; tag = 1; }

        public Union(T2 value) { this.case2 = value; tag = 2; }

        public Union(T3 value) { this.case3 = value; tag = 3; }











        public virtual TResult Match<TResult>(

            Func<T1, TResult> case1Handler,

            Func<T2, TResult> case2Handler,

            Func<T3, TResult> case3Handler









            )

        {

            if (case1Handler == null) throw new ArgumentNullException("case1Handler");

            if (case2Handler == null) throw new ArgumentNullException("case2Handler");

            if (case3Handler == null) throw new ArgumentNullException("case3Handler");











            switch (tag)

            {

                case 1: return case1Handler(case1);

                case 2: return case2Handler(case2);

                case 3: return case3Handler(case3);









                default: throw new InvalidOperationException();

            }

        }



        public virtual void Match(

            Action<T1> case1Handler,

            Action<T2> case2Handler,

            Action<T3> case3Handler









            )

        {

            if (case1Handler == null) throw new ArgumentNullException("case1Handler");

            if (case2Handler == null) throw new ArgumentNullException("case2Handler");

            if (case3Handler == null) throw new ArgumentNullException("case3Handler");











            switch (tag)

            {

                case 1: case1Handler(case1); break;

                case 2: case2Handler(case2); break;

                case 3: case3Handler(case3); break;









                default: throw new InvalidOperationException();

            }

        }



        public static bool operator ==(

            Union<T1,

                T2,

                T3









                > instanceA, 

            Union<T1,

                T2,

                T3









                > instanceB

            )

        {

            if (ReferenceEquals(instanceA, null)) return ReferenceEquals(instanceB, null);



            return instanceA.Equals(instanceB);

        }



        public static bool operator !=(

            Union<T1,

            T2,

            T3









            > instanceA, 

            Union<T1,

            T2,

            T3









            > instanceB)

        {

            return !(instanceA == instanceB);

        }



        public override bool Equals(object obj)

        {

            if (obj == null) return false;



            var other = obj as Union<T1,

                T2,

                T3









                >;



            if (other == null) return false;



            if (this.tag != other.tag) return false;



            return Case(this).Equals(Case(other));

        }



        private static object Case(Union<T1,

            T2,

            T3









            > u)

        {

            switch (u.tag)

            {

                case 1: return u.case1;

                case 2: return u.case2;

                case 3: return u.case3;









                default: throw new InvalidOperationException();

            }

        }



        public override int GetHashCode()

        {

            return 17 * this.tag + Case(this).GetHashCode();

        }



        public string CaseToString()

        {

            switch (tag)

            {

                case 1: return case1.ToString();

                case 2: return case2.ToString();

                case 3: return case3.ToString();









                default: throw new InvalidOperationException();

            }

        }



        public Type CaseType()

        {

            switch (tag)

            {

                case 1: return case1.GetType();

                case 2: return case2.GetType();

                case 3: return case3.GetType();









                default: throw new InvalidOperationException();

            }

        }

    }

}



