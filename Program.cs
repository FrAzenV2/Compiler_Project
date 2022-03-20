using System;
using System.IO;
using Compiler_Project.SemanticsAnalyzer;
using Compiler_Project.SyntaxAnalyzer;
using Compiler_Project.TokenAnalyzer;

namespace Compiler_Project
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var source = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"Tests/2"));
            var tokens = Manager.Analyze(source);

            Console.WriteLine("Tokens Done.");
            
            var ast = Parser.ParseProgram(tokens);
            
            Console.WriteLine("Syntax Analyzer Done. Ast tree built.");
            
            SemanticAnalyzer.SemanticAnalysis(ref ast, ref tokens);
            
            Console.WriteLine("Semantics Analysis complete.");
            
            Console.WriteLine("Job done.");
            Console.Read();

        }
    }
}
