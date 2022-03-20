using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class ConstructorDeclarationParser 
    {
        public static ConstructorDeclarationNode Parse(int tokenId)
        {
            
            var node = new ConstructorDeclarationNode();
            
            if (!Parser.NeededToken(tokenId, "value", "this", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            tokenId++;
            
            if (Parser.NeededToken(tokenId, "value", "(", 0))
            {
                tokenId++;
                
                while (true)
                {
                    
                    if (!(Parser.NeededToken(tokenId, "type", "name", 0) && 
                    Parser.NeededToken(tokenId + 1, "value", ":", 0) && 
                    Parser.NeededToken(tokenId + 2, "type", "name", 0)))
                    {
                        node.SetParsingError(true);
                        node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                        return node;
                    }
                    node.AddParameter(Parser.TokensArray[tokenId].Value, Parser.TokensArray[tokenId + 2].Value);
                    
                    tokenId += 3;
                    
                    if (Parser.NeededToken(tokenId, "value", ",", 0)) 
                    {
                        tokenId++;
                        continue;
                    }
                    
                    if (Parser.NeededToken(tokenId, "value", ")", 0))
                    {
                        tokenId++;
                        break;
                    }
                    
                    node.SetParsingError(true);
                    node.SetErrLine(Parser.TokensArray[Math.Min(Parser.TokensArray.Length - 1, tokenId)].Line);
                    return node;
                }
            }
            
            if (!Parser.NeededToken(tokenId, "value", "is", 0))
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
            
            foreach (var t in bodyNodes)
            {
                node.AddBodyNode(t);
            }
            
            tokenId = lastNode.GetTokenId();
            
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