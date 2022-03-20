using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST 
{
    public class CallNode : Node 
    {
        public string CallerName; //if exists
        public List<string> CalleeNames; //if exists
        public readonly List<List<ExpressionNode>> Arguments;

        public CallNode()
        {
            Arguments = new List<List<ExpressionNode>>();
            CalleeNames = new List<string>();
        }
        public void SetCallerName(string callerName)
        {
            CallerName = callerName;
        }
        public void AddCaller(string callerName, List<ExpressionNode> arguments)
        {
            CalleeNames.Add(callerName);
            Arguments.Add(arguments);
        }
    }
}
