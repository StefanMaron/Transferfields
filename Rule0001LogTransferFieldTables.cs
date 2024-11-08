using Microsoft.Dynamics.Nav.CodeAnalysis;
using Microsoft.Dynamics.Nav.CodeAnalysis.Diagnostics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Semantics;
using Microsoft.Dynamics.Nav.CodeAnalysis.Symbols;
using System.Collections.Immutable;

namespace CustomCodeCop;

[DiagnosticAnalyzer]
public class Rule0001LogTransferFieldTables : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create<DiagnosticDescriptor>(DiagnosticDescriptors.Rule0001LogTransferFieldTables);

    public override void Initialize(AnalysisContext context)
    {
            context.RegisterOperationAction(new Action<OperationAnalysisContext>(this.AnalyzeTransferField), OperationKind.InvocationExpression);
    }

    private void AnalyzeTransferField(OperationAnalysisContext ctx)
    {
        if (ctx.IsObsoletePendingOrRemoved()) return;

        IInvocationExpression operation = (IInvocationExpression)ctx.Operation;
        // IOperation
        // if (operation.TargetMethod.Name == "TransferFields")
        // var a = ((BoundCall)operation).ReceiverOpt.Type;
        // ((Microsoft.Dynamics.Nav.CodeAnalysis.BoundCall)operation).ReceiverOpt
        // ((Microsoft.Dynamics.Nav.CodeAnalysis.BoundCall)ctx.Operation).ReceiverOpt
        var Parameter = ctx.Compilation.GetSemanticModel(operation.Arguments[0].Syntax.SyntaxTree).GetSymbolInfo(operation.Arguments[0].Syntax).Symbol;
        // operation.Syntax.Parent.ChildNodes().First()
        // ctx.Compilation.GetSemanticModel(operation.Syntax.SyntaxTree).GetSymbolInfo(operation.Syntax.Parent)
        ITableTypeSymbol sourceTable = (ITableTypeSymbol)((IVariableSymbol)Parameter).Type.OriginalDefinition;
        IRecordTypeSymbol targetTable = (IRecordTypeSymbol)((IInvocationExpression)operation).Instance.Type;
        
        ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0001LogTransferFieldTables, operation.Syntax.GetLocation(), sourceTable.Id, sourceTable.Name,targetTable.Id,targetTable.Name));


        // if (field.FieldClass == FieldClassKind.FlowField && field.GetBooleanPropertyValue(PropertyKind.Editable).Value)
        // {
        //     ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.Rule0001LogTransferFieldTables, field.Location, field.Name));
        // }
    }

    public static class DiagnosticDescriptors
    {
        public static readonly DiagnosticDescriptor Rule0001LogTransferFieldTables = new(
            id: "CC0001",
            title: "My Custom Error",
            messageFormat: "Source Table {0}: {1} / Target Table: {2}: {3}",
            category: "Design",
            defaultSeverity: DiagnosticSeverity.Warning, isEnabledByDefault: true,
            description: "Raise a diagnostic when the field is of type FlowField, without the property Editable on the field is set to false.",
            helpLinkUri: "https://some.url/CC0001");
    }
}
