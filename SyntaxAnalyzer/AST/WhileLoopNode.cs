using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST 
{
    public class WhileLoopNode : Node 
    {
        public ExpressionNode Expression;
        public readonly List<Node> BodyNodes;
        public WhileLoopNode()
        {
            BodyNodes = new List<Node>();
        }
        
        public void SetExpression(ExpressionNode expression)
        {
            Expression = expression;
        }
        public void AddBodyNode(Node node)
        {
            BodyNodes.Add(node);
        }
    }
}
