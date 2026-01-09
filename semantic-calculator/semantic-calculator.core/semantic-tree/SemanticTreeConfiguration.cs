using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SemanticTreeConfiguration
    {
        private Dictionary<string, IOperator> _operators;
        private Dictionary<string, OperandDefinition> _operands;

        public IEnumerable<IOperator> Operators
        {
            get { return _operators.Values; }
        }
        public IEnumerable<OperandDefinition> Operands
        {
            get { return _operands.Values; }
        }

        public SemanticTreeConfiguration()
        {
            _operators = new Dictionary<string, IOperator>();
            _operands = new Dictionary<string, OperandDefinition>();
        }

        public void AddOperator(IOperator newOperator)
        {
            _operators.Add(newOperator.GetSymbol(), newOperator);
        }

        public void AddOperand(string symbol, SemanticTreeNodeType type)
        {
            _operands.Add(symbol, new OperandDefinition(symbol, type));
        }

        public bool IsDefined(string symbol)
        {
            return _operators.ContainsKey(symbol) || _operands.ContainsKey(symbol);
        }
    }
}
