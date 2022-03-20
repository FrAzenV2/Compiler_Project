namespace Compiler_Project.SyntaxAnalyzer {
    public class Node {
        
        private int _tokenId;
        private int _errorLine;
        private bool _parsingError;
        
        public Node(){
            _parsingError = false;
            _errorLine = -1;
        }
        public int GetTokenId()
        {
            return _tokenId;
        }

        public void SetTokenId(int num)
        {
            _tokenId = num;
        }

        public void SetErrLine(int line)
        {
            _errorLine = line;
        }

        public int GetErrLine()
        {
            return _errorLine;
        }

        public void SetParsingError(bool isError)
        {
            _parsingError = isError;
        }

        public bool HasParsingError() { return _parsingError; }

    }
}