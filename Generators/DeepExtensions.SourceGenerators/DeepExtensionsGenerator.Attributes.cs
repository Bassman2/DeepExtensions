
using Microsoft.CodeAnalysis;

namespace DeepExtensions.SourceGenerators;

partial class DeepGenerator
{
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
            

            """);
    }

}
