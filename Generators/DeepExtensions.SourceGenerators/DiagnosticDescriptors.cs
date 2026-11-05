namespace DeepExtensions.SourceGenerators;

public static class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor ClassMissingPartialModifieryError = new (
            "DEEPGEN001",
            "Class is not partial",
            "Class '{0}' is not partial. Please make it partial to generate deep extension methods.",
            "Usage",
             DiagnosticSeverity.Error,
             true);
}
