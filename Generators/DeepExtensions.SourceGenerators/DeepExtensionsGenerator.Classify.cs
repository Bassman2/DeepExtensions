namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private enum TypeClass
    {
        ReferenceArray,
        ValueArray,
        ReferenceList,
        ValueList,
        Reference,
        NullableReference,
        Value,
        Unknown
    }

    private TypeClass ClassifyType(ITypeSymbol typeSymbol)
    {
        // array data type
        if (typeSymbol is IArrayTypeSymbol arrayType)
        {
            if (arrayType.ElementType is INamedTypeSymbol elementType && elementType.IsReferenceType && elementType.SpecialType != SpecialType.System_String)
            {
                return TypeClass.ReferenceArray;
            }
            else
            {
                return TypeClass.ValueArray;
            }
        }
        else if (typeSymbol is INamedTypeSymbol namedType)
        {

            var iListInterface = (typeSymbol as INamedTypeSymbol)?.AllInterfaces
                .Prepend(typeSymbol as INamedTypeSymbol)
                .FirstOrDefault(i => i?.IsGenericType == true && i.OriginalDefinition.ToDisplayString() == "System.Collections.Generic.IList<T>");

            var elementType = typeSymbol is IArrayTypeSymbol listType ? listType.ElementType : iListInterface?.TypeArguments.FirstOrDefault();

            if (elementType is INamedTypeSymbol namedElem && namedElem.IsReferenceType && namedElem.SpecialType != SpecialType.System_String)
            {
                return TypeClass.ReferenceList;
            }
            else if (elementType is not null)
            {
                return TypeClass.ValueList;
            }
            else if (namedType.IsReferenceType && namedType.SpecialType != SpecialType.System_String)
            {
                // is nullable reference type
                if (namedType.NullableAnnotation == NullableAnnotation.Annotated)
                {
                    return TypeClass.NullableReference; 
                }
                else
                {
                    return TypeClass.Reference;
                }
            }
            return TypeClass.Value;
        }
        return TypeClass.Unknown;
    }
}
