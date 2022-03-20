using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST 
{
    public class VariableDeclarationNode : Node 
    {
        public string Name;
        public ExpressionNode Expression;

        public VariableDeclarationNode(string name, string type)
        {
            Name = name;
            Expression = new ExpressionNode()
            {
                ExpressionType = "Call",
                Call = new CallNode()
                {
                    CallerName = null,
                    CalleeNames = new List<string>() {type}
                }
            };
        }

        public VariableDeclarationNode(){}
        
        public void SetName(string name)
        {
            Name = name;
        }

        public void SetExpression(ExpressionNode expression){
            Expression = expression;
        }
    }
}
