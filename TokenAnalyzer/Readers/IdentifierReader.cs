using System.Collections.Generic;

namespace Compiler_Project.TokenAnalyzer.Readers
{
    internal class IdentifierReader : IReader
    {
        static readonly HashSet<string> Keywords = new HashSet<string>()
        {
            "class",
            "extends",
            "is",
            "end",
            "var",
            "method",
            "this",
            "while",
            "loop",
            "if",
            "else",
            "then",
            "return"
        };

        private static bool IsLatinLetter(char c) =>
            c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z';

        public bool CanTransition(string source, ref int i)
        {
            var c = source[i];
            return c == '_' || IsLatinLetter(c);
        }

        public bool Read(string source, ref int i, ref string buffer)
        {
            var c = source[i];
            if (c != '_' && !char.IsDigit(c) && !IsLatinLetter(c)) return false;
            buffer += c;
            i++;
            return true;
        }

        public string GetTokenType(string value) =>
            value == "true" || value == "false"
                ? "bool"
                : Keywords.Contains(value)
                    ? "keyword"
                    : "name";
    }
}
