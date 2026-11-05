using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    
    protected void CreateDebug()
    {
        //StringBuilder sb = new();
        //sb.AppendLine($"/*");
        //sb.AppendLine();
        //sb.AppendLine($"Global GlobalNamespace: {GlobalNamespace}");
        //sb.AppendLine($"Assembly Name: {AssemblyName}");
        //sb.AppendLine();

        ////sb.AppendLine($"Global GlobalNamespace: {Compilation.GlobalNamespace.Name} - {Compilation.GlobalNamespace.ToDisplayString()} {(Compilation.GlobalNamespace.IsGlobalNamespace ? "global" : "")}");
        //foreach (var x in Compilation.GlobalNamespace.GetNamespaceMembers())
        //{
        //    sb.AppendLine($"GlobalNamespace: {x.Name} - {x.ToDisplayString()} {(x.IsGlobalNamespace ? "global" : "")} {x.NamespaceKind}");
        //}
        //sb.AppendLine();

        //// global attributes
        //sb.AppendLine("Global Attributes");
        //sb.AppendLine();
        //DebugAttributes(sb, this, 1);
        //sb.AppendLine();

        //sb.AppendLine("Classes");
        //foreach (var cl in GetAllClasses())
        //{
        //    sb.AppendLine();
        //    sb.AppendLine($"  Class: Name: {cl.Name}, GlobalNamespace: {cl.NameSpace}, FullName: {cl.FullName}");
        //    sb.AppendLine();

        //    if (cl.Properties.Any())
        //    {
        //        DebugAttributes(sb, cl, 2);
        //        sb.AppendLine($"    Properties:");
        //        foreach (var prop in cl.Properties)
        //        {
        //            sb.AppendLine($"      {prop.Type.Name} {prop.Name} {{ {(prop.HasGet ? "get; " : "")}{(prop.HasSet ? "set;" : "")} }}");

        //            DebugAttributes(sb, prop, 4);
        //        }
        //    }

        //}

        //sb.AppendLine("Enums");
        //foreach (var en in GetAllEnums())
        //{
        //    sb.AppendLine();
        //    sb.AppendLine($"  Enum: Name: {en.Name}, GlobalNamespace: {en.NameSpace}, FullName: {en.FullName}");
        //    sb.AppendLine();


        //    if (en.Fields.Any())
        //    {
        //        DebugAttributes(sb, en, 2);
        //        sb.AppendLine($"    Properties:");
        //        foreach (var field in en.Fields)
        //        {
        //            sb.AppendLine($"      {field.Type.Name} {field.Name}");

        //            DebugAttributes(sb, field, 4);
        //        }
        //    }
        //}
        //sb.AppendLine($"*/");
        //AddSource($"Debug.g.cs", sb.ToString());
    }

    //private void DebugAttributes(StringBuilder sb, BaseAttributes attributes, int indent)
    //{
    //    string indentString = new(' ', indent * 2);
    //    foreach (var attr in attributes.Attributes)
    //    {
    //        sb.AppendLine($"{indentString}Attribute: Name: {attr.Name}, GlobalNamespace: {attr.NameSpace}, FullName {attr.FullName}");

    //        if (attr.ConstructorArguments.Any())
    //        {
    //            sb.AppendLine($"{indentString}  Constructor Arguments");
    //            foreach (var arg in attr.ConstructorArguments)
    //            {
    //                sb.AppendLine($"{indentString}    Value: {arg.Value}, Type: {arg.TypeName}, Kind: {arg.Kind}");
    //            }
    //        }

    //        if (attr.NamedArguments.Any())
    //        {
    //            sb.AppendLine($"{indentString}  Named Arguments");
    //            foreach (var arg in attr.NamedArguments)
    //            {
    //                sb.AppendLine($"{indentString}    Name: {arg.Name}, Value: {arg.Value}, Type: {arg.TypeName}, Kind: {arg.Kind}");
    //            }
    //        }
    //        sb.AppendLine();

    //    }
    //}
}
