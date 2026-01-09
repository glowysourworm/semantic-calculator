namespace semantic_calculator.core.semantic_tree
{
    /// <summary>
    /// Class that represents the evaluation result of a semantic tree node
    /// </summary>
    public class SemanticTreeNodeEvaluationResult
    {
        /// <summary>
        /// This represents the type of (successful) node evaluation that may happen
        /// </summary>
        public enum NumericResultType
        {
            Integer,
            FloatingPoint
        }

        /// <summary>
        /// Numeric Result Type
        /// </summary>
        public NumericResultType Type { get; private set; }

        /// <summary>
        /// Numeric Result
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Set if there is an error in the evaluation of the tree node
        /// </summary>
        public string? ErrorMessage { get; private set; }

        public SemanticTreeNodeEvaluationResult(NumericResultType type, double value, string? error = null)
        {
            this.Type = type;
            this.Value = value;
            this.ErrorMessage = error;
        }
    }
}
