using System.Collections.Generic;

namespace Compiler_Project.SyntaxAnalyzer.AST {
    public class ConstructorDeclarationNode : Node {
        public readonly List<string> ParameterNames;
        public readonly List<string> ParameterTypes;
        public readonly List<Node> BodyNodes;
        
        public ConstructorDeclarationNode(){
            ParameterNames = new List<string>();
            ParameterTypes = new List<string>();
            BodyNodes = new List<Node>();
        }

        public ConstructorDeclarationNode(List<string> parameterNames, List<string> parameterTypes)
        {
            ParameterNames = parameterNames;
            ParameterTypes = parameterTypes;
            BodyNodes = new List<Node>();
        }
        public void AddParameter(string parameterName, string parameterType){
            ParameterNames.Add(parameterName);
            ParameterTypes.Add(parameterType);
        }
        public void AddBodyNode(Node node){
            BodyNodes.Add(node);
        }
    }
}
