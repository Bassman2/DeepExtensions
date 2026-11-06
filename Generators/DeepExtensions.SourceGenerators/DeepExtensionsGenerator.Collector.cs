using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private INamedTypeSymbol? DeepCopyAttribute;
    private INamedTypeSymbol? DeepCopyIgnoreAttribute;

    private INamedTypeSymbol? DeepEqualAttribute;
    private INamedTypeSymbol? DeepEqualIgnoreAttribute;

    private INamedTypeSymbol? DeepMapAttribute;
    private INamedTypeSymbol? DeepMapIgnoreAttribute;

    private readonly Dictionary<string, INamedTypeSymbol> copyClasses = [];

    private readonly Dictionary<string, INamedTypeSymbol> equalClasses = [];

    private readonly Dictionary<string, INamedTypeSymbol> mapClasses = [];

    public void CollectAll()
    {
        DeepCopyAttribute = Compilation.GetTypeByMetadataName(DeepCopyAttributeName);
        DeepCopyIgnoreAttribute = Compilation.GetTypeByMetadataName(DeepCopyIgnoreAttributeName);
        DeepEqualAttribute = Compilation.GetTypeByMetadataName(DeepEqualAttributeName);
        DeepEqualIgnoreAttribute = Compilation.GetTypeByMetadataName(DeepEqualIgnoreAttributeName);
        DeepMapAttribute = Compilation.GetTypeByMetadataName(DeepMapAttributeName);
        DeepMapIgnoreAttribute = Compilation.GetTypeByMetadataName(DeepMapIgnoreAttributeName);

        Collect(copyClasses, DeepCopyAttribute!, DeepCopyIgnoreAttribute!);
        Collect(equalClasses, DeepEqualAttribute!, DeepEqualIgnoreAttribute!);
        Collect(mapClasses, DeepMapAttribute!, DeepMapIgnoreAttribute!);
    }

    public void Collect(Dictionary<string, INamedTypeSymbol> col, INamedTypeSymbol findAttr, INamedTypeSymbol ignoreAttr)
    {
        var classes = GetAllClassesWithAttribute(findAttr).ToArray();
        foreach (var cl in classes)
        {
            col[cl.ToDisplayString()] = cl;
        }
        foreach (var cl in classes)
        {
            CollectBaseClasses(cl, col, ignoreAttr);
        }
    }

    //public void CollectClass(Dictionary<string, INamedTypeSymbol> col, INamedTypeSymbol findAttr, INamedTypeSymbol ignoreAttr)

    private void CollectBaseClasses(INamedTypeSymbol cl, Dictionary<string, INamedTypeSymbol> col, INamedTypeSymbol ignoreAttr)
    {
        var current = cl.BaseType;
        while (current != null)
        {
            TryAdd(col, current);
            current = current.BaseType;
        }
    }

    private void CollectPropertyClasses(INamedTypeSymbol cl, Dictionary<string, INamedTypeSymbol> col, INamedTypeSymbol ignoreAttr)
    {
        foreach (var prop in cl.GetMembers().OfType<IPropertySymbol>())
        {
            if (prop.Type is INamedTypeSymbol propType)
            {
                TryAdd(col, propType);
            }
        }
    }


    private void TryAdd(Dictionary<string, INamedTypeSymbol> col, INamedTypeSymbol cl)
    {
        if (IsPartial(cl))
        {
            col[cl.ToDisplayString()] = cl;
        }
        else
        {
            Context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoPartialModifieryError, Location.None, cl.ToDisplayString()));
        }
    }

    public static bool IsPartial(INamedTypeSymbol symbol)
    {
        return symbol.DeclaringSyntaxReferences
            .Select(syntaxRef => syntaxRef.GetSyntax())
            .OfType<TypeDeclarationSyntax>() 
            .Any(typeSyntax => typeSyntax.Modifiers.Any(SyntaxKind.PartialKeyword));
    }

}
