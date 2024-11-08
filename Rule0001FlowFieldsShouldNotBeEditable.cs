using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace CustomCodeCop;

[DiagnosticAnalyzer]
public class Rule0001FlowFieldsShouldNotBeEditable : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0001FlowFieldsShouldNotBeEditable);

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSymbolAction(new Action<SymbolAnalysisContext>(this.AnalyzeFlowFieldEditable), SymbolKind.Field);
    }

    private void AnalyzeFlowFieldEditable(SymbolAnalysisContext ctx)
    {
        IFieldSymbol field = (IFieldSymbol)ctx.Symbol;

        if (field.FieldClass == FieldClassKind.FlowField && field.GetBooleanPropertyValue(PropertyKind.Editable).Value)
        {
            ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0001FlowFieldsShouldNotBeEditable, field.Location, field.Name));
        }
    }

    public static class DiagnosticDescriptors
    {
        public static readonly DiagnosticDescriptor Rule0001FlowFieldsShouldNotBeEditable = new(
            id: "CC0001",
            title: "My Custom Error",
            messageFormat: "My Custom Error, the field name is: {0}",
            category: "Design",
            defaultSeverity: DiagnosticSeverity.Warning, isEnabledByDefault: true,
            description: "Raise a diagnostic when the field is of type FlowField, without the property Editable on the field is set to false.",
            helpLinkUri: "https://some.url/CC0001");
    }
}
