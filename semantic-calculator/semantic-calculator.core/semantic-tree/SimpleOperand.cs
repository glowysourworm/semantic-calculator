using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SimpleOperand : IOperand
    {
        public string Syntax { get; }

        public SimpleOperand(string syntax)
        {
            this.Syntax = syntax;
        }
    }
}
