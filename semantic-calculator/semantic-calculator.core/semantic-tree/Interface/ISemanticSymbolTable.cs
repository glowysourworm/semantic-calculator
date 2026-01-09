namespace semantic_calculator.core.semantic_tree.Interface
{
    public interface ISemanticSymbolTable
    {
        void Add(string symbol, double symbolValue);
        void Remove(string symbol);
        double GetValue(string symbol);
        bool IsDefined(string symbol);
    }
}
