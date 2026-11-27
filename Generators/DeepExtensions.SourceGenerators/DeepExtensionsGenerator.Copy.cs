namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private void AppendDeepCopy(StringBuilder sb, INamedTypeSymbol cl, string className, bool hasLocalBaseClass)
    {
        sb.Append(
            $$"""
                #region Deep Copy

                public{{(hasLocalBaseClass ? " new" : "")}} {{className}} DeepCopy()
                {
                    return DeepCopy(new Dictionary<object, object>(ReferenceEqualityComparer.Instance));
                }

                public{{(hasLocalBaseClass ? " new" : "")}} {{className}} DeepCopy(Dictionary<object, object>? visited)
                {
                    if (visited is not null && visited.TryGetValue(this, out var existing))
                    {
                        return ({{className}})existing;
                    }
                    var copy = new {{className}}();
                    if (visited is not null)
                    {
                        visited[this] = copy;
                    }
                    DeepCopyTo(copy, visited);
                    return copy;
                }

                protected void DeepCopyTo({{className}} target, Dictionary<object, object> visited)
                {

            """);

        if (hasLocalBaseClass)
        {
            sb.AppendLine("        base.DeepCopyTo(target, visited);");
        }

        foreach ((IPropertySymbol prop, string propName, TypeClass propType) in LoopOverProperties(cl, DeepCopyIgnoreAttribute))
        {
            sb.Append("        ");
            sb.AppendLine(propType switch
            {
                TypeClass.ReferenceArray    /**/ => $"target.{propName} = DeepComparer.CopyItems(this.{propName});",
                TypeClass.ValueArray        /**/ => $"target.{propName} = [.. this.{propName}];",
                TypeClass.ReferenceList     /**/ => $"target.{propName} = [.. DeepComparer.CopyItems(this.{propName})];",
                TypeClass.ValueList         /**/ => $"target.{propName} = [.. this.{propName}];",
                TypeClass.Reference         /**/ => $"target.{propName} = this.{propName}.DeepCopy(visited);",
                TypeClass.NullableReference /**/ => $"target.{propName} = this.{propName}?.DeepCopy(visited);",
                TypeClass.Value             /**/ => $"target.{propName} = this.{propName};",
                _ => $"#warning Unknown type for property {propName}, manual handling may be required."
            });
        }

        sb.Append(
            """
                }

                #endregion

            """);
    }

}
