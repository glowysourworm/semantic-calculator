using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semantic_calculator.core.semantic_tree.Interface
{
    public enum OperandType
    {
        Number = 0,
        Function = 1,
        RFunction = 2,
        DataSet = 3
    }

    public interface IOperand
    {
        string Syntax { get; }
    }
}
