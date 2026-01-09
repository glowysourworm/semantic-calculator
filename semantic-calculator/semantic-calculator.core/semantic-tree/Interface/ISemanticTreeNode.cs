namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum SemanticTreeNodeType
    {
        Number = 0,
        Constant = 1,
        Variable = 2,
        Function = 3,

        /// <summary>
        /// Implies that the current node is a sub-tree that does not
        /// directly resolve into one of the other types. A function, for
        /// example, would be considered its own ISemanticTree, which would
        /// have a separate method of calculation. (Although, the function
        /// node will not necessarily be treated that way. The function will
        /// have its own designator, like "f")
        /// </summary>
        SubTree = 4
    }

    /// <summary>
    /// Represents node of semantic tree
    /// </summary>
    public interface ISemanticTreeNode
    {
        /// <summary>
        /// Raw string for this node. This will be recursively defined in the ISemanticTree. So, the meaning of this
        /// will not be the number, variable, constant, or function, but may be parsed to interpret the 
        /// syntax of this node's pieces.
        /// </summary>
        string Raw { get; }

        /// <summary>
        /// Left side of the semantic tree. This side is required; but may resolve to a primitive. It will
        /// also be set first.
        /// </summary>
        ISemanticTreeNode LeftNode { get; }

        /// <summary>
        /// Right side of the semantic tree. This will be set if there is another operation to carry out 
        /// (recursively).
        /// </summary>
        ISemanticTreeNode? RightNode { get; }

        /// <summary>
        /// Operator for the current node:  left (operator) right
        /// </summary>
        IOperator? Operator { get; }

        /// <summary>
        /// Primitive type of the subtree, which means that the IOperandPrimitive is set
        /// </summary>
        SemanticTreeNodeType Type { get; }

        /// <summary>
        /// Called during parsing to set the parameters of the node; and determine the meaning of its raw statement.
        /// </summary>
        void Set(SemanticTreeNodeType type, ISemanticTreeNode left, ISemanticTreeNode? right, IOperator? theOperator);

        /// <summary>
        /// Validates the node's preparedness for evaluation. 
        /// </summary>
        bool Validate();

        /// <summary>
        /// Recursively evaluates the semantic tree node
        /// </summary>
        SemanticTreeNodeEvaluationResult Evaluate(ISemanticSymbolTable symbolTable);
    }
}
