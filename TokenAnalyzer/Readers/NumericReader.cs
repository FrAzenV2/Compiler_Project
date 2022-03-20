namespace Compiler_Project.TokenAnalyzer.Readers
{
    internal class NumericReader : IReader
    {
        private int _points;
        public bool HasError() => _points > 1;
        public void Reset() => _points = 0;

        public bool CanTransition(string source, ref int i) =>
            char.IsDigit(source[i]);

        public bool Read(string source, ref int i, ref string buffer)
        {
            var c = source[i];
            if (char.IsDigit(c))
            {
                buffer += c;
                i++;
                return true;
            }
            else if (c == '.')
            {
                buffer += c;
                _points++;

                if (HasError())
                    return false;

                i++;
                return true;
            }
            return false;
        }

        public string GetTokenType(string word) =>
            _points == 0 ? "int" : "float";
    }
}
