namespace DeepExtensions.SourceGenerators;

[Flags]
public enum GenFlags { Copy = 1, Equal = 2, Map = 4 };

public class GenData { public GenFlags Flags = 0; }

partial class DeepExtensionsGenerator
{
    public Dictionary<INamedTypeSymbol, GenData> Collect() 
    {
        Dictionary<INamedTypeSymbol, GenData> classCollection = new(SymbolEqualityComparer.Default);

        List<INamedTypeSymbol> allClasses = [.. 
            Syntax.OfType<ClassDeclarationSyntax>().
            Select(c => Compilation.GetSemanticModel(c.SyntaxTree).
            GetDeclaredSymbol(c)).
            Distinct(SymbolEqualityComparer.Default).
            OfType<INamedTypeSymbol>()];


        INamedTypeSymbol deepCopyAttribute = Compilation.GetTypeByMetadataName(DeepCopyAttributeName)!;
        INamedTypeSymbol deepCopyIgnoreAttribute = Compilation.GetTypeByMetadataName(DeepCopyIgnoreAttributeName)!;
        foreach (var cl in allClasses.Where(cl => cl.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, deepCopyAttribute))))
        {
            // recursive over all base and property classes
            RecursiveScan(classCollection, cl, deepCopyIgnoreAttribute, GenFlags.Copy);
        }

        INamedTypeSymbol deepEqualAttribute = Compilation.GetTypeByMetadataName(DeepEqualAttributeName)!;
        INamedTypeSymbol deepEqualIgnoreAttribute = Compilation.GetTypeByMetadataName(DeepEqualIgnoreAttributeName)!;
        foreach (var cl in allClasses.Where(cl => cl.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, deepEqualAttribute))))
        {
            // recursive over all base and property classes
            RecursiveScan(classCollection, cl, deepEqualIgnoreAttribute, GenFlags.Equal);
        }

        INamedTypeSymbol deepMapAttribute = Compilation.GetTypeByMetadataName(DeepMapAttributeName)!;
        foreach (var cl in allClasses.Where(cl => cl.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, deepMapAttribute))))
        {
            // throw error message if class is not partial
            EnsureIsPartial(cl);

            // already in the collection with this flag
            if (Contains(classCollection, cl, GenFlags.Map)) continue;

            // add to collection
            GetOrCreate(classCollection, cl).Flags |= GenFlags.Map;
        }
        CreateTraceFile(classCollection);
        return classCollection;
    }

    public void RecursiveScan(Dictionary<INamedTypeSymbol, GenData> classCollection, INamedTypeSymbol cl, INamedTypeSymbol ignoreAttr, GenFlags flag)
    {
        // throw error message if class is not partial
        EnsureIsPartial(cl);

        // already in the collection with this flag
        if (Contains(classCollection, cl, flag)) return;

        // add to collection
        GetOrCreate(classCollection, cl).Flags |= flag;


        // loop over all base classes inside this assembly (DeclaringSyntaxReferences.Length > 0)
        //for (var current = cl.BaseType; current != null && /*current.DeclaringSyntaxReferences.Length > 0*/ /*&& current.SpecialType != SpecialType.System_Object*/; current = current.BaseType)
        
        for (var current = cl.BaseType; current != null && IsScannableClass(current); current = current.BaseType)
        {
            RecursiveScan(classCollection, current, ignoreAttr, flag);
        }

        // TODO: best prectise to make the recursive

        // loop over all properties without the ignoreAttr attribute
        var filteredProperties = cl.GetMembers().OfType<IPropertySymbol>()
            .Where(p => !p.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, ignoreAttr)));

        foreach (var prop in filteredProperties)
        {
            ScanType(classCollection, prop.Type, ignoreAttr, flag);
        }

        //foreach (var prop in filteredProperties)
        //{
        //    // check if it is an array type
        //    if (prop.Type is IArrayTypeSymbol arrayType && arrayType.ElementType is INamedTypeSymbol arrayElementType
        //        && arrayElementType.TypeKind == TypeKind.Class
        //        && arrayElementType.SpecialType != SpecialType.System_String
        //        && !arrayElementType.IsGenericType)
        //    {
        //        RecursiveScan(classCollection, arrayElementType, ignoreAttr, flag);
        //    }
        //    // check if it is a generic list type
        //    else if (prop.Type is INamedTypeSymbol genericType && genericType.IsGenericType)
        //    {
        //        var argsType = genericType.TypeArguments.OfType<INamedTypeSymbol>().Where(g =>
        //            g.TypeKind == TypeKind.Class &&
        //            g.SpecialType != SpecialType.System_String &&
        //            !g.IsGenericType);
        //        foreach (var argType in argsType)
        //        {
        //            RecursiveScan(classCollection, argType, ignoreAttr, flag);
        //        }
        //    }
        //    // check if it is a class type and not a string or generic type
        //    else if (prop.Type is INamedTypeSymbol classType
        //        && classType.TypeKind == TypeKind.Class
        //        && classType.SpecialType != SpecialType.System_String
        //        && !classType.IsGenericType)
        //    {
        //        RecursiveScan(classCollection, classType, ignoreAttr, flag);
        //    }
        //}
    }

    private void ScanType(Dictionary<INamedTypeSymbol, GenData> classCollection, ITypeSymbol type, INamedTypeSymbol ignoreAttr, GenFlags flag)
    {
        if (type is IArrayTypeSymbol arrayType)
        {
            // Unwrap array element type
            ScanType(classCollection, arrayType.ElementType, ignoreAttr, flag);
        }
        else if (type is INamedTypeSymbol namedType)
        {
            if (namedType.IsGenericType)
            {
                // Recursively inspect generic type arguments (e.g., List<T>, Dictionary<K, V>)
                foreach (var typeArg in namedType.TypeArguments)
                {
                    ScanType(classCollection, typeArg, ignoreAttr, flag);
                }
            }
            else if (namedType.TypeKind == TypeKind.Class
                  && namedType.SpecialType != SpecialType.System_String
                  && IsScannableClass(namedType))
            {
                // Scan custom reference types defined in the current compilation
                RecursiveScan(classCollection, namedType, ignoreAttr, flag);
            }
        }
    }

    private bool IsPartial(INamedTypeSymbol cl)
    {
        return cl.DeclaringSyntaxReferences
            .Select(syntaxRef => syntaxRef.GetSyntax())
            .OfType<TypeDeclarationSyntax>()
            .Any(typeSyntax => typeSyntax.Modifiers.Any(SyntaxKind.PartialKeyword));
    }

    private static bool IsScannableClass(INamedTypeSymbol cl)
    {
        return cl.DeclaringSyntaxReferences.Length > 0;
    }

    private void EnsureIsPartial(INamedTypeSymbol cl)
    {
        if (!IsPartial(cl))
        {
            var location = cl.Locations.FirstOrDefault() ?? Location.None;
            Context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NoPartialModifieryError, location, cl.ToDisplayString()));
        }
    }

    private bool Contains(Dictionary<INamedTypeSymbol, GenData> classCollection, INamedTypeSymbol cl, GenFlags flag)
    {
        return classCollection.TryGetValue(cl, out var data) && data.Flags.HasFlag(flag);
    }

    private GenData GetOrCreate(Dictionary<INamedTypeSymbol, GenData> classCollection, INamedTypeSymbol cl)
    {
        if (!classCollection.TryGetValue(cl, out var data))
        {
            data = new();
            classCollection.Add(cl, data);
        }
        return data;
    }

    private void CreateTraceFile(Dictionary<INamedTypeSymbol, GenData> classCollection)
    {

        StringBuilder sb = new();
        sb.AppendLine("/*");
        foreach (var cl in classCollection.OrderBy(c => c.Key.ToDisplayString()))
        {
            sb.AppendLine($"{cl.Key.ToDisplayString()} : {cl.Value.Flags}");
        }

        sb.AppendLine("*/");
        AddSource($"CollectorTrace.g.cs", sb.ToString());
    }
}
