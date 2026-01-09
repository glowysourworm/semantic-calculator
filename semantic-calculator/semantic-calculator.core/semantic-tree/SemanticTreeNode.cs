using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class SemanticTreeNode : ISemanticTreeNode
    {
        public string Raw { get; }
        public SemanticTreeNodeType Type { get; private set; }

        public ISemanticTreeNode LeftNode { get; private set; }
        public ISemanticTreeNode? RightNode { get; private set; }
        public IOperator? Operator { get; private set; }

        public SemanticTreeNode(string raw)
        {
            this.Raw = raw;
            this.Type = SemanticTreeNodeType.SubTree;
            this.LeftNode = null;       // Leave null until the set function is called
            this.RightNode = null;
            this.Operator = null;
        }

        /// <summary>
        /// Called first to setup the (required) left node
        /// </summary>
        public void Set(SemanticTreeNodeType type, ISemanticTreeNode left, ISemanticTreeNode? right, IOperator? theOperator)
        {
            this.Type = type;
            this.LeftNode = left;
            this.RightNode = right;
            this.Operator = theOperator;
        }

        public bool Validate()
        {
            if (this.LeftNode == null)
                return false;

            switch (this.Type)
            {
                case SemanticTreeNodeType.Number:
                case SemanticTreeNodeType.Constant:
                case SemanticTreeNodeType.Variable:
                case SemanticTreeNodeType.Function:
                    return true;
                case SemanticTreeNodeType.SubTree:
                    return this.RightNode != null && this.Operator != null;
                default:
                    throw new Exception("Unhandled ISemanticTreeNode type");
            }
        }

        public SemanticTreeNodeEvaluationResult Evaluate(ISemanticSymbolTable symbolTable)
        {
            if (!Validate())
                throw new Exception("Trying to evaluate SemanticTreeNode without either: 1) Parsing it first, or 2) Without a valid structure");

            switch (this.Type)
            {
                case SemanticTreeNodeType.Number:
                {
                    var result = double.Parse(this.Raw);

                    return new SemanticTreeNodeEvaluationResult(double.IsInteger(result) ?
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.Integer :
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.FloatingPoint, result);
                }
                case SemanticTreeNodeType.Constant:
                case SemanticTreeNodeType.Variable:
                {
                    var result = symbolTable.GetValue(this.Raw);

                    return new SemanticTreeNodeEvaluationResult(double.IsInteger(result) ?
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.Integer :
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.FloatingPoint, result);
                }

                case SemanticTreeNodeType.SubTree:
                {
                    switch (this.Operator.Function)
                    {
                        case OperatorFunction.Arithmetic:
                            return EvaluateAsArithmeticOperation(symbolTable);
                        case OperatorFunction.Assignment:
                        default:
                            throw new Exception("Unhandled Semantic Tree Node Operator Function Type");
                    }
                }

                case SemanticTreeNodeType.Function:
                default:
                    throw new Exception("Unhandled ISemanticTreeNode type");
            }
        }

        private SemanticTreeNodeEvaluationResult EvaluateAsArithmeticOperation(ISemanticSymbolTable symbolTable)
        {
            if (this.LeftNode == null ||
                this.RightNode == null ||
                this.Operator == null)
                throw new Exception("Invalid Semantic Tree Node:  Must parse and validate before calling Evaluate()");

            var leftResult = this.LeftNode.Evaluate(symbolTable);
            var rightResult = this.RightNode.Evaluate(symbolTable);
            var result = 0.0D;
            var divideByZero = false;

            switch (this.Operator.ArithmeticType)
            {
                case OperatorArithmeticType.Addition:
                    result = leftResult.Value + rightResult.Value;
                    break;
                case OperatorArithmeticType.Subtraction:
                    result = leftResult.Value - rightResult.Value;
                    break;
                case OperatorArithmeticType.Multiplication:
                    result = leftResult.Value * rightResult.Value;
                    break;
                case OperatorArithmeticType.Division:
                    divideByZero = (rightResult.Value == 0);
                    result = !divideByZero ? (leftResult.Value / rightResult.Value) : 0;
                    break;
                default:
                    throw new Exception("Unhandled Semantic Tree Node Operator Arithmetic Function Type");
            }

            return new SemanticTreeNodeEvaluationResult(double.IsInteger(result) ?
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.Integer :
                                                                SemanticTreeNodeEvaluationResult.NumericResultType.FloatingPoint, result,
                                                        divideByZero ? "Divide by zero error!" : null);
        }
    }
}
