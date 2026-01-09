using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    /// <summary>
    /// Class that represents the current value of variables and constants
    /// </summary>
    public class SemanticSymbolTable : ISemanticSymbolTable
    {
        private readonly Dictionary<string, double> _table;

        public SemanticSymbolTable()
        {
            _table = new Dictionary<string, double>();
        }

        public void Add(string symbol, double symbolValue)
        {
            _table.Add(symbol, symbolValue);
        }
        public void Remove(string symbol)
        {
            _table.Remove(symbol);
        }
        public double GetValue(string symbol)
        {
            return _table[symbol];
        }
        public bool IsDefined(string symbol)
        {
            return _table.ContainsKey(symbol);
        }
    }
}
