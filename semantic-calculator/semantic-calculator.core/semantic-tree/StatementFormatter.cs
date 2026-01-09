using semantic_calculator.core.semantic_tree.Interface;

namespace semantic_calculator.core.semantic_tree
{
    public class StatementFormatter : IStatementFormatter
    {
        public bool IsValidPreformat(string statement)
        {
            return true;
        }

        public string PreFormat(string statement)
        {
            // TBD:  The rest of the formatting / validation process
            return statement.Replace(" ", "");
        }
    }
}
