using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class ClassDeclarationNode : Node {
        
        public string Name;
        public string SuperClass;
        
        public readonly List<Node> VariableDeclarations;
        public readonly List<Node> MethodDeclarations;
        public readonly List<Node> ConstructorDeclarations;
        public ClassDeclarationNode()
        {
            VariableDeclarations = new List<Node>();
            MethodDeclarations = new List<Node>();
            ConstructorDeclarations = new List<Node>();
        }
        public ClassDeclarationNode(string name,string superClass, List<Node> methodDeclarationNodes,
            List<Node> constructorDeclarationNodes, List<Node> variableDeclarations)
        {
            Name = name;
            SuperClass = superClass;
            VariableDeclarations = variableDeclarations;
            MethodDeclarations = methodDeclarationNodes;
            ConstructorDeclarations = constructorDeclarationNodes;
        }
        public void SetName (string name)
        {
            Name = name;
        }
        public void SetSuperClass (string superClass)
        {
            SuperClass = superClass;
        }
        public void AddVariableDeclaration(VariableDeclarationNode node)
        {
            VariableDeclarations.Add(node);
        }
        public void AddMethodDeclaration(MethodDeclarationNode node)
        {
            MethodDeclarations.Add(node);
        }
        public void AddConstructorDeclaration(ConstructorDeclarationNode node)
        {
            ConstructorDeclarations.Add(node);
        }
    }
}
