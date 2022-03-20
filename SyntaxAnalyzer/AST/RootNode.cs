using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST 
{
    public class RootNode : Node 
    {
        public readonly List<Node> ClassDeclarations;
        public RootNode(){
            ClassDeclarations = new List<Node>();
        }
        public void AddClassDeclaration(ClassDeclarationNode node){
            ClassDeclarations.Add(node);
        }
    }
}
