namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum SemanticTreeResultStatus
    {
        None = 0,
        Success = 1,
        SynataxError = 2,
        ExecutionError = 3
    }

    public interface ISemanticTreeResult
    {
        public SemanticTreeResultStatus Status { get; }

        /// <summary>
        /// Relevant messages about semantic tree evaluation
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Resulting numeric value (for now.. could handle symbolic results, and complex numbers)
        /// </summary>
        public double NumericResult { get; }

        /// <summary>
        /// Numeric type for the result
        /// </summary>
        public SemanticTreeNodeEvaluationResult.NumericResultType NumericType { get; }
    }
}
