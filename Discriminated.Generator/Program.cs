// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Discriminated.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var numOfImplementedCases = NumOfImplementedCases(ReadSource());
            for (int currMaxCase = numOfImplementedCases-1; currMaxCase >= 2; currMaxCase--)
            {
                WriteGeneratedClassToFile(
                    DropExtraCasesFromSource(currMaxCase, numOfImplementedCases, ReadSource()), 
                    string.Format("Union{0}.generated.cs", currMaxCase));
            }
        }

        static int NumOfImplementedCases(string source)
        {
            return Regex.Matches(source, "T\\d+")
                .Cast<Match>().ToList()
                .Select(x => x.Value.TrimStart('T'))
                .Select(x => int.Parse(x))
                .Max();
        }

        static string ReadSource()
        {
            return File.ReadAllText("Union.cs");
        }

        static void WriteGeneratedClassToFile(string content, string filename)
        {
            File.WriteAllText(filename, content);
        }

        static string DropExtraCasesFromSource(int maxCaseToLeave, int maxCaseToRemove, string source)
        {
            string linesToDropComma = string.Format("T{0}", maxCaseToLeave);

            var linesToRemove = Enumerable
                .Range(maxCaseToLeave + 1, maxCaseToRemove - maxCaseToLeave)
                .Select(x => new[] { string.Format("T{0}", x), string.Format("case{0}", x) })
                .SelectMany(x => x).ToList();

            Console.WriteLine(linesToRemove);

            return string.Join(Environment.NewLine,
                source.Split(new[] { '\r', '\n' })
                .ToList()
                .Select(sourceLine => {
                    if (linesToRemove.Any(p => Regex.IsMatch(sourceLine, p))) return string.Empty;
                    if (Regex.IsMatch(sourceLine, linesToDropComma)) return sourceLine.TrimEnd(',');
                    return sourceLine;
                }));
        }
    }
}
