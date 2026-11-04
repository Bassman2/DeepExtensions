using Microsoft.CodeAnalysis;

namespace DeepExtensions.SourceGenerators;

public static class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor ClassMissingPartialModifieryError = new(
        id: "MTSG001",                                         
        title: "Missing partial class modifier",    
        messageFormat: "Missing partial modifier on class declaration of type '{0}'; another partial declaration of this type exists", 
        category: "Usage",                        
        defaultSeverity: DiagnosticSeverity.Error,              
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor PropertyMissingPartialModifieryError = new(
        id: "MTSG002",
        title: "Missing partial property modifier",
        messageFormat: "Missing partial modifier on property declaration of type '{0}'; another partial declaration of this type exists",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
