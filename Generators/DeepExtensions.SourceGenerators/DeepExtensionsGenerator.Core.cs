
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;


namespace DeepExtensions.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public partial class DeepExtensionsGenerator : IIncrementalGenerator
{

    private ClassCollection? copyClassCollection;
    private ClassCollection? equalClassCollection;
    private ClassCollection? mapClassCollection;


    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(postInitializationContext => CreateAttributes(postInitializationContext));

        var provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => IsSyntaxTargetForGeneration(node),           //node is ClassDeclarationSyntax || node is EnumDeclarationSyntax,
                transform: static (ctx, _) => (BaseTypeDeclarationSyntax)ctx.Node           // (ClassDeclarationSyntax)ctx.Node
                ).Where(m => m is not null).Collect();

        var compilation = context.CompilationProvider.Combine(provider);

        context.RegisterSourceOutput(compilation, Execute);
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
     => (node is ClassDeclarationSyntax c && c.AttributeLists.Count > 0); // || (node is EnumDeclarationSyntax e && e.AttributeLists.Count > 0);


    public static readonly SymbolDisplayFormat FormatWithoutNullable = SymbolDisplayFormat.CSharpErrorMessageFormat.WithMiscellaneousOptions(
            SymbolDisplayFormat.CSharpErrorMessageFormat.MiscellaneousOptions & ~SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<BaseTypeDeclarationSyntax> Right) tuple)
    {
        DeepCopyIgnoreAttribute = tuple.Left.GetTypeByMetadataName(DeepCopyIgnoreAttributeName)!;
        DeepEqualIgnoreAttribute = tuple.Left.GetTypeByMetadataName(DeepEqualIgnoreAttributeName)!;

        copyClassCollection = ClassCollection.Collect(context, tuple, DeepCopyAttributeName, DeepCopyIgnoreAttributeName);
        equalClassCollection = ClassCollection.Collect(context, tuple, DeepEqualAttributeName, DeepEqualIgnoreAttributeName);
        mapClassCollection = ClassCollection.Collect(context, tuple, DeepMapAttributeName, DeepMapIgnoreAttributeName);

        Context = context;
        Compilation = tuple.Left;
        Classes = [.. tuple.Right.Where(i => i is ClassDeclarationSyntax).Cast<ClassDeclarationSyntax>()];         // OfType<string>();
        try
        {
            context.AddSource("Statistc.g.cs",
                $$"""
                /*
                Copy:  {{copyClassCollection?.Classes.Count() ?? 0}} {{string.Join(", ", copyClassCollection?.Classes.Select(c => c.ToDisplayString()) ?? [])}}
                Equal: {{equalClassCollection?.Classes.Count() ?? 0}} {{string.Join(", ", equalClassCollection?.Classes.Select(c => c.ToDisplayString()) ?? [])}}
                Map:   {{mapClassCollection?.Classes.Count() ?? 0}} {{string.Join(", ", mapClassCollection?.Classes.Select(c => c.ToDisplayString()) ?? [])}}
                */
                """);

            GenerateDeepCopy();
            GenerateDeepEqual();
            GenerateDeepMap();
        }
        catch (Exception ex)
        {
            var message = ex.ToString().Replace("/*", "/ *").Replace("*/", "* /");
            context.AddSource($"Error_{Guid.NewGuid():N}.g.cs", $"/*\r\n{message}\r\n*/");
        }
    }

    protected SourceProductionContext Context { get; private set; }

    protected Compilation Compilation { get; private set; } = null!;

    //protected ImmutableArray<ClassDeclarationSyntax> Classes { get; private set; }
    protected ClassDeclarationSyntax[] Classes { get; private set; } = [];

    //protected EnumDeclarationSyntax[] Enums { get; private set; } = [];

    
    public void AddSource(string hintName, string source) => Context.AddSource(hintName, source);

    //public override IEnumerable<Attribute> Attributes => Compilation.Assembly.GetAttributes().Select(a => new Attribute(a));

    public static string? FirstLetterToLower(string? str)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length == 1)
        {
            return str.ToLower();
        }

        return char.ToLower(str[0]) + str.Substring(1);
    }

    public static string? FirstLetterToUpper(string? str)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length == 1)
        {
            return str.ToLower();
        }

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
