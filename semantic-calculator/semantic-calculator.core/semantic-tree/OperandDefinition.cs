using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    /// <summary>
    /// Definition of "operand" related data:  OperandType; and Operand String (key). The meaning of
    /// operand is that it is a logical unit of a semantic tree - which may be evaluated independently;
    /// and paired with a meaningful key, or symbol.
    /// </summary>
    public class OperandDefinition
    {
        public SemanticTreeNodeType Type { get; set; }
        public string Symbol { get; set; }

        public OperandDefinition()
        {
            this.Type = SemanticTreeNodeType.Number;
            this.Symbol = "0";
        }

        public OperandDefinition(string symbol, SemanticTreeNodeType type)
        {
            this.Type = type;
            this.Symbol = symbol;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj is not OperandDefinition)
                return false;

            var definition = (OperandDefinition)obj;

            return this.Symbol.Equals(definition.Symbol);
        }

        public override int GetHashCode()
        {
            return this.Symbol.GetHashCode();
        }
    }
}
