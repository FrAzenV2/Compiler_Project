namespace Compiler_Project.TokenAnalyzer
{
    public class Token
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public Token(string value, string type, int line, int column)
        {
            Value = value;
            Type = type;
            Line = line;
            Column = column;
        }
    }
}
