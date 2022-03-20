using System.Linq;

namespace Compiler_Project.TokenAnalyzer.Readers
{
    internal class OperandReader : IReader
    {
        static readonly char[] Operands = { '.', ',', ':', '=', '[', ']', '(', ')' };

        static bool IsOperand(char c)
        {
            return Operands.Any(o => o == c);
        }

        public bool CanTransition(string source, ref int i) =>
            IsOperand(source[i]);

        public bool Read(string source, ref int i, ref string buffer)
        {
            var c = source[i];
            if (c == ':' && source[i + 1] == '=')
            {
                buffer = ":=";
                i += 2;
            }
            else if (IsOperand(c))
            {
                buffer += c;
                i++;
            }
            return false;
        }

        public string GetTokenType(string word) => "operand";
    }
}
