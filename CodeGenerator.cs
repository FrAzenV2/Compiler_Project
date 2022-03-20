using System;
using System.Reflection;
using System.Reflection.Emit;
using Compiler_Project.SyntaxAnalyzer.AST;

namespace Compiler_Project
{
    public class CodeGenerator
    {
        private void GenerateCode()
        {
            var aName = new AssemblyName(Guid.NewGuid().ToString());
            var ab =  AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name + ".dll");
        }
        private Type TypeGenerator(ModuleBuilder mb, ClassDeclarationNode classDeclarationNode)
        {
            
            TypeBuilder tb = mb.DefineType(
                classDeclarationNode.Name,
                TypeAttributes.Public);

            var fields = new FieldBuilder[classDeclarationNode.VariableDeclarations.Count];
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = tb.DefineField(
                    ((VariableDeclarationNode)classDeclarationNode.VariableDeclarations[i]).Name,
                    typeof(int),
                    FieldAttributes.Private);
            }
            
            Type[] parameterTypes = { typeof(int) };
            var ctor1 = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes);

            return tb.CreateType();
        }
    }
}