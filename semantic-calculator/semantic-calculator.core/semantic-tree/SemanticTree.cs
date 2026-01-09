using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SemanticTree : ISemanticTree
    {
        readonly private ISemanticTreeNode _root;

        /// <summary>
        /// Initiailizes a semantic tree with an already parsed, completed,
        /// node tree which is ready to be evaluated.
        /// </summary>
        public SemanticTree(ISemanticTreeNode root)
        {
            _root = root;
        }

        public ISemanticTreeResult Execute(ISemanticSymbolTable symbolTable)
        {
            var result = _root.Evaluate(symbolTable);

            return new SemanticTreeResult(result.ErrorMessage == null ?
                                            SemanticTreeResultStatus.Success :
                                            SemanticTreeResultStatus.ExecutionError,
                                          result.ErrorMessage ?? string.Empty,
                                          result.Value,
                                          result.Type);
        }
    }
}
