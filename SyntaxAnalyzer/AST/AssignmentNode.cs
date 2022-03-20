namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class AssignmentNode : Node 
    {
        public string Name;
        public ExpressionNode Expression;
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetExpression(ExpressionNode expression)
        {
            Expression = expression;
        }
    }
}
