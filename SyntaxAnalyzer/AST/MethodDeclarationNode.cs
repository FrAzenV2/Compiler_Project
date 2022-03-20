using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST 
{
    public class MethodDeclarationNode : Node 
    {
        
        public string Name;
        public string ReturnType;
        public readonly List<string> ParameterNames;
        public readonly List<string> ParameterTypes;
        public readonly List<Node> BodyNodes;
        public readonly bool IsDestruction;
        
        public MethodDeclarationNode()
        {
            ParameterNames = new List<string>();
            ParameterTypes = new List<string>();
            BodyNodes = new List<Node>();
        }
        
        public MethodDeclarationNode(bool isDestruction)
        {
            IsDestruction = isDestruction;
            ParameterNames = new List<string>();
            ParameterTypes = new List<string>();
            BodyNodes = new List<Node>();
            Name = "Destruct";
        }
        
        public MethodDeclarationNode(string name, string returnType)
        {
            Name = name;
            ReturnType = returnType;
            ParameterNames = new List<string>();
            ParameterTypes = new List<string>();
            BodyNodes = new List<Node>();
        }
        
        public MethodDeclarationNode(string name, List<string> parameterNames, List<string> parameterTypes)
        {
            Name = name;
            ParameterNames = parameterNames;
            ParameterTypes = parameterTypes;
            BodyNodes = new List<Node>();
        }
        
        public MethodDeclarationNode(string name, string returnType, List<string> parameterNames, List<string> parameterTypes)
        {
            Name = name;
            ReturnType = returnType;
            ParameterNames = parameterNames;
            ParameterTypes = parameterTypes;
            BodyNodes = new List<Node>();
        }
        
        public void SetName(string name)
        {
            Name = name;
        }
        
        public void SetReturnType(string returnType)
        {
            ReturnType = returnType;
        }
        
        public void AddParameter(string parameterName, string parameterType)
        {
            ParameterNames.Add(parameterName);
            ParameterTypes.Add(parameterType);
        }
        public void AddBodyNode(Node node)
        {
            BodyNodes.Add(node);
        }
    }
}
