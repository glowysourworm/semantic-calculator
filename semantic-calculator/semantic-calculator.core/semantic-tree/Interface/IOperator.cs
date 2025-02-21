using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum OperatorType
    {
        Unary = 0,
        Binary = 1
    }

    public interface IOperator
    {
        string Syntax { get; }

        OperatorType Type { get; }
    }
}
