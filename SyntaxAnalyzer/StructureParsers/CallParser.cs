using System;
using System.Collections.Generic;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project.SyntaxAnalyzer.StructureParsers 
{
    public static class CallParser 
    {
        public static CallNode Parse(int tokenNumber)
        {
            
            var node = new CallNode();
            
            if ((Parser.NeededToken(tokenNumber, "type", "name", 0) ||
                    Parser.NeededToken(tokenNumber, "value", "this", 0)) &&
                Parser.NeededToken(tokenNumber + 1, "value", ".", 0))
            {
                node.SetCallerName(Parser.TokensArray[tokenNumber].Value);
                tokenNumber += 2;
            }
            
            while (true)
            {
                var arguments = new List<ExpressionNode>();
                
                if (!Parser.NeededToken(tokenNumber, "type", "name", 0))
                {
                    node.SetParsingError(true);
                    node.SetErrLine(Parser.TokensArray[Math.Min(Parser.TokensArray.Length - 1, tokenNumber)].Line);
                    return node;
                }
                
                var currentCaller = Parser.TokensArray[tokenNumber].Value;
                tokenNumber++;
                
                if (Parser.NeededToken(tokenNumber, "value", "(", 0))
                {
                    
                    tokenNumber++;
                    
                    while(true)
                    {
                        
                        var expressionNode = ExpressionParser.Parse(tokenNumber);
                        
                        if (expressionNode.HasParsingError())
                        {
                            node.SetParsingError(true);
                            node.SetErrLine(expressionNode.GetErrLine());
                            return node;
                        }
                        
                        tokenNumber = expressionNode.GetTokenId();
                        arguments.Add(expressionNode);
                        
                        if (Parser.NeededToken(tokenNumber, "value", ",", 0))
                        {
                            tokenNumber++;
                            continue;
                        }
                        
                        if (Parser.NeededToken(tokenNumber, "value", ")", 0))
                        {
                            tokenNumber++;
                            break;
                        }
                        
                        node.SetParsingError(true);
                        node.SetErrLine(Parser.TokensArray[Math.Min(Parser.TokensArray.Length - 1, tokenNumber)].Line);
                        return node;
                    }
                    
                }
                
                node.AddCaller(currentCaller, arguments);
                
                if (Parser.NeededToken(tokenNumber, "value", ".", 0))
                {
                    tokenNumber++;
                    continue;
                }
                
                break;
            }
            
            node.SetTokenId(tokenNumber);
            return node;
        }
    }
}