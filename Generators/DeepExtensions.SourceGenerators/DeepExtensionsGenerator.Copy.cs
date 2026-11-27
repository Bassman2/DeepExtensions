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

        foreach (var p in LoopOverProperties(cl, DeepCopyIgnoreAttribute))
        {
            sb.Append("        ");
            sb.AppendLine(p.PropType switch
            {
                TypeClass.ReferenceArray    /**/ => $"target.{p.PropName} = DeepComparer.CopyItems(this.{p.PropName});",
                TypeClass.ValueArray        /**/ => $"target.{p.PropName} = [.. this.{p.PropName}];",
                TypeClass.ReferenceList     /**/ => $"target.{p.PropName} = [.. DeepComparer.CopyItems(this.{p.PropName})];",
                TypeClass.ValueList         /**/ => $"target.{p.PropName} = [.. this.{p.PropName}];",
                TypeClass.Reference         /**/ => $"target.{p.PropName} = this.{p.PropName}.DeepCopy(visited);",
                TypeClass.NullableReference /**/ => $"target.{p.PropName} = this.{p.PropName}?.DeepCopy(visited);",
                TypeClass.Value             /**/ => $"target.{p.PropName} = this.{p.PropName};",
                _ => $"#warning Unknown type for property {p.PropName}, manual handling may be required."
            });
        }

        sb.Append(
            """
                }

                #endregion

            """);
    }

}
