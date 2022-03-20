using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Compiler_Project.TokenAnalyzer.Readers;

namespace Compiler_Project.TokenAnalyzer
{
    public static class Manager
    {
        private static readonly IReader[] Readers =
        {
            new IdentifierReader(),
            new NumericReader(),
            new OperandReader(),
            new CommentBlockReader(),
            new CommentLineReader()
        };

        private static List<Token> GetTokens(string source)
        {
            var tokens = new List<Token>();
            var reader = Readers[0];
            var buffer = "";
            var line = 1;
            var lineI = 0;

            if (source[^1] != '\n')
                source += '\n';

            for (var i = 0; i < source.Length - 1;)
            {
                if (source[i] == '\n')
                {
                    line++;
                    lineI = i;
                }

                if (reader != null && reader.Read(source, ref i, ref buffer)) continue;
                if (buffer != string.Empty)
                {
                    if (reader?.GetTokenType(buffer) is { } type)
                        tokens.Add(new Token(buffer, type, line, i - lineI));
                    buffer = string.Empty;
                }
                    
                if (reader != null)
                {
                    if (reader.HasError())
                        Error(line, i - lineI);
                    reader.Reset();
                    reader = null;
                }

                if (source[i] == ' ' || char.IsControl(source[i]))
                    i++;
                else
                {
                    foreach (var r in Readers)
                        if (r.CanTransition(source, ref i))
                        {
                            reader = r;
                            break;
                        }
                    if (reader == null)
                        Error(line, i - lineI);
                }
            }
            return tokens;
        }

        public static List<Token> Analyze(string source, bool serialize = true)
        {
            var tokens = GetTokens(source);
            if (!serialize) return tokens;
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var result = JsonSerializer.Serialize(tokens, serializeOptions);
            File.WriteAllText("tokens.json", result);
            return tokens;
        }

        static void Error(int line, int column)
        {
            Console.WriteLine($"Invalid token at {line}:{column}.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
