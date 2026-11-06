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

            public class KeyEqualityComparer<T, TKey> : System.Collections.Generic.IEqualityComparer<T>
            {
                private readonly Func<T, TKey> _keySelector;

                public KeyEqualityComparer(Func<T, TKey> keySelector)
                {
                    _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
                }

                public bool Equals(T x, T y)
                {
                    if (ReferenceEquals(x, y)) return true;
                    if (x is null || y is null) return false;
                    return Equals(_keySelector(x), _keySelector(y));
                }

                public int GetHashCode(T obj)
                {
                    if (obj is null) return 0;
                    return _keySelector(obj)?.GetHashCode() ?? 0;
                }
            }

            """);
    }

}
