using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SemanticTreeResult : ISemanticTreeResult
    {
        public SemanticTreeResultStatus Status { get; }
        public string Message { get; }
        public double NumericResult { get; }
        public SemanticTreeNodeEvaluationResult.NumericResultType NumericType { get; }

        public SemanticTreeResult(SemanticTreeResultStatus status,
                                  string message,
                                  double numericResult,
                                  SemanticTreeNodeEvaluationResult.NumericResultType numericType)
        {
            this.Status = status;
            this.Message = message;
            this.NumericResult = numericResult;
            this.NumericType = numericType;
        }
    }
}
