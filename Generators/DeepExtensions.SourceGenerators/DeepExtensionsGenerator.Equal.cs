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

        foreach (var p in LoopOverProperties(cl, DeepCopyIgnoreAttribute))
        {
            sb.Append("            ");
            sb.AppendLine(p.PropType switch
            {
                TypeClass.ReferenceArray    /**/ => $"DeepComparer.CompareItems({p.PropName}, other.{p.PropName}) &&",
                TypeClass.ValueArray        /**/ => $"{p.PropName}.SequenceEqual(other.{p.PropName}) &&",
                TypeClass.ReferenceList     /**/ => $"DeepComparer.CompareItems({p.PropName}, other.{p.PropName}) &&",
                TypeClass.ValueList         /**/ => $"{p.PropName}.SequenceEqual(other.{p.PropName}) &&",
                TypeClass.Reference         /**/ => $"{p.PropName}.IsDeepEqual(other.{p.PropName}) &&",
                TypeClass.NullableReference /**/ => $"({p.PropName} is null ? other.{p.PropName} is null : {p.PropName}.IsDeepEqual(other.{p.PropName})) &&",
                TypeClass.Value             /**/ => $"{p.PropName} == other.{p.PropName} &&",
                _ => $"#warning Unknown type for property {p.PropName}, manual handling may be required."
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
