using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SimpleOperator : IOperator
    {
        public string Syntax { get; }
        public OperatorType Type { get; }

        public SimpleOperator(string syntax, OperatorType type)
        {
            this.Syntax = syntax;
            this.Type = type;
        }
    }
}
