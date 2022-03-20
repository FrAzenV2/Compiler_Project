using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class WhileLoopParser 
    {
        public static WhileLoopNode Parse(int tokenId)
        {
            
            var whileLoopNode = new WhileLoopNode();
            
            if (!Parser.NeededToken(tokenId, "value", "while", 0))
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return whileLoopNode;
            }
            
            tokenId++;
            var exprNode = ExpressionParser.Parse(tokenId);
            
            if (exprNode.HasParsingError())
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(exprNode.GetErrLine());
                return whileLoopNode;
            }
            
            whileLoopNode.SetExpression(exprNode);
            tokenId = exprNode.GetTokenId();
            
            if (!Parser.NeededToken(tokenId, "value", "loop", 0))
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return whileLoopNode;
            }
            
            tokenId++;
            var bodyNodes = BodyParser.Parse(tokenId);
            
            if (bodyNodes.Count == 0)
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return whileLoopNode;
            }
            
            var lastNode = bodyNodes[^1];
            
            if (lastNode.HasParsingError())
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(lastNode.GetErrLine());
                return whileLoopNode;
            }
            
            foreach (var bodyNode in bodyNodes)
            {
                whileLoopNode.AddBodyNode(bodyNode);
            }
            
            tokenId = lastNode.GetTokenId();
            
            if (!Parser.NeededToken(tokenId, "value", "end", 0))
            {
                whileLoopNode.SetParsingError(true);
                whileLoopNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return whileLoopNode;
            }
            
            tokenId++;
            whileLoopNode.SetTokenId(tokenId);
            return whileLoopNode;
        }
    }
}
