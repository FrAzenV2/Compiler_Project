namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class ReturnStatementNode : Node {

        public ExpressionNode Expression;
        public void SetExpression(ExpressionNode expression)
        {
            Expression = expression;
        }
    }
}
