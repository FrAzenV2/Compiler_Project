using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class ExpressionParser 
    {
        public static ExpressionNode Parse(int tokenId)
        {
            
            var node = new ExpressionNode();
            
            if (Parser.NeededToken(tokenId, "type", "int", 0))
            {
                node.ExpressionType = "literal";
                node.IntegerLiteral = Parser.TokensArray[tokenId].Value;
                tokenId++;
                node.SetTokenId(tokenId);
                return node;
            }
            
            if (Parser.NeededToken(tokenId, "type", "bool", 0))
            {
                node.ExpressionType = "literal";
                node.BooleanLiteral = Parser.TokensArray[tokenId].Value;
                tokenId++;
                node.SetTokenId(tokenId);
                return node;
            }
            
            if (Parser.NeededToken(tokenId, "type", "float", 0))
            {
                node.ExpressionType = "literal";
                node.FloatLiteral = Parser.TokensArray[tokenId].Value;
                tokenId++;
                node.SetTokenId(tokenId);
                return node;
            }
            
            var callNode = CallParser.Parse(tokenId);
            
            if (callNode.HasParsingError()){
                node.SetParsingError(true);
                node.SetErrLine(callNode.GetErrLine());
                return node;
            }
            
            node.ExpressionType = "call";
            
            node.SetTokenId(callNode.GetTokenId());
            node.SetCall(callNode);
            return node;
        }
    }
}