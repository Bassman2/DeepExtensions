namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private const string DeepCopyAttributeName = "DeepExtensions.DeepCopyAttribute";
    private const string DeepCopyIgnoreAttributeName = "DeepExtensions.DeepCopyIgnoreAttribute";

    private const string DeepEqualAttributeName = "DeepExtensions.DeepEqualAttribute";
    private const string DeepEqualIgnoreAttributeName = "DeepExtensions.DeepEqualIgnoreAttribute";

    private const string DeepMapAttributeName = "DeepExtensions.DeepMapAttribute";
    private const string DeepMapIgnoreAttributeName = "DeepExtensions.DeepMapIgnoreAttribute";
    private const string DeepMapPropertyAttributeName = "DeepExtensions.DeepMapPropertyAttribute";

    protected void CreateAttributes(IncrementalGeneratorPostInitializationContext context)
    {
        context.AddSource($"DeepExtensionsAttributes.g.cs",
            """
            namespace DeepExtensions;
            
            [AttributeUsage(AttributeTargets.Class)]
            public class DeepCopyAttribute(): Attribute
            { }

            [AttributeUsage(AttributeTargets.Property)]
            public class DeepCopyIgnoreAttribute(): Attribute
            { }

            [AttributeUsage(AttributeTargets.Class)]
            public class DeepEqualAttribute(): Attribute
            { }

            [AttributeUsage(AttributeTargets.Property)]
            public class DeepEqualIgnoreAttribute(): Attribute
            { }

            [AttributeUsage(AttributeTargets.Class)]
            public class DeepMapAttribute(): Attribute
            { }
            
            [AttributeUsage(AttributeTargets.Class)]
            public class DeepMapIgnoreAttribute(): Attribute
            { }

            [AttributeUsage(AttributeTargets.Class)]
            public class DeepMapPropertyAttribute(string name): Attribute
            { 
                public string Name { get; } = name;
            }

            public interface IDeepCopy<T>
            {
                T DeepCopy();
            }

            public interface IDeepEqual<T>
            {
                bool IsDeepEqual(T other);
            }

            """);
    }

}
