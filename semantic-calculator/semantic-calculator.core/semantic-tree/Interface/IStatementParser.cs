namespace semantic_calculator.core.semantic_tree.Interface
{
    public interface IStatementParser
    {
        ISemanticTree Parse(string statement);
    }
}
