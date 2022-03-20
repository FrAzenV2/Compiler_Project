using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class MethodDeclarationParser 
    {
        public static MethodDeclarationNode Parse(int tokenId)
        {
            
            var methodDeclarationNode = new MethodDeclarationNode();
            
            if (!(Parser.NeededToken(tokenId, "value", "method", 0) &&
                Parser.NeededToken(tokenId + 1, "type", "name", 0)))
            {
                methodDeclarationNode.SetParsingError(true);
                methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return methodDeclarationNode;
            }
            
            methodDeclarationNode.SetName(Parser.TokensArray[tokenId + 1].Value);
            tokenId += 2;
            
            if (Parser.NeededToken(tokenId, "value", "(", 0))
            {
                tokenId++;
                
                while (true)
                {
                    if (!(Parser.NeededToken(tokenId, "type", "name", 0) && 
                    Parser.NeededToken(tokenId + 1, "value", ":", 0) && 
                    Parser.NeededToken(tokenId + 2, "type", "name", 0)))
                    {
                        methodDeclarationNode.SetParsingError(true);
                        methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                        return methodDeclarationNode;
                    }
                    
                    methodDeclarationNode.AddParameter(Parser.TokensArray[tokenId].Value, Parser.TokensArray[tokenId + 2].Value);
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
                    
                    methodDeclarationNode.SetParsingError(true);
                    methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(Parser.TokensArray.Length - 1, tokenId)].Line);
                    return methodDeclarationNode;
                }
            }
            
            if (Parser.NeededToken(tokenId, "value", ":", 0) && 
                Parser.NeededToken(tokenId + 1, "type", "name", 0))
            {
                methodDeclarationNode.SetReturnType(Parser.TokensArray[tokenId + 1].Value);
                tokenId += 2;
            }
            
            if (!Parser.NeededToken(tokenId, "value", "is", 0))
            {
                methodDeclarationNode.SetParsingError(true);
                methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return methodDeclarationNode;
            }
            
            tokenId++;
            var bodyNodes = BodyParser.Parse(tokenId);
            
            if (bodyNodes.Count == 0)
            {
                methodDeclarationNode.SetParsingError(true);
                methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return methodDeclarationNode;
            }
            
            var lastNode = bodyNodes[^1];
            
            if (lastNode.HasParsingError())
            {
                methodDeclarationNode.SetParsingError(true);
                methodDeclarationNode.SetErrLine(lastNode.GetErrLine());
                return methodDeclarationNode;
            }
            
            foreach (var bodyNode in bodyNodes)
            {
                methodDeclarationNode.AddBodyNode(bodyNode);
            }
            
            tokenId = lastNode.GetTokenId();
            
            if (!Parser.NeededToken(tokenId, "value", "end", 0))
            {
                methodDeclarationNode.SetParsingError(true);
                methodDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return methodDeclarationNode;
            }
            
            tokenId++;
            
            methodDeclarationNode.SetTokenId(tokenId);
            return methodDeclarationNode;
        }
    }
}