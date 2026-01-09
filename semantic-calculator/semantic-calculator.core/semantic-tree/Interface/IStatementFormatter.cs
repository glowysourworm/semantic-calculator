namespace semantic_calculator.core.semantic_tree.Interface
{
    /// <summary>
    /// Component responsible for statement preperation; and user feedback
    /// </summary>
    public interface IStatementFormatter
    {
        /// <summary>
        /// Checks validity of input characters, and format of input characters. This happens before 
        /// parsing and syntax checking.
        /// </summary>
        bool IsValidPreformat(string statement);

        /// <summary>
        /// Prepares statement for parsing:  removes white space; ...
        /// </summary>
        string PreFormat(string statement);
    }
}
