using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class ClassDeclarationParser 
    {
        public static ClassDeclarationNode Parse(int tokenId)
        {
            
            var node = new ClassDeclarationNode();
            
            if (!(Parser.NeededToken(tokenId, "value", "class", 0) &&
                Parser.NeededToken(tokenId + 1, "type", "name", 0)))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            node.SetName(Parser.TokensArray[tokenId + 1].Value);
            tokenId += 2;
            
            if (Parser.NeededToken(tokenId, "value", "extends", 0) &&
                Parser.NeededToken(tokenId + 1, "type", "name", 0))
            {
                
                node.SetSuperClass(Parser.TokensArray[tokenId + 1].Value);
                tokenId += 2;
            }
            
            if (!Parser.NeededToken(tokenId, "value", "is", 0))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            tokenId++;
            
            while (true)
            {
                if (Parser.NeededToken(tokenId, "value", "end", 0))
                {
                    tokenId++;
                    node.SetTokenId(tokenId);
                    return node;
                }
                
                var varNode = VariableDeclarationParser.Parse(tokenId);
                
                if (!varNode.HasParsingError())
                {
                    tokenId = varNode.GetTokenId();
                    node.AddVariableDeclaration(varNode);
                    continue;
                }
                
                var methodNode = MethodDeclarationParser.Parse(tokenId);
                
                if (!methodNode.HasParsingError())
                {
                    tokenId = methodNode.GetTokenId();
                    node.AddMethodDeclaration(methodNode);
                    continue;
                }
                
                var constructorNode = ConstructorDeclarationParser.Parse(tokenId);
                
                if (constructorNode.HasParsingError())
                {
                    node.SetParsingError(true);
                    node.SetErrLine(Math.Max(varNode.GetErrLine(), Math.Max(methodNode.GetErrLine(), constructorNode.GetErrLine())));
                    return node;
                }
                
                tokenId = constructorNode.GetTokenId();
                node.AddConstructorDeclaration(constructorNode);

                if (tokenId < Parser.TokensArray.Length) continue;

                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[^1].Line);
                
                return node;
            }
        }
    }
}