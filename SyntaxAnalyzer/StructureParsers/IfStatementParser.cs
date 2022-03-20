using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class IfStatementParser 
    {
        public static IfStatementNode Parse(int tokenId)
        {
            var node = new IfStatementNode();
        
            if (!Parser.NeededToken(tokenId, "value", "if", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            tokenId++;
            var exprNode = ExpressionParser.Parse(tokenId);
            
            if (exprNode.HasParsingError())
            {
                node.SetParsingError(true);
                node.SetErrLine(exprNode.GetErrLine());
                return node;
            }
            
            node.SetExpression(exprNode);
            tokenId = exprNode.GetTokenId();
            
            if (!Parser.NeededToken(tokenId, "value", "then", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            tokenId++;
            var bodyNodes = BodyParser.Parse(tokenId);
            
            if (bodyNodes.Count == 0)
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            var lastNode = bodyNodes[^1];
            
            if (lastNode.HasParsingError())
            {
                node.SetParsingError(true);
                node.SetErrLine(lastNode.GetErrLine());
                return node;
            }
            
            foreach (var bodyNode in bodyNodes)
            {
                node.AddIfBodyNode(bodyNode);
            }
            
            tokenId = lastNode.GetTokenId();
            
            if (Parser.NeededToken(tokenId, "value", "else", 0))
            {
                tokenId++;
                bodyNodes = BodyParser.Parse(tokenId);
                if (bodyNodes.Count == 0)
                {
                    node.SetParsingError(true);
                    node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                    return node;
                }
                
                lastNode = bodyNodes[^1];
                
                if (lastNode.HasParsingError())
                {
                    node.SetParsingError(true);
                    node.SetErrLine(lastNode.GetErrLine());
                    return node;
                }
                
                foreach (var t in bodyNodes)
                {
                    node.AddElseBodyNode(t);
                }
                
                tokenId = lastNode.GetTokenId();
                
            }
            
            if (!Parser.NeededToken(tokenId, "value", "end", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            tokenId++;
            node.SetTokenId(tokenId);
            return node;
        }
    }
}