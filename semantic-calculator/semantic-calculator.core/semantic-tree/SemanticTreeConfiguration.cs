using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SemanticTreeConfiguration
    {
        private List<IOperator> _operators;

        public IEnumerable<IOperator> Operators
        {
            get { return _operators; }
        }

        public SemanticTreeConfiguration()
        {
            _operators = new List<IOperator>();
        }

        public void AddOperator(IOperator newOperator)
        {
            _operators.Add(newOperator);
        }

    }
}
