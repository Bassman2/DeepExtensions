namespace DeepExtensions.SourceGenerators;

partial class DeepExtensionsGenerator
{
    private const string DeepCopyAttributeName = "DeepExtensions.DeepCopyAttribute";
    private const string DeepCopyIgnoreAttributeName = "DeepExtensions.DeepCopyIgnoreAttribute";

    private INamedTypeSymbol DeepCopyIgnoreAttribute = null!;

    private const string DeepEqualAttributeName = "DeepExtensions.DeepEqualAttribute";
    private const string DeepEqualIgnoreAttributeName = "DeepExtensions.DeepEqualIgnoreAttribute";

    private INamedTypeSymbol DeepEqualIgnoreAttribute = null!;

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

            public class DeepComparer
            {
                public static bool CompareItems<T>(T[] list1, T[] list2) where T : IDeepEqual<T>
                {
                    if (list1 is null || list2 is null) return false;
                    if (list1.Length != list2.Length) return false;
                    for (int i = 0; i < list1.Length; i++)
                    {
                        var a = list1[i];
                        var b = list2[i];
                        if (a is null && b is null) continue;
                        if (a is null || b is null) return false;

                        if (!a.IsDeepEqual(b)) return false; 
                    }
                    return true;
                }

                public static bool CompareItems<T>(IList<T> list1, IList<T> list2) where T : IDeepEqual<T>
                {
                    if (list1 is null || list2 is null) return false;
                    if (list1.Count != list2.Count) return false;
                    for (int i = 0; i < list1.Count; i++)
                    {
                        var a = list1[i];
                        var b = list2[i];
                        if (a is null && b is null) continue;
                        if (a is null || b is null) return false;
            
                        if (!a.IsDeepEqual(b)) return false; 
                    }
                    return true;
                }

                public static T[] CopyItems<T>(T[] array) where T : IDeepCopy<T>
                {
                    T[] copy = new T[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        copy[i] = array[i].DeepCopy(); 
                    }
                    return copy;
                }

                public static IList<T> CopyItems<T>(IList<T> list) where T : IDeepCopy<T>
                {
                    List<T> copy = new List<T>(list.Count);
                    foreach (var item in list)
                    {
                        copy.Add(item.DeepCopy());
                    }
                    return copy;
                }
            }
            """);
    }

}
