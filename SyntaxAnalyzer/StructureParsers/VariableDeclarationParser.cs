using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{  
    public static class VariableDeclarationParser 
    {
        public static VariableDeclarationNode Parse(int tokenId)
        {
            var variableDeclarationNode = new VariableDeclarationNode();
            
            if (!(Parser.NeededToken(tokenId, "value", "var", 0) && 
            Parser.NeededToken(tokenId + 1, "type", "name", 0) &&
            Parser.NeededToken(tokenId + 2, "value", ":", 0)))
            {
                variableDeclarationNode.SetParsingError(true);
                variableDeclarationNode.SetErrLine(Parser.TokensArray[Math.Min(tokenId, Parser.TokensArray.Length - 1)].Line);
                return variableDeclarationNode;
            }
            
            //var a :(=?) TYPE(value)
            // 0  1  2     3 per line
            
            variableDeclarationNode.SetName(Parser.TokensArray[tokenId + 1].Value);
            tokenId += 3; 
            var exprNode = ExpressionParser.Parse(tokenId);
            
            if (exprNode.HasParsingError())
            {
                variableDeclarationNode.SetParsingError(true);
                variableDeclarationNode.SetErrLine(exprNode.GetErrLine());
                return variableDeclarationNode;
            }
            
            variableDeclarationNode.SetExpression(exprNode);
            variableDeclarationNode.SetTokenId(exprNode.GetTokenId());
            
            return variableDeclarationNode;
        }
    }
}