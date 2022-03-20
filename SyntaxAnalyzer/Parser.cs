using System;
using System.Collections.Generic;
using Compiler_Project.SyntaxAnalyzer.AST;
using Compiler_Project.SyntaxAnalyzer.StructureParsers;
using Compiler_Project.TokenAnalyzer;

namespace Compiler_Project.SyntaxAnalyzer 
{
    public static class Parser 
    {
        
        public static Token[] TokensArray;
        private static void ExitWithError(int line)
        {
            Console.WriteLine($"Syntax Error at line {line}.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        
        public static bool NeededToken(int tokenNumber, string attributeType, string valueOrType, int lineOrColumn)
        {
            if (tokenNumber >= TokensArray.Length)
            {
                return false;
            }
            return attributeType switch
            {
                "value" => TokensArray[tokenNumber].Value.Equals(valueOrType),
                "type" => TokensArray[tokenNumber].Type.Equals(valueOrType),
                "line" => (TokensArray[tokenNumber].Line == lineOrColumn),
                "column" => (TokensArray[tokenNumber].Column == lineOrColumn),
                _ => false
            };
        }
        public static RootNode ParseProgram(List<Token> tokensList)
        {
            TokensArray = tokensList.ToArray();
            var tokenId = 0;
            var node = new RootNode();

            while(true)
            {
                var classDeclNode = ClassDeclarationParser.Parse(tokenId);
             
                if (classDeclNode.HasParsingError())
                {
                    ExitWithError(classDeclNode.GetErrLine());
                }
                else 
                {
                    node.AddClassDeclaration(classDeclNode);
                    tokenId = classDeclNode.GetTokenId();
                }
                
                if (tokenId >= TokensArray.Length)
                {
                    break;
                }
            }
            return node;
        }
    }
}