using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class IfStatementNode : Node {
        public ExpressionNode Expression;
        public readonly List<Node> IfBodyNodes;
        public readonly List<Node> ElseBodyNodes;
        public IfStatementNode(){
            IfBodyNodes = new List<Node>();
            ElseBodyNodes = new List<Node>();
        }
        public void SetExpression(ExpressionNode expression){
            Expression = expression;
        }
        public void AddIfBodyNode(Node node){
            IfBodyNodes.Add(node);
        }
        public void AddElseBodyNode(Node node){
            ElseBodyNodes.Add(node);
        }
    }
}
