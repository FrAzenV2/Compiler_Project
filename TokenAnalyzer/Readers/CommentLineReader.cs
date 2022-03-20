namespace Compiler_Project.TokenAnalyzer.Readers
{
    internal class CommentLineReader : IReader
    {
        public bool CanTransition(string source, ref int i)
        {
            if (source[i] != '/' || source[i + 1] != '/') return false;
            i += 2;
            return true;
        }

        public bool Read(string source, ref int i, ref string token) =>
            source[i++] != '\n';
    }
}
