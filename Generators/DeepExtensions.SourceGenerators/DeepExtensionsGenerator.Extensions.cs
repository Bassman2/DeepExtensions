using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DeepExtensions.SourceGenerators;

partial class DeepGenerator
{
    public string AssemblyName => Compilation.AssemblyName ?? "";

    public string GlobalNamespace => Compilation.AssemblyName ?? "";


    public string GetPropertyTypeRecursive(ITypeSymbol symbol, string propertyName)
    {
        var currentSymbol = symbol;

        // Schleife wandert die Vererbungskette nach oben (bis zu Object)
        while (currentSymbol != null)
        {
            var property = currentSymbol.GetMembers(propertyName).OfType<IPropertySymbol>().FirstOrDefault();
            if (property != null)
            {
                return property.Type.ToDisplayString();
            }

            // Eine Ebene nach oben in der Vererbung gehen
            currentSymbol = currentSymbol.BaseType;
        }

        return string.Empty;
    }

    #region Classes

    public IEnumerable<INamedTypeSymbol> GetAllClasses()
    {
        foreach (var cla in Classes)
        {
            if (Compilation.GetSemanticModel(cla.SyntaxTree).GetDeclaredSymbol(cla) is INamedTypeSymbol symbol)
            {
                yield return symbol;
            }
        }
    }

    public IEnumerable<INamedTypeSymbol> GetAllClassesWithAttribute(INamedTypeSymbol attribute)
    { 
        foreach (var cla in GetAllClasses())
        {
            if (cla.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attribute)))
            {
                yield return cla;
            }
        }
    }

    public IEnumerable<IPropertySymbol> GetProperties(INamedTypeSymbol cla) => cla.GetMembers().OfType<IPropertySymbol>();

    #endregion

    #region Enums

    public IEnumerable<INamedTypeSymbol> GetAllEnums()
    {
        foreach (var enu in Enums)
        {
            if (Compilation.GetSemanticModel(enu.SyntaxTree).GetDeclaredSymbol(enu) is INamedTypeSymbol symbol)
            {
                yield return symbol;
            }
        }
    }

    public IEnumerable<INamedTypeSymbol> GetAllEnumsWithAttribute(INamedTypeSymbol attribute)
    {
        foreach (var enu in GetAllEnums())
        {
            if (enu.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attribute)))
            {
                yield return enu;
            }
        }
    }


    #endregion

    #region ITypeSymbol

    //public string FullName(ITypeSymbol symbol) => symbol.ToDisplayString();

    public bool IsObservableCollection(ITypeSymbol symbol) => symbol is INamedTypeSymbol namedType && namedType.IsGenericType && namedType.OriginalDefinition.ToDisplayString() == "System.Collections.ObjectModel.ObservableCollection<T>";

    public ITypeSymbol? GetInnerType(ITypeSymbol symbol, int index = 0)
    {
        if (symbol is INamedTypeSymbol named && named.TypeArguments.Length > index)
        {
            var arg = named.TypeArguments[index];
            // If the symbol is an unbound generic (type parameter) there's no concrete inner type:
            if (arg is ITypeParameterSymbol)
                return null;
            return arg;
        }

        if (symbol is IArrayTypeSymbol arr)
            return arr.ElementType;

        if (symbol is IPointerTypeSymbol ptr)
            return ptr.PointedAtType;
        return null;
    }

    public bool ImplementsInterface(ITypeSymbol symbol, string interfaceFullyQualifiedName)
    {
        if (symbol.AllInterfaces.Any(i => i.ToDisplayString() == interfaceFullyQualifiedName))
        {
            return true;
        }

        if (symbol is INamedTypeSymbol namedType && namedType.ToDisplayString() == interfaceFullyQualifiedName)
        {
            return true;
        }
        return false;
    }


    #endregion

    #region Attributes

    public bool HasNamedArgument(AttributeData data,string name) => data.NamedArguments.Any(a => a.Key == name);

    public KeyValuePair<string, TypedConstant>? GetNamedArgument(AttributeData data, string name) => data.NamedArguments.FirstOrDefault(a => a.Key == name);


    #endregion

    #region Global

    public AttributeData? GetAttribute(INamedTypeSymbol obj, INamedTypeSymbol attribute)
    {
        return obj.GetAttributes().FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attribute));
    }

    public AttributeData? GetAttribute(IPropertySymbol obj, INamedTypeSymbol attribute)
    {
        return obj.GetAttributes().FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attribute));
    }

    #endregion
}
