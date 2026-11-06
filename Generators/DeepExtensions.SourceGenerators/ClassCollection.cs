using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace DeepExtensions.SourceGenerators;

public class ClassCollection
{
    private readonly Dictionary<string, INamedTypeSymbol> items = [];

    private readonly SourceProductionContext context;
    private readonly Compilation compilation;
    private readonly ImmutableArray<BaseTypeDeclarationSyntax> immutableArray;

    private readonly INamedTypeSymbol findAttr;
    private readonly INamedTypeSymbol ignoreAttr;

    public IEnumerable<INamedTypeSymbol> Classes => items.Values;

    protected ClassDeclarationSyntax[] allClasses = [];

    private ClassCollection(
        SourceProductionContext context,
        (Compilation Left, ImmutableArray<BaseTypeDeclarationSyntax> Right) tuple,
        string findAttr, 
        string ignoreAttr)
    {
        this.context = context;
        this.compilation = tuple.Left;
        this.immutableArray = tuple.Right;
        this.findAttr = compilation.GetTypeByMetadataName(findAttr)!;
        this.ignoreAttr = compilation.GetTypeByMetadataName(ignoreAttr)!;
    }

    public static ClassCollection Collect(
        SourceProductionContext context,
        (Compilation Left, ImmutableArray<BaseTypeDeclarationSyntax> Right) tuple,
        string findAttr,
        string ignoreAttr)
    {
        var collection = new ClassCollection(context, tuple, findAttr, ignoreAttr);
        collection.Collect();
        return collection;
    }

    private void Collect()
    {
        // get all classes from the syntax tree
        allClasses = [.. immutableArray.OfType<ClassDeclarationSyntax>()];

        // get all classes with the findAttr attribute
        var classes = allClasses
            .Select(cla => compilation.GetSemanticModel(cla.SyntaxTree).GetDeclaredSymbol(cla))
            .OfType<INamedTypeSymbol>()
            .Where(cl => cl.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, findAttr)))
            .ToArray();

        // look for all classes with the findAttr attribute 
        foreach (var cl in classes)
        {
            DeepScan(cl);
        }
    }


    public void DeepScan(INamedTypeSymbol cl)
    {
        // throw error message if class is not partial
        if (!IsPartial(cl))
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoPartialModifieryError, Location.None, cl.ToDisplayString()));
        }

        // already in the collection
        if (items.ContainsKey(cl.ToDisplayString())) return;

        // add to collection
        items.Add(cl.ToDisplayString(), cl);

        // loop over all base classes
        for (var current = cl.BaseType; current != null && current.SpecialType != SpecialType.System_Object; current = current.BaseType)
        {
            DeepScan(current);
        }

        // loop over all properties without the ignoreAttr attribute
        var filteredProperties = cl.GetMembers().OfType<IPropertySymbol>()
            .Where(p => !p.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, ignoreAttr)));
        foreach (var prop in filteredProperties)
        {
            // ...
        }

    }


    private static bool IsPartial(INamedTypeSymbol symbol)
    {
        return symbol.DeclaringSyntaxReferences
            .Select(syntaxRef => syntaxRef.GetSyntax())
            .OfType<TypeDeclarationSyntax>()
            .Any(typeSyntax => typeSyntax.Modifiers.Any(SyntaxKind.PartialKeyword));
    }
}
