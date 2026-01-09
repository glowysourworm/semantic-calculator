namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum OperatorArithmeticType
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3
    }

    public enum OperatorFunction
    {
        Assignment = 0,
        Arithmetic = 1,
    }

    public interface IOperator : IComparable<IOperator>
    {
        /// <summary>
        /// Returns the exact symbol syntax of the operator.
        /// </summary>
        string GetSymbol();

        /// <summary>
        /// Returns the order number of the operator. This is used to create order of
        /// operations.
        /// </summary>
        uint GetOrder();

        OperatorArithmeticType ArithmeticType { get; }
        OperatorFunction Function { get; }
    }
}
