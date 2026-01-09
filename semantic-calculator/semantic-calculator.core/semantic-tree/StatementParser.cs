using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class StatementParser : IStatementParser
    {
        private readonly SemanticTreeConfiguration _configuration;

        public StatementParser(SemanticTreeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISemanticTree Parse(string statement)
        {
            ISemanticTreeNode root = new SemanticTreeNode(statement);

            ParseImpl(root.Raw, ref root);

            return new SemanticTree(root);
        }

        private void ParseImpl(string statement, ref ISemanticTreeNode currentNode)
        {
            // Procedure
            //
            // 0) Locate parenthesis pairs - CHECK FOR ENTIRE STATEMENT
            // 1) Locate outer-most parenthesis sub-tree(s)
            // 2) Locate operators between these sub-tree(s)
            //     - Treat outer-most paren'd sub-tree (becomes left operand)
            //     - Then, operator
            //     - Then, right operand
            //
            // 3) Parse each sub-tree type as a sub-node of the current node, recursively
            //

            // CHECK ENTIRE STATEMENT BRACKETING
            if (statement.StartsWith('(') && statement.EndsWith(')'))
                statement = statement.Substring(1, statement.Length - 2);

            var outermostStatements = new List<SubstringLocator>();
            var parenStack = new Stack<int>();

            for (int index = 0; index < statement.Length; index++)
            {
                // Start
                if (statement[index] == '(')
                {
                    parenStack.Push(index);
                }

                // End
                else if (statement[index] == ')')
                {
                    var outerMost = parenStack.Count == 1;
                    var startIndex = parenStack.Pop();

                    if (outerMost)
                    {
                        // Treat this as a sub-statement
                        outermostStatements.Add(new SubstringLocator(statement, startIndex, index - startIndex + 1));
                    }
                }
            }

            // Operator Index
            IOperator? nextOperator = null;
            var operatorIndex = LocateNextOperator(statement, outermostStatements, out nextOperator);

            // Operand (no operator present)
            if (operatorIndex == -1)
            {
                SemanticTreeNodeType nodeType = SemanticTreeNodeType.SubTree;

                var nextNode = ParseAsOperand(statement, outermostStatements, out nodeType);

                // Operand Node (Left)
                currentNode.Set(nodeType, nextNode, null, null);
            }

            // Next Operator (outside paren'd statements)
            else
            {
                // Parse these as sub-statements. They may fall through to being "operands".
                //
                var leftStatement = statement.Substring(0, operatorIndex);
                var rightStatement = statement.Substring(operatorIndex + 1, statement.Length - operatorIndex - 1);

                ISemanticTreeNode leftNode = new SemanticTreeNode(leftStatement);
                ISemanticTreeNode rightNode = new SemanticTreeNode(rightStatement);

                // -> ParseImpl (RECURSE)
                ParseImpl(leftStatement, ref leftNode);
                ParseImpl(rightStatement, ref rightNode);

                // Sub tree node type implies that there are two operands present with an operator
                currentNode.Set(SemanticTreeNodeType.SubTree, leftNode, rightNode, nextOperator);
            }
        }

        private ISemanticTreeNode ParseAsOperand(string operandString, List<SubstringLocator> outermostParenStatements, out SemanticTreeNodeType nodeType)
        {
            // Pre-defined Operand
            foreach (var operand in _configuration.Operands)
            {
                switch (operand.Type)
                {
                    case SemanticTreeNodeType.Number:
                    case SemanticTreeNodeType.Constant:
                    case SemanticTreeNodeType.Variable:
                    case SemanticTreeNodeType.Function:          // Could treat function differently
                    {
                        // ISSUE WITH PRE-FORMATTING STATEMENTS (NEEDS TO BE DONE FIRST)
                        if (operand.Symbol == operandString ||
                           SurroundBy(operand.Symbol, '(', ')') == operandString)
                        {
                            nodeType = operand.Type;

                            return new SemanticTreeNode(operandString);
                        }
                    }
                    break;
                    default:
                        throw new Exception("Unhandled OperandType:  SimpleStatementParser.cs");
                }
            }

            // Parsed Operand:  Will be a number unless there is an undefined constant, variable, or function
            double number = 0;
            if (double.TryParse(operandString, out number))
            {
                nodeType = SemanticTreeNodeType.Number;
                return new SemanticTreeNode(operandString);
            }
            else
            {
                throw new Exception("Unable to parse operand. Please make sure statement is valid; and that constants and variables have defined numeric values");
            }
        }

        /// <summary>
        /// Returns next operator location, respecting paren'd statements, and order of operations.
        /// </summary>
        private int LocateNextOperator(string statement, List<SubstringLocator> outermostParenStatements, out IOperator? resultOperator)
        {
            resultOperator = null;

            var subStatementPieces = statement.Split(_configuration.Operators
                                                                   .Select(x => x.GetSymbol())
                                                                   .ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            var operators = new Dictionary<int, IOperator>();

            for (int index = 0; index < statement.Length; index++)
            {
                // Operator contained in sub-statement
                if (outermostParenStatements.Any(x => x.ContainsIndex(index)))
                    continue;

                var nextOperator = _configuration.Operators.FirstOrDefault(x => x.GetSymbol() == statement[index].ToString());

                // Keep these to chose next in order of operations
                if (nextOperator != null)
                {
                    operators.Add(index, nextOperator);
                }
            }

            if (!operators.Any())
                return -1;

            // ORDER OF OPERATIONS!
            var resultIndex = operators.OrderBy(x => x.Value.GetOrder()).First().Key;

            // Set operator output parameter
            resultOperator = operators[resultIndex];

            return resultIndex;
        }

        private bool IsOperand(string statement)
        {
            return _configuration.Operands.Any(x => x.Symbol == statement || SurroundBy(statement, '(', ')') == x.Symbol);
        }

        private string SurroundBy(string statement, char leftChar, char rightChar)
        {
            return leftChar + statement + rightChar;
        }
    }
}
