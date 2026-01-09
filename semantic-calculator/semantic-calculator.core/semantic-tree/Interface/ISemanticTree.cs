namespace semantic_calculator.core.semantic_tree.Interface
{
    public interface ISemanticTree
    {
        ISemanticTreeResult Execute(ISemanticSymbolTable symbolTable);
    }
}
