using System;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class AssignmentParser 
    {
        public static AssignmentNode Parse(int tokenNumber)
        {
            
            var node = new AssignmentNode();
            
            if (!(Parser.NeededToken(tokenNumber, "type", "name", 0) &&
                Parser.NeededToken(tokenNumber + 1, "value", ":=", 0)))
            {
                node.SetParsingError(true);
                node.SetErrLine(Parser.TokensArray[Math.Min(tokenNumber, Parser.TokensArray.Length - 1)].Line);
                return node;
            }
            
            node.SetName(Parser.TokensArray[tokenNumber].Value);
            tokenNumber += 2;
            
            var exprNode = ExpressionParser.Parse(tokenNumber);
            
            if (exprNode.HasParsingError())
            {
                node.SetParsingError(true);
                node.SetErrLine(exprNode.GetErrLine());
                return node;
            }
            
            node.SetExpression(exprNode);
            node.SetTokenId(exprNode.GetTokenId());
            return node;
        }
    }
}