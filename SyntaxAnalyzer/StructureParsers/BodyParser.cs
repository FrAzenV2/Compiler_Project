using System;
using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class BodyParser 
    {
        public static List<Node> Parse(int tokenId)
        {
            var bodyNodes = new List<Node>();
            
            while (true)
            {
                if (!(Parser.NeededToken(tokenId, "type", "name", 0) ||
                Parser.NeededToken(tokenId, "value", "while", 0) ||
                Parser.NeededToken(tokenId, "value", "if", 0) ||
                Parser.NeededToken(tokenId, "value", "return", 0) ||
                Parser.NeededToken(tokenId, "value", "var", 0) ||
                Parser.NeededToken(tokenId, "value", "this", 0)))
                {
                    return bodyNodes;
                }
                
                var varNode = VariableDeclarationParser.Parse(tokenId);
                
                if (!varNode.HasParsingError())
                {
                    bodyNodes.Add(varNode);
                    tokenId = varNode.GetTokenId();
                    continue;
                }
                
                var assignmentNode = AssignmentParser.Parse(tokenId);
                
                if (!assignmentNode.HasParsingError())
                {
                    bodyNodes.Add(assignmentNode);
                    tokenId = assignmentNode.GetTokenId();
                    continue;
                }
                
                var loopNode = WhileLoopParser.Parse(tokenId);
                
                if (!loopNode.HasParsingError())
                {
                    bodyNodes.Add(loopNode);
                    tokenId = loopNode.GetTokenId();
                    continue;
                }
                
                var ifStatementNode = IfStatementParser.Parse(tokenId);
                
                if (!ifStatementNode.HasParsingError())
                {
                    bodyNodes.Add(ifStatementNode);
                    tokenId = ifStatementNode.GetTokenId();
                    continue;
                }
                
                var returnNode = ReturnStatementParser.Parse(tokenId);
                
                if (!returnNode.HasParsingError())
                {
                    bodyNodes.Add(returnNode);
                    tokenId = returnNode.GetTokenId();
                    continue;
                }
                
                var callNode = CallParser.Parse(tokenId);
                
                if (callNode.HasParsingError())
                {
                    var newNode = new Node();
                    newNode.SetParsingError(true);
                    newNode.SetErrLine(Math.Max(Math.Max(varNode.GetErrLine(), assignmentNode.GetErrLine()), 
                        Math.Max(loopNode.GetErrLine(), ifStatementNode.GetErrLine())));
                    
                    newNode.SetErrLine(Math.Max(Math.Max(returnNode.GetErrLine(), callNode.GetErrLine()), newNode.GetErrLine()));
                    
                    bodyNodes.Add(newNode);
                    return bodyNodes;
                }
                
                bodyNodes.Add(callNode);
                tokenId = callNode.GetTokenId();
                
                if (tokenId < Parser.TokensArray.Length) continue;
                
                var finalNode = new Node();
                finalNode.SetParsingError(true);
                finalNode.SetErrLine(Parser.TokensArray[^1].Line);
                return bodyNodes;
            }
        }
    }
}