using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SimpleOperator : IOperator
    {
        protected string Symbol { get; private set; }
        protected uint Order { get; private set; }

        public OperatorArithmeticType ArithmeticType { get; }
        public OperatorFunction Function { get; }

        public SimpleOperator(string symbol, uint order, OperatorArithmeticType type, OperatorFunction function)
        {
            this.Symbol = symbol;
            this.Order = order;
            this.ArithmeticType = type;
            this.Function = function;
        }

        public string GetSymbol()
        {
            return this.Symbol;
        }

        public uint GetOrder()
        {
            return this.Order;
        }

        public int CompareTo(IOperator? other)
        {
            return this.Order.CompareTo(other?.GetOrder() ?? 0);
        }
    }
}
