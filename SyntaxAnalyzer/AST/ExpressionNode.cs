namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class ExpressionNode : Node 
    {
        public string ExpressionType; //Literal or Call
        public string IntegerLiteral; //if expression is integer
        public string BooleanLiteral; //if expression is boolean
        public string FloatLiteral; //if expression is float
        public CallNode Call;
        public ExpressionNode()
        {
            IntegerLiteral = "";
            BooleanLiteral = "";
            FloatLiteral = "";
        }
        public void SetCall(CallNode call)
        {
            Call = call;
        }
    }
}
