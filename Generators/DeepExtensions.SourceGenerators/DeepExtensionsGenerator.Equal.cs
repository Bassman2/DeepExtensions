namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private void AppendDeepEqual(StringBuilder sb, INamedTypeSymbol cl, string className, bool hasLocalBaseClass)
    {
        sb.Append(
            $$"""
                #region Deep Equal

                public bool IsDeepEqual({{cl.Name}} other)
                {
                    return 
                        other is not null &&

            """);
        if (hasLocalBaseClass)
        {
            sb.AppendLine($"            base.IsDeepEqual(other) &&");
        }

        foreach ((IPropertySymbol prop, string propName, TypeClass propType) in LoopOverProperties(cl, DeepCopyIgnoreAttribute))
        {
            sb.Append("            ");
            sb.AppendLine(propType switch
            {
                TypeClass.ReferenceArray    /**/ => $"DeepComparer.CompareItems({propName}, other.{propName}) &&",
                TypeClass.ValueArray        /**/ => $"{propName}.SequenceEqual(other.{propName}) &&",
                TypeClass.ReferenceList     /**/ => $"DeepComparer.CompareItems({propName}, other.{propName}) &&",
                TypeClass.ValueList         /**/ => $"{propName}.SequenceEqual(other.{propName}) &&",
                TypeClass.Reference         /**/ => $"{propName}.IsDeepEqual(other.{propName}) &&",
                TypeClass.NullableReference /**/ => $"({propName} is null ? other.{propName} is null : {propName}.IsDeepEqual(other.{propName})) &&",
                TypeClass.Value             /**/ => $"{propName} == other.{propName} &&",
                _ => $"#warning Unknown type for property {propName}, manual handling may be required."
            });
        }

        sb.Append(
            """
                        true;
                }

                #endregion
            
            """);
    }
}
