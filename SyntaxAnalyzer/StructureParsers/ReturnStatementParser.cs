using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class ReturnStatementParser 
    {
        public static ReturnStatementNode Parse(int tokenNumber)
        {
            var node = new ReturnStatementNode();
            
            if (!Parser.NeededToken(tokenNumber, "value", "return", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenNumber, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            tokenNumber++;
            var exprNode = ExpressionParser.Parse(tokenNumber);
            
            if (!exprNode.HasParsingError())
            {
                node.SetExpression(exprNode);
                tokenNumber = exprNode.GetTokenId();
            }
            
            node.SetTokenId(tokenNumber);
            return node;
        }
    }
}